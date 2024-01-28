using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHand : MonoBehaviour
{
    public Deck deck;
    bool isDrawingCard = false;
    public bool isPlayingCard = false;
    public AudioClip cardDraw, cardShuffle;

    // Start is called before the first frame update
    void Start()
    {
        for( int i = 0; i < 3; i++)
        {
            var newCard = deck.DrawCard();
            newCard.transform.SetParent(this.transform);
            newCard.transform.localScale = Vector3.one;
            GetComponent<AudioSource>().PlayOneShot(cardShuffle);
        }
    }

    public void DrawCard()
    {
        StartCoroutine(DrawCardIE());
    }

    public IEnumerator DrawCardIE( bool skipWait = false )
    {
        if( isDrawingCard )
        {
            yield break;
        }

        Debug.Log("Draw!");
        isDrawingCard = true;

        GameObject? newCard = deck.DrawCard();
        if( newCard )
        {
            newCard.transform.SetParent(this.transform);
            newCard.transform.localScale = Vector3.one;
            GetComponent<AudioSource>().PlayOneShot(cardDraw);

            yield return new WaitForSeconds(.5f);
            isDrawingCard = false;
            yield return null;
        }
        else
        {
            yield break;
        }
    }
}

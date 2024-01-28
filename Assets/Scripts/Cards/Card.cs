using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public CardDetails details;
    public Text cardText, cardTraits;

    // Start is called before the first frame update
    void Start()
    {
        cardText.text = "";
        cardTraits.text = "";
        cardText.text = details.dialogueText.text;
        foreach( var type in details.cardTypes )
        {
            cardTraits.text += type.ToString() + "\n";
        }
    }

    public void PlayCard()
    {
        StartCoroutine(PlayCardIE());
    }

    public IEnumerator PlayCardIE()
    {
        var hand = FindObjectOfType<CardHand>();
        if( !hand.isPlayingCard)
        {
            GetComponent<AudioSource>().Play();
            transform.SetParent(null);
            GetComponent<Image>().color = Color.clear;
            hand.isPlayingCard = true;
            var dm = FindObjectOfType<DateDialogueManager>();
            yield return dm.PlayerSendMessage(details.dialogueText, details.cardTypes);
            if( !dm.DateIsConcluding() )
            {
                hand.DrawCard();
                hand.isPlayingCard = false;
                Destroy(this.gameObject);
            }
        }
    }
}

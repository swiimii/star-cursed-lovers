using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DateSprite : MonoBehaviour
{
    public Sprite positive, negative, neutral;

    public const float REACTION_DURATION = 3f;

    public void DoReaction(int reactionValue )
    {
        if( reactionValue > 0 )
        {
            StartCoroutine(PositiveReaction());
        }
        else if( reactionValue < 0 )
        {
            StartCoroutine(NegativeReaction());
        }
    }
    public IEnumerator PositiveReaction()
    {
        GetComponent<Image>().sprite = positive;
        yield return new WaitForSeconds(REACTION_DURATION);
        GetComponent<Image>().sprite = neutral;
    }

    public IEnumerator NegativeReaction()
    {
        GetComponent<Image>().sprite = negative;
        yield return new WaitForSeconds(REACTION_DURATION);
        GetComponent<Image>().sprite = neutral;
    }
}

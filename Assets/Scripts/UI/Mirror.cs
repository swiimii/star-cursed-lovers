using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    public List<LevelCard> levelCards;

    private void Start()
    {
        if( GameState.singleton )
        {
            for( int i = 0; i < GameState.singleton.daysPassed && i < levelCards.Count; i++)
            {
                levelCards[i].gameObject.SetActive(false);
            }

            GameState.singleton.CheckGameEnd();
        }
    }   
}

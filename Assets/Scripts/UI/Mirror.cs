using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    public List<LevelCard> levels;


    private void Start()
    {
        if( GameState.singleton )
        {
            for( int i = 0; i < GameState.singleton.daysPassed && i < levels.Count; i++)
            {
                levels[i].gameObject.SetActive(false);
            }
        }
    }
}

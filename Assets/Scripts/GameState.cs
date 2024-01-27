using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        /** 
         * Game state shouldn't be destroyed on load.
         * Whenever the player goes to the main menu,
         * a new game state should be created and any old ones should
         * be destroyed.
        */
        var gameStates = FindObjectsOfType<GameState>();
        if ( gameStates.Length > 1 )
        {
            foreach( var state in gameStates )
            {
                if( state != this )
                {
                    Destroy(state);
                }
            }
        }

        DontDestroyOnLoad(this);
    }
}

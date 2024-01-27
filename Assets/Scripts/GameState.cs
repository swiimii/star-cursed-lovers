using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    private int daysPassed = 0;
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

    /**
     * This function should be called when a date is skipped, or a level is completed/failed.
     * When each date is either skipped or completed, the final screen should be shown and 
     * the player's final choice is provided; which ending is wanted.
     * 
     * After an ending is chosen, the player should be led to the main menu, which will delete
     * this gameObject and instantiate a new GameState instance (per Start() function)
     */
    private void IncrementDaysPassed()
    {
        // increment daysPassed

        int maxDays = 7;
        if( daysPassed >= maxDays )
        {
            // transition to final screen
        }
    }
}

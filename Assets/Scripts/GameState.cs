using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    public int daysPassed = 0;
    public List<int> daysWon;
    public static GameState singleton;
    public GameDefs.Decks deckMode = GameDefs.Decks.Rizzler;

    // Start is called before the first frame update
    void Start()
    {
        daysWon = new List<int>();
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
        singleton = this;
    }

    public void SetDeckMode(int mode)
    {
        switch (mode)
        {
            case 0:
                deckMode = GameDefs.Decks.Rizzler;
                break;
            case 1:
                deckMode = GameDefs.Decks.Wizard;
                break;
            case 2:
                deckMode = GameDefs.Decks.Rebel;
                break;
            default:
                Debug.LogError("Unexpecte Int for Deck Mode");
                break;
        }

    }

    public void CheckGameEnd()
    {
        const int TOTAL_CHARACTERS_DONE = 2;
        if (GameState.singleton.daysPassed >= TOTAL_CHARACTERS_DONE )
        {
            // Game over; go to end screen!
            GameState.singleton.TransitionToScene("EndChoice");
        }
    }

    // Helper Scene Functions
    public void TransitionToLevelSelect()
    {
        StartCoroutine(SceneTransitionIE("LevelSelect"));
    }

    public void TransitionToHome()
    {
        StartCoroutine(SceneTransitionIE("MainMenu"));

    }

    public void TransitionToScene(string sceneName)
    {
        StartCoroutine(SceneTransitionIE(sceneName));
    }

    public IEnumerator SceneTransitionIE(string sceneName)
    {
        // TODO
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene(sceneName);
    }
}

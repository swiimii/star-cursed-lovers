using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCard : MonoBehaviour
{
    public string levelName;
    public bool swiped = false;
    public void SwipeRight()
    {
        if(!swiped)
        {
            swiped = true;
            if( GameState.singleton )
                GameState.singleton.daysPassed += 1;
            Debug.Log("Swiped Right!");
            GetComponent<Animator>().SetTrigger("SwipeRight");
            GameState.singleton.TransitionToScene(levelName);
        }
    }

    public void SwipeLeft()
    {
        if(!swiped)
        {
            swiped = true;
            if (GameState.singleton)
                GameState.singleton.daysPassed += 1;
            Debug.Log("Swiped Left");
            GetComponent<Animator>().SetTrigger("SwipeLeft");
            
            GameState.singleton.CheckGameEnd();
        }
    }
}

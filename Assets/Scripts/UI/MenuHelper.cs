using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHelper : MonoBehaviour
{
    public void GoToLevelSelect()
    {
        GameState.singleton.TransitionToLevelSelect();
    }

    public void GoToMainMenu()
    {
        GameState.singleton.TransitionToHome();
    }

    public void GoToLevel(string level )
    {
        GameState.singleton.TransitionToScene(level);
    }
}

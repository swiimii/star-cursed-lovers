using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalMirror : MonoBehaviour
{
    public Button[] suitors;
    public GameObject[] objsToDisable, objsToEnable;
    private void Start()
    {
        for( int i = 0; i < suitors.Length; i ++ )
        {
            if( !GameState.singleton.daysWon.Contains(i))
            {
                suitors[i].interactable = false;
            }
        }
    }

    public void ChooseEnding(GameObject ending )
    {        
        ending.SetActive(true);

        foreach( var obj in objsToDisable )
        {
            obj.SetActive(false);
        }

        foreach( var obj in objsToEnable )
        {
            obj.SetActive(true);
        }
        
        foreach( var s in suitors)
        {
            s.gameObject.SetActive(false);
        }
    }
}

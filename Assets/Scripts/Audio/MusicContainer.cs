using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicContainer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach( var cont in FindObjectsOfType<MusicContainer>() )
        {
            if( cont != this )
            {
                Destroy(cont.gameObject);
            }
        }
    }
}

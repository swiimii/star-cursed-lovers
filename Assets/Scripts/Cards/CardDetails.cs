using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/CardDetails", order = 1)]
public class CardDetails : ScriptableObject
{
    public string dialogueText;
    public List<GameDefs.Type> cardTypes;


}

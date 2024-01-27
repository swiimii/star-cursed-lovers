using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/DateDetails", order = 1)]
public class DateDetails : ScriptableObject
{
    List<GameDefs.Type> positiveTypes;
    List<GameDefs.Type> negativeTypes;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/DateDetails", order = 1)]
public class DateDetails : ScriptableObject
{
    public List<GameDefs.Type> positiveTypes, negativeTypes;

    public List<Line> positiveLines, negativeLines, neutralLines, introLines, winLines, loseLines;
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Line", order = 1)]
public class Line : ScriptableObject
{
    public string text;
    public Color color = Color.black;
    public AudioClip voice;
}

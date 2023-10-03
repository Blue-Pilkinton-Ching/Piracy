using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ButtonSettings", menuName = "ScriptableObjects/ButtonSettings", order = 0)]
public class ButtonSettings : ScriptableObject {
    public float FadeDuration = 0.2f;
    public float ClickDelay = 0.1f;
    public Color NormalColor = Color.white;
    public Color HoverColor = Color.grey;
    public Color ClickColor = Color.black;
}
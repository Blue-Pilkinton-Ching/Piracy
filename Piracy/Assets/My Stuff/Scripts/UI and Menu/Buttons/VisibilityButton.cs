using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VisibilityButton : ButtonBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;
    public bool IsPrivate {get; private set;} = true;
    protected override void OnClick()
    {
        IsPrivate = !IsPrivate;

        text.text = IsPrivate ? "Private" : "Public";
    }
}

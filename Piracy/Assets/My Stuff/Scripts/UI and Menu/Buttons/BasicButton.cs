using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicButton : ButtonBehaviour
{
    [SerializeField]
    GameObject showMenu;

    [SerializeField]
    GameObject hideMenu;

    protected override void Awake()
    {
        base.Awake();
        shouldFreezeButtons = true;
    }
    protected override void OnClick()
    {
        if (showMenu != null)
        {
            showMenu.SetActive(true);
        }
        
        if (hideMenu != null)
        {
            hideMenu.SetActive(false);
        }

        ButtonsFrozen = false;
    }
}

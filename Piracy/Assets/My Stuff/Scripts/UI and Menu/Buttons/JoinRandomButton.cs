using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinRandomButton : BasicButton
{
    protected override async void OnClick()
    {
        bool result = await ScenelessDependencies.Singleton.NetworkHelper.JoinRandom();

        if (result)
        {
            base.OnClick();
        }
        else
        {
            ButtonsFrozen = false;
        }
    }
}

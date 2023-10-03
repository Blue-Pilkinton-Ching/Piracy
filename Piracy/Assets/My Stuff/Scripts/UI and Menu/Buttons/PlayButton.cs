using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class PlayButton : BasicButton
{
    protected override async void OnClick()
    {
        bool result = await ScenelessDependencies.Singleton.NetworkHelper.AuthenticatePlayer();
        ScenelessDependencies.Singleton.VivoxManager.Initialize();

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

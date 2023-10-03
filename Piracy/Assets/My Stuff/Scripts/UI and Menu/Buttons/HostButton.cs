using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostButton : BasicButton
{
    [SerializeField]
    private VisibilityButton visibilityButton;

    protected override async void OnClick()
    {
        bool result = await ScenelessDependencies.Singleton.NetworkHelper.Host(visibilityButton.IsPrivate);

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

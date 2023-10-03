using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayJoinCode : ButtonBehaviour
{
    private void OnEnable() {
        buttonText.text = ScenelessDependencies.Singleton.NetworkHelper.Lobby.LobbyCode;
    }
    protected override void OnClick()
    {
        GUIUtility.systemCopyBuffer = ScenelessDependencies.Singleton.NetworkHelper.Lobby.LobbyCode;
    }
}

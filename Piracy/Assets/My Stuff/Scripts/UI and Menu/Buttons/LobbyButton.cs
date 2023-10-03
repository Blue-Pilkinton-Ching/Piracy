using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class LobbyButton : ButtonBehaviour
{
    bool isReady = false;
    protected override void OnClick()
    {
        UpdateButton(true);
    }

    protected override void Awake() 
    {
        base.Awake();
        ScenelessDependencies.Singleton.OnPartnerClientManagerChange += PartnerClientManagerChanged;
    }
    private void PartnerClientManagerChanged() 
    {
        if (ScenelessDependencies.Singleton.PartnerClientManager != null)
        {
            ScenelessDependencies.Singleton.PartnerClientManager.Ready.OnValueChanged += PartnerReadyStatusChanged;
        }
    }
    private void PartnerReadyStatusChanged(bool previous, bool current) 
    {
        UpdateButton(false);
    }

    private void UpdateButton(bool buttonPressed, bool partnerDisconnected = false) 
    {
        bool isPartnerReady = ScenelessDependencies.Singleton.PartnerClientManager?.Ready.Value == true && !partnerDisconnected;

        if (isPartnerReady && isReady && NetworkManager.Singleton.IsServer)
        {
            if (buttonPressed)
            {
                ButtonsFrozen = true;
                ScenelessDependencies.Singleton.GameLoader.StartGame();
                return;
            }
        }
        else
        {
            if (buttonPressed)
            {
                isReady = !isReady;
                ScenelessDependencies.Singleton.OwnerClientManager.SetReadyStatus(isReady); 
            }
        }

        if (isReady && isPartnerReady)
        {
            if (NetworkManager.Singleton.IsServer)
            {
                buttonText.text = "Start";
            }
            else
            {
                buttonText.text = "Waiting";
            }
            return;
        }
        if (isReady && !isPartnerReady)
        {
            buttonText.text = "Waiting";
            return;
        }
        if (!isReady)
        {
            buttonText.text = "Ready Up!";
            return;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Collections;
using TMPro;
using System;

public class NetworkedClientManager : NetworkBehaviour
{
    public NetworkVariable<FixedString32Bytes> Username {get; private set;} = 
        new NetworkVariable<FixedString32Bytes>(
            readPerm: NetworkVariableReadPermission.Everyone, 
            writePerm: NetworkVariableWritePermission.Owner);

    public NetworkVariable<bool> Ready { get; private set; } =
        new NetworkVariable<bool>(
            readPerm: NetworkVariableReadPermission.Everyone,
            writePerm: NetworkVariableWritePermission.Owner);
    
    private void Awake() {
        DontDestroyOnLoad(this);

        Username.OnValueChanged += UpdateUsername;
    }
    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            Username.Value = PlayerPrefs.GetString(ScenelessDependencies.Singleton.SharedKeys.UsernameSaveKey, "Noobie" + UnityEngine.Random.Range(0, 1000));
            ScenelessDependencies.Singleton.SetOwnerClientManager(this);
        }
        else
        {
            UpdateUsername();
            ScenelessDependencies.Singleton.SetPartnerClientManager(this);
        }
    }

    public void SetReadyStatus(bool status) 
    {
        Ready.Value = status;
    }

    private void UpdateUsername() {
        if (IsOwner)
        {
            TextMeshProUGUI text = MenuDependencies.Singleton.OwnerUsernameText;
            text.text = Username.Value.ToString();
        }
        else
        {
            TextMeshProUGUI text = MenuDependencies.Singleton.PartnerUsernameText;
            text.text = Username.Value.ToString();
        }
    }

    private void UpdateUsername(FixedString32Bytes previous, FixedString32Bytes current)
    {
        UpdateUsername();
    }
}

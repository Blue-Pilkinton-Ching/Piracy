using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Authentication;
using Unity.Services;
using Unity.Services.Core;
using System;
using System.Threading.Tasks;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using Unity.Services.Relay.Models;
using Unity.Services.Relay;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Vivox;

public class NetworkHelper : MonoBehaviour
{
    public Action<Exception> OnConnectionError;
    public Lobby Lobby { get; private set; } = null;
    public Allocation RelayAllocation { get; private set; } = null;
    public JoinAllocation RelayJoinAllocation { get; private set; } = null;

    string joinCodeID = "JoinCode";
    string guidID = "GUID";
    string seedID = "Seed";
    bool signedIn = false;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        ScenelessDependencies.Singleton.NetworkManager.OnClientConnectedCallback += OnClientConnected;
    }

    private void OnClientConnected(ulong id)
    {
        Debug.Log("Client " + id + " connected!");
    }

    private void ConnectionError(Exception ex)
    {
        Debug.LogWarning(ex);
    }

    public async Task<bool> AuthenticatePlayer()
    {

        if (signedIn)
        {
            return true;
        }

        signedIn = true;

        Debug.Log("Authenticating");

        var options = new InitializationOptions();

        // Uncomment this line in production
        //options.SetProfile("Profile");

        // Comment this line in production
        options.SetProfile(UnityEngine.Random.Range(int.MinValue, int.MaxValue).ToString());

        try
        {
            await UnityServices.InitializeAsync(options);
        }
        catch (Exception ex)
        {
            OnConnectionError.Invoke(ex);
            return false;
        }

        try
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
        catch (Exception ex)
        {
            OnConnectionError.Invoke(ex);
            return false;
        }

        return true;
    }

    public async Task<bool> JoinRandom()
    {
        return await JoinAsClient(true);
    }

    public async Task<bool> JoinByCode(string code)
    {
        return await JoinAsClient(false, code);
    }

    private async Task<bool> JoinAsClient(bool random, string code = "")
    {
        if (Lobby != null)
        {
            Debug.Log("Leaving Current Lobby");

            try
            {
                await LobbyService.Instance.RemovePlayerAsync(Lobby.Id, AuthenticationService.Instance.PlayerId);
            }
            catch (System.Exception ex)
            {
                OnConnectionError.Invoke(ex);
                return false;
            }
        }

        Debug.Log("Joining Lobby");

        try
        {
            if (random)
            {
                Lobby = await Lobbies.Instance.QuickJoinLobbyAsync();
            }
            else
            {
                Lobby = await Lobbies.Instance.JoinLobbyByCodeAsync(code);
            }
        }
        catch (System.Exception ex)
        {
            OnConnectionError.Invoke(ex);
            return false;
        }

        Debug.Log("Joining Allocation");

        try
        {
            RelayJoinAllocation = await RelayService.Instance.JoinAllocationAsync(Lobby.Data[joinCodeID].Value);
        }
        catch (System.Exception ex)
        {
            OnConnectionError.Invoke(ex);
            return false;
        }

        ScenelessDependencies.Singleton.UnityTransport.SetClientRelayData(RelayJoinAllocation.RelayServer.IpV4,
            (ushort)RelayJoinAllocation.RelayServer.Port,
            RelayJoinAllocation.AllocationIdBytes,
            RelayJoinAllocation.Key,
            RelayJoinAllocation.ConnectionData,
            RelayJoinAllocation.HostConnectionData);

        ScenelessDependencies.Singleton.SetGameSettings(
            new GameSettings(int.Parse(Lobby.Data[seedID].Value)));

        JoinNewVivoxChannel();

        NetworkManager.Singleton.StartClient();

        return true;
    }

    public async Task<bool> Host(bool isPrivate)
    {
        ScenelessDependencies.Singleton.SetGameSettings(new GameSettings(UnityEngine.Random.Range(int.MinValue, int.MaxValue)));

        Debug.Log("Creating Relay Allocation");

        try
        {
            RelayAllocation = await RelayService.Instance.CreateAllocationAsync(2);
        }
        catch (System.Exception ex)
        {
            OnConnectionError.Invoke(ex);
            return false;
        }

        Debug.Log("Getting Allocation Join Code");

        string joinCode;

        try
        {
            joinCode = await RelayService.Instance.GetJoinCodeAsync(RelayAllocation.AllocationId);
        }
        catch (System.Exception ex)
        {
            OnConnectionError.Invoke(ex);
            return false;
        }

        CreateLobbyOptions options = new CreateLobbyOptions();

        Dictionary<string, DataObject> lobbyOptionsData = new()
        {
          { seedID, new DataObject(visibility: DataObject.VisibilityOptions.Public, value: ScenelessDependencies.Singleton.GameSettings.Seed.ToString()) },
          { joinCodeID, new DataObject(visibility: DataObject.VisibilityOptions.Public, value: joinCode) },
          { guidID, new DataObject(visibility: DataObject.VisibilityOptions.Public, value: Guid.NewGuid().ToString()) }
        };
        options.Data = lobbyOptionsData;
        options.IsPrivate = isPrivate;

        Debug.Log("Creating Lobby");

        try
        {
            Lobby = await LobbyService.Instance.CreateLobbyAsync("Lobby", 2, options);
        }
        catch (System.Exception ex)
        {
            OnConnectionError.Invoke(ex);
            return false;
        }

        Debug.Log("Lobby Code: " + Lobby.LobbyCode);

        ScenelessDependencies.Singleton.UnityTransport.SetHostRelayData(RelayAllocation.RelayServer.IpV4,
            (ushort)RelayAllocation.RelayServer.Port,
            RelayAllocation.AllocationIdBytes,
            RelayAllocation.Key,
            RelayAllocation.ConnectionData);


        JoinNewVivoxChannel();

        NetworkManager.Singleton.StartHost();

        return true;
    }
    private void JoinNewVivoxChannel()
    {
        ScenelessDependencies.Singleton.VivoxManager.JoinChannelWhenReady(Lobby.Data[guidID].Value);
    }
    IEnumerator LobbyHeartBeat()
    {
        while (true)
        {
            yield return new WaitForSeconds(20);
            LobbyService.Instance.SendHeartbeatPingAsync(Lobby.Id);
        }
    }
}
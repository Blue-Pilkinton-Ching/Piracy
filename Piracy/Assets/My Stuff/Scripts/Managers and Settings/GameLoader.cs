using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoader : MonoBehaviour
{
    [SerializeField]
    private GameObject entityPrefab;
    
    [SerializeField]
    private GameObject playerPrefab;
    private void Awake() {
        ScenelessDependencies.Singleton.NetworkManager.OnServerStarted += OnServerStarted;
    }
    private void OnServerStarted() 
    {
        NetworkManager.Singleton.SceneManager.OnLoadEventCompleted += OnSceneLoaded;
    }

    public void StartGame() 
    {
        DOTween.KillAll();
        NetworkManager.Singleton.SceneManager.LoadScene(ScenelessDependencies.Singleton.SharedKeys.OrphangeSceneName, LoadSceneMode.Single);
    }
    private void OnSceneLoaded(string sceneName, LoadSceneMode loadSceneMode, List<ulong> clientsCompleted, List<ulong> clientsTimedOut)  
    {
        if (clientsTimedOut.Count > 0)
        {
            Debug.LogError("One or more clients timed out during scene loading");
            return;
        }
    }
}

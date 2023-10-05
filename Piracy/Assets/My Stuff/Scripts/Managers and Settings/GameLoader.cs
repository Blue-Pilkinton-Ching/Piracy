using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoader : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);

        ScenelessDependencies.Singleton.NetworkManager.OnServerStarted += OnServerStarted;
    }
    private void OnServerStarted()
    {
        NetworkManager.Singleton.SceneManager.OnLoadEventCompleted += OnSceneLoaded;
        StartGame();
    }

    public void StartGame()
    {
        DOTween.KillAll();
        NetworkManager.Singleton.SceneManager.LoadScene(ScenelessDependencies.Singleton.SharedKeys.GameSceneName, LoadSceneMode.Single);
    }
    private void OnSceneLoaded(string sceneName, LoadSceneMode loadSceneMode, List<ulong> clientsCompleted, List<ulong> clientsTimedOut)
    {
        Debug.Log("Game Loaded");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using TMPro;
using System;
using Cinemachine;

public class ScenelessDependencies : MonoBehaviour
{
    // Class that holds all Managers and Dependancies for easy accessibility
    // This class should ONLY hold dependencys for other classes, and not contain any functionality

    public static ScenelessDependencies Singleton;

    [field: Header("Managers & Controllers")]
    [field: SerializeField] public NetworkManager NetworkManager { get; private set; }
    [field: SerializeField] public NetworkHelper NetworkHelper { get; private set; }
    [field: SerializeField] public UnityTransport UnityTransport { get; private set; }
    [field: SerializeField] public GameLoader GameLoader { get; private set; }
    [field: SerializeField] public VivoxManager VivoxManager { get; private set; }
    [field: SerializeField] public AudioDeviceManager AudioDeviceManager { get; private set; }
    public GameSettings GameSettings { get; private set; }

    [field: Header("Settings")]
    [field: SerializeField] public SharedKeys SharedKeys { get; private set; }
    [field: SerializeField] public ButtonSettings ButtonSettings { get; private set; }
    [field: SerializeField] public VivoxCredentials VivoxCredentials { get; private set; }

    public void Awake()
    {
        Singleton = this;
        DontDestroyOnLoad(this);
    }
    public void SetGameSettings(GameSettings GameSettings)
    {
        this.GameSettings = GameSettings;
    }
}

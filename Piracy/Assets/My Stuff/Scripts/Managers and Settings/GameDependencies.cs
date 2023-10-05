using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDependencies : MonoBehaviour
{
    public static GameDependencies Singleton;

    [field: Header("Managers & Controllers")]
    [field: SerializeField] public MapGenerator MapGenerator { get; private set; }

    [field: Header("Settings")]

    float _;
    public void Awake()
    {
        Singleton = this;
        DontDestroyOnLoad(this);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDependencies : MonoBehaviour
{
    public static GameDependencies Singleton;

    [field: Header("Managers & Controllers")]
    [field: SerializeField] public MapGenerator MapGenerator { get; private set; }

    [field: Header("Settings")]

    [field: SerializeField] public MapGenSettings MapGenSettings { get; private set; }

    public void SetMapGenSettings(MapGenSettings settings)
    {
        MapGenSettings = settings;
    }

    public void Awake()
    {
        Singleton = this;
        DontDestroyOnLoad(this);
    }
}

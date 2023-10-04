using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameSettings
{

    public int Seed { get; }

    public GameSettings(int seed)
    {
        Seed = seed;
    }
}

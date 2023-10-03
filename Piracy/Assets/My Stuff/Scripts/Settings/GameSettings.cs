using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameSettings {
    public enum DifficultyOptions
    {
        Easy,
        Normal,
        Hard,
        Nightmare
    }
    public DifficultyOptions Difficulty {get;}
    public int Seed { get; }

    public GameSettings (DifficultyOptions difficulty, int seed) 
    {
        this.Difficulty = difficulty;
        Seed = seed;
    }
}

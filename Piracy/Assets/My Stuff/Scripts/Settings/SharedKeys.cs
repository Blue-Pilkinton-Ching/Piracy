using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RandomSettings", menuName = "ScriptableObjects/RandomSettings", order = 0)]
public class SharedKeys : ScriptableObject
{
    public string UsernameSaveKey = "Username";
    public string OrphangeSceneName = "Orphanage";

}
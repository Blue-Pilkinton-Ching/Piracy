using System;
using UnityEngine;

[CreateAssetMenu(fileName = "VivoxCredentials", menuName = "ScriptableObjects/VivoxCredentials", order = 0)]
public class VivoxCredentials : ScriptableObject {
    public string Server;
    public string Issuer;
    public string Domain;
    public string TolkenKey;
}
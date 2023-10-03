using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UsernameInputField : MonoBehaviour
{
    TMP_InputField inputField;

    private void Awake() {
        inputField = GetComponent<TMP_InputField>();
        inputField.onValueChanged.AddListener(UsernameChanged); 
    }

    private void UsernameChanged(string newUsername)
    {
        PlayerPrefs.SetString(ScenelessDependencies.Singleton.SharedKeys.UsernameSaveKey, newUsername);
        PlayerPrefs.Save();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuErrorAlert : MonoBehaviour
{
    public TextMeshProUGUI ErrorText;
    public GameObject ErrorObject;

    private void Awake() {
        ScenelessDependencies.Singleton.NetworkHelper.OnConnectionError += OnConnectionError;
    }

    private void OnConnectionError(System.Exception ex){
        ErrorText.text = ex.Message;

        ErrorObject.gameObject.SetActive(true);
    }
}

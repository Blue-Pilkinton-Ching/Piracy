using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[RequireComponent(typeof(TextMeshProUGUI))]
public class MicrophoneDeviceText : MonoBehaviour
{
    private TextMeshProUGUI text;
    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        ScenelessDependencies.Singleton.AudioDeviceManager.OnCurrentMicChanged += CurrentMicChanged;
    }

    private void CurrentMicChanged() 
    {
        text.text = ScenelessDependencies.Singleton.AudioDeviceManager.CurrentMicName;
    }
}

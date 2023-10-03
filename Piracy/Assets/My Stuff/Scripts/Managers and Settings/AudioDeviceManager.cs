using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class AudioDeviceManager : MonoBehaviour
{
    public string CurrentMicName {get; private set;}
    public Action OnCurrentMicChanged;
    private int currentMicIndex = 0;

    private void Awake() 
    {
        DontDestroyOnLoad(this);
    }

    private void Start() 
    {
        CurrentMicName = Microphone.devices[currentMicIndex];
        try
        {
            OnCurrentMicChanged.Invoke();
        }
        catch { }
    }

    public void SetNextMic() 
    {
        currentMicIndex += 1;
        currentMicIndex = currentMicIndex % Microphone.devices.Length;
        CurrentMicName = Microphone.devices[currentMicIndex];

        OnCurrentMicChanged.Invoke();
        
    }
    public void SetPrevMic()
    {
        currentMicIndex -= 1;
        currentMicIndex = (currentMicIndex + Microphone.devices.Length) % Microphone.devices.Length;
        CurrentMicName = Microphone.devices[currentMicIndex];

        OnCurrentMicChanged.Invoke();
    }
}

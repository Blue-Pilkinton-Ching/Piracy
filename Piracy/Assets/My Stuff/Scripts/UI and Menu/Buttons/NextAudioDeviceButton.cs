using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextAudioDeviceButton : ButtonBehaviour
{
    [SerializeField]
    private bool isNext = true;
    
    [SerializeField]
    private bool isSpeakers = true;
    protected override void OnClick()
    {
        if (isSpeakers)
        {
            if (isNext)
            {
                
            }
        }
        else
        {
            if (isNext)
            {
                ScenelessDependencies.Singleton.AudioDeviceManager.SetNextMic();
            }
            else
            {
                ScenelessDependencies.Singleton.AudioDeviceManager.SetPrevMic();
            }
        }
    }
}

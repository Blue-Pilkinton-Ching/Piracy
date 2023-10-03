using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public abstract class ButtonBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    // Any button class that inherits from this, can set 'shouldFreezeButtons' inside the Awake method, to freeze the buttons until ButtonsFrozen is set to false
    public static bool ButtonsFrozen {get; protected set;} = false;

    protected bool shouldFreezeButtons = false;

    Button button;

    protected TextMeshProUGUI buttonText;
    protected virtual void Awake(){
        button = GetComponent<Button>();
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!ButtonsFrozen)
        {
            button.image.DOColor(ScenelessDependencies.Singleton.ButtonSettings.HoverColor, ScenelessDependencies.Singleton.ButtonSettings.FadeDuration);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!ButtonsFrozen)
        {
            button.image.DOColor(ScenelessDependencies.Singleton.ButtonSettings.ClickColor, ScenelessDependencies.Singleton.ButtonSettings.FadeDuration).OnComplete(() =>
                button.image.DOColor(ScenelessDependencies.Singleton.ButtonSettings.NormalColor, ScenelessDependencies.Singleton.ButtonSettings.FadeDuration));

            if (shouldFreezeButtons)
            {
                ButtonsFrozen = true;
            }

            Invoke("OnClick", ScenelessDependencies.Singleton.ButtonSettings.ClickDelay);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!ButtonsFrozen)
        {
            button.image.DOColor(ScenelessDependencies.Singleton.ButtonSettings.NormalColor, ScenelessDependencies.Singleton.ButtonSettings.FadeDuration);
        }
    }
    
    protected abstract void OnClick();
}

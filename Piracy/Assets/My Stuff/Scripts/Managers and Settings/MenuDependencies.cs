using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuDependencies : MonoBehaviour
{
    public static MenuDependencies Singleton;

    [field: Header("Instantiated Component References")]
    [field: SerializeField] public TextMeshProUGUI OwnerUsernameText { get; private set; }
    [field: SerializeField] public TextMeshProUGUI PartnerUsernameText { get; private set; }

    private void Awake() {
        Singleton = this;
    }
}

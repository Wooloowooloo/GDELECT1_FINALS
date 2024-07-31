using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WristUI : MonoBehaviour
{
    public InputActionAsset InputActions;

    private Canvas wristUICanvas;
    private InputAction menu;

    private void Start()
    {
        wristUICanvas = GetComponent<Canvas>();
        menu = InputActions.FindActionMap("XRI LeftHand").FindAction("Menu");
        menu.Enable();
        menu.performed += ToggleMenu;
    }

    private void OnDestroy()
    {
        menu.performed -= ToggleMenu;
    }

    public void ToggleMenu(InputAction.CallbackContext context)
    {
        wristUICanvas.enabled = !wristUICanvas.enabled;
    }
}
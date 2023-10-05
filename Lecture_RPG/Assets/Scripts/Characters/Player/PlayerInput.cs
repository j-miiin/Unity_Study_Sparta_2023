using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public PlayerInputActions InputActions {  get; private set; }

    public PlayerInputActions.PlayerActions PlayerActions { get; private set; }

    // InputActionÀ» Å°°Å³ª ²¨ÁÜ
    private void OnEnable()
    {
        InputActions.Enable();
    }

    private void OnDisable()
    {
        InputActions.Disable();
    }
}

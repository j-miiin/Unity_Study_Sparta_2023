using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public event Action OnOpenPlayerStatusEvent;
    public event Action OnOpenPlayerInventoryEvent;
    public event Action OnClosePlayerInventoryEvent;

    public void CallOpenPlayerStatusEvent()
    {
        OnOpenPlayerStatusEvent?.Invoke();
    }

    public void CallOpenPlayerInventoryEvent()
    {
        OnOpenPlayerInventoryEvent?.Invoke();
    }

    public void CallOnClosePlayerInventoryEvent()
    {
        OnClosePlayerInventoryEvent?.Invoke();
    }
}

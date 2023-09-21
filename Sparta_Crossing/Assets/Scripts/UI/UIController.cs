using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public event Action OnOpenPlayerStatusEvent;
    public event Action OnOpenPlayerInventoryEvent;
    public event Action OnClosePlayerInventoryEvent;
    public event Action<ItemDTO, Vector3> OnShowItemDescEvent;
    public event Action OnHideItemDescEvent;

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

    public void CallOnShowItemDescEvent(ItemDTO item, Vector3 position)
    {
        OnShowItemDescEvent?.Invoke(item, position);
    }

    public void CallOnHideItemDescEvent()
    {
        OnHideItemDescEvent?.Invoke();
    }
}

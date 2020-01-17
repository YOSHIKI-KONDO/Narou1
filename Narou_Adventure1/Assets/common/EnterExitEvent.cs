using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnterExitEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Action EnterEvent;
    public Action ExitEvent;
    public Action DisableEvent;

    public void OnPointerEnter(PointerEventData e)
    {
        EnterEvent?.Invoke();
    }

    public void OnPointerExit(PointerEventData e)
    {
        ExitEvent?.Invoke();
    }

    void OnDisable()
    {
        DisableEvent?.Invoke();
    }
}

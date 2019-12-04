using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnterExitEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Action EnterEvent;
    public Action ExitEvent;

    public void OnPointerEnter(PointerEventData e)
    {
        EnterEvent();
    }

    public void OnPointerExit(PointerEventData e)
    {
        ExitEvent();
    }
}

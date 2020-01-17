using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class OnEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public List<UnityAction> EnterEvents = new List<UnityAction>();
    public List<UnityAction> ExitEvents = new List<UnityAction>();
    public List<UnityAction> DisableEvents = new List<UnityAction>();

    public void OnPointerEnter(PointerEventData e)
    {
        DoActions(EnterEvents);
    }

    public void OnPointerExit(PointerEventData e)
    {
        DoActions(ExitEvents);
    }

    void OnDisable()
    {
        DoActions(DisableEvents);
    }

    void DoActions(IEnumerable<UnityAction> actions)
    {
        foreach (var action in actions)
        {
            action?.Invoke();
        }
    }
}

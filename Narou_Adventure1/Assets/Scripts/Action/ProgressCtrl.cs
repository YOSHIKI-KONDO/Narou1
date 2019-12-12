using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

/// <summary>
/// OnlyActionのListを持っている。これは、各OnlyActionが生成された時に自動で追加されることになっている。
/// OnlyActionにSwitchProgressを持たせることで、同時に一つまでしか操作できないようにしている。
/// </summary>
public class ProgressCtrl : BASE {
    public List<OnlyAction> list = new List<OnlyAction>();
    public OnlyAction restFunction;
    public OnlyAction previousFunction;
    public OnlyAction currentFunction;

    //UI
    public Text text;

    //OnlyActionから呼ぶ
    public void SwitchProgress(OnlyAction function)
    {
        DeactivateAll();
        ActivateProgress(function); 
        previousFunction = function;
    }

    public void DeactivateAll()
    {
        foreach (var p in list)
        {
            p.isOn = false;
        }
    }

    public void ActivateProgress(OnlyAction function)
    {
        foreach (var p in list)
        {
            if(p == function)
            {
                p.isOn = true;
                currentFunction = p;
                //Focus関連
                ApplyFocus();
            }
        }
        currentFunction.SelectedAction?.Invoke();
    }

    public void Rest()
    {
        DeactivateAll();
        ActivateProgress(restFunction);
    }

    public void ActivatePrevious()
    {
        if (previousFunction == null) { return; }
        if(previousFunction == restFunction) { return;}
        DeactivateAll();
        ActivateProgress(previousFunction);
    }


    //apply関連
    void ApplyFocus()
    {
        if (currentFunction is AbilityFunction)
        {
            Debug.Log(currentFunction.name);
            main.focus.Activate();
        }
        else
        {
            main.focus.Deactivate();
        }
    }

    // Use this for initialization
    void Awake()
    {
        StartBASE();
    }

    private void Start()
    {
        ApplyFocus();
    }

    private void Update()
    {
        if(currentFunction != null)
        {
            text.text = currentFunction.actionName;
            text.text += "(" + (currentFunction.sliderValue * 100).ToString("F0") + "%)";
        }
    }
}

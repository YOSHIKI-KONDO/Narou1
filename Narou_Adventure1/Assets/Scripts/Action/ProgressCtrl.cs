using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class ProgressCtrl : BASE {
    public List<ProgressFunction> list = new List<ProgressFunction>();
    public ProgressFunction restFunction;
    public ProgressFunction previousFunction;
    public ProgressFunction currentFunction;
    //progressFunctionから呼ぶ
    public void SwitchProgress(ProgressFunction function)
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

    public void ActivateProgress(ProgressFunction function)
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
    }

    public void Rest()
    {
        DeactivateAll();
        ActivateProgress(restFunction);
    }

    public void ActivatePrevious()
    {
        if(previousFunction == restFunction) { return;}
        DeactivateAll();
        ActivateProgress(previousFunction);
    }


    //apply関連
    void ApplyFocus()
    {
        if (currentFunction is AbilityFunction)
        {
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
}

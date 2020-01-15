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
    public GameObject actionObj_main, actionObj_ability, actionObj_dungeon;
    List<(GameObject, ElementKind)> actionObjs = new List<(GameObject, ElementKind)>(); //actionObjを追加したらここに追加する。

    //OnlyActionから呼ぶ
    public void SwitchProgress(OnlyAction function)
    {
        DeactivateAll();
        ActivateProgress(function); 
        previousFunction = function;
        ApplyTopActionMark();
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
            }

            //行動マークの更新
            if (p.isOn == true  && p.isOnObj != null)
                setActive(p.isOnObj);
            if (p.isOn == false && p.isOnObj != null)
                setFalse(p.isOnObj);
        }
        currentFunction.SelectedAction?.Invoke();
    }

    void ApplyTopActionMark()
    {
        if(currentFunction == null) { return; }
        foreach (var obj in actionObjs)
        {
            if(obj.Item2 == currentFunction.elementKind)
            {
                setActive(obj.Item1);
            }
            else
            {
                setFalse(obj.Item1);
            }
        }
    }

    public void Rest()
    {
        DeactivateAll();
        ActivateProgress(restFunction);
    }

    public void DontDoAnything()
    {
        DeactivateAll();
        currentFunction = null;
        previousFunction = null;
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
        //ApplyFocus();
        actionObjs.Add((actionObj_main, ElementKind.main));
        actionObjs.Add((actionObj_ability, ElementKind.ability));
        actionObjs.Add((actionObj_dungeon, ElementKind.dungeon));
    }

    private void Update()
    {
        if(currentFunction != null)
        {
            text.text = currentFunction.actionName;
            text.text += "(" + (currentFunction.sliderValue * 100).ToString("F0") + "%)";
        }
        else
        {
            text.text = "None";
        }
    }
}

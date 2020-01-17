using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;
using MainAction;

/// <summary>
/// MEMO:enumはnull許容型にもできる
/// </summary>
[DefaultExecutionOrder(-10)]
public class ActionContainer : BASE {
    public bool checkHierarchy;
    public INSTANT_ACTION[] instants;
    public LOOP_ACTION[] loops;
    public UPGRADE_ACTION[] upgrades;

    // Use this for initialization
    void Awake()
    {
        StartBASE();

        instants = new INSTANT_ACTION[main.SD.num_instantAction];   //初期化
        loops = new LOOP_ACTION[main.SD.num_loopAction];            //初期化
        upgrades = new UPGRADE_ACTION[main.SD.num_upgradeAction];   //初期化
    }

    // Use this for initialization
    void Start()
    {
        if (checkHierarchy)
        {
            CheckHierarchy(instants);
            CheckHierarchy(loops);
            CheckHierarchy(upgrades);
        }
    }


    void CheckHierarchy<T>(T[] actions)
        where T:ACTION
    {
        for (int i = 0; i < actions.Length; i++)
        {
            if (i == 0) { continue; }
            if (actions[i] == null)
            {
                string sum = "";
                if(typeof(T) == typeof(INSTANT_ACTION))
                {
                    sum += "[Instant Action]" + main.enumCtrl.instantActions[i].Name();
                }
                else if(typeof(T) == typeof(LOOP_ACTION))
                {
                    sum += "[Loop Action]" + main.enumCtrl.loopActions[i].Name();
                }
                else if(typeof(T) == typeof(UPGRADE_ACTION))
                {
                    sum += "[Upgrade Action]" + main.enumCtrl.upgradeActions[i].Name();
                }
                else
                {
                    continue;
                }
                Debug.Log(sum + "がヒエラルキーにありません。");
            }
        }
    }

    void ZeroCheckEnum<T>(T[] ary)
        where T : Enum
    {
        for (int i = 0; i < ary.Length; i++)
        {
            if (i == 0) { continue; }
            if (ary[i].ToString() == Enum.GetName(typeof(T), 0))
            {
                Debug.Log("[" + (typeof(T)).ToString() + "]" + Enum.GetName(typeof(T), i) + "がヒエラルキーにありません。");
            }
        }
    }
}

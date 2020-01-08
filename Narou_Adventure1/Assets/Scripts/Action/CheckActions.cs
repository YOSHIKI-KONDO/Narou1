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
public class CheckActions : BASE {
    public ActionEnum.Instant[] instants;
    public ActionEnum.Loop[] loops;
    public ActionEnum.Upgrade[] upgrades;

    // Use this for initialization
    void Awake () {
		StartBASE();

        instants = new ActionEnum.Instant[main.SD.num_instantAction];   //初期化
        loops = new ActionEnum.Loop[main.SD.num_loopAction];            //初期化
        upgrades = new ActionEnum.Upgrade[main.SD.num_upgradeAction];   //初期化
    }

	// Use this for initialization
	void Start () {
        ZeroCheckEnum(instants);
        ZeroCheckEnum(loops);
        ZeroCheckEnum(upgrades);
    }

    void ZeroCheckEnum<T>(T[] ary)
        where T: Enum
    {
        for (int i = 0; i < ary.Length; i++)
        {
            if (i == 0) { continue; }
            if(ary[i].ToString() == Enum.GetName(typeof(T), 0))
            {
                Debug.Log("[" + (typeof(T)).ToString() + "]" + Enum.GetName(typeof(T), i) + "がヒエラルキーにありません。");
            }
        }
    }
}

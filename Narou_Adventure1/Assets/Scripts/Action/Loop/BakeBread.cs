using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class BakeBread : LOOP_ACTION
{
    public override bool Requires()
    {
        return main.itemCtrl.exitSourceNums[(int)NeedKind.kiln] > 0;
    }

    // Use this for initialization
    void Awake () {
        AwakeLoopAction(MainAction.ActionEnum.Loop.bake_bread, 10,1);
        progress.initCostList.Add(new Dealing(ResourceKind.dough, Dealing.R_ParaKind.current, -1));
        progress.progressCostList.Add(new Dealing(ResourceKind.action, Dealing.R_ParaKind.current, -0.3));
        progress.completeEffectList.Add(new Dealing(ResourceKind.bread, Dealing.R_ParaKind.current, 1));
        
        need.AddSourceNeed(NeedKind.kiln);
    }

	// Use this for initialization
	void Start () {
        StartLoopAction();
	}
	
	// Update is called once per frame
	void Update () {
        UpdateLoopAction();
	}

    void FixedUpdate()
    {
        FixedUpdateLoopAction();
    }
}

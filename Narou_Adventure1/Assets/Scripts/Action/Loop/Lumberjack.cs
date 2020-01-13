using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class Lumberjack : LOOP_ACTION
{
    public override bool Requires()
    {
        return main.a_rsc.CurrentLevels[(int)AbilityKind.use_tools] >= 4;
    }

    // Use this for initialization
    void Awake () {
        AwakeLoopAction(MainAction.ActionEnum.Loop.lumberjack, 10,1);
        progress.progressCostList.Add(new Dealing(ResourceKind.action, Dealing.R_ParaKind.current, -0.8));
        progress.completeEffectList.Add(new Dealing(ResourceKind.wood, Dealing.R_ParaKind.current, 1));
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

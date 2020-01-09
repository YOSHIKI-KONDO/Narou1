using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class Pickpocket : LOOP_ACTION
{
    public override bool Requires()
    {
        return main.a_rsc.CurrentLevels[(int)AbilityKind.seclusion] >= 1;
    }

    // Use this for initialization
    void Awake () {
        AwakeLoopAction(MainAction.ActionEnum.Loop.pickpocket, 1,1);
        progress.progressCostList.Add(new Dealing(ResourceKind.action, Dealing.R_ParaKind.current, -0.5));
        progress.completeEffectList.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, 1));
        progress.completeEffectList.Add(new Dealing(ResourceKind.research, Dealing.R_ParaKind.current, -0.2));
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

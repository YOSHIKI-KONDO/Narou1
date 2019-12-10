﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class HarvestWheat : LOOP_ACTION
{
    public override bool Requires()
    {
        return main.a_rsc.CurrentLevels[(int)AbilityKind.use_tools] >= 3;
    }

    // Use this for initialization
    void Awake () {
        AwakeLoopAction(MainAction.ActionEnum.Loop.harvest_wheat, 5);
        progress.progressCostList.Add(new Dealing(ResourceKind.stamina, Dealing.R_ParaKind.current, -0.5));
        progress.progressEffectList.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, 0.1));
        progress.completeEffectList.Add(new Dealing(ResourceKind.wheat, Dealing.R_ParaKind.current, 1));
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

﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class WritingPaper : INSTANT_ACTION
{
    public override bool Requires()
    {
        return main.a_rsc.CurrentLevels[(int)AbilityKind.life_magic] >= 3;
    }

    // Use this for initialization
    void Awake()
    {
        AwakeInstantAction(MainAction.ActionEnum.Instant.writing_paper);
        instant.initCostList.Add(new Dealing(ResourceKind.mp, Dealing.R_ParaKind.current, -1));
        instant.initCostList.Add(new Dealing(ResourceKind.research, Dealing.R_ParaKind.current, -10));
        instant.completeEffectList.Add(new Dealing(ResourceKind.paper, Dealing.R_ParaKind.current, 1));
	}

	// Use this for initialization
	void Start ()
    {
        StartInstantAction();
	}
	
	// Update is called once per frame
	void Update ()
    {
        UpdateInstantAction();
	}

    void FixedUpdate()
    {
        FixedUpdateInstantAction();
    }
}

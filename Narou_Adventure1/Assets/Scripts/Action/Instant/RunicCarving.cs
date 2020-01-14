using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class RunicCarving : INSTANT_ACTION
{
    public override bool Requires()
    {
        return main.a_rsc.CurrentLevels[(int)AbilityKind.life_magic] >= 7;
    }

    // Use this for initialization
    void Awake()
    {
        AwakeInstantAction(MainAction.ActionEnum.Instant.runic_carving);
        instant.initCostList.Add(new Dealing(ResourceKind.mp, Dealing.R_ParaKind.current, -3));
        instant.initCostList.Add(new Dealing(ResourceKind.research, Dealing.R_ParaKind.current, -50));
        instant.initCostList.Add(new Dealing(ResourceKind.paper, Dealing.R_ParaKind.current, -20));
        instant.initCostList.Add(new Dealing(ResourceKind.magi_stone, Dealing.R_ParaKind.current, -1));
        instant.completeEffectList.Add(new Dealing(ResourceKind.book, Dealing.R_ParaKind.current, 1));
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

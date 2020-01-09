using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class Mugged : INSTANT_ACTION
{
    public override bool Requires()
    {
        return main.a_rsc.CurrentLevels[(int)AbilityKind.hoodlum] >=1;
    }

    // Use this for initialization
    void Awake()
    {
        AwakeInstantAction(MainAction.ActionEnum.Instant.mugged);
        instant.initCostList.Add(new Dealing(ResourceKind.action, Dealing.R_ParaKind.current, -0.5));
        instant.initCostList.Add(new Dealing(ResourceKind.research, Dealing.R_ParaKind.current, -0.2));
        instant.completeEffectList.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, 1));
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

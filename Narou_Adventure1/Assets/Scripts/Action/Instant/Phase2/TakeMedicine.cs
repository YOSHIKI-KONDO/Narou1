using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class TakeMedicine : INSTANT_ACTION
{
    public override bool Requires()
    {
        return main.a_rsc.CurrentLevels[(int)AbilityKind.doctor] >=1;
    }

    // Use this for initialization
    void Awake()
    {
        AwakeInstantAction(MainAction.ActionEnum.Instant.take_medicine);
        instant.initCostList.Add(new Dealing(ResourceKind.medicine, Dealing.R_ParaKind.current, -1));
        instant.completeEffectList.Add(new Dealing(ResourceKind.hp, Dealing.R_ParaKind.current, 5));
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

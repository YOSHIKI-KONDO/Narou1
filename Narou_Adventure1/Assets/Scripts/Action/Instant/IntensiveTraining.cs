using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class IntensiveTraining : INSTANT_ACTION
{
    public override bool Requires()
    {
        return main.SR.clearNum_upgrade[(int)MainAction.ActionEnum.Upgrade.talk_fatherD] >= 1;
    }

    // Use this for initialization
    void Awake()
    {
        AwakeInstantAction(MainAction.ActionEnum.Instant.intensive_training);
        instant.initCostList.Add(new Dealing(ResourceKind.hp, Dealing.R_ParaKind.current, -0.2));
        instant.completeEffectList.Add(new Dealing(ResourceKind.research, Dealing.R_ParaKind.current, 0.5));
    }

    // Use this for initialization
    void Start()
    {
        StartInstantAction();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInstantAction();
    }

    void FixedUpdate()
    {
        FixedUpdateInstantAction();
    }
}

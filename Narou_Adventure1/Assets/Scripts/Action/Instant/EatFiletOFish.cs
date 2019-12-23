using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class EatFiletOFish : INSTANT_ACTION
{
    public override bool Requires()
    {
        return main.rsc.Max((int)ResourceKind.stamina) >= 5 &&
               main.rsc.Value[(int)ResourceKind.anchovy_sandwich] >= 1;
    }

    // Use this for initialization
    void Awake()
    {
        AwakeInstantAction(MainAction.ActionEnum.Instant.eat_filet_o_fish);
        instant.initCostList.Add(new Dealing(ResourceKind.filet_o_fish, Dealing.R_ParaKind.current, -1));
        instant.completeEffectList.Add(new Dealing(ResourceKind.mp, Dealing.R_ParaKind.current, 5));
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

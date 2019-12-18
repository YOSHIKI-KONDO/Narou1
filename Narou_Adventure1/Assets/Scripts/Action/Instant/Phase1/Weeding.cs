using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class Weeding : INSTANT_ACTION
{
    public override bool Requires()
    {
        return main.rsc.Value[(int)ResourceKind.research] >= 5 &&
               main.rsc.Value[(int)ResourceKind.mp] >= 1;
    }

    // Use this for initialization
    void Awake()
    {
        AwakeInstantAction(MainAction.ActionEnum.Instant.weeding);
        instant.initCostList.Add(new Dealing(ResourceKind.stamina, Dealing.R_ParaKind.current, -0.4));
        instant.completeEffectList.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, 0.5));
        instant.completeEffectList.Add(new Dealing(ResourceKind.herb, Dealing.R_ParaKind.current, 0.05));
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

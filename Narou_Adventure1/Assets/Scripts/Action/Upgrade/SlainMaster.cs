using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class SlainMaster : UPGRADE_ACTION
{
    public override bool Requires()
    {
        return main.rsc.Value[(int)ResourceKind.codices] >= 10;
    }

    // Use this for initialization
    void Awake()
    {
        AwakeUpgradeAction(MainAction.ActionEnum.Upgrade.SlainMaster, 3, 10);
        progress.initCostList.Add(new Dealing(ResourceKind.codices, Dealing.R_ParaKind.current, -1));
        progress.progressCostList.Add(new Dealing(ResourceKind.mana, Dealing.R_ParaKind.current, -1));
        progress.progressEffectList.Add(new Dealing(ResourceKind.scrolls, Dealing.R_ParaKind.current, 1));
        progress.completeEffectList.Add(new Dealing(ResourceKind.fire, Dealing.R_ParaKind.max, 100));
        progress.completeEffectList.Add(new Dealing(ResourceKind.water, Dealing.R_ParaKind.regen, 10));
    }

    // Use this for initialization
    void Start()
    {
        StartUpgradeAction();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUpgradeAction();
    }

    void FixedUpdate()
    {
        FixedUpdateUpgradeAction();
    }
}

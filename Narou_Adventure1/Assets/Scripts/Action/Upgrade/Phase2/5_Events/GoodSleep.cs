using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class GoodSleep : UPGRADE_ACTION
{
    public override bool Requires()
    {
        return main.SR.clearNum_upgrade[(int)MainAction.ActionEnum.Upgrade.into_a_dormitory] >= 1;
    }
    public override void CompleteAction()
    {
        main.SR.level_loop[(int)MainAction.ActionEnum.Loop.rest] = 100;
    }

    // Use this for initialization
    void Awake () {
        AwakeUpgradeAction(MainAction.ActionEnum.Upgrade.good_sleep, 1, 0, null, false, false);
    }

	// Use this for initialization
	void Start () {
        StartUpgradeAction();
	}
	
	// Update is called once per frame
	void Update () {
        UpdateUpgradeAction();
	}

    void FixedUpdate()
    {
        FixedUpdateUpgradeAction();
    }
}

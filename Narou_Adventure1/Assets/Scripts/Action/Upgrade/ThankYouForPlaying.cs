﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class ThankYouForPlaying : UPGRADE_ACTION
{
    public override bool Requires()
    {
        return main.SR.clearNum_Dungeon[(int)DungeonKind.demonic_cellar] >= 1;
    }

    public override void CompleteAction()
    {
        main.uiCtrl.thankPanel.SetActive(true);
    }

    // Use this for initialization
    void Awake () {
        AwakeUpgradeAction(MainAction.ActionEnum.Upgrade.thank_you_for_playing, 1, 0, null, false, false);
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

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class ThankYouForPlaying : UPGRADE_ACTION
{
    public override bool Requires()
    {
        return main.SR.clearNum_upgrade[(int)MainAction.ActionEnum.Upgrade.apprentice_warrior] >= 1 ||
               main.SR.clearNum_upgrade[(int)MainAction.ActionEnum.Upgrade.apprentice_sorcerer] >= 1 ||
               main.SR.clearNum_upgrade[(int)MainAction.ActionEnum.Upgrade.apprentice_tamer] >= 1 ;
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

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class AcademicCity : UPGRADE_ACTION
{
    public override bool Requires()
    {
        return main.SR.clearNum_upgrade[(int)MainAction.ActionEnum.Upgrade.leave_the_town] >= 1;
    }
    public override void CompleteAction()
    {
        main.SR.released_Norn = true;
        main.SR.watched_element[(int)ElementKind.status] = false;
    }

    void Awake () {
        AwakeUpgradeAction(MainAction.ActionEnum.Upgrade.academic_city, 1, 0, null, false, false);
        progress.initCostList.Add(new Dealing(ResourceKind.anchovy_sandwich, Dealing.R_ParaKind.current, -2));
        progress.completeEffectList.Add(new Dealing(ResourceKind.research, Dealing.R_ParaKind.max, 10));
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

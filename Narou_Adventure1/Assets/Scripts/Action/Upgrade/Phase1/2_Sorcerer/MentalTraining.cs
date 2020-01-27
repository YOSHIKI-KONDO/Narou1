using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class MentalTraining : UPGRADE_ACTION
{
    public override bool Requires()
    {
        return main.SR.clearNum_upgrade[(int)MainAction.ActionEnum.Upgrade.talk_fatherA] >= 1;
    }
    public override bool CompleteCondition()
    {
        return main.SR.clearNum_upgrade[(int)MainAction.ActionEnum.Upgrade.academic_city] >= 1;
    }

    public override void CompleteAction()
    {
        //フォーカス解放
        if (main.SR.released_resource[(int)ResourceKind.focus] == false)
        {
            main.rsc.Max_Base[(int)ResourceKind.focus] = 1;
            main.rsc.Value[(int)ResourceKind.focus] = 1;
            StartCoroutine(NewInvokeCor(() => //1秒遅れでアナウンスする。
            main.announce.Add("<b><i>By using mp, you can use \"focus\". Focus makes the progress of your actions speed up.</i></b>"),0.1f));
        }
    }

    // Use this for initialization
    void Awake () {
        AwakeUpgradeAction(MainAction.ActionEnum.Upgrade.mental_training, 5,10);
        progress.progressCostList.Add(new Dealing(ResourceKind.action, Dealing.R_ParaKind.current, -0.4));
        progress.completeEffectList.Add(new Dealing(ResourceKind.mp, Dealing.R_ParaKind.max, 1));
        progress.completeEffectList.Add(new Dealing(ResourceKind.hp, Dealing.R_ParaKind.max, 0.2));
        progress.completeEffectList.Add(new Dealing(ResourceKind.research, Dealing.R_ParaKind.current, 1));
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

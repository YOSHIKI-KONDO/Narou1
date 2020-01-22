using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class BuyBag : UPGRADE_ACTION
{
    public override bool Requires()
    {
        return main.SR.clearNum_upgrade[(int)MainAction.ActionEnum.Upgrade.go_to_the_town] >= 1; //main.a_rsc.CurrentLevels[(int)AbilityKind.use_tools] >= 1 ||
               //main.a_rsc.CurrentLevels[(int)AbilityKind.life_magic] >= 1;
    }
    public override void CompleteAction()
    {
        main.SR.released_element[(int)ElementKind.item] = true;
        main.announce.Add("<b><i>Equip items! You can purchase items in the Shop. Items work by equipping them.</i></b>");
        main.announce.Add("<b><i>Also new items will be unlocked by Upgrade Actions and new resources.</i></b>");
    }

    // Use this for initialization
    void Awake () {
        AwakeUpgradeAction(MainAction.ActionEnum.Upgrade.buy_bag, 1, 0, null, false, false);
        progress.initCostList.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, -30));
        progress.completeEffectList.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.max, 40));
        progress.completeEffectList.Add(new Dealing(ResourceKind.equipSpace, Dealing.R_ParaKind.max, 5));
        progress.completeEffectList.Add(new Dealing(ResourceKind.inventorySpace, Dealing.R_ParaKind.max, 5));
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

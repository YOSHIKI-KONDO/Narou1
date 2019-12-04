using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class AB_Lore : ABILITY {

	// Use this for initialization
	void Awake () {
        AwakeAbility(AbilityKind.lore, 100, 1.5);
        progress.unlockCostList.Add(new Dealing(ResourceKind.mana, Dealing.R_ParaKind.current, -10));
        progress.progressCostList.Add(new Dealing(ResourceKind.mana, Dealing.R_ParaKind.current, -1));
        progress.completeEffectList.Add(new Dealing(ResourceKind.mana, Dealing.R_ParaKind.max, 5));
	}

	// Use this for initialization
	void Start () {
        StartAbility();
	}
	
	// Update is called once per frame
	void Update () {
        UpdateAbility();
	}

    private void FixedUpdate()
    {
        FixedUpdateAbility();
    }
}

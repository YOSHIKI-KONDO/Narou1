using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class PickingUpCoins : SKILL
{
    public override bool Requires()
    {
        return main.a_rsc.CurrentLevels[(int)AbilityKind.animal_handling] >= 4;
    }

    // Use this for initialization
    void Awake () {
		AwakeSkill(SkillKind.picking_up_coins, 9);
        learnF.initCostList.Add(new Dealing(ResourceKind.research, Dealing.R_ParaKind.current, -5));
        useCosts.Add(new Dealing(ResourceKind.animal, Dealing.R_ParaKind.current, -2));
        useEffects.Add(new Temp_Regen_Deal(ResourceKind.gold, 0.5, 20));

        SetSource(NeedKind.production, NeedKind.animal);
    }

	// Use this for initialization
	void Start () {
        StartSkill();
	}
	
	// Update is called once per frame
	void Update () {
        UpdateSkill();
	}

    private void FixedUpdate()
    {
        FixedUpdateSkill();
    }
}

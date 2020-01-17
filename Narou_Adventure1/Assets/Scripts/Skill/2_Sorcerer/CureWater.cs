using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class CureWater : SKILL
{
    public override bool Requires()
    {
        return false;//main.a_rsc.CurrentLevels[(int)AbilityKind.primary_water_magic] >= 7;
    }

    // Use this for initialization
    void Awake () {
		AwakeSkill(SkillKind.cure_water, 2);
        learnF.initCostList.Add(new Dealing(ResourceKind.research, Dealing.R_ParaKind.current, -5));
        useCosts.Add(new Dealing(ResourceKind.water, Dealing.R_ParaKind.current, -3));
        useEffects.Add(new Temp_Regen_Deal(ResourceKind.hp, 0.2, 20));

        SetSource(NeedKind.enhance, NeedKind.water);
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

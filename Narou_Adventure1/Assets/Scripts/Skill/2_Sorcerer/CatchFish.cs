using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class CatchFish : SKILL
{
    public override bool Requires()
    {
        return main.a_rsc.CurrentLevels[(int)AbilityKind.primary_water_magic] >= 4;
    }

    // Use this for initialization
    void Awake () {
		AwakeSkill(SkillKind.catch_fish, 10);
        learnF.initCostList.Add(new Dealing(ResourceKind.research, Dealing.R_ParaKind.current, -10));
        useCosts.Add(new Dealing(ResourceKind.water, Dealing.R_ParaKind.current, -4));
        useEffects.Add(new Temp_Regen_Deal(ResourceKind.fish, 0.5, 14));

        SetSource(NeedKind.production, NeedKind.water);
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

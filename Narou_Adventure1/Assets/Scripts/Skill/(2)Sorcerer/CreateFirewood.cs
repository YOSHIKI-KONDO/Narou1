using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class CreateFirewood : SKILL
{
    public override bool Requires()
    {
        return main.a_rsc.CurrentLevels[(int)AbilityKind.primary_wind_magic] >= 4;
    }

    // Use this for initialization
    void Awake () {
		AwakeSkill(SkillKind.create_firewood, 10);
        learnF.initCostList.Add(new Dealing(ResourceKind.research, Dealing.R_ParaKind.current, -5));
        useCosts.Add(new Dealing(ResourceKind.wind, Dealing.R_ParaKind.current, -1));
        useCosts.Add(new Dealing(ResourceKind.wood, Dealing.R_ParaKind.current, -1));
        useEffects.Add(new Temp_Regen_Deal(ResourceKind.firewood, 1, 8));

        SetSource(NeedKind.production, NeedKind.wind);
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

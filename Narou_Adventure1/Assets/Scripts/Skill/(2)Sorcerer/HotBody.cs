using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class HotBody : SKILL
{
    public override bool Requires()
    {
        return main.a_rsc.CurrentLevels[(int)AbilityKind.primary_fire_magic] >= 6;
    }

    // Use this for initialization
    void Awake () {
		AwakeSkill(SkillKind.hot_body, 2);
        learnF.initCostList.Add(new Dealing(ResourceKind.research, Dealing.R_ParaKind.current, -5));
        useCosts.Add(new Dealing(ResourceKind.fire, Dealing.R_ParaKind.current, -1));
        useEffects.Add(new Temp_Regen_Deal(ResourceKind.stamina, 0.1, 20));
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

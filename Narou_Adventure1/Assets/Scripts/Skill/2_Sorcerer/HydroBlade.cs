using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class HydroBlade : SKILL
{
    public override bool Requires()
    {
        return main.a_rsc.CurrentLevels[(int)AbilityKind.primary_water_magic] >= 7;
    }

    // Use this for initialization
    void Awake () {
		AwakeSkill(SkillKind.hydro_blade, 1);
        learnF.initCostList.Add(new Dealing(ResourceKind.research, Dealing.R_ParaKind.current, -5));
        useCosts.Add(new Dealing(ResourceKind.mp, Dealing.R_ParaKind.current, -0.8));
        useCosts.Add(new Dealing(ResourceKind.water, Dealing.R_ParaKind.current, -1.8));
        sorcererAtks.Add(new SorcererAttack(19));

        SetSource(NeedKind.attack, NeedKind.water);
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

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class Waterball : SKILL
{
    public override bool Requires()
    {
        return main.a_rsc.CurrentLevels[(int)AbilityKind.primary_water_magic] >= 1;
    }

    // Use this for initialization
    void Awake () {
		AwakeSkill(SkillKind.waterball, 3);
        learnF.initCostList.Add(new Dealing(ResourceKind.research, Dealing.R_ParaKind.current, -5));
        useCosts.Add(new Dealing(ResourceKind.water, Dealing.R_ParaKind.current, -0.9));
        sorcererAtks.Add(new SorcererAttack(16));

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

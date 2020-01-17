using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class BurningShot : SKILL
{
    public override bool Requires()
    {
        return main.a_rsc.CurrentLevels[(int)AbilityKind.primary_fire_magic] >= 7;
    }

    // Use this for initialization
    void Awake () {
		AwakeSkill(SkillKind.burning_shot, 1.5);
        learnF.initCostList.Add(new Dealing(ResourceKind.research, Dealing.R_ParaKind.current, -5));
        useCosts.Add(new Dealing(ResourceKind.mp, Dealing.R_ParaKind.current, -0.8));
        useCosts.Add(new Dealing(ResourceKind.fire, Dealing.R_ParaKind.current, -2));
        sorcererAtks.Add(new SorcererAttack(28));

        SetSource(NeedKind.attack, NeedKind.fire);
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

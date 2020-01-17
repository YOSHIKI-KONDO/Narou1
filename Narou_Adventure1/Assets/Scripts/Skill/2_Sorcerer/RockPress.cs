using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class RockPress : SKILL
{
    public override bool Requires()
    {
        return main.a_rsc.CurrentLevels[(int)AbilityKind.primary_earth_magic] >= 7;
    }

    // Use this for initialization
    void Awake () {
		AwakeSkill(SkillKind.rock_press, 2);
        learnF.initCostList.Add(new Dealing(ResourceKind.research, Dealing.R_ParaKind.current, -5));
        useCosts.Add(new Dealing(ResourceKind.mp, Dealing.R_ParaKind.current, -0.8));
        useCosts.Add(new Dealing(ResourceKind.earth, Dealing.R_ParaKind.current, -2.4));
        sorcererAtks.Add(new SorcererAttack(32));

        SetSource(NeedKind.attack, NeedKind.earth);
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

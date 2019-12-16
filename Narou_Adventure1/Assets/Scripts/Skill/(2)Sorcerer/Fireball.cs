using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class Fireball : SKILL
{
    public override bool Requires()
    {
        return main.a_rsc.CurrentLevels[(int)AbilityKind.primary_fire_magic] >= 1;
    }

    // Use this for initialization
    void Awake () {
		AwakeSkill(SkillKind.fireball, 4);
        learnF.initCostList.Add(new Dealing(ResourceKind.research, Dealing.R_ParaKind.current, -5));
        useCosts.Add(new Dealing(ResourceKind.fire, Dealing.R_ParaKind.current, -1.2));
        sorcererAtks.Add(new SorcererAttack(8));
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

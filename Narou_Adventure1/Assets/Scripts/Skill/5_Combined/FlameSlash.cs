using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class FlameSlash : SKILL
{
    public override bool Requires()
    {
        return main.a_rsc.CurrentLevels[(int)AbilityKind.beginner_swordmanship] >= 5 &&
               main.a_rsc.CurrentLevels[(int)AbilityKind.primary_fire_magic] >= 1;
    }

    // Use this for initialization
    void Awake () {
		AwakeSkill(SkillKind.flame_slash, 2);
        learnF.initCostList.Add(new Dealing(ResourceKind.research, Dealing.R_ParaKind.current, -20));
        useCosts.Add(new Dealing(ResourceKind.stamina, Dealing.R_ParaKind.current, -1.2));
        useCosts.Add(new Dealing(ResourceKind.fire, Dealing.R_ParaKind.current, -1.2));
        warriorAtks.Add(new WarriorAttack(14));
        sorcererAtks.Add(new SorcererAttack(14));

        SetSource(NeedKind.attack, NeedKind.sword, NeedKind.fire);
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

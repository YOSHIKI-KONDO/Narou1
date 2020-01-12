using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class RightSlash : SKILL
{
    public override bool Requires()
    {
        return main.a_rsc.CurrentLevels[(int)AbilityKind.beginner_swordmanship] >= 1;
    }

    // Use this for initialization
    void Awake () {
		AwakeSkill(SkillKind.right_slash, 2);
        learnF.initCostList.Add(new Dealing(ResourceKind.research, Dealing.R_ParaKind.current, -5));
        useCosts.Add(new Dealing(ResourceKind.stamina, Dealing.R_ParaKind.current, -1));
        warriorAtks.Add(new WarriorAttack(12));

        SetSource(NeedKind.attack, NeedKind.sword, NeedKind.combo_arts);

        combo = new Cost_SC(NeedKind.combo_arts, 0.5f);
        combo = new Effect_SC(NeedKind.combo_arts, 10f);
        combo = new Interval_SC(NeedKind.combo_arts, 0.1f);
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

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class UpperSlash : SKILL
{
    public override bool Requires()
    {
        return main.a_rsc.CurrentLevels[(int)AbilityKind.beginner_spearmanship] >= 7;
    }

    // Use this for initialization
    void Awake () {
		AwakeSkill(SkillKind.upper_slash, 2.3);
        learnF.initCostList.Add(new Dealing(ResourceKind.research, Dealing.R_ParaKind.current, -5));
        useCosts.Add(new Dealing(ResourceKind.stamina, Dealing.R_ParaKind.current, -1.3));
        warriorAtks.Add(new WarriorAttack(15));

        SetSource(NeedKind.attack, NeedKind.spear, NeedKind.combo_arts);

        combo = new Interval_SC(NeedKind.combo_arts, 0.7f);
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

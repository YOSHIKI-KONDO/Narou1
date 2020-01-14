﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class LeftSmallSwing : SKILL
{
    public override bool Requires()
    {
        return main.a_rsc.CurrentLevels[(int)AbilityKind.beginner_bojutsu] >= 1;
    }

    // Use this for initialization
    void Awake () {
		AwakeSkill(SkillKind.left_small_swing, 1);
        learnF.initCostList.Add(new Dealing(ResourceKind.research, Dealing.R_ParaKind.current, -5));
        useCosts.Add(new Dealing(ResourceKind.stamina, Dealing.R_ParaKind.current, -1));
        warriorAtks.Add(new WarriorAttack(6));

        SetSource(NeedKind.attack, NeedKind.rod, NeedKind.combo_arts);

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
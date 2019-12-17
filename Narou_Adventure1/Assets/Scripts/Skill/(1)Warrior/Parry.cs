﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class Parry : SKILL
{
    public override bool Requires()
    {
        return main.a_rsc.CurrentLevels[(int)AbilityKind.beginner_bojutsu] >= 6;
    }

    // Use this for initialization
    void Awake () {
		AwakeSkill(SkillKind.parry, 1.5);
        learnF.initCostList.Add(new Dealing(ResourceKind.stamina, Dealing.R_ParaKind.current, -3));
        useCosts.Add(new Dealing(ResourceKind.mp, Dealing.R_ParaKind.current, -1));
        useEffects.Add(new Temp_Regen_Deal(ResourceKind.strength, 10, 4));
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
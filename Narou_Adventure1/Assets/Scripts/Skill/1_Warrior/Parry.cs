using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class Parry : SKILL
{
    public override bool Requires()
    {
        return main.a_rsc.CurrentLevels[(int)AbilityKind.beginner_bojutsu] >= 7;
    }

    // Use this for initialization
    void Awake () {
		AwakeSkill(SkillKind.parry, 1.5);
        learnF.initCostList.Add(new Dealing(ResourceKind.stamina, Dealing.R_ParaKind.current, -3));
        useCosts.Add(new Dealing(ResourceKind.stamina, Dealing.R_ParaKind.current, -1));
        useEffects.Add(new Temp_Regen_Deal(ResourceKind.strength, 10, 4));

        SetSource(NeedKind.enhance, NeedKind.rod, NeedKind.combo_arts);

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

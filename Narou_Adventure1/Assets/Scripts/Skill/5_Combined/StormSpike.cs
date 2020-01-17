using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class StormSpike : SKILL
{
    public override bool Requires()
    {
        return main.a_rsc.CurrentLevels[(int)AbilityKind.beginner_spearmanship] >= 1 &&
               main.a_rsc.CurrentLevels[(int)AbilityKind.primary_wind_magic] >= 5;
    }

    // Use this for initialization
    void Awake () {
		AwakeSkill(SkillKind.storm_spike, 2.3);
        learnF.initCostList.Add(new Dealing(ResourceKind.research, Dealing.R_ParaKind.current, -20));
        useCosts.Add(new Dealing(ResourceKind.stamina, Dealing.R_ParaKind.current, -0.5));
        useCosts.Add(new Dealing(ResourceKind.wind, Dealing.R_ParaKind.current, -2));
        warriorAtks.Add(new WarriorAttack(13));
        sorcererAtks.Add(new SorcererAttack(32));

        SetSource(NeedKind.attack, NeedKind.spear, NeedKind.wind);
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

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class AirCutter : SKILL
{
    public override bool Requires()
    {
        return main.a_rsc.CurrentLevels[(int)AbilityKind.primary_wind_magic] >= 1;
    }

    // Use this for initialization
    void Awake () {
		AwakeSkill(SkillKind.air_cutter, 2.6);
        learnF.initCostList.Add(new Dealing(ResourceKind.research, Dealing.R_ParaKind.current, -5));
        useCosts.Add(new Dealing(ResourceKind.wind, Dealing.R_ParaKind.current, -1));
        sorcererAtks.Add(new SorcererAttack(20));

        SetSource(NeedKind.attack, NeedKind.wind);
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

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class AirBazooka : SKILL
{
    public override bool Requires()
    {
        return main.a_rsc.CurrentLevels[(int)AbilityKind.primary_wind_magic] >= 7;
    }

    // Use this for initialization
    void Awake () {
		AwakeSkill(SkillKind.air_bazooka, 1.7);
        learnF.initCostList.Add(new Dealing(ResourceKind.research, Dealing.R_ParaKind.current, -5));
        useCosts.Add(new Dealing(ResourceKind.mp, Dealing.R_ParaKind.current, -0.8));
        useCosts.Add(new Dealing(ResourceKind.wind, Dealing.R_ParaKind.current, -1.9));
        sorcererAtks.Add(new SorcererAttack(24));

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

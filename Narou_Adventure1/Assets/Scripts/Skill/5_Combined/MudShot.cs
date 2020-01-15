using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class MudShot : SKILL
{
    public override bool Requires()
    {
        return main.a_rsc.CurrentLevels[(int)AbilityKind.primary_water_magic] >= 2 &&
               main.a_rsc.CurrentLevels[(int)AbilityKind.primary_earth_magic] >= 2;
    }

    // Use this for initialization
    void Awake () {
		AwakeSkill(SkillKind.mud_shot, 4);
        learnF.initCostList.Add(new Dealing(ResourceKind.research, Dealing.R_ParaKind.current, -20));
        useCosts.Add(new Dealing(ResourceKind.water, Dealing.R_ParaKind.current, -1.5));
        useCosts.Add(new Dealing(ResourceKind.earth, Dealing.R_ParaKind.current, -1.5));
        sorcererAtks.Add(new SorcererAttack(30));

        SetSource(NeedKind.attack, NeedKind.water, NeedKind.earth);
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

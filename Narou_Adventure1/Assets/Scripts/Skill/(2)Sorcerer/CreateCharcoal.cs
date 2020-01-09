using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class CreateCharcoal : SKILL
{
    public override bool Requires()
    {
        return main.a_rsc.CurrentLevels[(int)AbilityKind.primary_fire_magic] >= 4;
    }

    // Use this for initialization
    void Awake () {
		AwakeSkill(SkillKind.create_charcoal, 6);
        learnF.initCostList.Add(new Dealing(ResourceKind.research, Dealing.R_ParaKind.current, -5));
        useCosts.Add(new Dealing(ResourceKind.fire, Dealing.R_ParaKind.current, -3));
        useCosts.Add(new Dealing(ResourceKind.firewood, Dealing.R_ParaKind.current, -1));
        useEffects.Add(new Dealing(ResourceKind.charcoal, Dealing.R_ParaKind.current,1));

        SetSource(NeedKind.production, NeedKind.fire);
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

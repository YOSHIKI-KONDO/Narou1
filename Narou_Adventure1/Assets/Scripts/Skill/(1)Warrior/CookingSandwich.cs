using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class CookingSandwich : SKILL
{
    public override bool Requires()
    {
        return main.a_rsc.CurrentLevels[(int)AbilityKind.beginner_swordmanship] >= 4;
    }

    // Use this for initialization
    void Awake () {
		AwakeSkill(SkillKind.cooking_sandwich, 3);
        learnF.initCostList.Add(new Dealing(ResourceKind.research, Dealing.R_ParaKind.current, -5));
        useCosts.Add(new Dealing(ResourceKind.stamina, Dealing.R_ParaKind.current, -1));
        useCosts.Add(new Dealing(ResourceKind.bread, Dealing.R_ParaKind.current, -1));
        useCosts.Add(new Dealing(ResourceKind.fish, Dealing.R_ParaKind.current, -1));
        useEffects.Add(new Dealing(ResourceKind.anchovy_sandwich, Dealing.R_ParaKind.current, 1));

        SetSource(NeedKind.production, NeedKind.sword);
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

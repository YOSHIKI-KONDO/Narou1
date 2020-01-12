using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class KneadDough : SKILL
{
    public override bool Requires()
    {
        return main.a_rsc.CurrentLevels[(int)AbilityKind.beginner_bojutsu] >= 4;
    }

    // Use this for initialization
    void Awake () {
		AwakeSkill(SkillKind.knead_dough, 4);
        learnF.initCostList.Add(new Dealing(ResourceKind.research, Dealing.R_ParaKind.current, -10));
        useCosts.Add(new Dealing(ResourceKind.stamina, Dealing.R_ParaKind.current, -1));
        useCosts.Add(new Dealing(ResourceKind.wheat, Dealing.R_ParaKind.current, -8));
        useCosts.Add(new Dealing(ResourceKind.charcoal, Dealing.R_ParaKind.current, -1));
        useEffects.Add(new Dealing(ResourceKind.bread, Dealing.R_ParaKind.current, 1));

        SetSource(NeedKind.production, NeedKind.rod);
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

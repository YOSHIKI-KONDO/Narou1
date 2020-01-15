using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class Boost : SKILL
{
    public override bool Requires()
    {
        return main.SR.level >= 13;
    }

    // Use this for initialization
    void Awake () {
		AwakeSkill(SkillKind.boost, 5);
        learnF.initCostList.Add(new Dealing(ResourceKind.research, Dealing.R_ParaKind.current, -5));
        useCosts.Add(new Dealing(ResourceKind.mp, Dealing.R_ParaKind.current, -3));
        useEffects.Add(new Temp_Regen_Deal(ResourceKind.strength, 3, 18));
        useEffects.Add(new Temp_Regen_Deal(ResourceKind.defense, 3, 18));

        SetSource(NeedKind.enhance);
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

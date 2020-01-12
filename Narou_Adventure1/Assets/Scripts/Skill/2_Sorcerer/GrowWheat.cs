using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class GrowWheat : SKILL
{
    public override bool Requires()
    {
        return main.a_rsc.CurrentLevels[(int)AbilityKind.primary_earth_magic] >= 4;
    }

    // Use this for initialization
    void Awake () {
		AwakeSkill(SkillKind.grow_wheat, 7);
        learnF.initCostList.Add(new Dealing(ResourceKind.research, Dealing.R_ParaKind.current, -10));
        useCosts.Add(new Dealing(ResourceKind.earth, Dealing.R_ParaKind.current, -2));
        useEffects.Add(new Temp_Regen_Deal(ResourceKind.wheat, 0.4, 25));

        SetSource(NeedKind.production, NeedKind.earth);
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

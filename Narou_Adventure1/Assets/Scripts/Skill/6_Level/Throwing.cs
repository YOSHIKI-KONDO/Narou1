using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class Throwing : SKILL
{
    public override bool Requires()
    {
        return main.SR.level >= 8;
    }

    // Use this for initialization
    void Awake () {
		AwakeSkill(SkillKind.throwing, 2.6);
        learnF.initCostList.Add(new Dealing(ResourceKind.research, Dealing.R_ParaKind.current, -5));
        useCosts.Add(new Dealing(ResourceKind.stone, Dealing.R_ParaKind.current, -1));
        warriorAtks.Add(new WarriorAttack(14));

        SetSource(NeedKind.attack);
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

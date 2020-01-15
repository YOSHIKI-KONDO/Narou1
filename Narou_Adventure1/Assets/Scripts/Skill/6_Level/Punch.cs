using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class Punch : SKILL
{
    public override bool Requires()
    {
        return main.SR.level >= 2;
    }

    // Use this for initialization
    void Awake () {
		AwakeSkill(SkillKind.punch, 1.5);
        learnF.initCostList.Add(new Dealing(ResourceKind.research, Dealing.R_ParaKind.current, -5));
        useCosts.Add(new Dealing(ResourceKind.action, Dealing.R_ParaKind.current, -1));
        warriorAtks.Add(new WarriorAttack(6));

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

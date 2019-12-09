using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class ThunderBolt : SKILL {

	// Use this for initialization
	void Awake () {
		AwakeSkill(SkillKind.thuderBolt, 10);
        learnF.initCostList.Add(new Dealing(ResourceKind.mp, Dealing.R_ParaKind.current, -10));
        useCosts.Add(new Dealing(ResourceKind.mp, Dealing.R_ParaKind.current, -1));
        useEffects.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, 10));
        warriorAtks.Add(new WarriorAttack(10));
        sorcererAtks.Add(new SorcererAttack(5));
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

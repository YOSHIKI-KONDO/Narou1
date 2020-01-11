using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class AnimalAttack : SKILL
{
    public override bool Requires()
    {
        return main.a_rsc.CurrentLevels[(int)AbilityKind.animal_handling] >= 1;
    }

    // Use this for initialization
    void Awake () {
		AwakeSkill(SkillKind.animal_attack, 10);
        learnF.initCostList.Add(new Dealing(ResourceKind.research, Dealing.R_ParaKind.current, -5));
        useCosts.Add(new Dealing(ResourceKind.animal, Dealing.R_ParaKind.current, -2));
        warriorAtks.Add(new WarriorAttack(12));

        SetSource(NeedKind.attack, NeedKind.animal);
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

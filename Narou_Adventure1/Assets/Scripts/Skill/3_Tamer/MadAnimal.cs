using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class MadAnimal : SKILL
{
    public override bool Requires()
    {
        return main.a_rsc.CurrentLevels[(int)AbilityKind.animal_handling] >= 7;
    }

    // Use this for initialization
    void Awake () {
		AwakeSkill(SkillKind.mad_animal, 2.2);
        learnF.initCostList.Add(new Dealing(ResourceKind.research, Dealing.R_ParaKind.current, -5));
        useCosts.Add(new Dealing(ResourceKind.mp, Dealing.R_ParaKind.current, -1));
        useCosts.Add(new Dealing(ResourceKind.animal, Dealing.R_ParaKind.current, -1));
        warriorAtks.Add(new WarriorAttack(13));
        sorcererAtks.Add(new SorcererAttack(13));

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

﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class NormalAttack : SKILL {

    public override bool Requires()
    {
        return false;
    }

    // Use this for initialization
    void Awake()
    {
        AwakeSkill(SkillKind.normalAttack, 3);
        //learnF.initCostList.Add(new Dealing(ResourceKind.mp, Dealing.R_ParaKind.current, -10));
        //useCosts.Add(new Dealing(ResourceKind.mp, Dealing.R_ParaKind.current, -1));
        //useEffects.Add(new Dealing(ResourceKind.gold, Dealing.R_ParaKind.current, 10));
        warriorAtks.Add(new WarriorAttack(1));
        //sorcererAtks.Add(new SorcererAttack(5));

        //attributes.Add(AttributeKind.fireMagic);    //add
        //attributes.Add(AttributeKind.waterSword);   //add
    }

    // Use this for initialization
    void Start()
    {
        StartSkill();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSkill();
    }

    private void FixedUpdate()
    {
        FixedUpdateSkill();
    }
}
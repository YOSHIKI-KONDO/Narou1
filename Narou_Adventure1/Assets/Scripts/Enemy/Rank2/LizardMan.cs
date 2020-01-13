﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class LizardMan : ENEMY
{
    // Use this for initialization
    void Awake()
    {
        AwakeEnemy(EnemyKind.lizard_man);
        drops.Add(new Drop(ResourceKind.stone, 1, 10));
        drops.Add(new Item_Drop(ItemKind.stone_shield, 2));
    }

    // Use this for initialization
    void Start()
    {
        StartEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateEnemy();
    }

    void FixedUpdate()
    {
        FixedUpdateEnemy();
    }
}

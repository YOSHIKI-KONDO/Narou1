﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class PoisonRat : ENEMY
{
    // Use this for initialization
    void Awake()
    {
        AwakeEnemy(EnemyKind.poison_rat, 25, 2, 12, 0, 10, 20, 1);
        drops.Add(new Drop(ResourceKind.herb, 1, 3));
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

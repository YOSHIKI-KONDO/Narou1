﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class Embla : ENEMY
{
    // Use this for initialization
    void Awake()
    {
        AwakeEnemy(EnemyKind.slime, 7, 3, 2, 0, 4, 3);
        drops.Add(new Drop(ResourceKind.anchovy_sandwich, 1, 5));
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

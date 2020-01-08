using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class Orc : ENEMY
{
    // Use this for initialization
    void Awake()
    {
        AwakeEnemy(EnemyKind.orc);
        drops.Add(new Drop(ResourceKind.stone, 1, 10));
        drops.Add(new Drop(ResourceKind.wood, 1, 5));
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

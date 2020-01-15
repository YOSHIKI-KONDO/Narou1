using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class Wolf : ENEMY
{
    // Use this for initialization
    void Awake()
    {
        AwakeEnemy(EnemyKind.wolf);
        drops.Add(new Item_Drop(ItemKind.old_robe, 3));
        drops.Add(new Drop(ResourceKind.fur, 1, 2));

        drops.Add(Drop.Attribute_AND_Drop(ResourceKind.premium_fur, 1, 100, NeedKind.dark));
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

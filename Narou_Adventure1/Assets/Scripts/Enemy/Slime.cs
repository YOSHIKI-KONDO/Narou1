using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class Slime : ENEMY
{
    // Use this for initialization
    void Awake()
    {
        AwakeEnemy(EnemyKind.slime, 10, 5, 1, 0, 1, 1);
        drops.Add(new Drop(ResourceKind.anchovy_sandwich, 1, 20));
        drops.Add(new Item_Drop(ItemKind.animalfood, 100));
        drops.Add(new Item_Drop(ItemKind.fire_orb, 10));
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

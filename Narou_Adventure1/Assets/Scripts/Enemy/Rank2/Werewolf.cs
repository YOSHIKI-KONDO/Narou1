using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class Werewolf : ENEMY
{
    // Use this for initialization
    void Awake()
    {
        AwakeEnemy(EnemyKind.werewolf);
        drops.Add(new Drop(ResourceKind.fur, 1, 5));
        drops.Add(new Item_Drop(ItemKind.animalfood, 3));

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

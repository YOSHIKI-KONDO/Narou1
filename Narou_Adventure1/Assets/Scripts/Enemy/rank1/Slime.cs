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
        AwakeEnemy(EnemyKind.slime, 4, 5, 1, 0, 1, 1);
        drops.Add(new Drop(ResourceKind.herb, 1, 5));

        //drops.Add(Drop.OneShotDrop(ResourceKind.medicine, 1, 100));
        //drops.Add(Drop.Skill_OR_Drop(ResourceKind.medicine, 1, 100, SkillKind.meteor));                //余分OK
        //drops.Add(Drop.Skill_AND_Drop(ResourceKind.medicine, 1, 100, SkillKind.meteor));               //余分ダメ
        //drops.Add(Drop.Attribute_OR_Drop(ResourceKind.medicine, 1, 100, AttributeKind.fireMagic));     //余分OK
        //drops.Add(Drop.Attribute_AND_Drop(ResourceKind.medicine, 1, 100, AttributeKind.fireMagic));    //余分ダメ

        //drops.Add(Item_Drop.OneShot_Item_Drop(ItemKind.toolbox, 100));
        //drops.Add(Item_Drop.Skill_OR_Item_Drop(ItemKind.toolbox, 100, SkillKind.meteor));                //余分OK
        //drops.Add(Item_Drop.Skill_AND_Item_Drop(ItemKind.toolbox, 100, SkillKind.meteor));               //余分ダメ
        //drops.Add(Item_Drop.Attribute_OR_Item_Drop(ItemKind.toolbox, 100, AttributeKind.fireMagic));     //余分OK
        //drops.Add(Item_Drop.Attribute_AND_Item_Drop(ItemKind.toolbox, 100, AttributeKind.fireMagic));    //余分ダメ
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

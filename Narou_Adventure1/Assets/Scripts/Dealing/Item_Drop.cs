using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class Item_Drop : Drop {
    public ItemKind itemKind;
    
    /// <param name="itemKind">リソースの種類</param>
    /// <param name="probability">入手確率 (0.0f ~ 100.0f)</param>
    public Item_Drop(ItemKind itemKind, float probability):base(ResourceKind.nothing,1,probability)
    {
        this.itemKind = itemKind;
    }


    /// <param name="itemKind">リソースの種類</param>
    /// <param name="probability">入手確率 (0.0f ~ 100.0f)</param>
    public static Item_Drop OneShot_Item_Drop(ItemKind itemKind, float probability)
    {
        var obj = new Item_Drop(itemKind, probability);
        obj.dropKind = DropKind.oneShot;
        return obj;
    }


    /// <param name="itemKind">リソースの種類</param>
    /// <param name="probability">入手確率 (0.0f ~ 100.0f)</param>
    /// <param name="skill">スキルのEnum</param>
    public static Item_Drop Skill_AND_Item_Drop(ItemKind itemKind, float probability, SkillKind skill)
    {
        var obj = new Item_Drop(itemKind, probability);
        obj.skill_AND = skill;
        obj.dropKind = DropKind.skill_and;
        return obj;
    }


    /// <param name="itemKind">リソースの種類</param>
    /// <param name="probability">入手確率 (0.0f ~ 100.0f)</param>
    /// <param name="skill">スキルのEnum</param>
    public static Item_Drop Skill_OR_Item_Drop(ItemKind itemKind, float probability, SkillKind skill)
    {
        var obj = new Item_Drop(itemKind, probability);
        obj.skill_OR = skill;
        obj.dropKind = DropKind.skill_or;
        return obj;
    }


    /// <param name="itemKind">リソースの種類</param>
    /// <param name="probability">入手確率 (0.0f ~ 100.0f)</param>
    /// <param name = "attribute" > 属性のEnumの種類 </ param >
    public static Item_Drop Attribute_AND_Item_Drop(ItemKind itemKind, float probability, AttributeKind attribute)
    {
        var obj = new Item_Drop(itemKind, probability);
        obj.attributes_AND = attribute;
        obj.dropKind = DropKind.attribute_and;
        return obj;
    }


    /// <param name="itemKind">リソースの種類</param>
    /// <param name="probability">入手確率 (0.0f ~ 100.0f)</param>
    /// <param name = "attribute" > 属性のEnumの種類 </ param >
    public static Item_Drop Attribute_OR_Item_Drop(ItemKind itemKind, float probability, AttributeKind attribute)
    {
        var obj = new Item_Drop(itemKind, probability);
        obj.attributes_OR = attribute;
        obj.dropKind = DropKind.attribute_or;
        return obj;
    }
}

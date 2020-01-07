using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class Drop {
    public enum DropKind
    {
        normal,             //
        oneShot,            //一撃で倒された場合
        skill_and,          //特定のスキルで倒された場合(AND)
        skill_or,           //特定のスキルで倒された場合(OR)
        attribute_and,      //特定の属性の組み合わせで倒された場合(AND)
        attribute_or        //特定の属性の組み合わせで倒された場合(OR) 
    }
    public DropKind dropKind;
    public ResourceKind kind;
    public double amount;
    public float probability;

    //特定のスキル
    public SkillKind skill_AND;
    public SkillKind skill_OR;

    //特定の属性の組み合わせ
    public NeedKind attributes_AND;
    public NeedKind attributes_OR;

    /// <param name="kind">リソースの種類</param>
    /// <param name="amount">リソースの量</param>
    /// <param name="probability">入手確率 (0.0f ~ 100.0f)</param>
    public Drop(ResourceKind kind, double amount, float probability)
    {
        this.kind = kind;
        this.amount = amount;
        this.probability = probability;
    }

    /// <param name="kind">リソースの種類</param>
    /// <param name="amount">リソースの量</param>
    /// <param name="probability">入手確率 (0.0f ~ 100.0f)</param>
    public static Drop OneShotDrop(ResourceKind kind, double amount, float probability)
    {
        var drop = new Drop(kind, amount, probability);
        drop.dropKind = DropKind.oneShot;
        return drop;
    }

    /// <param name="skill">スキルのEnum</param>
    /// <param name="kind">指定したスキルで倒された時に入手できるリソースの種類</param>
    /// <param name="amount">リソースの量</param>
    /// <param name="probability">入手確率 (0.0f ~ 100.0f)</param>
    public static Drop Skill_AND_Drop(ResourceKind kind, double amount, float probability, SkillKind skill)
    {
        var drop = new Drop(kind, amount, probability);
        drop.skill_AND = skill;
        drop.dropKind = DropKind.skill_and;
        return drop;
    }

    /// <param name="skill">スキルのEnum</param>
    /// <param name="kind">指定したスキルで倒された時に入手できるリソースの種類</param>
    /// <param name="amount">リソースの量</param>
    /// <param name="probability">入手確率 (0.0f ~ 100.0f)</param>
    public static Drop Skill_OR_Drop(ResourceKind kind, double amount, float probability, SkillKind skill)
    {
        var drop = new Drop(kind, amount, probability);
        drop.skill_OR = skill;
        drop.dropKind = DropKind.skill_or;
        return drop;
    }

    /// <param name="attribute">属性のEnumの種類</param>
    /// <param name="kind">指定した属性で倒された時に入手できるリソースの種類</param>
    /// <param name="amount">リソースの量</param>
    /// <param name="probability">入手確率 (0.0f ~ 100.0f)</param>
    public static Drop Attribute_AND_Drop(ResourceKind kind, double amount, float probability,　NeedKind attribute )
    {
        var drop = new Drop(kind, amount, probability);
        drop.attributes_AND　= attribute;
        drop.dropKind = DropKind.attribute_and;
        return drop;
    }

    /// <param name="attribute">属性のEnumの種類</param>
    /// <param name="kind">指定した属性で倒された時に入手できるリソースの種類</param>
    /// <param name="amount">リソースの量</param>
    /// <param name="probability">入手確率 (0.0f ~ 100.0f)</param>
    public static Drop Attribute_OR_Drop(ResourceKind kind, double amount, float probability, NeedKind attribute)
    {
        var drop = new Drop(kind, amount, probability);
        drop.attributes_OR = attribute;
        drop.dropKind = DropKind.attribute_or;
        return drop;
    }
}

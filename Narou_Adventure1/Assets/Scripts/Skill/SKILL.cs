﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class SKILL : BASE, INeed
{
    public SkillKind kind;
    public PopUp popUp;
    public ReleaseFunction release;
    public InstantFunction learnF;
    public NeedFunciton need;
    SkillComponents components;
    GameObject newObject;

    public bool isCombo;    //コンボが発生しているかどうか
    public bool PayedCost;   //コストを払ったかどうか
    public bool equipped;    //設置してあるかどうか
    public bool casted;     //使用されたかどうか
    public double currentValue;
    double init_maxValue;
    public double Duration()
    {
        return init_maxValue * comboFactor_interval();
    }
    public float sliderValue()
    {
        return (float)(currentValue / Duration());
    }
    public List<AttributeKind> attributes = new List<AttributeKind>(); //属性
    public SKILL_COMBO combo; //直前のスキルによるコンボ
    public List<Dealing> useCosts = new List<Dealing>();
    public List<Dealing> useEffects = new List<Dealing>();
    public List<WarriorAttack> warriorAtks = new List<WarriorAttack>();//剣士攻撃
    public List<SorcererAttack> sorcererAtks = new List<SorcererAttack>();//魔法攻撃

    public virtual bool Requires() { return true; }
    public virtual bool CompleteCondition() { return false; }
    public virtual bool Need()                                //表示した後で設置したりするための条件
    {
        if (need.hasNeeds) { return need.TemplateNeed(); }
        return true;
    }




    float comboFactor_effect()
    {
        if (isCombo == false) { return 1f; }
        if(combo is Effect_SC == false) { return 1f; }
        return combo.magnification;
    }

    float comboFactor_cost()
    {
        if (isCombo == false) { return 1f; }
        if (combo is Cost_SC == false) { return 1f; }
        return combo.magnification;
    }

    float comboFactor_interval()
    {
        if (isCombo == false) { return 1f; }
        if (combo is Interval_SC == false) { return 1f; }
        return combo.magnification;
    }

    void Learn()
    {
        main.SR.learnt_Skill[(int)kind] = true;
    }

    public bool CanUse()
    {
        return CanPurchase(useCosts);
    }

    public void PayCost()
    {
        Calculate(useCosts, false, comboFactor_cost());
        PayedCost = true;
    }

    public void Produce()
    {
        Calculate(useEffects, false, comboFactor_effect(), kind.ToString());//リソース系
    }

    public double WarriorDamage()
    {
        double sum = 0;
        foreach (var atk in warriorAtks)
        {
            sum += atk.damage * comboFactor_effect();
        }
        return sum;
    }

    public double SorcererDamage()
    {
        double sum = 0;
        foreach (var atk in sorcererAtks)
        {
            sum += atk.damage * comboFactor_effect();
        }
        return sum;
    }


    public void Equip()
    {
        main.battleCtrl.skillKind = kind;
    }

    string WarriorDetail()
    {
        string sum_str = "";
        foreach (var atk in warriorAtks)
        {
            sum_str += "damage(W):" + tDigit(atk.damage * comboFactor_effect()) + "\n";
        }
        return sum_str;
    }

    string SorcererDetail()
    {
        string sum_str = "";
        foreach (var atk in sorcererAtks)
        {
            sum_str += "damage(S):" + tDigit(atk.damage * comboFactor_effect()) + "\n";
        }
        return sum_str;
    }

    public string Name_str, Description_str, Need_str,LearnCost_str, UseCost_str, UseEffect_str, Interval_str;

    // Use this for initialization
    protected void AwakeSkill(SkillKind Kind, double init_maxValue)
    {
        StartBASE();
        components = GetComponent<SkillComponents>();
        components.nameText.text = Name_str;
        newObject = components.newObject;

        this.kind = Kind;
        this.init_maxValue = init_maxValue;
        main.battleCtrl.skills[(int)kind] = this;

        need = gameObject.AddComponent<NeedFunciton>();
        popUp = main.skillPopUp.StartPopUp(gameObject, main.windowShowCanvas);
        popUp.EnterAction = ApplyPopUp;
        release = gameObject.AddComponent<ReleaseFunction>();
        release.StartFunction(gameObject, x => Sync(ref main.SR.released_Skill[(int)kind], x),
            x => Sync(ref main.SR.completed_Skill[(int)kind], x),
            x => Requires(),
            x => Sync(ref main.SR.watched_Skill[(int)kind], x),
            newObject,
            main.enumCtrl.skills[(int)kind].Name() + "(Skill)");
        learnF = gameObject.AddComponent<InstantFunction>();
        learnF.StartInstant(components.LearnBtnObj, Need);
        learnF.CompleteAction = Learn;
        components.setButton.onClick.AddListener(Equip);
    }

    // Use this for initialization
    protected void StartSkill()
    {
        ApplyPopUp();
    }

    // Update is called once per frame
    protected void UpdateSkill()
    {

    }

    protected void FixedUpdateSkill()
    {
        learnF.FixedUpdateInstant();
        components.nameText.text = Name_str;
        if (popUp.gameObject.activeSelf) { ApplyPopUp(); }

        if (CompleteCondition())//条件を満たしたらもう出なくなる
        {
            release.Completed(true);
            setFalse(popUp.gameObject);
        }

        if (main.SR.learnt_Skill[(int)kind])
        {
            //learn後
            setFalse(components.LearnBtnObj);
            setActive(components.setButton.gameObject);
            components.setButton.interactable = !equipped;
        }
        else
        {
            //learn前
            setActive(components.LearnBtnObj);
            setFalse(components.setButton.gameObject);
        }
    }

    string AttributeDetail()
    {
        string sum = "";
        foreach (var attribute in attributes)
        {
            if (sum != "") { sum += ", "; }
            sum += main.enumCtrl.attributes[(int)attribute].Name();
        }
        return sum;
    }

    void ApplyPopUp()
    {
        //とりあえず今はdescriptionの部分に追加する
        Name_str = main.enumCtrl.skills[(int)kind].Name();
        Description_str = AttributeDetail();//Description_str = main.enumCtrl.skills[(int)kind].Description();
        UseCost_str = ProgressDetail(useCosts, comboFactor_cost());
        UseEffect_str = WarriorDetail();
        UseEffect_str += SorcererDetail();
        UseEffect_str += ProgressDetail(useEffects, comboFactor_effect());
        Interval_str = tDigit(Duration(),1) + "s";
        LearnCost_str = main.SR.learnt_Skill[(int)kind] ? "" : learnF.ProgressDetail(learnF.initCostList);

        //色変更
        if (combo != null)
        {
            if (isCombo)
            {
                if (combo is Effect_SC)
                {
                    popUp.texts[9].color = Color.green;
                }
                if (combo is Cost_SC)
                {
                    popUp.texts[7].color = Color.green;
                }
                if (combo is Interval_SC)
                {
                    popUp.texts[11].color = Color.green;
                }
            }
            else
            {
                popUp.texts[9].color = Color.black;
                popUp.texts[7].color = Color.black;
                popUp.texts[11].color = Color.black;
            }
        }

        //needが設定されている場合にのみ書き換える。
        //そのため、ない場合は手動でNeed_strを変えることが可能。
        if (need.hasNeeds) { Need_str = need.Detail(); }
            
        ChangeTextAdaptive(Name_str, popUp.texts[0], popUp.texts[0].gameObject);
        ChangeTextAdaptive(Description_str, popUp.texts[1], popUp.texts[1].gameObject);
        ChangeTextAdaptive(Need_str, popUp.texts[3], popUp.texts[2].gameObject, popUp.texts[3].gameObject);
        ChangeTextAdaptive(LearnCost_str, popUp.texts[5], popUp.texts[4].gameObject, popUp.texts[5].gameObject);
        ChangeTextAdaptive(UseCost_str, popUp.texts[7], popUp.texts[6].gameObject, popUp.texts[7].gameObject);
        ChangeTextAdaptive(UseEffect_str, popUp.texts[9], popUp.texts[8].gameObject, popUp.texts[9].gameObject);
        ChangeTextAdaptive(Interval_str, popUp.texts[11], popUp.texts[10].gameObject, popUp.texts[11].gameObject);
    }
}

using System.Collections;
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

    public bool PayedCost;   //コストを払ったかどうか
    public bool equipped;    //設置してあるかどうか
    public bool casted;     //使用されたかどうか
    public double currentValue;
    double init_maxValue;
    public double Duration()
    {
        return init_maxValue;
    }
    public float sliderValue()
    {
        return (float)(currentValue / Duration());
    }
    public List<AttributeKind> attributes = new List<AttributeKind>(); //属性
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
        Calculate(useCosts, false);
        PayedCost = true;
    }

    public void Produce()
    {
        Calculate(useEffects, false, kind.ToString());//リソース系
    }

    public double WarriorDamage()
    {
        double sum = 0;
        foreach (var atk in warriorAtks)
        {
            sum += atk.damage;
        }
        return sum;
    }

    public double SorcererDamage()
    {
        double sum = 0;
        foreach (var atk in sorcererAtks)
        {
            sum += atk.damage;
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
            sum_str += "damage(W):" + tDigit(atk.damage) + "\n";
        }
        return sum_str;
    }

    string SorcererDetail()
    {
        string sum_str = "";
        foreach (var atk in sorcererAtks)
        {
            sum_str += "damage(S):" + tDigit(atk.damage) + "\n";
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

        this.kind = Kind;
        this.init_maxValue = init_maxValue;
        main.battleCtrl.skills[(int)kind] = this;

        need = gameObject.AddComponent<NeedFunciton>();
        popUp = main.skillPopUp.StartPopUp(gameObject, main.windowShowCanvas);
        popUp.EnterAction = ApplyPopUp;
        release = gameObject.AddComponent<ReleaseFunction>();
        release.StartFunction(gameObject, x => Sync(ref main.SR.released_Skill[(int)kind], x), x => Sync(ref main.SR.completed_Skill[(int)kind], x), x => Requires());
        learnF = gameObject.AddComponent<InstantFunction>();
        learnF.StartInstant(components.LearnBtnObj, Need, x => Sync(ref main.SR.watched_Skill[(int)kind], x));
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
        UseCost_str = ProgressDetail(useCosts);
        UseEffect_str = WarriorDetail();
        UseEffect_str += SorcererDetail();
        UseEffect_str += ProgressDetail(useEffects);
        Interval_str = tDigit(Duration(),1) + "s";
        LearnCost_str = main.SR.learnt_Skill[(int)kind] ? "" : learnF.ProgressDetail(learnF.initCostList);

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

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
        learnF.StartInstant(components.LearnBtnObj, Need);
        learnF.CompleteAction = Learn;
        components.setButton.onClick.AddListener(Equip);
    }

    // Use this for initialization
    protected void StartSkill()
    {

    }

    // Update is called once per frame
    protected void UpdateSkill()
    {

    }

    protected void FixedUpdateSkill()
    {
        learnF.FixedUpdateInstant();
        components.nameText.text = Name_str;
        ApplyPopUp();

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

    void ApplyPopUp()
    {
        //自動でコストの文章を生成
        Name_str = main.enumCtrl.skills[(int)kind].Name();
        if (popUp.gameObject.activeSelf)
        {
            //自動でコストの文章を生成
            Description_str = main.enumCtrl.skills[(int)kind].Description();
            LearnCost_str = learnF.ProgressDetail(learnF.initCostList);
            UseCost_str = ProgressDetail(useCosts);
            UseEffect_str = WarriorDetail();
            UseEffect_str += SorcererDetail();
            UseEffect_str += ProgressDetail(useEffects);
            Interval_str = tDigit(Duration(),1) + "s";

            //needが設定されている場合にのみ書き換える。
            //そのため、ない場合は手動でNeed_strを変えることが可能。
            if (need.hasNeeds) { Need_str = need.Detail(); }

            if (Name_str == "" || Name_str == null)
            {
                setFalse(popUp.texts[0].gameObject);
            }
            else
            {
                popUp.texts[0].text = Name_str;
            }

            if (Description_str == "" || Description_str == null)
            {
                setFalse(popUp.texts[1].gameObject);
            }
            else
            {
                popUp.texts[1].text = Description_str;
            }

            if (Need_str == "" || Need_str == null)
            {
                setFalse(popUp.texts[2].gameObject);
                setFalse(popUp.texts[3].gameObject);
            }
            else
            {
                popUp.texts[3].text = Need_str;
            }

            if (LearnCost_str == "" || LearnCost_str == null)
            {
                setFalse(popUp.texts[4].gameObject);
                setFalse(popUp.texts[5].gameObject);
            }
            else
            {
                popUp.texts[5].text = LearnCost_str;
            }

            if (UseCost_str == "" || UseCost_str == null)
            {
                setFalse(popUp.texts[6].gameObject);
                setFalse(popUp.texts[7].gameObject);
            }
            else
            {
                popUp.texts[7].text = UseCost_str;
            }

            if (UseEffect_str == "" || UseEffect_str == null)
            {
                setFalse(popUp.texts[8].gameObject);
                setFalse(popUp.texts[9].gameObject);
            }
            else
            {
                popUp.texts[9].text = UseEffect_str;
            }

            if (Interval_str == "" || Interval_str == null)
            {
                setFalse(popUp.texts[10].gameObject);
                setFalse(popUp.texts[11].gameObject);
            }
            else
            {
                popUp.texts[11].text = Interval_str;
            }
        }
    }
}

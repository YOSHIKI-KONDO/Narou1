﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

/// <summary>
/// ①UnlockCost ②ProgressCost ③CompleteEffect を設定する
/// </summary>
public class ABILITY : BASE, INeed
{
    public AbilityKind kind;
    public PopUp popUp;
    public ReleaseFunction release;
    public AbilityFunction progress;
    public NeedFunciton need;
    AbilityComponents components;

    double init_exp;
    double bottom_exp;

    public int level { get => main.SR.levels_ability[(int)kind]; set => main.SR.levels_ability[(int)kind] = value; }
    public int MaxLevel { get => main.SR.maxLevels_ability[(int)kind]; set => main.SR.maxLevels_ability[(int)kind] = value; }
    public double MaxExp()
    {
        return init_exp * Math.Pow(bottom_exp, level);
    }
    public virtual double AbilityRate()//毎秒上がる量
    {
        return 1 * Mul_Rate() + Add_Rate();
    }
    public virtual double Add_Rate() { return main.rsc.Value[(int)ResourceKind.focus]; }//加算
    public virtual double Mul_Rate() { return 1; }//乗算

    public virtual bool Requires() { return true; }
    public virtual bool CompleteCondition() { return false; }
    public virtual bool Need()                                //表示した後で設置したりするための条件
    {
        if (need.hasNeeds) { return need.TemplateNeed(); }
        return true;
    }

    public string Name_str, Description_str, Need_str, Cost_str, ProgressCost_str, ProgressEffect_str, CompleteEffect_str;
    public string Unlock_str;

    void LevelUp()
    {
        level++;
        if(level >= MaxLevel)
        {
            level = MaxLevel;
            progress.CurrentValue(0);
            main.progressCtrl.Rest();
        }
    }

    // Use this for initialization
    protected void AwakeAbility(AbilityKind Kind, double initExp = 100, double bottomExp = 1.5)
    {
        StartBASE();
        components = GetComponent<AbilityComponents>();

        this.kind = Kind;
        this.init_exp = initExp;
        this.bottom_exp = bottomExp;

        popUp = main.AbilityPopUpPre.StartPopUp(gameObject, main.windowShowCanvas);
        need = gameObject.AddComponent<NeedFunciton>();
        release = gameObject.AddComponent<ReleaseFunction>();
        release.StartFunction(gameObject, x => Sync(ref main.SR.released_ability[(int)kind], x), x => Sync(ref main.SR.completed_ability[(int)kind], x), x => Requires());
        progress = gameObject.AddComponent<AbilityFunction>();
        progress.StartAbility(components.TrainBtnObj,
            components.unlockButton,
            Need,
            () => { return level >= MaxLevel; },//最大レベルに達していたらtrue
            x => Sync(ref main.SR.unlocked_ability[(int)kind], x),
            components.slider,
            x => Sync(ref main.SR.paid_ability[(int)kind], x),
            x => Sync(ref main.SR.currentValue_ability[(int)kind], x));
        progress.CompleteAction = LevelUp;//回数を増やす処理
        progress.IsMax = () => { return level >= MaxLevel; };

        if (!main.S.isContinuePlay) { MaxLevel = 5; }//最大値の初期値
    }

    // Use this for initialization
    protected void StartAbility()
    {
        progress.ApplySlider(MaxExp());
    }

    // Update is called once per frame
    protected void UpdateAbility()
    {

    }

    protected void FixedUpdateAbility()
    {
        progress.Progress(AbilityRate(), MaxExp());

        ApplyPopUp();
        ApplyComponents();

        if (CompleteCondition())//条件を満たしたらもう出なくなる
        {
            release.Completed(true);
            setFalse(popUp.gameObject);
        }
    }

    void ApplyComponents()
    {
        components.nameText.text = Name_str;
        
        if (progress.UnLocked())
        {
            setActive(components.levelText.gameObject);
            setActive(components.progressText.gameObject);
            setActive(components.slider.gameObject);

            components.levelText.text = "Lv " + level.ToString() + "/" + MaxLevel.ToString();
            components.progressText.text = "Exp " + tDigit(progress.CurrentValue()) + "/" + tDigit(MaxExp());
        }
        else
        {
            setFalse(components.levelText.gameObject);
            setFalse(components.progressText.gameObject);
            setFalse(components.slider.gameObject);
        }
    }

    void ApplyPopUp()
    {
        //自動でコストの文章を生成
        Name_str = main.enumCtrl.abilitys[(int)kind].Name();
        if (popUp.gameObject.activeSelf)
        {
            //自動でコストの文章を生成
            //Name_str = main.enumCtrl.abilitys[(int)kind].Name();
            Description_str = main.enumCtrl.abilitys[(int)kind].Description();
            Cost_str = progress.ProgressDetail(progress.initCostList);
            ProgressCost_str = progress.ProgressDetail(progress.progressCostList);
            ProgressEffect_str = progress.ProgressDetail(progress.progressEffectList);
            CompleteEffect_str = progress.ProgressDetail(progress.completeEffectList);
            Unlock_str = progress.ProgressDetail(progress.unlockCostList);

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

            if (Unlock_str == "" || Unlock_str == null || progress.UnLocked())
            {
                setFalse(popUp.texts[2].gameObject);
                setFalse(popUp.texts[3].gameObject);
            }
            else
            {
                popUp.texts[3].text = Unlock_str;
            }

            if (Need_str == "" || Need_str == null)
            {
                setFalse(popUp.texts[4].gameObject);
                setFalse(popUp.texts[5].gameObject);
            }
            else
            {
                popUp.texts[5].text = Need_str;
            }

            if (Cost_str == "" || Cost_str == null)
            {
                setFalse(popUp.texts[6].gameObject);
                setFalse(popUp.texts[7].gameObject);
            }
            else
            {
                popUp.texts[7].text = Cost_str;
            }

            if (ProgressCost_str == "" || ProgressCost_str == null)
            {
                setFalse(popUp.texts[8].gameObject);
                setFalse(popUp.texts[9].gameObject);
            }
            else
            {
                popUp.texts[9].text = ProgressCost_str;
            }

            if (ProgressEffect_str == "" || ProgressEffect_str == null)
            {
                setFalse(popUp.texts[10].gameObject);
                setFalse(popUp.texts[11].gameObject);
            }
            else
            {
                popUp.texts[11].text = ProgressEffect_str;
            }

            if (CompleteEffect_str == "" || CompleteEffect_str == null)
            {
                setFalse(popUp.texts[12].gameObject);
                setFalse(popUp.texts[13].gameObject);
            }
            else
            {
                popUp.texts[13].text = CompleteEffect_str;
            }
        }
    }
}

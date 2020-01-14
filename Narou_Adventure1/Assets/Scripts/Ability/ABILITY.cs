using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

/// <summary>
/// ①UnlockCost ②ProgressCost ③CompleteEffect を設定する
/// </summary>
public class ABILITY : BASE, INeed, ISetSource
{
    public AbilityKind kind;
    public PopUp popUp;
    public ReleaseFunction release;
    public AbilityFunction progress;
    public NeedFunciton need;
    public UnlockFunction unlockF;
    AbilityComponents components;
    GameObject newObject;
    public List<NeedKind> sources = new List<NeedKind>();
    protected List<IInternalUnlock> unlocks = new List<IInternalUnlock>();

    double init_exp;
    double bottom_exp;

    public int level { get => main.SR.levels_ability[(int)kind]; set => main.SR.levels_ability[(int)kind] = value; }
    //public int MaxLevel { get => main.SR.maxLevels_ability[(int)kind]; set => main.SR.maxLevels_ability[(int)kind] = value; }
    public int MaxLevel { get => main.a_rsc.MaxLevel((int)kind); }
    public double MaxExp()
    {
        return init_exp * Math.Pow(bottom_exp, level);
    }
    public virtual double AbilityRate() { return main.a_rsc.TrainRate((int)kind); }
    //public virtual double AbilityRate()//毎秒上がる量
    //{
    //    return 1 * Mul_Rate() + Add_Rate();
    //}
    //public virtual double Add_Rate() { return 0; /* main.rsc.Value[(int)ResourceKind.focus];*/ }//加算
    //public virtual double Mul_Rate() { return main.focus.FocusFactor(); }//乗算

    public virtual bool Requires() { return true; }
    public virtual bool CompleteCondition() { return false; }
    public virtual bool Need()                                //表示した後で設置したりするための条件
    {
        if (need.hasNeeds) { return need.TemplateNeed(); }
        return true;
    }

    public string Name_str, Description_str, Need_str, Cost_str, ProgressCost_str, ProgressEffect_str, CompleteEffect_str;
    public string Unlock_str, UnlockContent_str;

    void LevelUp()
    {
        level++;
        if(level >= MaxLevel)
        {
            level = MaxLevel;
            progress.CurrentValue(0);
            main.progressCtrl.SwitchProgress(main.progressCtrl.restFunction);
        }
    }

    // Use this for initialization
    protected void AwakeAbility(AbilityKind Kind, double initExp = 100, double bottomExp = 1.5)
    {
        StartBASE();
        components = GetComponent<AbilityComponents>();
        newObject = components.newText;

        this.kind = Kind;
        this.init_exp = initExp;
        this.bottom_exp = bottomExp;

        popUp = main.AbilityPopUpPre.StartPopUp(gameObject, main.windowShowCanvas);
        popUp.EnterAction = ApplyPopUp;
        need = gameObject.AddComponent<NeedFunciton>();
        unlockF = gameObject.AddComponent<UnlockFunction>();
        release = gameObject.AddComponent<ReleaseFunction>();
        release.StartFunction(gameObject, x => Sync(ref main.SR.released_ability[(int)kind], x),
            x => Sync(ref main.SR.completed_ability[(int)kind], x),
            x => Requires(),
            x => Sync(ref main.SR.watched_ability[(int)kind], x),
            newObject,
            main.enumCtrl.abilitys[(int)kind].Name() + "(Ability)");
        progress = gameObject.AddComponent<AbilityFunction>();
        progress.StartAbility(components.TrainBtnObj,
            components.unlockButton,
            Need,
            () => { return level >= MaxLevel; },//最大レベルに達していたらtrue
            x => Sync(ref main.SR.unlocked_ability[(int)kind], x),
            components.slider,
            x => Sync(ref main.SR.paid_ability[(int)kind], x),
            x => Sync(ref main.SR.currentValue_ability[(int)kind], x),
            main.enumCtrl.abilitys[(int)kind].Name());
        progress.CompleteAction = LevelUp;//回数を増やす処理
        progress.IsMax = () => { return level >= MaxLevel; };
    }

    // Use this for initialization
    protected void StartAbility()
    {
        progress.ApplySlider(MaxExp());
        main.iconCtrl.AddIcon(sources, components.attributesParent);
        InitializeUnlodkFunction();
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

    public void SetSource(params NeedKind[] sourceKinds)
    {
        //haveSource = true;
        foreach (var n in sourceKinds)
        {
            sources.Add(n);
        }
    }

    void InitializeUnlodkFunction()
    {
        foreach (var _unlock in unlocks)
        {
            if (_unlock is InstantUnlock)
            {
                var castedUnlock = _unlock as InstantUnlock;
                unlockF.unlocks.Add(new Unlock(castedUnlock.kind, () => { return castedUnlock.level <= level; },
                    (x) => Sync(ref main.SR.released_instant[(int)castedUnlock.kind], x)));
            }
            else if (_unlock is LoopUnlock)
            {
                var castedUnlock = _unlock as LoopUnlock;
                unlockF.unlocks.Add(new Unlock(castedUnlock.kind, () => { return castedUnlock.level <= level; },
                    (x) => Sync(ref main.SR.released_loop[(int)castedUnlock.kind], x)));
            }
            else if (_unlock is UpgradeUnlock)
            {
                var castedUnlock = _unlock as UpgradeUnlock;
                unlockF.unlocks.Add(new Unlock(castedUnlock.kind, () => { return castedUnlock.level <= level; },
                    (x) => Sync(ref main.SR.released_upgrade[(int)castedUnlock.kind], x)));
            }
            else if (_unlock is AbilityUnlock)
            {
                var castedUnlock = _unlock as AbilityUnlock;
                unlockF.unlocks.Add(new Unlock(castedUnlock.kind, () => { return castedUnlock.level <= level; },
                    (x) => Sync(ref main.SR.released_ability[(int)castedUnlock.kind], x)));
            }
            else if (_unlock is SkillUnlock)
            {
                var castedUnlock = _unlock as SkillUnlock;
                unlockF.unlocks.Add(new Unlock(castedUnlock.kind, () => { return castedUnlock.level <= level; },
                    (x) => Sync(ref main.SR.released_Skill[(int)castedUnlock.kind], x)));
            }
            else if (_unlock is ItemUnlock)
            {
                var castedUnlock = _unlock as ItemUnlock;
                unlockF.unlocks.Add(new Unlock(castedUnlock.kind, () => { return castedUnlock.level <= level; },
                    (x) => Sync(ref main.SR.released_Item[(int)castedUnlock.kind], x)));
            }
            else if (_unlock is DungeonUnlock)
            {
                var castedUnlock = _unlock as DungeonUnlock;
                unlockF.unlocks.Add(new Unlock(castedUnlock.kind, () => { return castedUnlock.level >= level; },
                    (x) => Sync(ref main.SR.released_Dungeon[(int)castedUnlock.kind], x)));
            }
            else
            {
                Debug.Log("何かがおかしいです。");
                continue;
            }
        }
    }

    string unlockDetail()
    {
        string sum = "";
        for (int i = 0; i < unlocks.Count; i++)
        {
            if (sum != "") { sum += "\n"; }
            if (unlocks[i] is InstantUnlock)
            {
                var castedUnlock = unlocks[i] as InstantUnlock;
                sum += "Lv." + castedUnlock.level.ToString() + ":";
                if (unlockF.unlocks[i].released())
                {
                    sum += main.enumCtrl.instantActions[(int)castedUnlock.kind].Name();
                }
                else
                {
                    sum += "???";
                }
            }
            else if (unlocks[i] is LoopUnlock)
            {
                var castedUnlock = unlocks[i] as LoopUnlock;
                sum += "Lv." + castedUnlock.level.ToString() + ":";
                if (unlockF.unlocks[i].released())
                {
                    sum += main.enumCtrl.loopActions[(int)castedUnlock.kind].Name();
                }
                else
                {
                    sum += "???";
                }
            }
            else if (unlocks[i] is UpgradeUnlock)
            {
                var castedUnlock = unlocks[i] as UpgradeUnlock;
                sum += "Lv." + castedUnlock.level.ToString() + ":";
                if (unlockF.unlocks[i].released())
                {
                    sum += main.enumCtrl.upgradeActions[(int)castedUnlock.kind].Name();
                }
                else
                {
                    sum += "???";
                }
            }
            else if (unlocks[i] is AbilityUnlock)
            {
                var castedUnlock = unlocks[i] as AbilityUnlock;
                sum += "Lv." + castedUnlock.level.ToString() + ":";
                if (unlockF.unlocks[i].released())
                {
                    sum += main.enumCtrl.abilitys[(int)castedUnlock.kind].Name();
                }
                else
                {
                    sum += "???";
                }
            }
            else if (unlocks[i] is SkillUnlock)
            {
                var castedUnlock = unlocks[i] as SkillUnlock;
                sum += "Lv." + castedUnlock.level.ToString() + ":";
                if (unlockF.unlocks[i].released())
                {
                    sum += main.enumCtrl.skills[(int)castedUnlock.kind].Name();
                }
                else
                {
                    sum += "???";
                }
            }
            else if (unlocks[i] is ItemUnlock)
            {
                var castedUnlock = unlocks[i] as ItemUnlock;
                sum += "Lv." + castedUnlock.level.ToString() + ":";
                if (unlockF.unlocks[i].released())
                {
                    sum += main.enumCtrl.items[(int)castedUnlock.kind].Name();
                }
                else
                {
                    sum += "???";
                }
            }
            else if (unlocks[i] is DungeonUnlock)
            {
                var castedUnlock = unlocks[i] as DungeonUnlock;
                sum += "Lv." + castedUnlock.level.ToString() + ":";
                if (unlockF.unlocks[i].released())
                {
                    sum += main.enumCtrl.dungeons[(int)castedUnlock.kind].Name();
                }
                else
                {
                    sum += "???";
                }
            }
            else
            {
                Debug.Log("何かがおかしいです。");
                continue;
            }
        }
        return sum;
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
            UnlockContent_str = unlockDetail();

            //needが設定されている場合にのみ書き換える。
            //そのため、ない場合は手動でNeed_strを変えることが可能。
            if (need.hasNeeds) { Need_str = need.Detail(); }

            ChangeTextAdaptive(Name_str, popUp.texts[0], popUp.texts[0].gameObject);
            ChangeTextAdaptive(Description_str, popUp.texts[1], popUp.texts[1].gameObject);
            ChangeTextAdaptive(Unlock_str, popUp.texts[3], popUp.texts[2].gameObject, popUp.texts[3].gameObject);
            ChangeTextAdaptive(Need_str, popUp.texts[5], popUp.texts[4].gameObject, popUp.texts[5].gameObject);
            ChangeTextAdaptive(Cost_str, popUp.texts[7], popUp.texts[6].gameObject, popUp.texts[7].gameObject);
            ChangeTextAdaptive(ProgressCost_str, popUp.texts[9], popUp.texts[8].gameObject, popUp.texts[9].gameObject);
            ChangeTextAdaptive(ProgressEffect_str, popUp.texts[11], popUp.texts[10].gameObject, popUp.texts[11].gameObject);
            ChangeTextAdaptive(CompleteEffect_str, popUp.texts[13], popUp.texts[12].gameObject, popUp.texts[13].gameObject);
            ChangeTextAdaptive(UnlockContent_str, popUp.texts[14], popUp.texts[14].gameObject);
        }
    }




    //内部クラス
    protected interface IInternalUnlock { }
    protected class InstantUnlock : IInternalUnlock
{
        public MainAction.ActionEnum.Instant kind;
        public int level;
        public InstantUnlock(int level, MainAction.ActionEnum.Instant kind)
        {
            this.kind = kind;
            this.level = level;
        }
    }
    protected class LoopUnlock : IInternalUnlock
{
        public MainAction.ActionEnum.Loop kind;
        public int level;
        public LoopUnlock(int level, MainAction.ActionEnum.Loop kind)
        {
            this.kind = kind;
            this.level = level;
        }
    }
    protected class UpgradeUnlock : IInternalUnlock
{
        public MainAction.ActionEnum.Upgrade kind;
        public int level;
        public UpgradeUnlock(int level, MainAction.ActionEnum.Upgrade kind)
        {
            this.kind = kind;
            this.level = level;
        }
    }
    protected class AbilityUnlock : IInternalUnlock
    {
        public AbilityKind kind;
        public int level;
        public AbilityUnlock(int level, AbilityKind kind)
        {
            this.kind = kind;
            this.level = level;
        }
    }
    protected class SkillUnlock : IInternalUnlock
    {
        public SkillKind kind;
        public int level;
        public  SkillUnlock(int level, SkillKind kind)
        {
            this.kind = kind;
            this.level = level;
        }
    }
    protected class ItemUnlock : IInternalUnlock
    {
        public ItemKind kind;
        public int level;
        public ItemUnlock(int level, ItemKind kind)
        {
            this.kind = kind;
            this.level = level;
        }
    }
    protected class DungeonUnlock : IInternalUnlock
    {
        public DungeonKind kind;
        public int level;
        public DungeonUnlock(int level, DungeonKind kind)
        {
            this.kind = kind;
            this.level = level;
        }
    }
}

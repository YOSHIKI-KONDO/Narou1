using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;
using MainAction;

public class UPGRADE_ACTION : ACTION, INeed
{
    public ActionEnum.Upgrade kind;
    public PopUp popUp;
    public ReleaseFunction release;
    public ProgressFunction progress;
    public NeedFunciton need;
    HighLightFunction highLightF;
    ActionComponents components;
    public Slider slider;
    Text text;
    GameObject newObject;
    GameObject actionMark;
    public GameObject highLight;

    public double MaxValue;
    public double CurrentValue;
    public double? PlusValue;
    public int maxNum;
    public int ClearNum { get => main.SR.clearNum_upgrade[(int)kind]; set => main.SR.clearNum_upgrade[(int)kind] = value; }
    protected bool LockOtherAction;//onならpopupなどが出る

    public virtual bool Requires() { return true; }
    public virtual bool CompleteCondition() { return false; }
    public virtual bool Need()                                //表示した後で設置したりするための条件
    {
        if (need.hasNeeds) { return need.TemplateNeed(); }
        return true;
    }
    public virtual void CompleteAction() { }

    public string Name_str, Description_str, Need_str, Cost_str, ProgressCost_str, ProgressEffect_str, CompleteEffect_str, MaxNum_str;

    void AddClerNum()
    {
        ClearNum++;
    }

    // Use this for initialization
    /// <summary>
    /// maxValueがnullだと、押してもループアクションだと認識されない。
    /// plusVaueがnullだと、focusの値が反映されない。
    /// </summary>
    protected void AwakeUpgradeAction(ActionEnum.Upgrade Kind,
        int maxNum = 1, double maxValue = 60, double? plusValue = 1, bool onSlider = true, bool addCtrl = true)
    {
        StartBASE();
        components = GetComponent<ActionComponents>();
        text = components.text;
        newObject = components.newObject;
        actionMark = components.actionMark;
        highLight = components.highLight;
        if (components.slider != null)
        {
            slider = components.slider;
        }
        if (onSlider == false) { setFalse(slider.gameObject); }

        this.kind = Kind;
        //main.checkActions.upgrades[(int)kind] = kind;   //hierarchyチェック
        main.actionContainer.upgrades[(int)kind] = this;
        this.maxNum = maxNum;
        PlusValue = plusValue;
        MaxValue = maxValue;


        popUp = main.ActionPopUpPre.StartPopUp(gameObject, main.windowShowCanvas);
        popUp.EnterAction = ApplyPopUp;
        need = gameObject.AddComponent<NeedFunciton>();
        release = gameObject.AddComponent<ReleaseFunction>();
        release.StartFunction(gameObject, x => Sync(ref main.SR.released_upgrade[(int)kind], x),
            x => Sync(ref main.SR.completed_upgrade[(int)kind], x),
            x => Requires(),
            x => Sync(ref main.SR.watched_upgrade[(int)kind], x),
            newObject,
            main.enumCtrl.upgradeActions[(int)kind].Name() + "(Upgrade Action)");
        progress = gameObject.AddComponent<ProgressFunction>();
        progress.StartProgress(gameObject, Need, slider,
            x => Sync(ref main.SR.paid_upgrade[(int)kind], x),
            x => Sync(ref main.SR.currentValue_upgrade[(int)kind], x),
            main.enumCtrl.upgradeActions[(int)kind].Name(),
            actionMark,
            addCtrl);
        progress.elementKind = ElementKind.main; //溜まったらStartProgressに統合する
        progress.CompleteAction = AddClerNum;//回数を増やす処理
        progress.CompleteActionForSub = CompleteAction;
    }

    // Use this for initialization
    protected void StartUpgradeAction()
    {
        progress.ApplySlider(MaxValue);
        ApplyEffectLevel();
        highLightF = gameObject.AddComponent<HighLightFunction>();
        highLightF.StartContents(highLight, progress.progressEffectList, progress.completeEffectList);
    }

    // Update is called once per frame
    protected void UpdateUpgradeAction()
    {

    }

    protected void FixedUpdateUpgradeAction()
    {
        progress.Progress(CalPlusValue(), MaxValue);
        text.text = Name_str;
        ApplyPopUp();

        if (CompleteCondition() || ClearNum >= maxNum)//条件を満たしたらもう出なくなる
        {
            release.Completed(true);
            setFalse(popUp.gameObject);
        }
    }

    void ApplyEffectLevel()
    {
        foreach (var dealing in progress.progressEffectList)
        {
            dealing.Level = (x) => Sync(ref main.SR.level_upgrade[(int)kind], x);
        }
        foreach (var dealing in progress.completeEffectList)
        {
            dealing.Level = (x) => Sync(ref main.SR.level_upgrade[(int)kind], x);
        }
    }

    double CalPlusValue()
    {
        if (PlusValue == null)
        {
            return 0;
        }
        else
        {
            return (double)PlusValue * main.focus.FocusFactor();
        }
    }

    void ApplyPopUp()
    {
        //自動でコストの文章を生成
        Name_str = main.enumCtrl.upgradeActions[(int)kind].Name();
        if (LockOtherAction) { Name_str += "*"; }
        if (popUp.gameObject.activeSelf)
        {
            //自動でコストの文章を生成
            //Name_str = main.enumCtrl.upgradeActions[(int)kind].Name();
            Description_str = main.enumCtrl.upgradeActions[(int)kind].Description();
            Cost_str = progress.ProgressDetail(progress.initCostList);
            ProgressCost_str = progress.ProgressDetail(progress.progressCostList);
            ProgressEffect_str = progress.ProgressDetail(progress.progressEffectList);
            CompleteEffect_str = progress.ProgressDetail(progress.completeEffectList);
            MaxNum_str = maxNum == 1 ? "" : "Max:" + ClearNum + "/" + maxNum;

            //needが設定されている場合にのみ書き換える。
            //そのため、ない場合は手動でNeed_strを変えることが可能。
            if (need.hasNeeds) { Need_str = need.Detail(); }


            ChangeTextAdaptive(Name_str, popUp.textPros[0], popUp.textPros[0].gameObject);
            ChangeTextAdaptive(Description_str, popUp.textPros[1], popUp.textPros[1].gameObject);
            ChangeTextAdaptive(Need_str, popUp.textPros[3], popUp.textPros[2].gameObject, popUp.textPros[3].gameObject);
            ChangeTextAdaptive(Cost_str, popUp.textPros[5], popUp.textPros[4].gameObject, popUp.textPros[5].gameObject);
            ChangeTextAdaptive(ProgressCost_str, popUp.textPros[7], popUp.textPros[6].gameObject, popUp.textPros[7].gameObject);
            ChangeTextAdaptive(ProgressEffect_str, popUp.textPros[9], popUp.textPros[8].gameObject, popUp.textPros[9].gameObject);
            ChangeTextAdaptive(CompleteEffect_str, popUp.textPros[11], popUp.textPros[10].gameObject, popUp.textPros[11].gameObject);
            ChangeTextAdaptive(MaxNum_str, popUp.textPros[12], popUp.textPros[12].gameObject);
        }
    }
}

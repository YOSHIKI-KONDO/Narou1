using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;
using MainAction;

public class LOOP_ACTION : ACTION, INeed {
    public ActionEnum.Loop kind;
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

    public virtual bool Requires() { return true; }
    public virtual bool CompleteCondition() { return false; }
    public virtual bool Need()                                //表示した後で設置したりするための条件
    {
        if (need.hasNeeds) { return need.TemplateNeed(); }
        return true;
    }

    public string Name_str, Description_str, Need_str, Cost_str, ProgressCost_str, ProgressEffect_str, CompleteEffect_str;

    // Use this for initialization
    /// <summary>
    /// maxValueがnullだと、押してもループアクションだと認識されない。
    /// plusVaueがnullだと、focusの値が反映されない。
    /// </summary>
    protected void AwakeLoopAction(ActionEnum.Loop Kind,
        double maxValue = 60, double? plusValue = 1, bool onSlider = true, bool addCtrl = true) {
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
        //main.checkActions.loops[(int)kind] = kind;   //hierarchyチェック
        main.actionContainer.loops[(int)kind] = this;
        PlusValue = plusValue;
        MaxValue = maxValue;

        popUp = main.ActionPopUpPre.StartPopUp(gameObject, main.windowShowCanvas);
        popUp.EnterAction = ApplyPopUp;
        need = gameObject.AddComponent<NeedFunciton>();
        release = gameObject.AddComponent<ReleaseFunction>();
        release.StartFunction(gameObject, x => Sync(ref main.SR.released_loop[(int)kind], x),
        x => Sync(ref main.SR.completed_loop[(int)kind], x),
        x => Requires(),
        x => Sync(ref main.SR.watched_loop[(int)kind], x),
        newObject,
        main.enumCtrl.loopActions[(int)kind].Name() + "(Loop Action)");
        progress = gameObject.AddComponent<ProgressFunction>();
        progress.StartProgress(gameObject, Need, slider,
            x => Sync(ref main.SR.paid_loop[(int)kind], x),
            x => Sync(ref main.SR.currentValue_loop[(int)kind], x),
            main.enumCtrl.loopActions[(int)kind].Name(),
            actionMark,
            addCtrl);
        progress.elementKind = ElementKind.main; //溜まったらStartProgressに統合する
    }

    // Use this for initialization
    protected void StartLoopAction() {
        progress.ApplySlider(MaxValue);
        ApplyEffectLevel();
        highLightF = gameObject.AddComponent<HighLightFunction>();
        highLightF.StartContents(highLight, progress.progressEffectList, progress.completeEffectList);
    }

    // Update is called once per frame
    protected void UpdateLoopAction() {
        
    }

    protected void FixedUpdateLoopAction()
    {
        progress.Progress(CalPlusValue(), MaxValue);
        ApplyPopUp();
        text.text = Name_str;

        if (CompleteCondition())//条件を満たしたらもう出なくなる
        {
            release.Completed(true);
            setFalse(popUp.gameObject);
        }
    }

    void ApplyEffectLevel()
    {
        foreach (var dealing in progress.progressEffectList)
        {
            dealing.Level = (x) => Sync(ref main.SR.level_loop[(int)kind], x);
        }
        foreach (var dealing in progress.completeEffectList)
        {
            dealing.Level = (x) => Sync(ref main.SR.level_loop[(int)kind], x);
        }
    }

    double CalPlusValue()
    {
        if(PlusValue == null)
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
        Name_str = main.enumCtrl.loopActions[(int)kind].Name();
        if (popUp.gameObject.activeSelf)
        {
            //自動でコストの文章を生成
            //Name_str = main.enumCtrl.loopActions[(int)kind].Name();
            Description_str = main.enumCtrl.loopActions[(int)kind].Description();
            Cost_str = progress.ProgressDetail(progress.initCostList);
            ProgressCost_str = progress.ProgressDetail(progress.progressCostList);
            ProgressEffect_str = progress.ProgressDetail(progress.progressEffectList);
            CompleteEffect_str = progress.ProgressDetail(progress.completeEffectList);

            //needが設定されている場合にのみ書き換える。
            //そのため、ない場合は手動でNeed_strを変えることが可能。
            if (need.hasNeeds) { Need_str = need.Detail(); }


            ChangeTextAdaptive(Name_str, popUp.texts[0], popUp.texts[0].gameObject);
            ChangeTextAdaptive(Description_str, popUp.texts[1], popUp.texts[1].gameObject);
            ChangeTextAdaptive(Need_str, popUp.texts[3], popUp.texts[2].gameObject, popUp.texts[3].gameObject);
            ChangeTextAdaptive(Cost_str, popUp.texts[5], popUp.texts[4].gameObject, popUp.texts[5].gameObject);
            ChangeTextAdaptive(ProgressCost_str, popUp.texts[7], popUp.texts[6].gameObject, popUp.texts[7].gameObject);
            ChangeTextAdaptive(ProgressEffect_str, popUp.texts[9], popUp.texts[8].gameObject, popUp.texts[9].gameObject);
            ChangeTextAdaptive(CompleteEffect_str, popUp.texts[11], popUp.texts[10].gameObject, popUp.texts[11].gameObject);
            setFalse(popUp.texts[12].gameObject);
        }
    }
}

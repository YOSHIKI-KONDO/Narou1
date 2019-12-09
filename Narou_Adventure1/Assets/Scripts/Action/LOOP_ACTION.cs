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
    public Slider slider;
    Text text;

    public double MaxValue;
    public double CurrentValue;
    public double PlusValue;

    public virtual bool Requires() { return true; }
    public virtual bool CompleteCondition() { return false; }
    public virtual bool Need()                                //表示した後で設置したりするための条件
    {
        if (need.hasNeeds) { return need.TemplateNeed(); }
        return true;
    }

    public string Name_str, Description_str, Need_str, Cost_str, ProgressCost_str, ProgressEffect_str, CompleteEffect_str;

    // Use this for initialization
    protected void AwakeLoopAction(ActionEnum.Loop Kind,
        double maxValue = 60, double plusValue = 1) {
        StartBASE();
        text = GetComponentInChildren<Text>();
        if(GetComponentInChildren<Slider>() != null)
        {
            slider = GetComponentInChildren<Slider>();
        }

        this.kind = Kind;
        MaxValue = maxValue;
        PlusValue = plusValue;

        popUp = main.ActionPopUpPre.StartPopUp(gameObject, main.windowShowCanvas);
        need = gameObject.AddComponent<NeedFunciton>();
        release = gameObject.AddComponent<ReleaseFunction>();
        release.StartFunction(gameObject, x => Sync(ref main.SR.released_loop[(int)kind], x), x => Sync(ref main.SR.completed_loop[(int)kind], x), x => Requires());
        progress = gameObject.AddComponent<ProgressFunction>();
        progress.StartProgress(gameObject, Need, slider, x => Sync(ref main.SR.paid_loop[(int)kind], x), x => Sync(ref main.SR.currentValue_loop[(int)kind], x));
    }

    // Use this for initialization
    protected void StartLoopAction() {
        progress.ApplySlider(MaxValue);
    }

    // Update is called once per frame
    protected void UpdateLoopAction() {
        
    }

    protected void FixedUpdateLoopAction()
    {
        progress.Progress(PlusValue, MaxValue);
        ApplyPopUp();
        text.text = Name_str;

        if (CompleteCondition())//条件を満たしたらもう出なくなる
        {
            release.Completed(true);
            setFalse(popUp.gameObject);
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

            if (Cost_str == "" || Cost_str == null)
            {
                setFalse(popUp.texts[4].gameObject);
                setFalse(popUp.texts[5].gameObject);
            }
            else
            {
                popUp.texts[5].text = Cost_str;
            }

            if (ProgressCost_str == "" || ProgressCost_str == null)
            {
                setFalse(popUp.texts[6].gameObject);
                setFalse(popUp.texts[7].gameObject);
            }
            else
            {
                popUp.texts[7].text = ProgressCost_str;
            }

            if (ProgressEffect_str == "" || ProgressEffect_str == null)
            {
                setFalse(popUp.texts[8].gameObject);
                setFalse(popUp.texts[9].gameObject);
            }
            else
            {
                popUp.texts[9].text = ProgressEffect_str;
            }

            if (CompleteEffect_str == "" || CompleteEffect_str == null)
            {
                setFalse(popUp.texts[10].gameObject);
                setFalse(popUp.texts[11].gameObject);
            }
            else
            {
                popUp.texts[11].text = CompleteEffect_str;
            }
        }
    }
}

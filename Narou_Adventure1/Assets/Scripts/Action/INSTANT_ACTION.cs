using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;
using MainAction;

public class INSTANT_ACTION : ACTION
{
    public ActionEnum.Instant kind;
    public PopUp popUp;
    public ReleaseFunction release;
    public InstantFunction instant;
    Text text;

    public virtual bool Requires() { return true; }
    public virtual bool Need() { return true; }
    public virtual bool CompleteCondition() { return false; }

    public string Name_str, Description_str, Need_str, Cost_str, CompleteEffect_str;

    // Use this for initialization
    protected void AwakeInstantAction(ActionEnum.Instant Kind)
    {
        StartBASE();
        text = GetComponentInChildren<Text>();

        this.kind = Kind;


        popUp = main.ActionPopUpPre.StartPopUp(gameObject, main.windowShowCanvas);
        release = gameObject.AddComponent<ReleaseFunction>();
        release.StartFunction(gameObject, x => Sync(ref main.SR.released_instant[(int)kind], x), x => Sync(ref main.SR.completed_instant[(int)kind], x), x => Requires());
        instant = gameObject.AddComponent<InstantFunction>();
        instant.StartInstant(gameObject, Need);
    }

    // Use this for initialization
    protected void StartInstantAction()
    {

    }

    // Update is called once per frame
    protected void UpdateInstantAction()
    {

    }

    protected void FixedUpdateInstantAction()
    {
        instant.FixedUpdateInstant();

        ApplyPopUp();

        if (CompleteCondition())//条件を満たしたらもう出なくなる
        {
            release.Completed(true);
        }
    }

    void ApplyPopUp()
    {
        //自動でコストの文章を生成
        Name_str = main.enumCtrl.instantActions[(int)kind].Name();
        Description_str = main.enumCtrl.instantActions[(int)kind].Description();
        Cost_str = instant.ProgressDetail(instant.initCostList);
        CompleteEffect_str = instant.ProgressDetail(instant.completeEffectList);

        setFalse(popUp.texts[6].gameObject);
        setFalse(popUp.texts[7].gameObject);
        setFalse(popUp.texts[8].gameObject);
        setFalse(popUp.texts[9].gameObject);


        text.text = Name_str;
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

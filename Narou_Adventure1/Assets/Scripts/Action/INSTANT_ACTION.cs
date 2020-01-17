using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;
using MainAction;

public class INSTANT_ACTION : ACTION, INeed
{
    public ActionEnum.Instant kind;
    public PopUp popUp;
    public ReleaseFunction release;
    public InstantFunction instant;
    public NeedFunciton need;
    HighLightFunction highLightF;
    ActionComponents components;
    Text text;
    GameObject newObject;
    public GameObject highLight;

    public virtual bool Requires() { return true; }
    public virtual bool CompleteCondition() { return false; }
    public virtual bool Need()                                //表示した後で設置したりするための条件
    {
        if (need.hasNeeds) { return need.TemplateNeed(); }
        return true;
    }

    public string Name_str, Description_str, Need_str, Cost_str, CompleteEffect_str;

    // Use this for initialization
    protected void AwakeInstantAction(ActionEnum.Instant Kind)
    {
        StartBASE();
        components = GetComponent<ActionComponents>();
        text = components.text;
        newObject = components.newObject;
        highLight = components.highLight;
        setFalse(components.slider.gameObject);

        this.kind = Kind;
        //main.checkActions.instants[(int)kind] = kind;   //hierarchyチェック
        main.actionContainer.instants[(int)kind] = this;

        need = gameObject.AddComponent<NeedFunciton>();
        popUp = main.ActionPopUpPre.StartPopUp(gameObject, main.windowShowCanvas);
        popUp.EnterAction = ApplyPopUp;
        release = gameObject.AddComponent<ReleaseFunction>();
        release.StartFunction(gameObject, x => Sync(ref main.SR.released_instant[(int)kind], x),
            x => Sync(ref main.SR.completed_instant[(int)kind], x),
            x => Requires(),
            x => Sync(ref main.SR.watched_instant[(int)kind], x),
            newObject,
            main.enumCtrl.instantActions[(int)kind].Name() + "(Action)");
        instant = gameObject.AddComponent<InstantFunction>();
        instant.StartInstant(gameObject, Need);
    }

    // Use this for initialization
    protected void StartInstantAction()
    {
        ApplyEffectLevel();
        highLightF = gameObject.AddComponent<HighLightFunction>();
        highLightF.StartContents(highLight, instant.completeEffectList);
    }

    // Update is called once per frame
    protected void UpdateInstantAction()
    {

    }

    protected void FixedUpdateInstantAction()
    {
        instant.FixedUpdateInstant();
        text.text = Name_str;
        ApplyPopUp();

        if (CompleteCondition())//条件を満たしたらもう出なくなる
        {
            release.Completed(true);
            setFalse(popUp.gameObject);
        }
    }

    void ApplyEffectLevel()
    {
        foreach (var dealing in instant.completeEffectList)
        {
            dealing.Level = (x) => Sync(ref main.SR.level_instant[(int)kind], x);
        }
    }

    void ApplyPopUp()
    {
        //自動でコストの文章を生成
        Name_str = main.enumCtrl.instantActions[(int)kind].Name();
        if (popUp.gameObject.activeSelf)
        {
            //自動でコストの文章を生成
            //Name_str = main.enumCtrl.instantActions[(int)kind].Name();
            Description_str = main.enumCtrl.instantActions[(int)kind].Description();
            Cost_str = instant.ProgressDetail(instant.initCostList);
            CompleteEffect_str = instant.ProgressDetail(instant.completeEffectList);

            //needが設定されている場合にのみ書き換える。
            //そのため、ない場合は手動でNeed_strを変えることが可能。
            if (need.hasNeeds) { Need_str = need.Detail(); }

            setFalse(popUp.texts[6].gameObject);
            setFalse(popUp.texts[7].gameObject);
            setFalse(popUp.texts[8].gameObject);
            setFalse(popUp.texts[9].gameObject);

            ChangeTextAdaptive(Name_str, popUp.texts[0], popUp.texts[0].gameObject);
            ChangeTextAdaptive(Description_str, popUp.texts[1], popUp.texts[1].gameObject);
            ChangeTextAdaptive(Need_str, popUp.texts[3], popUp.texts[2].gameObject, popUp.texts[3].gameObject);
            ChangeTextAdaptive(Cost_str, popUp.texts[5], popUp.texts[4].gameObject, popUp.texts[5].gameObject);
            ChangeTextAdaptive(CompleteEffect_str, popUp.texts[11], popUp.texts[10].gameObject, popUp.texts[11].gameObject);
        }
    }
}

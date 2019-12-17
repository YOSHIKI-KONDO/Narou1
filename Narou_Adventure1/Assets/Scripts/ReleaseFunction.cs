using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

/// <summary>
/// 全てこのクラスのインスタンスを通じてActive(true/false)を変更する
/// StartFunctionでreleasedとcompletedとreleaseConditionを設定したのちは、
/// conditionを満たすと自動でreleasedがtrueとなる。
/// また、外からcompletedに代入した変数を変更すると自動でcompletedも変更される。
/// </summary>
public class ReleaseFunction : BASE {
    public delegate bool FlagDelegate(bool? x = null);
    public FlagDelegate Released;
    public FlagDelegate Completed;
    public FlagDelegate ReleaseCondition;
    GameObject Obj;

    private void Awake()
    {
        StartBASE();
    }

    public void StartFunction(GameObject obj, FlagDelegate released, FlagDelegate completed, FlagDelegate releaseCondition)
    {
        Obj = obj;
        Released = released;
        Completed = completed;
        ReleaseCondition = releaseCondition;
        main.releaseCtrl.list.Add(this);//ReleaseCtrlから一括でUpdateFunctionを回す。

        if (Completed()) { Deactivate(); return; }
        if (Released()) { Activate(); }
    }

    public void RemoveRelease()
    {
        if(main.releaseCtrl.list.IndexOf(this) >= 0)
        {
            main.releaseCtrl.list.Remove(this);
        }
    }

    public void UpdateFunction()
    {
        if (Completed()) { Deactivate(); return; }
        if (Released()) { Activate(); return; }

        if (ReleaseCondition())
        {
            Activate();
            main.announce.Add(Obj.name + " has been released.");
            Released(true);
        }
        else
        {
            Deactivate();
        }
    }

    public void Activate()
    {
        setActive(Obj);
    }

    public void Deactivate()
    {
        setFalse(Obj);
    }
}

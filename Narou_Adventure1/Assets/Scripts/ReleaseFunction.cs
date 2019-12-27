using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static UsefulMethod;

/// <summary>
/// 全てこのクラスのインスタンスを通じてActive(true/false)を変更する
/// StartFunctionでreleasedとcompletedとreleaseConditionを設定したのちは、
/// conditionを満たすと自動でreleasedがtrueとなる。
/// また、外からcompletedに代入した変数を変更すると自動でcompletedも変更される。
/// </summary>
public class ReleaseFunction : BASE, IPointerDownHandler{
    public delegate bool FlagDelegate(bool? x = null);
    public FlagDelegate Released;
    public FlagDelegate Completed;
    public FlagDelegate ReleaseCondition;
    public FlagDelegate Watched;
    public GameObject newObject;
    GameObject Obj;

    private void Awake()
    {
        StartBASE();
    }

    public void StartFunction(GameObject obj, FlagDelegate released, FlagDelegate completed, FlagDelegate releaseCondition, FlagDelegate watched, GameObject newObject)
    {
        Obj = obj;
        Released = released;
        Completed = completed;
        ReleaseCondition = releaseCondition;
        Watched = watched;
        this.newObject = newObject;
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
        //アクティブにしたりする処理
        if (Completed()) { Deactivate(); return; }
        if (Released())
        {
            Activate();
            //毎フレーム実行する処理をここに書く
            ApplyNewObj();



            //毎フレーム実行する処理ここまで
            return;
        }

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

    //見られていたらnewをfalseにする。
    void ApplyNewObj()
    {
        if (Watched != null)
        {
            if (Watched())
            {
                setFalse(newObject);
            }
            else
            {
                setActive(newObject);
            }
        }
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        Watched?.Invoke(true);  //もしもwatchedがあればtrueにする。
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

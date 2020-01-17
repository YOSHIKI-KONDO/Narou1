using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class HighLightFunction : BASE {
	EnterExitEvent eeevent;
    public GameObject highLightObj;

    private void OnDisable()
    {
        eeevent?.ExitEvent(); //falseになったとき、マウスホバーアウトの挙動をする。
    }

    /// <summary>
    /// コンテンツからAddした後に呼ぶ
    /// </summary>
    public void StartContents(GameObject highLightObj, params List<Dealing>[] Dealings)
    {
        StartBASE();
        this.highLightObj = highLightObj;
        //関数を設定
        if (eeevent == null)
        {
            eeevent = gameObject.AddComponent<EnterExitEvent>();
        }

        eeevent.EnterEvent = () => ApplyResource(true, Dealings);
        eeevent.ExitEvent = () => ApplyResource(false, Dealings);
        foreach (var dealings in Dealings)
        {
            //highlightctrlに反映
            foreach (var deal in dealings)
            {
                if (deal.rscKind is ResourceKind == false) { continue; }
                main.highLightCtrl.resourceHighLights[(int)(ResourceKind)deal.rscKind].functions.Add(this);
            }
        }
    }

    void ApplyResource(bool onHighLight, params List<Dealing>[] Dealings)
    {
        foreach (var dealings in Dealings)
        {
            for (int i = 0; i < dealings.Count; i++)
            {
                if (dealings[i].rscKind is ResourceKind == false) { continue; }
                //dealingのrscKindがリソース
                main.highLightCtrl.ApplyResource((ResourceKind)dealings[i].rscKind, onHighLight);
            }
        }
    }
}

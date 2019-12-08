using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;
using MainAction;

public class InstantFunction : BASE
{
    public Action CompleteAction;//リソースとは別な処理
    public delegate bool Condition();
    public Condition Need;
    Button button;

    public List<Dealing> initCostList = new List<Dealing>();
    public List<Dealing> completeEffectList = new List<Dealing>();


    private void Awake()
    {
        StartBASE();
    }

    /// <summary>
    /// 初期設定。Obj以外は、無ければnullでいい。
    /// ObjにはgameObjectを入れる。
    /// </summary>
    public void StartInstant(GameObject Obj, Condition Need)
    {
        if (Obj != null)
        {
            button = Obj.GetComponent<Button>();
            button.onClick.AddListener(InstantBuy);
        }
        this.Need = Need;
    }

    public void FixedUpdateInstant()
    {
        CheckButton();
    }

    public void InstantBuy()
    {
        if (CanPurchase(initCostList))
        {
            Calculate(initCostList, false);
        }
        else
        {
            return;
        }

        Calculate(completeEffectList, false);
        CompleteAction?.Invoke();//upgradeの回数などを増やす
    }


    void CheckButton()
    {
        bool condition = Need == null || Need();
        if (condition && (CanPurchase(initCostList)))
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }
    }
}
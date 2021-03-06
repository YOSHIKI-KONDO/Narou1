﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

/// <summary>
/// virtual...
/// StartProgress
/// Progress
/// CheckButton
/// </summary>
public class ProgressFunction : BASE
{
    public bool isOn;
    //public enum ParaKind
    //{
    //    current,
    //    max,
    //    regen,
    //}
    public delegate bool Condition();
    public Condition Need;
    public delegate bool BoolSync(bool? x = null);
    public BoolSync HasPaid;
    public delegate double DoubleSync(double? x = null);
    public DoubleSync CurrentValue;
    public Action CompleteAction;
    protected Button button;
    Slider slider;

    public List<Dealing> initCostList = new List<Dealing>();
    public List<Dealing> progressCostList = new List<Dealing>();
    public List<Dealing> progressEffectList = new List<Dealing>();
    public List<Dealing> completeEffectList = new List<Dealing>();


    private void Awake()
    {
        StartBASE();
        main.progressCtrl.list.Add(this);
    }

    /// <summary>
    /// 初期設定。Obj以外は、無ければnullでいい。
    /// ObjにはgameObjectを入れる。
    /// </summary>
    public virtual void StartProgress(GameObject Obj, Condition Need, Slider slider, BoolSync hasPaid, DoubleSync currentValue)
    {
        if(Obj != null)
        {
            button = Obj.GetComponent<Button>();
            button.onClick.AddListener(SwitchProgress);
        }
        this.Need = Need;
        this.slider = slider;
        HasPaid = hasPaid;
        CurrentValue = currentValue;
    }

    /// <summary>
    /// Progressを呼ぶ前に必ずInitListとProgressListの設定をする。
    /// Needはsourceなど、必要な条件を書く。ないならnullでいい。
    /// </summary>
    public virtual void Progress(double plusValue, double Max)
    {
        CheckButton();
        if(isOn == false) { return; }//falseだったら抜ける
        if (HasPaid() == false)
        {
            //コストを支払う。無ければ抜ける。
            if (CanPurchase(initCostList))
            {
                Calculate(initCostList, false);
                HasPaid(true);
            }
            else
            {
                return;
            }
        }

        //もしも現在のリソースの値がプログレスコストよりも少なければ、Rest()
        if (CanPurchase(progressCostList))
        {
            Calculate(progressCostList, true);
            Calculate(progressEffectList, true);
        }
        else
        {
            main.progressCtrl.Rest();
            return;
        }

        CurrentValue(CurrentValue() + plusValue * Time.fixedDeltaTime / main.tick);//main.tickで調整している？
        if (CurrentValue() >= Max)
        {
            //完了した時の処理
            Calculate(completeEffectList, false);
            CompleteAction?.Invoke();//upgradeの回数などを増やす
            CurrentValue(0);
            HasPaid(false);
        }

        ApplySlider(Max);
    }
    //インスタンス先のclassのスタートなどで呼ぶ
    public void ApplySlider(double Max)
    {
        if (slider == null) { return; }
        slider.value = (float)(CurrentValue() / Max);
    }

    public void SwitchProgress()
    {
        main.progressCtrl.SwitchProgress(this);
    }

    protected virtual void CheckButton()
    {
        bool condition = Need == null || Need();
        if(condition && ((CanPurchase(initCostList) && CanPurchase(progressCostList)) || HasPaid()))
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }
    }
}

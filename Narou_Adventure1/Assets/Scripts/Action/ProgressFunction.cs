using System.Collections;
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

    //public class Dealing
    //{
    //    public ResourceKind rscKind;
    //    public ParaKind paraKind;
    //    public double Value;

    //    //リソース系のコンストラクタ。
    //    public Dealing(ResourceKind rscKind, ParaKind paraKind, double Value)
    //    {
    //        this.rscKind = rscKind;
    //        this.paraKind = paraKind;
    //        this.Value = Value;
    //    }

    //    public void Update(double newValue)
    //    {
    //        Value = newValue;
    //    }
    //}
    public List<Dealing> initCostList = new List<Dealing>();
    public List<Dealing> progressCostList = new List<Dealing>();
    public List<Dealing> progressEffectList = new List<Dealing>();
    public List<Dealing> completeEffectList = new List<Dealing>();

    /// <summary>
    /// listの全てのコストが足りているかどうかを返す
    /// </summary>
    protected bool CanPurchase(List<Dealing> dealings)
    {
        foreach(var deal in dealings)
        {
            if(deal.rscKind is ResourceKind)
            {
                /* リソース */
                if((deal.paraKind is Dealing.R_ParaKind) == false)
                {
                    throw new Exception("増減させる項目の種類と内容が違います。");
                }
                switch ((Dealing.R_ParaKind)deal.paraKind)
                {
                    case Dealing.R_ParaKind.current:
                        if (main.rsc.Value[(int)(ResourceKind)deal.rscKind] + deal.Value < 0) { return false; }
                        break;
                    case Dealing.R_ParaKind.max:
                        if (main.rsc.Max_Base[(int)(ResourceKind)deal.rscKind] + deal.Value < 0) { return false; }
                        break;
                    case Dealing.R_ParaKind.regen:
                        //判定
                        break;
                    case Dealing.R_ParaKind.effect:
                        //判定
                        break;
                    default:
                        throw new Exception("対応していない項目です。");
                }
            }
            else if(deal.rscKind is AbilityKind)
            {
                /* アビリティ */
                if((deal.paraKind is Dealing.A_ParaKind) == false)
                {
                    throw new Exception("増減させる項目の種類と内容が違います。");
                }
                switch ((Dealing.A_ParaKind)deal.paraKind)
                {
                    case Dealing.A_ParaKind.maxLevel:
                        if(main.a_rsc.MaxLevels[(int)(AbilityKind)deal.rscKind] + deal.Value < 0) { return false; }
                        break;
                    case Dealing.A_ParaKind.trainRate:
                        //判定
                        break;
                    case Dealing.A_ParaKind.currentExp:
                        if (main.a_rsc.CurrentExp[(int)(AbilityKind)deal.rscKind] + deal.Value < 0) { return false; }
                        break;
                    default:
                        throw new Exception("対応していない項目です。");
                }
            }
            else
            {
                throw new Exception("対応していない項目です。");
            }
        }
        return true;
    }

    /// <summary>
    /// 実際にコストの計算をする
    /// </summary>
    protected void Calculate(List<Dealing> dealings, bool isProgress)
    {
        float mag = isProgress ? Time.fixedDeltaTime : 1f;
        foreach (var deal in dealings)
        {
            if (deal.rscKind is ResourceKind)
            {
                /* リソース */
                if ((deal.paraKind is Dealing.R_ParaKind) == false)
                {
                    throw new Exception("増減させる項目の種類と内容が違います。");
                }
                switch ((Dealing.R_ParaKind)deal.paraKind)
                {
                    case Dealing.R_ParaKind.current:
                        main.rsc.Value[(int)(ResourceKind)deal.rscKind] += deal.Value * mag;//計算
                        break;
                    case Dealing.R_ParaKind.max:
                        main.rsc.Max_Base[(int)(ResourceKind)deal.rscKind] += deal.Value * mag;//計算
                        break;
                    case Dealing.R_ParaKind.regen:
                        main.rsc.Regen_Base[(int)(ResourceKind)deal.rscKind] += deal.Value * mag;//計算
                        break;
                    case Dealing.R_ParaKind.effect:
                        throw new Exception("まだ対応してないお♡");
                    default:
                        throw new Exception("対応していない項目です。");
                }
            }
            else if (deal.rscKind is AbilityKind)
            {
                /* アビリティ */
                if ((deal.paraKind is Dealing.A_ParaKind) == false)
                {
                    throw new Exception("増減させる項目の種類と内容が違います。");
                }
                switch ((Dealing.A_ParaKind)deal.paraKind)
                {
                    case Dealing.A_ParaKind.maxLevel:
                        if (isProgress) { throw new Exception("この項目をProgressに指定できません。"); }
                        main.a_rsc.MaxLevels[(int)(AbilityKind)deal.rscKind] += (int)deal.Value;
                        break;
                    case Dealing.A_ParaKind.trainRate:
                        main.a_rsc.TrainRate[(int)(AbilityKind)deal.rscKind] += deal.Value * mag;
                        break;
                    case Dealing.A_ParaKind.currentExp:
                        main.a_rsc.CurrentExp[(int)(AbilityKind)deal.rscKind] += deal.Value * mag;
                        break;
                    default:
                        throw new Exception("対応していない項目です。");
                }
            }
            else
            {
                throw new Exception("対応していない項目です。");
            }
        }
    }

    /// <summary>
    /// popUpに表示しやすいようなstringを出力する
    /// </summary>
    public string ProgressDetail(List<Dealing> dealings)
    {
        string sum_str = "";
        foreach (var deal in dealings)
        {
            if (sum_str != "") { sum_str += "\n"; }
            if (deal.rscKind is ResourceKind)
            {
                /* リソース */
                if ((deal.paraKind is Dealing.R_ParaKind) == false)
                {
                    throw new Exception("増減させる項目の種類と内容が違います。");
                }
                switch ((Dealing.R_ParaKind)deal.paraKind)
                {
                    case Dealing.R_ParaKind.current:
                        sum_str += main.enumCtrl.resources[(int)(ResourceKind)deal.rscKind].Name() + ":" + tDigit(deal.Value);
                        break;
                    case Dealing.R_ParaKind.max:
                        sum_str += main.enumCtrl.resources[(int)(ResourceKind)deal.rscKind].Name() + " max:" + tDigit(deal.Value);
                        break;
                    case Dealing.R_ParaKind.regen:
                        sum_str += main.enumCtrl.resources[(int)(ResourceKind)deal.rscKind].Name() + " rate:" + tDigit(deal.Value) +"/s";
                        break;
                    case Dealing.R_ParaKind.effect:
                        throw new Exception("まだ対応してないお♡");
                    default:
                        throw new Exception("対応していない項目です。");
                }
            }
            else if (deal.rscKind is AbilityKind)
            {
                /* アビリティ */
                if ((deal.paraKind is Dealing.A_ParaKind) == false)
                {
                    throw new Exception("増減させる項目の種類と内容が違います。");
                }
                switch ((Dealing.A_ParaKind)deal.paraKind)
                {
                    case Dealing.A_ParaKind.maxLevel:
                        sum_str += main.enumCtrl.abilitys[(int)(AbilityKind)deal.rscKind].Name() + " max:" + tDigit(deal.Value);
                        break;
                    case Dealing.A_ParaKind.trainRate:
                        sum_str += main.enumCtrl.abilitys[(int)(AbilityKind)deal.rscKind].Name() + " train rate:" + tDigit(deal.Value) + "/s";
                        break;
                    case Dealing.A_ParaKind.currentExp:
                        sum_str += main.enumCtrl.abilitys[(int)(AbilityKind)deal.rscKind].Name() + " exp:" + tDigit(deal.Value);
                        break;
                    default:
                        throw new Exception("対応していない項目です。");
                }
            }
            else
            {
                throw new Exception("対応していない項目です。");
            }
        }
        return sum_str;
    }


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

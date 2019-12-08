using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UsefulMethod;
using System;

public class BASE : MonoBehaviour
{
    public Main main;
    public void StartBASE()
    {
        main = UsefulMethod.GetMain();
    }

    /// <summary>
    /// listの全てのコストが足りているかどうかを返す
    /// </summary>
    protected bool CanPurchase(List<Dealing> dealings)
    {
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
                        if (main.a_rsc.MaxLevel((int)(AbilityKind)deal.rscKind) + deal.Value < 0) { return false; }
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
                        main.a_rsc.MaxLevels_Base[(int)(AbilityKind)deal.rscKind] += (int)deal.Value;
                        break;
                    case Dealing.A_ParaKind.trainRate:
                        main.a_rsc.TrainRate_Base[(int)(AbilityKind)deal.rscKind] += deal.Value * mag;
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
                        sum_str += main.enumCtrl.resources[(int)(ResourceKind)deal.rscKind].Name() + ":" + tDigit(deal.Value,1);
                        break;
                    case Dealing.R_ParaKind.max:
                        sum_str += main.enumCtrl.resources[(int)(ResourceKind)deal.rscKind].Name() + " max:" + tDigit(deal.Value,1);
                        break;
                    case Dealing.R_ParaKind.regen:
                        sum_str += main.enumCtrl.resources[(int)(ResourceKind)deal.rscKind].Name() + " rate:" + tDigit(deal.Value,1) + "/s";
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
                        sum_str += main.enumCtrl.abilitys[(int)(AbilityKind)deal.rscKind].Name() + " train rate:" + tDigit(deal.Value,1) + "/s";
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
}
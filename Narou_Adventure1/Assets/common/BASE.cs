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

    public delegate int IntSync(int? x = null);
    public delegate double DoubleSync(double? x = null);
    public delegate bool BoolSync(bool? x = null);

    public string DropsDetail(List<Drop> drops)
    {
        string sum = "";
        foreach (var drop in drops)
        {
            if (sum != "") { sum += "\n"; }
            //itemチェック
            if (drop is Item_Drop)
            {
                sum += main.enumCtrl.items[(int)((Item_Drop)drop).itemKind].Name() + " ... " + drop.probability.ToString("F0") + "%";
            }
            else
            {
                sum += main.enumCtrl.resources[(int)drop.kind].Name() + ":" + tDigit(drop.amount) + " ... " + drop.probability.ToString("F0") + "%";
            }
        }
        return sum;
    }

    /// <summary>
    /// listの全てのコストが足りているかどうかを返す
    /// </summary>
    protected bool CanPurchase(List<Dealing> dealings)
    {
        foreach (var deal in dealings)
        {
            //Temporary Effectの判定
            //いつも購入可能
            if(deal is Temp_Regen_Deal || deal is Temp_TRate_Deal)
            {
                return true;
            }
            //Itemの場合の判定
            if(deal is Item_Dealing)
            {
                //今の所確率100%、数1のみ
                throw new Exception("アイテムはコストに選択できません。");
            }
            if (deal.rscKind is ResourceKind)
            {
                /* リソース */
                if ((deal.paraKind is Dealing.R_ParaKind) == false)
                {
                    throw new Exception("増減させる項目の種類と内容が違います。");
                }
                /* ステータス */
                if ((IsStatus((ResourceKind)deal.rscKind) == true  && (Dealing.R_ParaKind)deal.paraKind != Dealing.R_ParaKind.status) ||
                    (IsStatus((ResourceKind)deal.rscKind) == false && (Dealing.R_ParaKind)deal.paraKind == Dealing.R_ParaKind.status))
                {
                    throw new Exception("ステータスの設定が間違っています。");
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
                    case Dealing.R_ParaKind.status:
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
    /// 実際にコストの計算をする。
    /// specificNameはtemporaryEffectの計算をする時のみ必要。
    /// </summary>
    protected void Calculate(List<Dealing> dealings, bool isProgress, float factor = 1, string specificName = "")
    {
        float mag = isProgress ? Time.fixedDeltaTime : 1f;
        mag *= factor;
        foreach (var deal in dealings)
        {
            //Temporary Effectの判定
            if (deal is Temp_Regen_Deal)
            {
                main.tempEffectsCtrl.AddRegen(
                    new TempRegen((deal as Temp_Regen_Deal).resourceKind,
                                  (deal as Temp_Regen_Deal).resourceKind.ToString() + specificName,
                                   ((Temp_Regen_Deal)deal).duration,
                                   deal.Value * mag));
                continue; //次のループへ
            }
            if (deal is Temp_TRate_Deal)
            {
                main.tempEffectsCtrl.AddTrainRate(
                    new TempTrainRate((deal as Temp_TRate_Deal).abilityKind,
                                  (deal as Temp_TRate_Deal).abilityKind.ToString() + specificName,
                                   ((Temp_TRate_Deal)deal).duration,
                                   deal.Value * mag));
                continue; //次のループへ
            }
            //Itemの場合の判定
            if (deal is Item_Dealing)
            {
                //今の所確率100%、数1のみ
                main.itemCtrl.Drop_Inventory((deal as Item_Dealing).itemKind);
                continue; //次のループへ
            }


            // 普通の処理。上の条件に該当しなかった時呼ばれる。
            if (deal.rscKind is ResourceKind)
            {
                /* リソース */
                if ((deal.paraKind is Dealing.R_ParaKind) == false)
                {
                    throw new Exception("増減させる項目の種類と内容が違います。");
                }
                /* ステータス */
                if ((IsStatus((ResourceKind)deal.rscKind) == true && (Dealing.R_ParaKind)deal.paraKind != Dealing.R_ParaKind.status) ||
                    (IsStatus((ResourceKind)deal.rscKind) == false && (Dealing.R_ParaKind)deal.paraKind == Dealing.R_ParaKind.status))
                {
                    throw new Exception("ステータスの設定が間違っています。");
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
                    case Dealing.R_ParaKind.status:
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
    /// NOTE:factorは現状、リソースのstatus,regen,max アビリティのmax, trainrateにしか反映させていない(アイテムでそこしかあげないため)
    /// NOTE:↑とりあえず全てに反映させてみた(2019/12/29)
    /// </summary>
    public string ProgressDetail(List<Dealing> dealings, double factor = 1)
    {
        string sum_str = "";
        foreach (var deal in dealings)
        {
            if (sum_str != "") { sum_str += "\n"; }
            //Temporary Effectの判定
            if (deal is Temp_Regen_Deal)
            {
                if (IsStatus((deal as Temp_Regen_Deal).resourceKind))
                {
                    sum_str += main.enumCtrl.resources[(int)(deal as Temp_Regen_Deal).resourceKind].Name() + ":" + tDigit(deal.Value * factor, 1) + " (" + (deal as Temp_Regen_Deal).duration.ToString("F1") + "s)";
                }
                else
                {
                    sum_str += main.enumCtrl.resources[(int)(deal as Temp_Regen_Deal).resourceKind].Name() + " rate:" + tDigit(deal.Value * factor, 1) + "/s (" + (deal as Temp_Regen_Deal).duration.ToString("F1") + "s)";
                }
                continue; //次のループへ
            }
            if (deal is Temp_TRate_Deal)
            {
                sum_str += main.enumCtrl.abilitys[(int)(deal as Temp_TRate_Deal).abilityKind].Name() + " exp rate:" + tDigit(deal.Value * factor, 1) + "/s (" + (deal as Temp_TRate_Deal).duration.ToString("F1") + "s)";
                continue; //次のループへ
            }
            //Itemの場合の判定
            if (deal is Item_Dealing)
            {
                //今の所確率100%、数1のみ
                sum_str += main.enumCtrl.items[(int)(deal as Item_Dealing).itemKind].Name();
                continue; //次のループへ
            }



            // 普通の処理。上の条件に該当しなかった時呼ばれる。
            if (deal.rscKind is ResourceKind)
            {
                /* リソース */
                if ((deal.paraKind is Dealing.R_ParaKind) == false)
                {
                    throw new Exception("増減させる項目の種類と内容が違います。");
                }
                /* ステータス */
                if ((IsStatus((ResourceKind)deal.rscKind) == true && (Dealing.R_ParaKind)deal.paraKind != Dealing.R_ParaKind.status) ||
                    (IsStatus((ResourceKind)deal.rscKind) == false && (Dealing.R_ParaKind)deal.paraKind == Dealing.R_ParaKind.status))
                {
                    throw new Exception("ステータスの設定が間違っています。");
                }

                switch ((Dealing.R_ParaKind)deal.paraKind)
                {
                    case Dealing.R_ParaKind.current:
                        sum_str += main.enumCtrl.resources[(int)(ResourceKind)deal.rscKind].Name() + ":" + tDigit(deal.Value * factor, main.resourceTextCtrl.points[(int)(ResourceKind)deal.rscKind].current_deal);
                        break;
                    case Dealing.R_ParaKind.max:
                        sum_str += main.enumCtrl.resources[(int)(ResourceKind)deal.rscKind].Name() + " max:" + tDigit(deal.Value * factor, main.resourceTextCtrl.points[(int)(ResourceKind)deal.rscKind].max_deal);
                        break;
                    case Dealing.R_ParaKind.regen:
                        sum_str += main.enumCtrl.resources[(int)(ResourceKind)deal.rscKind].Name() + " rate:" + tDigit(deal.Value * factor, main.resourceTextCtrl.points[(int)(ResourceKind)deal.rscKind].regen_deal) + "/s";
                        break;
                    case Dealing.R_ParaKind.status:
                        sum_str += main.enumCtrl.resources[(int)(ResourceKind)deal.rscKind].Name() + ":" + tDigit(deal.Value * factor, 1);
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
                        sum_str += main.enumCtrl.abilitys[(int)(AbilityKind)deal.rscKind].Name() + " exp rate:" + tDigit(deal.Value * factor, 1) + "/s";
                        break;
                    case Dealing.A_ParaKind.currentExp:
                        sum_str += main.enumCtrl.abilitys[(int)(AbilityKind)deal.rscKind].Name() + " exp:" + tDigit(deal.Value * factor);
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

    //List<Dealing>が全てリソースで、現在値を増やす場合で、かつ満タンの時にtrueを返す。
    public bool EffectIsCompleted(List<Dealing> dealings)
    {
        bool isCompleted = true;
        if(dealings.Count == 0) { return false; }
        foreach (var deal in dealings)
        {
            if (deal is Temp_Regen_Deal || deal is Temp_TRate_Deal)
            {
                return false; //終わりということはありません
            }
            if(deal is Item_Dealing)
            {
                return !main.itemCtrl.CanGetInventory((deal as Item_Dealing).itemKind);//「ゲットできる」の反対を返す
            }
            if(deal.rscKind is AbilityKind)
            {
                return false; //終わりということはありません
            }
            if(deal.rscKind is ResourceKind)
            {
                if((Dealing.R_ParaKind)deal.paraKind != Dealing.R_ParaKind.current) { return false; } //max, regenに終わりはない
                if(main.rsc.Value[(int)(ResourceKind)deal.rscKind] < main.rsc.Max((int)(ResourceKind)deal.rscKind))
                {
                    isCompleted = false;
                }
            }
        }
        return isCompleted;
    }


    public bool IsStatus(ResourceKind kind)
    {
        if (kind == ResourceKind.strength || kind == ResourceKind.mentalStrength || kind == ResourceKind.defense
            || kind == ResourceKind.dodge || kind == ResourceKind.criticalChance || kind == ResourceKind.attack
            || kind == ResourceKind.magic_attack)
        {
            return true;
        }
        return false;
    }
}
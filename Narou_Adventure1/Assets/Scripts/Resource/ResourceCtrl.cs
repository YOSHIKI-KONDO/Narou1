using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;
using UnityEngine.Analytics;

/// <summary>
/// リソースを増やしたり減らしたりするクラス
/// </summary>
public class ResourceCtrl : BASE {

    //読み取り用
    public double Max(int index)
    {
        return Max_Base[index] + main.itemCtrl.R_max[index] + R_max[index];
    }
    public double Regen(int index)
    {
        return Regen_Base[index] + main.itemCtrl.R_regen[index] + main.tempEffectsCtrl.Regens[index] + R_regen[index];
    }
    //読み取り・書き換え可能
    public double[] Value { get => main.SR.current_resource; set => main.SR.current_resource = value; }

    public double[] Max_Base { get => main.SR.max_resource; set => main.SR.max_resource = value; }
    public double[] Regen_Base { get => main.SR.regen_resource; set => main.SR.regen_resource = value; }



    public double[] R_max;//リソースに加算
    public double[] R_regen;//リソースに加算
    public int[] A_maxLevel;//アビリティに加算
    public double[] A_trainRate;//アビリティに加算


    public int ResourceNum { get => main.SD.num_resource;}

    // Use this for initialization
    void Awake () {
		StartBASE();

        R_max = new double[Enum.GetNames(typeof(ResourceKind)).Length];
        R_regen = new double[Enum.GetNames(typeof(ResourceKind)).Length];
        A_maxLevel = new int[Enum.GetNames(typeof(AbilityKind)).Length];
        A_trainRate = new double[Enum.GetNames(typeof(AbilityKind)).Length];
    }

	// Use this for initialization
	void Start () {
        index_exp = (int)ResourceKind.exp;

        CalculateAllEffect();
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        ApplyLevel();
        Truncate();
    }

    //rateは0.1/s
    private void FixedUpdate()
    {
        //regeneration
        for (int i = 0; i < ResourceNum; i++)
        {
            Value[i] += Regen(i) * Time.fixedDeltaTime;
            Truncate();
        }

        CalculateAllEffect();
        CheckDiscover();
    }

    void Truncate()
    {
        //max以下切り捨て
        for (int i = 0; i < ResourceNum; i++)
        {
            Value[i] = Domain(Value[i], Max(i), 0d);
        }
    }

    int index_exp;
    //levelと経験値の設定
    void ApplyLevel()
    {
        Max_Base[index_exp] = 16.5 * main.SR.level + Math.Pow(1.2, main.SR.level);  //最大値を設定
        while (Value[index_exp] >= Max(index_exp))
        {
            Max_Base[index_exp] = 16.5 * main.SR.level + Math.Pow(1.2, main.SR.level);  //最大値を設定
            Value[index_exp] -= Max(index_exp);
            main.SR.level++;
            main.announce.Add("Level UP! (" + (main.SR.level - 1).ToString() + "→" + main.SR.level.ToString() + ")", Color.green);
            main.announce_d.Add("Level UP! (" + (main.SR.level - 1).ToString() + "→" + main.SR.level.ToString() + ")", Color.green);
            main.status.LevelUp(); //ステータスを増やす
            AnalyticsEvent.LevelUp(main.SR.level); //レベルを送信
        }
    }

    //Effectの計算
    //0にした後加算する書き方をしている。
    void CalculateAllEffect()
    {
        ArrayToDefault(R_max);
        ArrayToDefault(R_regen);
        ArrayToDefault(A_maxLevel);
        ArrayToDefault(A_trainRate);
        for (int i = 0; i < main.SD.num_resource; i++)
        {
            if (i == 0) { continue; }
            if (main.resourceTextCtrl.effectAry[i] == null) { continue; }
            CalculateResourceEffect(main.resourceTextCtrl.effectAry[i].effects, (int)Value[i], (ResourceKind)i);
        }
    }

    // 未発見かどうかの更新
    void CheckDiscover()
    {
        for (int i = 0; i < main.SD.num_resource; i++)
        {
            if (i == 0) { continue; }
            if (main.SR.discover_resource[i] == false)
            {
                //現在地が1以上なら発見
                if(Value[i] >= 1) { main.SR.discover_resource[i] = true; }
            }
        }
    }

    /// <summary>
    /// エフェクトの計算。
    /// </summary>
    void CalculateResourceEffect(List<Dealing> dealings, int num, ResourceKind kind)
    {
        foreach (var deal in dealings)
        {
            if(deal is Temp_Regen_Deal || deal is Temp_TRate_Deal || deal is Item_Dealing)
            {
                throw new Exception("対応していない項目です。");
            }
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
                    throw new Exception("ステータスの設定が間違っています。(" + kind.ToString() + ")");
                }
                switch ((Dealing.R_ParaKind)deal.paraKind)
                {
                    case Dealing.R_ParaKind.current:
                        throw new Exception("対応していない項目です。");
                    case Dealing.R_ParaKind.max:
                        R_max[(int)(ResourceKind)deal.rscKind] += deal.Value * num;//計算
                        break;
                    case Dealing.R_ParaKind.regen:
                        R_regen[(int)(ResourceKind)deal.rscKind] += deal.Value * num;//計算
                        break;
                    case Dealing.R_ParaKind.status:
                        R_regen[(int)(ResourceKind)deal.rscKind] += deal.Value * num;//計算
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
                        A_maxLevel[(int)(ResourceKind)deal.rscKind] += (int)deal.Value * num;//計算
                        break;
                    case Dealing.A_ParaKind.trainRate:
                        A_trainRate[(int)(ResourceKind)deal.rscKind] += deal.Value * num;//計算
                        break;
                    case Dealing.A_ParaKind.currentExp:
                        throw new Exception("対応していない項目です。");
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
}

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

/// <summary>
/// main.tickの影響を受ける
/// </summary>
public class TempEffectCtrl : BASE {
    List<TempRegen> regens = new List<TempRegen>();
    List<TempTrainRate> trainRates = new List<TempTrainRate>();

    public double[] Regens = new double[Enum.GetNames(typeof(ResourceKind)).Length];      //リソースに加算
    public double[] TRates = new double[Enum.GetNames(typeof(AbilityKind)).Length];       //アビリティに加算

    // Use this for initialization
    void Awake () {
		StartBASE();
        StartCoroutine(EffectCalculateCor());
	}

    IEnumerator EffectCalculateCor()
    {
        while (true)
        {
            yield return new WaitForSeconds(main.tick);
            ArrayToDefault(TRates);
            ArrayToDefault(Regens);

            if (regens.Count > 0)
            {
                ElapseTempList(Regens, regens);
                if (regens.Count <= 0) { continue; }
                // 値の計算
                for (int i = 0; i < regens.Count; i++)
                {
                    Regens[(int)regens[i].kind] += regens[i].amount;
                }
            }
            if (trainRates.Count > 0)
            {
                ElapseTempList(TRates, trainRates);
                if (trainRates.Count <= 0) { continue; }
                // 値の計算
                for (int i = 0; i < trainRates.Count; i++)
                {
                    TRates[(int)trainRates[i].kind] += trainRates[i].amount;
                }
            }
        }
    }

    public void AddRegen(TempRegen regen)
    {
        AddTempList(regens, regen);
    }

    public void AddTrainRate(TempTrainRate tRate)
    {
        AddTempList(trainRates, tRate);
    }

    void AddTempList<T>(List<T> list, T target)
        where T:TemporaryEffect
    {
        foreach (var effect in list)
        {
            //もしも同じ名前のものがあったら
            //そのremainTimeを回復するだけ
            if(effect.Name == target.Name)
            {
                effect.remainTime = target.remainTime;
                return;
            }
        }
        //被っていなかったらAdd
        list.Add(target);
    }

    //一秒経過させる。残り時間が０以下なら削除。値の計算。
    void ElapseTempList<T>(double[] targets, List<T> effectLists)
        where T : TemporaryEffect
    {
        // 秒数を減らす
        foreach (var effect in effectLists)
        {
            effect.remainTime -= 1f;
        }
        // 0f以下の物を一括で削除
        effectLists.RemoveAll(s => s.remainTime <= 0f);
    }
}

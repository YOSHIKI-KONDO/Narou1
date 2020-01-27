using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class AbilityFunction : ProgressFunction {
    public Condition IsMax;
    public BoolSync UnLocked;
    Button unlockButton;
    public List<Dealing> unlockCostList = new List<Dealing>();

    private void Awake()
    {
        StartBASE();
        main.progressCtrl.list.Add(this);
    }

    public void StartAbility(GameObject Obj, Button unlockButton, Condition Need, Condition IsMax, BoolSync UnLocked, Slider slider, BoolSync hasPaid, DoubleSync currentValue, string actionName, GameObject isOnObj)
    {
        this.IsMax = IsMax;
        this.UnLocked = UnLocked;
        this.unlockButton = unlockButton;
        unlockButton.onClick.AddListener(Unlock);
        StartProgress(Obj, Need, slider, hasPaid, currentValue, actionName, isOnObj, true);
    }

    protected override void CheckButton()
    {
        /* override */
        bool condition = (Need == null || Need()) && // Needを満たしていて
            (IsMax == null || IsMax() == false) &&   // Maxでなくて
            (UnLocked == null || UnLocked());        // Lockされていなければ true

        if (UnLocked())//ロックされていたらunlockButtonを表示
        {
            setFalse(unlockButton.gameObject);
            setActive(button.gameObject);
        }
        else
        {
            setActive(unlockButton.gameObject);
            setFalse(button.gameObject);
            //unlockButton.interactable = CanPurchase(unlockCostList);
        }

        //unlockButtonにもneedを反映させる
        if ((Need == null || Need()) && CanPurchase(unlockCostList))
        {
            unlockButton.interactable = true;
        }
        else
        {
            unlockButton.interactable = false;
        }

        /* ******** */
        if (condition && CanPurchase(initCostList) && CanPurchase(progressCostList))
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }
    }

    //途中で条件を満たさなくなった時にtrainをやめるための関数
    //Abilityのupdate or fixedupdate から呼ぶ
    public void CheckTrain()
    {
        if (isOn == false) { return; }
        if(Need != null && Need() == false)
        {
            main.progressCtrl.DontDoAnything();
        }

        if (IsMax())
        {
            main.progressCtrl.DontDoAnything();
        }
    }

    void Unlock()
    {
        Calculate(unlockCostList, false);
        UnLocked(true);
    }
}

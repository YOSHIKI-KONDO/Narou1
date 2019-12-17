using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class TemporaryEffect {
    public float remainTime;
    public string Name;
    public double amount;

    public TemporaryEffect(string Name, float duration, double amount)
    {
        this.Name = Name;
        this.remainTime = duration;
        this.amount = amount;
    }
}

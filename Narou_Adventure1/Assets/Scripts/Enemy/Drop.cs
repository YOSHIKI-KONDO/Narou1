using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class Drop {
    public ResourceKind kind;
    public double amount;
    public float probability;

    /// <param name="kind">リソースの種類</param>
    /// /// <param name="amount">リソースの量</param>
    /// <param name="probability">入手確率 (0.0f ~ 100.0f)</param>
    public Drop(ResourceKind kind, double amount, float probability)
    {
        this.kind = kind;
        this.amount = amount;
        this.probability = probability;
    }
}

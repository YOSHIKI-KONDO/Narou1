using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class TempRegen : TemporaryEffect
{
    public ResourceKind kind;
    public TempRegen(ResourceKind kind, string Name, float duration, double amount) : base(Name, duration, amount)
    {
        this.kind = kind;
    }
}

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class Temp_TRate_Deal : Dealing
{
    public AbilityKind abilityKind;
    public int duration;
    public Temp_TRate_Deal(AbilityKind kind, double Value, int duration) : base(null, null, Value)
    {
        this.abilityKind = kind;
        this.duration = duration;
    }
}

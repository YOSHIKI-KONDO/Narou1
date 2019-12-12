using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class Temp_Regen_Deal : Dealing {
    public ResourceKind resourceKind;
    public int duration;
    public Temp_Regen_Deal(ResourceKind kind, double Value, int duration) : base(null, null, Value)
    {
        this.resourceKind = kind;
        this.duration = duration;
    }
}

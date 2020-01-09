using System.Collections;
using System.Collections.Generic;
using System;

public class Unlock {
    public delegate bool Condition();
    public delegate bool BoolSync(bool? x = null);
    //Enum UnlockFunctionから is で分岐
    public Enum kind;
    //解放条件
    public Condition condition;
    //解放状況
    public BoolSync released;

    public Unlock(Enum kind, Condition condition, BoolSync released)
    {
        this.kind = kind;
        this.condition = condition;
        this.released = released;
    }
}

using System;
using static BASE;
public class Dealing
{
    public enum R_ParaKind
    {
        nothing,
        current,
        max,
        regen,
        status,
        effect,//一括で変わる仕様なら楽
    }

    public enum A_ParaKind
    {
        nothing,
        maxLevel,
        trainRate,
        currentExp,
    }

    public Enum rscKind;
    public Enum paraKind;
    public double Value
    {
        get
        {
            if (Level != null)
            {
                return _value * (1d + 0.01d * Level()); // level * 1%加算
            }
            else
            {
                return _value;
            }
        }
    }
    double _value;
    public IntSync Level;
    public bool toDisplay;//falseだとdetailに出さない

    //リソース系のコンストラクタ。
    public Dealing(Enum rscKind, Enum paraKind, double Value, bool toDisplay = true)
    {
        this.rscKind = rscKind;
        this.paraKind = paraKind;
        this.toDisplay = toDisplay;
        _value = Value;
    }
}

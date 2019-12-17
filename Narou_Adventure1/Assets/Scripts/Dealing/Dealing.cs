using System;
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
    public double Value;

    //リソース系のコンストラクタ。
    public Dealing(Enum rscKind, Enum paraKind, double Value)
    {
        this.rscKind = rscKind;
        this.paraKind = paraKind;
        this.Value = Value;
    }

    public void Update(double newValue)
    {
        Value = newValue;
    }
}

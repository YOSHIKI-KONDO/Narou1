using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class StatusCtrl : BASE {
    public int Level { get => main.SR.level; }
    public double Hp { get => main.rsc.Max((int)ResourceKind.hp); }
    public double Strength { get => main.rsc.Regen((int)ResourceKind.strength) + Level * 2; }
    public double MentalStrength { get => main.rsc.Regen((int)ResourceKind.mentalStrength) + Level * 2; }
    public double Attack { get => (Strength + main.rsc.Regen((int)ResourceKind.attack)); }
    public double MagicAttack { get => (MentalStrength + main.rsc.Regen((int)ResourceKind.magic_attack)); }
    public double Defense { get => main.rsc.Regen((int)ResourceKind.defense) + Level * 1; }
    public double DodgeChance { get => main.rsc.Regen((int)ResourceKind.dodge); }
    public double CriticalChance { get => main.rsc.Regen((int)ResourceKind.criticalChance); }
    public double CriticalFactor { get => 2; }


    /*  UI  */
    public Text txt_Name_Level;
    public Text txt_Name_Exp;
    public Text txt_Name_Hp;
    public Text txt_Name_Strength;
    public Text txt_Name_MStrength;
    public Text txt_Name_Attack;
    public Text txt_Name_MAttack;
    public Text txt_Name_Defense;
    public Text txt_Name_Dodge;
    public Text txt_Name_CriticalC;
    

    public Text txt_Value_Level;
    public Text txt_Value_Exp;
    public Text txt_Value_Hp;
    public Text txt_Value_Strength;
    public Text txt_Value_MStrength;
    public Text txt_Value_Attack;
    public Text txt_Value_MAttack;
    public Text txt_Value_Defense;
    public Text txt_Value_Dodge;
    public Text txt_Value_CriticalC;
    /*  **  */

    // Use this for initialization
    void Awake () {
		StartBASE();
	}

    private void FixedUpdate()
    {
        txt_Name_Level.text = "Level";
        txt_Name_Exp.text = main.enumCtrl.resources[(int)ResourceKind.exp].Name();
        txt_Name_Hp.text = main.enumCtrl.resources[(int)ResourceKind.hp].Name();
        txt_Name_Strength.text = main.enumCtrl.resources[(int)ResourceKind.strength].Name();
        txt_Name_MStrength.text = main.enumCtrl.resources[(int)ResourceKind.mentalStrength].Name();
        txt_Name_Attack.text = main.enumCtrl.resources[(int)ResourceKind.attack].Name();
        txt_Name_MAttack.text = main.enumCtrl.resources[(int)ResourceKind.magic_attack].Name();
        txt_Name_Defense.text = main.enumCtrl.resources[(int)ResourceKind.defense].Name();
        txt_Name_Dodge.text = main.enumCtrl.resources[(int)ResourceKind.dodge].Name();
        txt_Name_CriticalC.text = main.enumCtrl.resources[(int)ResourceKind.criticalChance].Name();

        
        txt_Value_Level.text = Level.ToString();
        txt_Value_Exp.text = tDigit(main.rsc.Value[(int)ResourceKind.exp]) + " / " + tDigit(main.rsc.Max((int)ResourceKind.exp));
        txt_Value_Hp.text = Hp.ToString();
        txt_Value_Strength.text = Strength.ToString();
        txt_Value_MStrength.text = MentalStrength.ToString();
        txt_Value_Attack.text = Attack.ToString();
        txt_Value_MAttack.text = MagicAttack.ToString();
        txt_Value_Defense.text = Defense.ToString();
        txt_Value_Dodge.text = DodgeChance.ToString();
        txt_Value_CriticalC.text = CriticalChance.ToString();





    }
}

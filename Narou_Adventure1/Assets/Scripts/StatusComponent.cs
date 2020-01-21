using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

public class StatusComponent : BASE {
    /*  UI  */
    public Text txt_Name;
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

    public Button btn_a, btn_b;
    public Button btn_changeName, btn_confirmName;
    public InputField inputField_name;
}

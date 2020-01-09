using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;

/// <summary>
/// ステータスの更新を行なっている。
/// ステータスはResourceKindのRegenを利用している。
/// </summary>
public class StatusCtrl : BASE {
    public int Level { get => main.SR.level; }
    public double Hp { get => main.rsc.Max((int)ResourceKind.hp); }
    public double Strength { get => 0d + main.rsc.Regen((int)ResourceKind.strength) + Level * 2.2; }
    public double MentalStrength { get => 0d + main.rsc.Regen((int)ResourceKind.mentalStrength) + Level * 16.5; }
    public double Attack { get => (Strength + main.rsc.Regen((int)ResourceKind.attack)); }
    public double MagicAttack { get => (MentalStrength + main.rsc.Regen((int)ResourceKind.magic_attack)); }
    public double Defense { get => main.rsc.Regen((int)ResourceKind.defense) + (Level-1) * 1.04d; }
    public double DodgeChance { get => main.rsc.Regen((int)ResourceKind.dodge); }
    public double CriticalChance { get => 5 + main.rsc.Regen((int)ResourceKind.criticalChance); }
    public double CriticalFactor { get => 2; }


    /*  UI  */
    public StatusComponent heroCmps;
    public StatusComponent nornCmps;
    /*  **  */

    // Use this for initialization
    void Awake () {
		StartBASE();

        setActive(nornCmps.btn_a.gameObject);
        setActive(nornCmps.btn_b.gameObject);
        nornCmps.btn_a.onClick.AddListener(() => main.npcSkillCtrl.JoinAlly(AllyKind.npcA));
        nornCmps.btn_b.onClick.AddListener(() => main.npcSkillCtrl.LeaveAlly(AllyKind.npcA));
        nornCmps.btn_a.GetComponentInChildren<Text>().text = "Join";
        nornCmps.btn_b.GetComponentInChildren<Text>().text = "Leave";
    }

    private void FixedUpdate()
    {
        heroCmps.txt_Name_Level.text = "Level";
        heroCmps.txt_Name_Exp.text = main.enumCtrl.resources[(int)ResourceKind.exp].Name();
        heroCmps.txt_Name_Hp.text = main.enumCtrl.resources[(int)ResourceKind.hp].Name();
        heroCmps.txt_Name_Strength.text = main.enumCtrl.resources[(int)ResourceKind.strength].Name();
        heroCmps.txt_Name_MStrength.text = main.enumCtrl.resources[(int)ResourceKind.mentalStrength].Name();
        heroCmps.txt_Name_Attack.text = main.enumCtrl.resources[(int)ResourceKind.attack].Name();
        heroCmps.txt_Name_MAttack.text = main.enumCtrl.resources[(int)ResourceKind.magic_attack].Name();
        heroCmps.txt_Name_Defense.text = main.enumCtrl.resources[(int)ResourceKind.defense].Name();
        heroCmps.txt_Name_Dodge.text = main.enumCtrl.resources[(int)ResourceKind.dodge].Name();
        heroCmps.txt_Name_CriticalC.text = main.enumCtrl.resources[(int)ResourceKind.criticalChance].Name();
        
        
        heroCmps.txt_Value_Level.text = Level.ToString();
        heroCmps.txt_Value_Exp.text = tDigit(main.rsc.Value[(int)ResourceKind.exp]) + " / " + tDigit(main.rsc.Max((int)ResourceKind.exp));
        heroCmps.txt_Value_Hp.text = Hp.ToString("F0");
        heroCmps.txt_Value_Strength.text = Strength.ToString("F0");
        heroCmps.txt_Value_MStrength.text = MentalStrength.ToString("F0");
        heroCmps.txt_Value_Attack.text = Attack.ToString("F0");
        heroCmps.txt_Value_MAttack.text = MagicAttack.ToString("F0");
        heroCmps.txt_Value_Defense.text = Defense.ToString("F0");
        heroCmps.txt_Value_Dodge.text = DodgeChance.ToString("F1") + "%";
        heroCmps.txt_Value_CriticalC.text = CriticalChance.ToString("F1") + "%";

        //Norn
        nornCmps.txt_Name_Level.text = "Level";
        nornCmps.txt_Name_Exp.text = main.enumCtrl.resources[(int)ResourceKind.exp].Name();
        nornCmps.txt_Name_Hp.text = main.enumCtrl.resources[(int)ResourceKind.hp].Name();
        nornCmps.txt_Name_Strength.text = main.enumCtrl.resources[(int)ResourceKind.strength].Name();
        nornCmps.txt_Name_MStrength.text = main.enumCtrl.resources[(int)ResourceKind.mentalStrength].Name();
        nornCmps.txt_Name_Attack.text = main.enumCtrl.resources[(int)ResourceKind.attack].Name();
        nornCmps.txt_Name_MAttack.text = main.enumCtrl.resources[(int)ResourceKind.magic_attack].Name();
        nornCmps.txt_Name_Defense.text = main.enumCtrl.resources[(int)ResourceKind.defense].Name();
        nornCmps.txt_Name_Dodge.text = main.enumCtrl.resources[(int)ResourceKind.dodge].Name();
        nornCmps.txt_Name_CriticalC.text = main.enumCtrl.resources[(int)ResourceKind.criticalChance].Name();


        nornCmps.txt_Value_Level.text = main.npcSkillCtrl.npcs[(int)AllyKind.npcA].level().ToString();
        nornCmps.txt_Value_Exp.text = tDigit(main.npcSkillCtrl.npcs[(int)AllyKind.npcA].currentExp())
            + "/" + tDigit(main.npcSkillCtrl.npcs[(int)AllyKind.npcA].MaxExp);
        nornCmps.txt_Value_Hp.text = main.npcSkillCtrl.npcs[(int)AllyKind.npcA].Hp.ToString("F0");
        nornCmps.txt_Value_Strength.text = main.npcSkillCtrl.npcs[(int)AllyKind.npcA].Strength.ToString("F0");
        nornCmps.txt_Value_MStrength.text = main.npcSkillCtrl.npcs[(int)AllyKind.npcA].MentalStrength.ToString("F0");
        nornCmps.txt_Value_Attack.text = main.npcSkillCtrl.npcs[(int)AllyKind.npcA].Attack.ToString("F0");
        nornCmps.txt_Value_MAttack.text = main.npcSkillCtrl.npcs[(int)AllyKind.npcA].MagicAttack.ToString("F0");
        nornCmps.txt_Value_Defense.text = main.npcSkillCtrl.npcs[(int)AllyKind.npcA].Defense.ToString("F0");
        nornCmps.txt_Value_Dodge.text = main.npcSkillCtrl.npcs[(int)AllyKind.npcA].DodgeChance.ToString("F1") + "%";
        nornCmps.txt_Value_CriticalC.text = main.npcSkillCtrl.npcs[(int)AllyKind.npcA].CriticalChance.ToString("F1") + "%";

    }

    private void Update()
    {
        CheckNorn();
    }


    void CheckNorn()
    {
        if (main.SR.released_Norn)
        {
            setActive(nornCmps.gameObject);
            if (main.npcSkillCtrl.allyKinds.IndexOf(AllyKind.npcA) >= 0)
            {
                nornCmps.btn_a.interactable = false;
                nornCmps.btn_b.interactable = true;
            }
            else
            {
                nornCmps.btn_a.interactable = true;
                nornCmps.btn_b.interactable = false;
            }
        }
        else
        {
            setFalse(nornCmps.gameObject);
            if (main.npcSkillCtrl.allyKinds.Count > 0)
            {
                main.npcSkillCtrl.allyKinds = new List<AllyKind>();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UsefulMethod;
using static MainAction.ActionEnum;

public class UnlockFunction : BASE {
    public List<Unlock> unlocks = new List<Unlock>();

    private void Awake()
    {
        StartBASE();
    }

    private void Update()
    {
        JudgeUnlock();
    }

    void JudgeUnlock()
    {
        foreach (var unlock in unlocks)
        {
            //条件を満たしていたらrelease. すでにリリースされていたら呼ばない。
            if (unlock.condition() && unlock.released() == false)
            {
                unlock.released(true);
                string Name;
                if (unlock.kind is Instant)
                {
                    Name = main.enumCtrl.instantActions[(int)(Instant)unlock.kind].Name() + "(Action)";
                }
                else if (unlock.kind is Loop)
                {
                    Name = main.enumCtrl.loopActions[(int)(Loop)unlock.kind].Name() + "(Action)";
                }
                else if (unlock.kind is Upgrade)
                {
                    Name = main.enumCtrl.upgradeActions[(int)(Upgrade)unlock.kind].Name() + "(Action)";
                }
                else if (unlock.kind is AbilityKind)
                {
                    Name = main.enumCtrl.abilitys[(int)(AbilityKind)unlock.kind].Name() + "(Ability)";
                }
                else if (unlock.kind is SkillKind)
                {
                    Name = main.enumCtrl.skills[(int)(SkillKind)unlock.kind].Name() + "(Skill)";
                }
                else if (unlock.kind is ItemKind)
                {
                    Name = main.enumCtrl.items[(int)(ItemKind)unlock.kind].Name() + "(Shop)";
                }
                else if (unlock.kind is DungeonKind)
                {
                    Name = main.enumCtrl.dungeons[(int)(DungeonKind)unlock.kind].Name() + "(Dungeon)";
                }
                else
                {
                    Debug.Log("Enumが不適切です.");
                    continue;
                }
                main.announce.Add(Name + " has been unlocked.");
            }
        }
    }
}

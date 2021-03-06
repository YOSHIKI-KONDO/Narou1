﻿namespace MainAction
{
    public class ActionEnum
    {
        public enum Instant
        {
            nothing,
            //フェーズ１（入学前）
            weeding,
            eat_anchovy_sandwich,
            writing_paper,
            bind_a_book,

        }

        public enum Loop
        {
            nothing,
            //常用
            rest,
            study,
            //フェーズ１（入学前）
            chores,
            //フェーズ２（学校）
            manual_labor,
            desk_work,
            service_trade,

        }

        public enum Upgrade
        {
            nothing,
            //フェーズ１（入学前）
            //父の道場
            training,
            sword_practice,
            spear_practice,
            rod_practice,
            //母の書斎
            mental_training,
            read_fire_spellbook,
            read_water_spellbook,
            read_wind_spellbook,
            read_earth_spellbook,
            //村の広場
            play_with_cat,
            learn_use_tools,
            succession_life_magic,

            buy_wallet,
            study_in_church,
            buy_bag,
            //進学
            warrior_school,
            sorcerer_school,
            tamer_school,

            //フェーズ２（学校）
            
            //戦士
            apprentice_warrior,
            warrior,
            soldier,
            mercenary,
            trooper,
            knight,
            slayer,
            fighter,
            //魔導士
            apprentice_sorcerer,
            sorcerer,
            wizard,
            warlock,
            high_sorcerer,
            priest,
            conjurer,
            black_mage,
            //テイマー
            apprentice_tamer,
            tamer,
            beast_tamer,
            elementaler,
            monster_tamer,
            summoner,
            //生産職
            crafter,
            iron_crafter,
            magicrafter,
            weapon_maker,
            alchemist,

        }
    }
}

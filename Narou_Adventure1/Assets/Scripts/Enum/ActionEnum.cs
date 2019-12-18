namespace MainAction
{
    public class ActionEnum
    {
        public enum Instant
        {
            nothing,
            //フェーズ１（入学前）
            weeding,
            eat_anchovy_sandwich,
            rune_generation,
            runic_carving,
            split_firewood,
            sell_firewood,

        }

        public enum Loop
        {
            nothing,
            //常用
            rest,
            pray,
            //フェーズ１（入学前）
            chores,
            harvest_wheat,
            grow_herb,
            lumberjack,
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
            practical_skill,
            //少女イベント
            girl_is_crying,
            pick_flowers,
            punish_the_bad_kids,
            //進学
            warrior_school,
            sorcerer_school,
            tamer_school,

            //フェーズ２（学校）
            academic_city,
            into_a_dormitory,
            norns_room,
            rumor,
            //フリークエスト
            delivery_of_fur,
            house_clean_up,
            get_rid_of_rat,
            collect_sword,
            in_cellar,

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

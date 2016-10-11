using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;


//1. Прист должен след в след бегать за ведущим
//2. Поднимать лут
//3. Лечить себя


namespace PWFrameWork
{
    /// <summary>
    /// Набор смещений и адресов
    /// </summary>
    public static class PWOffssAndAddrss
    {
       //==============================================================
        //Игоровые смещения
        //==============================================================
        //выделенный адрес для инжектов
        public static int alloc_inject_address;
        //базовый адресс
       public static int base_address = 0xc7abcc;

        public static int game_address = 0xc7b36c;
        //адрес функции отправки пакетов
        public static int packet_function_address = 0x728320;
        //адрес функции использования скиллов
        public static int use_skill_function_address = 0x47CBF0; 
        //адрес gui-функции
        public static int gui_function_address = 0x77F1D0;
        //смещение к структуре игры
        public static int game_struct_offset = 0x1c;
        

       /* public static int base_address = 0xC7662C;

        public static int game_address = 0xC76DCC;
        //адрес функции отправки пакетов
        public static int packet_function_address = 0x725700;
        //адрес функции использования скиллов
        public static int use_skill_function_address = 0x47CBF0;
        //адрес gui-функции
        public static int gui_function_address = 0x77F1D0;
        //смещение к структуре игры
        public static int game_struct_offset = 0x1c;
        */
        #region GUI
        //=============================================================
        //смещение к структуре графики
        public static int grafic_offset = 0x18;
        //смещение к структуре окон
        public static int gui_offset = 0x8;
        //смещение к структуре текущего окну в списке окон
        public static int current_form_struct_offset_in_forms_list = 0x8;
        //смещение к указателю на следующее окно в списке окон
        public static int next_form_ptr_offset_in_forms_list = 0x0;
        //смещение к имени формы
        public static int form_name_offset = 0x4c;
        //смещение к показателю видимости окна
        public static int form_visible_offset = 0x90;
        //смещение к структуре активного окна
        public static int active_form_struct = 0x74;
        //смещение к списку окон высокого уровня
        public static int hight_level_forms_list_offset = 0x8c;
        //смещение к списку окон низкого уровня
        public static int low_level_forms_list_offset = 0xAC;
        //смещение к структуре контролов окна
        public static int form_controls_struct_offset = 0x1c8;
        //смещение к структуре текущего контрола на форме
        public static int current_control_struct_offset = 0x8;
        //смещение к указателю на следующий контрол на форме
        public static int next_control_struct_offset = 0x4;
        //Смещеине к адресу на имя окна
        public static int control_name_offset = 0x18;
        //Смещение к адресу имени команды
        public static int control_command_name_offset = 0x1C;
        //Смещение к показателю чекнутости контрола
        public static int control_checked_offset = 0x121;
        //Смещение к показателю индекса выбранного итема в листе
        public static int list_selected_index = 0x13c;
        //Смещение к адресу текста контрола
        public static int control_text_address_offset = 0x13FC;
        //Смещение к адресу надписи контрола
        public static int control_caption_address_offset = 0xb8;
        #endregion

        #region Лут
        public static int lut_id = 0x114;
        public static int lut_wid = 0x110;
        public static int lut_x = 0x3c;
        public static int lut_y = 0x44;
        public static int lut_z = 0x40;
        public static int lut_type = 0x150;
        public static int lut_name = 0x168;
        #endregion

        #region Персонаж
        //===================
        //Смещение HostPlayerStuct относительно GameRunAddress 
        public static int host_player_struct_offset = 0x2C;
        //Смещение HostPlayerName относительно HostPlayerStruct
        public static int host_player_name_offset = 0x6B0;
        //Смещение к классу - профессии персонажа
        public static int class_id = 0x6C8; // 0-воин 1-маг 2-шаман 3-друид 4-оборотень 5-убийца 6-лучник 7-жрец 8-страж 9-мистик
        //Смещение к здоровью персонажа
        public static int host_player_hp_offset = 0x4B8;
        //Смещение к максимальному здоровью персонажа
        public static int host_player_max_hp_offset = 0x504;
        //Смещение к количеству маны персонажа
        public static int host_player_mp_offset = 0x4BC;
        //Смещение к количеству максимальной маны персонажа
        public static int host_player_max_mp_offset = 0x508;
        //Смещение к ID цели в структуре персонажа
        public static int host_player_target_wid_offset = 0xd3c;
        public static int host_player_lvl_offset = 0x4a0;
        public static int host_player_wid_offset = 0x494;
        public static int host_player_chi_offset = 0x4bc;
        public static int host_player_free_stats_offset = 0x4b8;

        public static int host_player_x = 0x3c;
        public static int host_player_y = 0x44;
        public static int host_player_z = 0x40;
        #endregion

        #region Инвентарь
        public static int inv_ofs = 0xf10;
        public static int inv_ofs2 = 0xc;
        public static int inv_count = 0x10;
        public static int inv_type = 0x08;
        public static int inv_id = 0xc;
        public static int inv_name = 0x4c;
        public static int inv_ref = 0x90;
        #endregion

        #region Мобы
        public static int mob_str1 = 0x18;
        public static int mob_str2 = 0x20;
        public static int mob_str3 = 0x1c;
        public static int mob_name = 0x278;//+0
        public static int mob_wid = 0x120;
        public static int mob_type = 0xB4; // 6 мобы
        public static int mob_id = 0x124;
        public static int mob_lvl = 0x128;
        #endregion

        #region Скиллы
        public static int s_begin = 0x139C;
        public static int s_max = 0x13a0;
       // public static int s_begin = 0x13D4;
        //public static int s_max = 0x13D8;
        public static int n_begin = 0x128;
        public static int n_max = 0x134;

        #endregion

        #region Команда Party
        //====================
        public static int party_struct_offset = 0x70c;
        public static int party_members_count_offset = 0x18;
        public static int party_member_array_struct_offset = 0x14;//+14 +[i*4] MemberStruct, i = 0 to 6 - MemberIndex. PartyLeader i = 0 - всегда.
        public static int party_member_array_step = 0x4;
        public static int party_member_wid_offset = 0xC;//+C MemberWId
        public static int party_member_lvl_offset = 0x10;//+10 Lvl, dword
        public static int party_member_hp_offset = 0x1C;//+1C HP, dword
        public static int party_member_mp_offset = 0x20;//+20 MP, dword
        public static int party_member_max_hp_offset = 0x24;//+24 MaxHP, dword
        public static int party_member_max_mp_offset = 0x28;//+28 MaxMP, dword
        public static int party_member_loc_x = 0x34;//+34 LocX, float
        public static int party_member_loc_z = 0x38;//+38 LocZ, float
        public static int party_member_loc_y = 0x3C;//+3C LocY, float
        #endregion
        
        #region Char - другие игроки
        //========================
        public static int hash_tables_offset = 0x1c;
        public static int other_player_hash_table_offset = 0x20;
        public static int other_player_hash_table_start_offset = 0x88;
        //            count = game.addr +1C +20 +14
        //addr  = game.addr +1C +20 +88
        #endregion
    }

   
}



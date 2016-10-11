using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PWFrameWork;
using FHotkey;
using System.Threading;
using System.Runtime.InteropServices;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        PacketSender ps;
        public ClientFinder cf;
        public ClientWindow SelectedWindow;
        public PWGameWindow game_window;
        public PWHostPlayer MyPers;
        public MemoryWork memory;
        Config config = new Config(Application.StartupPath + @"\Offsets.ini");

        public Form1()
        {
            InitializeComponent();
            if (!File.Exists(Application.StartupPath + @"\Offsets.ini"))
            {
                config.Write("Offsets", "BaseAdress", Convert.ToString(PWOffssAndAddrss.base_address, 16).ToUpper());
                config.Write("Offsets", "PackCall", Convert.ToString(PWOffssAndAddrss.packet_function_address, 16).ToUpper());
                config.Write("Offsets", "GameStruct", Convert.ToString(PWOffssAndAddrss.game_struct_offset, 16).ToUpper());
                config.Write("Offsets", "HostPlayerStruct", Convert.ToString(PWOffssAndAddrss.host_player_struct_offset, 16).ToUpper());
                config.Write("Offsets", "NameOffs", Convert.ToString(PWOffssAndAddrss.host_player_name_offset, 16).ToUpper());
                config.Write("Offsets", "LvlOffs", Convert.ToString(PWOffssAndAddrss.host_player_lvl_offset, 16).ToUpper());
                config.Write("Ячейки", "Верх", textBox1.Text);
                config.Write("Ячейки", "Штаны", textBox2.Text);
                config.Write("Ячейки", "Сапоги", textBox3.Text);
                config.Write("Ячейки", "Наручи", textBox4.Text);
                config.Write("Ячейки", "Шапка", textBox5.Text);
                config.Write("Ячейки", "Накидка", textBox6.Text);
                config.Write("Ячейки", "Ожа", textBox7.Text);
                config.Write("Ячейки", "Пояс", textBox8.Text);
                config.Write("Ячейки", "Кольцо1", textBox9.Text);
                config.Write("Ячейки", "Кольцо2", textBox10.Text);
                config.Write("Ячейки", "Пушка", textBox11.Text);
                config.Write("Ячейки", "Трактат", textBox13.Text);
                config.Write("Ячейки", "Мешок", textBox14.Text);
                config.Write("Ячейки", "Стрелы", textBox15.Text);
                config.Write("Карты", "Разрушение", textBox21.Text);
                config.Write("Карты", "Уничтожение", textBox20.Text);
                config.Write("Карты", "Долголетие", textBox18.Text);
                config.Write("Карты", "Здоровье", textBox16.Text);
                config.Write("Карты", "Тайна", textBox17.Text);
                config.Write("Карты", "Загадка", textBox19.Text);
                config.Write("Настройки", "Модификатор", comboBox2.Text);
                config.Write("Настройки", "Клавиша", textBox12.Text);
                config.Write("Настройки", "Задержка", textBox22.Text);
            }
            PWOffssAndAddrss.base_address = Convert.ToInt32(config.Read("Offsets", "BaseAdress"), 16);
            PWOffssAndAddrss.packet_function_address = Convert.ToInt32(config.Read("Offsets", "PackCall"), 16);
            PWOffssAndAddrss.game_struct_offset = Convert.ToInt32(config.Read("Offsets", "GameStruct"), 16);
            PWOffssAndAddrss.host_player_struct_offset = Convert.ToInt32(config.Read("Offsets", "HostPlayerStruct"), 16);
            PWOffssAndAddrss.host_player_name_offset = Convert.ToInt32(config.Read("Offsets", "NameOffs"), 16);
            PWOffssAndAddrss.host_player_lvl_offset = Convert.ToInt32(config.Read("Offsets", "LvlOffs"), 16);

            cf = new ClientFinder(PWOffssAndAddrss.base_address, PWOffssAndAddrss.game_struct_offset,
            PWOffssAndAddrss.host_player_struct_offset, PWOffssAndAddrss.host_player_name_offset, PWOffssAndAddrss.host_player_lvl_offset);

            textBox1.Text = config.Read("Ячейки", "Верх");
            textBox2.Text = config.Read("Ячейки", "Штаны");
            textBox3.Text = config.Read("Ячейки", "Сапоги");
            textBox4.Text = config.Read("Ячейки", "Наручи");
            textBox5.Text = config.Read("Ячейки", "Шапка");
            textBox6.Text = config.Read("Ячейки", "Накидка");
            textBox7.Text = config.Read("Ячейки", "Ожа");
            textBox8.Text = config.Read("Ячейки", "Пояс");
            textBox9.Text = config.Read("Ячейки", "Кольцо1");
            textBox10.Text = config.Read("Ячейки", "Кольцо2");
            textBox11.Text = config.Read("Ячейки", "Пушка");
            textBox13.Text = config.Read("Ячейки", "Трактат");
            textBox14.Text = config.Read("Ячейки", "Мешок");
            textBox15.Text = config.Read("Ячейки", "Стрелы");
            textBox16.Text = config.Read("Карты", "Здоровье");
            textBox17.Text = config.Read("Карты", "Тайна");
            textBox18.Text = config.Read("Карты", "Долголетие");
            textBox19.Text = config.Read("Карты", "Загадка");
            textBox20.Text = config.Read("Карты", "Уничтожение");
            textBox21.Text = config.Read("Карты", "Разрушение");
            comboBox2.Text = config.Read("Настройки", "Модификатор");
            textBox12.Text = config.Read("Настройки", "Клавиша");
            textBox22.Text = config.Read("Настройки", "Задержка");
        }


        private void button1_Click(object sender, EventArgs e)
        { 
            if (comboBox1.Text != "")
            {
                foreach (ClientWindow win in comboBox1.Items)
                {
                    if (win.Name == comboBox1.SelectedItem.ToString())
                    {
                        SelectedWindow = win;
                        game_window = new PWGameWindow(SelectedWindow);
                        MyPers = game_window.HostPlayer;
                        this.memory = new MemoryWork(SelectedWindow);
                        ps = new PacketSender(this.memory);
                        comboBox1.Enabled = false;
                        button1.Enabled = false;
                        notifyIcon1.Text = string.Format("{0} [{1}]", MyPers.Name,MyPers.Level);
                    }
                }
            }
        }

        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            foreach (ClientWindow win in cf.ClientWindows)
            {
                comboBox1.Items.Add(win);
            }
        }

        

        public FHotkey.FHotkey hotKey = new FHotkey.FHotkey(1);
        public int id;
        public Keys key = new Keys();
        public KeyModifier modifkey = new KeyModifier();

        private void button2_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < 26; i++) Shmot[i] = 0;
            //Шмот
            Shmot[4] = int.Parse(textBox1.Text);
            Shmot[6] = int.Parse(textBox2.Text);
            Shmot[7] = int.Parse(textBox3.Text);
            Shmot[8] = int.Parse(textBox4.Text);
            Shmot[1] = int.Parse(textBox5.Text);
            Shmot[3] = int.Parse(textBox6.Text);
            //Бижа
            Shmot[2] = int.Parse(textBox7.Text);
            Shmot[5] = int.Parse(textBox8.Text);
            Shmot[9] = int.Parse(textBox9.Text);
            Shmot[10] = int.Parse(textBox10.Text);
            //Пуха
            Shmot[0] = int.Parse(textBox11.Text);
            //Стрелы
            Shmot[11] = int.Parse(textBox15.Text);
            //Книга
            Shmot[18] = int.Parse(textBox13.Text);
            //Мешок
            Shmot[22] = int.Parse(textBox14.Text);
            //Карты
            Shmot[32] = int.Parse(textBox21.Text);
            Shmot[33] = int.Parse(textBox20.Text);
            Shmot[34] = int.Parse(textBox18.Text);
            Shmot[35] = int.Parse(textBox16.Text);
            Shmot[36] = int.Parse(textBox17.Text);
            Shmot[37] = int.Parse(textBox19.Text);

            foreach (Keys k in textBox12.Text)
                if (k.ToString() == textBox12.Text) key = k;
            switch (comboBox2.Text)
            {
                case "Alt": { modifkey = FHotkey.KeyModifier.Alt; break; }
                case "Ctrl": { modifkey = FHotkey.KeyModifier.Control; break; }
                case "Shift": { modifkey = FHotkey.KeyModifier.Shift; break; }
                case "None": { modifkey = FHotkey.KeyModifier.None; break; }
            }
            hotKey.UnRegisterAll();
            hotKey.RegisterHotKey(modifkey, key, OnStart, out id);
        }


        /*
         Пуха 00
         Шапка 01
         Ожерелье 02
         Накидка 03
         Верх 04
         Пояс 05
         Штаны 06
         Сапоги 07
         Наручи 08
         Кольца 09 и 0A/10
         Стрелы 11
         Книга 18
         Мешок 22
         
         * Карты
         * Разрушение 20
         * Уничтожение 21
         * Долголетие 22
         * Здоровье 23
         * Тайна 24
         * Загадка 25
       */

        int[] Shmot = new int[38];
        public void OnStart()
        {
            for (int i = 0; i < 38; i++)
            {
                if (Shmot[i] != 0)
                {
                    ps.Send(Packet.ItemChangeAmmunition(Shmot[i] - 1, i));
                    Thread.Sleep(int.Parse(textBox22.Text));
                }
            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            config.Write("Offsets", "BaseAdress", Convert.ToString(PWOffssAndAddrss.base_address, 16).ToUpper());
            config.Write("Offsets", "PackCall", Convert.ToString(PWOffssAndAddrss.packet_function_address, 16).ToUpper());
            config.Write("Offsets", "GameStruct", Convert.ToString(PWOffssAndAddrss.game_struct_offset, 16).ToUpper());
            config.Write("Offsets", "HostPlayerStruct", Convert.ToString(PWOffssAndAddrss.host_player_struct_offset, 16).ToUpper());
            config.Write("Offsets", "NameOffs", Convert.ToString(PWOffssAndAddrss.host_player_name_offset, 16).ToUpper());
            config.Write("Offsets", "LvlOffs", Convert.ToString(PWOffssAndAddrss.host_player_lvl_offset, 16).ToUpper());
            config.Write("Ячейки","Верх",textBox1.Text);
            config.Write("Ячейки","Штаны",textBox2.Text);
            config.Write("Ячейки","Сапоги",textBox3.Text);
            config.Write("Ячейки","Наручи",textBox4.Text);
            config.Write("Ячейки","Шапка",textBox5.Text);
            config.Write("Ячейки","Накидка",textBox6.Text);
            config.Write("Ячейки","Ожа",textBox7.Text);
            config.Write("Ячейки","Пояс",textBox8.Text);
            config.Write("Ячейки","Кольцо1",textBox9.Text);
            config.Write("Ячейки","Кольцо2",textBox10.Text);
            config.Write("Ячейки", "Пушка", textBox11.Text);
            config.Write("Ячейки", "Трактат", textBox13.Text);
            config.Write("Ячейки", "Мешок", textBox14.Text);
            config.Write("Ячейки", "Стрелы", textBox15.Text);
            config.Write("Карты", "Разрушение", textBox21.Text);
            config.Write("Карты", "Уничтожение", textBox20.Text);
            config.Write("Карты", "Долголетие", textBox18.Text);
            config.Write("Карты", "Здоровье", textBox16.Text);
            config.Write("Карты", "Тайна", textBox17.Text);
            config.Write("Карты", "Загадка", textBox19.Text);
            config.Write("Настройки", "Модификатор", comboBox2.Text);
            config.Write("Настройки", "Клавиша", textBox12.Text);
            config.Write("Настройки", "Задержка", textBox22.Text);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
                Hide();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void развернутьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            WindowState = FormWindowState.Normal;
        }

        private void закрытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox12_KeyUp(object sender, KeyEventArgs e)
        {
            textBox12.Text = e.KeyCode.ToString();
           
        }
        
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prKol_ind4_karamov
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        List<string> catalog = new List<string>();
        Hashtable table = new Hashtable();
        private void CopyToListBox() //Добавляет изменения в listbox с песнями
        {
            listBox1.Items.Clear();
            for (int i = 0; i < Song.songs.Count; i++)
            {
                listBox1.Items.Add($"{Song.songs[i].Name}, {Song.songs[i].Author} : {Song.songs[i].Length}");
            }
        }
        private void CopyToCatalog() //Добавляет диски и песни в каталог
        {
            if (textBox4.Text == string.Empty) //Если поле "Введите исполнителя" пустое
            {
                listBox2.Items.Clear();
                for (int i = 0; i < table.Keys.Count; i++)
                {
                    Song[] songs_arr = table[comboBox1.Items[i]] as Song[];
                    listBox2.Items.Add(comboBox1.Items[i]);
                    for (int j = 0; j < songs_arr.Length; j++)
                    {
                        listBox2.Items.Add($"  {songs_arr[j].Name}, {songs_arr[j].Author} : {songs_arr[j].Length}");
                    }
                }
            }
            else
            {
                listBox2.Items.Clear();
                for (int i = 0; i < table.Keys.Count; i++)
                {
                    Song[] songs_arr = table[comboBox1.Items[i]] as Song[];
                    listBox2.Items.Add(comboBox1.Items[i]);
                    for (int j = 0; j < songs_arr.Length; j++)
                    {
                        if (songs_arr[j].Author == textBox4.Text)
                        {
                            listBox2.Items.Add($"  {songs_arr[j].Name}, {songs_arr[j].Author} : {songs_arr[j].Length}");
                        }
                    }
                }
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) //Выбор диска в combobox, после выбора содержимое хэш таблицы копирую в лист songs
        {
            Song.GetSongs(comboBox1.Text, table);
            CopyToListBox();
        }

        private void button1_Click(object sender, EventArgs e) //Создание нового диска
        {
            if (textBox3.Text != string.Empty)
            {
                if (table.ContainsKey(textBox3.Text) == false)
                {
                    Song.songs.Clear();
                    Song[] arr = Song.songs.ToArray();
                    table.Add(textBox3.Text, arr);
                    CopyToListBox();
                    comboBox1.Items.Add(textBox3.Text);
                    comboBox1.SelectedIndex = comboBox1.Items.IndexOf(textBox3.Text);
                    CopyToCatalog();
                }
                else
                {
                    MessageBox.Show("Такой диск уже есть");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e) //Добавление песни в выбранный диск
        {
            bool AnyDigits = false;
            for (int i = 0; i < textBox1.Text.Length; i++)
            {
                if (char.IsDigit(textBox1.Text[i]))
                {
                    AnyDigits = true;
                }
            }
            for (int i = 0; i < textBox2.Text.Length; i++)
            {
                if (char.IsDigit(textBox2.Text[i]))
                {
                    AnyDigits = true;
                }
            }
            if (textBox1.Text != string.Empty && textBox2.Text != string.Empty && comboBox1.Text != string.Empty && AnyDigits == false)
            {
                Song song = new Song();
                song.Author = textBox1.Text;
                song.Name = textBox2.Text;
                song.Length = $"{numericUpDown1.Value} минут {numericUpDown2.Value} секунд";
                if (listBox1.Items.Contains($"{song.Name}, {song.Author} : {song.Length}") == false)
                {
                    Song.songs.Add(song);
                    Song[] arr = Song.songs.ToArray<Song>();
                    table[comboBox1.Text] = arr;
                }
                CopyToListBox();
                CopyToCatalog();
            }
            else if (AnyDigits == true)
            {
                MessageBox.Show("В названии песни и имени исполнителя не должно быть цифр");
            }
            else
            {
                MessageBox.Show("Заполните все поля и выберите диск");
            }
        }

        private void button3_Click(object sender, EventArgs e) //Удаление песни
        {
            if (listBox1.SelectedIndex != -1)
            {
                Song.songs.RemoveAt(listBox1.SelectedIndex);
                Song[] arr = Song.songs.ToArray<Song>();
                table[comboBox1.Text] = arr;
                CopyToListBox();
                CopyToCatalog();
            }
            else
            {
                MessageBox.Show("Выберите песню");
            }
        }

        private void button4_Click(object sender, EventArgs e) //Удаление диска
        {
            if (comboBox1.Text != string.Empty)
            {
                table.Remove(comboBox1.Text);
                comboBox1.Items.Remove(comboBox1.Text);
                Song.songs.Clear();
                CopyToListBox();
                CopyToCatalog();
            }
        }

        private void button5_Click(object sender, EventArgs e) //Поиск песен по исполнителю
        {
            bool anyDigits = false;
            for (int i = 0; i < textBox4.Text.Length; i++)
            {
                if (char.IsDigit(textBox4.Text[i]))
                {
                    anyDigits = true;
                }
            }
            if (anyDigits == false)
            {
                CopyToCatalog();
            }
            else
            {
                MessageBox.Show("В имени исполнителя не должно быть цифр");
            }
        }
    }
}

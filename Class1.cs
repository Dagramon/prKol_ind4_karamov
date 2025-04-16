using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prKol_ind4_karamov
{
    class Song
    {
        public static List<Song> songs = new List<Song>();
        private string author;
        private string name;
        private string length;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Author
        {
            get { return author; }
            set { author = value; }
        }
        public string Length
        {
            get { return length; }
            set { length = value; }
        }
        public static void GetSongs(string text, Hashtable table)
        {
            songs.Clear();
            Song[] arr = table[text] as Song[];
            songs = arr.ToList();
        }
    }
}

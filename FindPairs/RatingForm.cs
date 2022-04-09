using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FindPairs
{
    public partial class RatingForm : Form
    {
        public RatingForm()
        {
            InitializeComponent();
            InitializeData();
        }


        private void InitializeData()
        {
            string path = $"{Environment.CurrentDirectory}\\Resources\\Base.txt";
            using (StreamReader streamReader = new StreamReader(path))
            {
                string line;
                int count = 0;
                while ((line = streamReader.ReadLine()) != null && count<10)
                {
                    count++;
                    var data = line.Trim().Split().ToList();
                    data.Add(count.ToString());
                    dataGridViewMain.Rows.Add(data.ToArray());
                }
            }
        }
    }
}

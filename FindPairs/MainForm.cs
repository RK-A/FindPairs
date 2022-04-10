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
    public partial class MainForm : Form
    {
        private const int COLUMNS = 6;
        private const int ROWS = 6;
        private string BASE_PATH = $@"{Environment.CurrentDirectory}\Resources\Base.txt";
        // нужно чтобы произведение 2х констант делилось на 2
        // Т.е по желанию можно сделать и 100 на 100 самое главное чтобы exception не вылетел

        private Random rnd;
        private StartForm startForm;
        private Action action;
        private long seconds;
        private string name;
        private int openedCard; //айди картинки у карточки которая в данный момент открыта
        private int score;
        
        public MainForm(StartForm startForm, string name)
        {
            rnd = new Random();
            this.name = name;
            this.startForm = startForm;
            InitializeComponent();
            initialGame();
            openedCard = -1;
            seconds = 0;
            score = 0;
        }

        private void initialGame()
        {
            //Задает игровое поле
            int[] rndIndexes = RandomIndexPair(COLUMNS * ROWS / 2);
            int height = groupBox1.Height / ROWS;
            int width = groupBox1.Width / COLUMNS;
            for (int i = 0; i < ROWS; i++)
            {
                for (int j = 0; j < COLUMNS; j++)
                {
                    groupBox1.Controls.Add(new CustomPanel(OpenCard,
                                                           j * width,
                                                           i * height,
                                                           width,
                                                           height,
                                                           rndIndexes[(COLUMNS * i) + j]));
                }

            }
        }

        private bool OpenCard(int idPic, Action action)
        {
            //При открытии карточки вызывается этот метод
            if (openedCard == -1)
            {
                openedCard = idPic;
                this.action = action; 
                return false;
            }
            if (openedCard !=idPic)
            {
                openedCard = -1;
                this.action?.Invoke();
                return true;
            }
            score ++;
            scoreUp();
            openedCard = -1;
            return false;
        }

        private void scoreUp()
        {
            textBox1.Text = $"score: {score}";
            var time = new DateTime(seconds * 10000000);
            if (score>=ROWS*COLUMNS/2)
            {
                timer1.Stop();
                MessageBox.Show($"Вы победили ваше время {time.Minute}:{time.Second}");
                WriteFile();
                startForm.OpenTable();
                Close();
                startForm.Show();
            }
        }

        private void WriteFile()
        {
            string fileContent = File.ReadAllText(BASE_PATH).Trim();
            string content = string.Join(" ", name, score, seconds);
            File.WriteAllText(BASE_PATH, fileContent + "\n" + content);
            SortFile();
        }

        private void SortFile()
        {
            var list = File.ReadAllText(BASE_PATH)
                .Trim()
                .Split('\n')
                .Select(x => x.Trim())
                .OrderByDescending(x => Convert.ToInt32(x.Split()[1]))
                .ThenBy(x => Convert.ToInt32(x.Split()[2]));
            File.WriteAllText(BASE_PATH, string.Join("\n", list));
        }

        private int[] RandomIndexPair(int countOfPair)
        {
            /* Генерация пар чисел стоящих в случайном порядке*/
            var arr = Enumerable.Range(0, countOfPair*2).ToArray();
            for (int i = arr.Length - 1; i > 0; i--)
            {
                int j = rnd.Next(i + 1);
                int t = arr[j];
                arr[j] = arr[i];
                arr[i] = t;
            }
            return arr.Select(x => x % countOfPair).ToArray();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Таймер для отслеживания времени
            seconds ++;
            DateTime time = new DateTime(seconds * 10000000);
            textBox2.Text = $"Time: {time.Minute}:{time.Second}";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Кнопка выхода
            WriteFile();
            Close();
            startForm.Show();
            startForm.OpenTable();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FindPairs
{
    class CustomPanel : Panel
    {
        private PictureBox pictureBox1;
        private int idPic;
        private Func<int,Action,bool> func; 
        public CustomPanel(Func<int,Action,bool> func, int x, int y, int w, int h, int idPic)
        {
            this.func = func;
            this.idPic = idPic;
            initialPictureBox(idPic);
            initialPanelBox(x,y,w,h);
        }

        private void initialPanelBox(int x, int y, int w, int h)
        {
            //Параметры панели 
            BackColor = SystemColors.Control;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(pictureBox1);
            Location = new Point(x, y);
            Size = new Size(w, h);
            TabIndex = 0;
            MouseClick += new MouseEventHandler(panel_MouseClick);
        }

        private void initialPictureBox(int idPic)
        {
            //Параметры picture box
            pictureBox1 = new PictureBox();
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.Visible = false;
            pictureBox1.Image = new Bitmap($@"{Environment.CurrentDirectory}\\Resources\\{idPic}.png");
        }

        private void panel_MouseClick(object sender, MouseEventArgs e)
        {
            // При открытии панели 
            pictureBox1.Visible = true;
            var flag = func?.Invoke(idPic, SleepPic);
            if (flag !=null && (bool)flag)
            {
                SleepPic();
            }
        }
        private async void SleepPic()
        {
            //Если картинки не совпали то запускается асинхорнно  
            // и оставляет картинку открытой пол секунды.
            await Task.Run(() => Thread.Sleep(500));
            pictureBox1.Visible = false;
        }
    }
}

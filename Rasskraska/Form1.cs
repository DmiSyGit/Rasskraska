using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rasskraska
{
   
    public partial class Form1 : Form
    {
        static List<pixel_coord> pixel = new List<pixel_coord> { };
        int pixel_current = 0;
        Color current_color = Color.White;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        { 
        }
        private bool is_not_border(Color pixel_color)
        {
            for(int i = 0; i <= 184; i += 8)
            {
                if (pixel_color.R == i && pixel_color.G == i && pixel_color.B == i) 
                    return false;
            }
            return true;
        }
        private void colorize(Bitmap img)
        {
            while (pixel_current < pixel.Count)
            {
                if (img.GetPixel(pixel[pixel_current].X, pixel[pixel_current].Y).ToArgb() != current_color.ToArgb() && is_not_border(img.GetPixel(pixel[pixel_current].X, pixel[pixel_current].Y)))
                //img.GetPixel(pixel[pixel_current].X, pixel[pixel_current].Y).ToArgb() == Color.White.ToArgb())
                {
                    img.SetPixel(pixel[pixel_current].X, pixel[pixel_current].Y, current_color);

                    if (is_not_border(img.GetPixel(pixel[pixel_current].X + 1, pixel[pixel_current].Y)) &&
                        img.GetPixel(pixel[pixel_current].X + 1, pixel[pixel_current].Y).ToArgb() != current_color.ToArgb() &&
                        !pixel.Contains(new pixel_coord(pixel[pixel_current].X + 1, pixel[pixel_current].Y)))
                    {
                        pixel.Add(new pixel_coord(pixel[pixel_current].X + 1, pixel[pixel_current].Y));
                    }

                    if (is_not_border(img.GetPixel(pixel[pixel_current].X, pixel[pixel_current].Y + 1)) &&
                        img.GetPixel(pixel[pixel_current].X, pixel[pixel_current].Y + 1).ToArgb() != current_color.ToArgb() &&
                        !pixel.Contains(new pixel_coord(pixel[pixel_current].X, pixel[pixel_current].Y + 1)))
                    {
                        pixel.Add(new pixel_coord(pixel[pixel_current].X, pixel[pixel_current].Y + 1));
                    }

                    if (is_not_border(img.GetPixel(pixel[pixel_current].X - 1, pixel[pixel_current].Y)) &&
                        img.GetPixel(pixel[pixel_current].X - 1, pixel[pixel_current].Y).ToArgb() != current_color.ToArgb() &&
                        !pixel.Contains(new pixel_coord(pixel[pixel_current].X - 1, pixel[pixel_current].Y)))
                    {
                        pixel.Add(new pixel_coord(pixel[pixel_current].X - 1, pixel[pixel_current].Y));
                    }

                    if (is_not_border(img.GetPixel(pixel[pixel_current].X, pixel[pixel_current].Y - 1)) &&
                        img.GetPixel(pixel[pixel_current].X, pixel[pixel_current].Y - 1).ToArgb() != current_color.ToArgb() &&
                        !pixel.Contains(new pixel_coord(pixel[pixel_current].X, pixel[pixel_current].Y - 1)))
                    {
                        pixel.Add(new pixel_coord(pixel[pixel_current].X, pixel[pixel_current].Y - 1));
                    }


                    //MessageBox.Show("Текущий" + pixel[pixel_current].X + " " + pixel[pixel_current].Y +
                    //    "\n левый: " + img.GetPixel(pixel[pixel_current].X + 1, pixel[pixel_current].Y) +
                    //    "\n правый: " + img.GetPixel(pixel[pixel_current].X - 1, pixel[pixel_current].Y) +
                    //    "\n верхний: " + img.GetPixel(pixel[pixel_current].X, pixel[pixel_current].Y + 1) +
                    //    "\n нижный: " + img.GetPixel(pixel[pixel_current].X, pixel[pixel_current].Y - 1));
                }
                pixel.RemoveAt(pixel_current);
            }
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            pixel.Clear();
            pixel.Add(new pixel_coord(e.Location.X, e.Location.Y));
            colorize((Bitmap)(sender as PictureBox).Image);
            (sender as PictureBox).Invalidate();
        }
        private void button_color_Click(object sender, EventArgs e)
        {
            current_color = (sender as Button).BackColor;
        }
    }
    public class pixel_coord
    {
        public int X;
        public int Y;

        public pixel_coord(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}

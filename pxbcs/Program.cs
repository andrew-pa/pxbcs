using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace pxbcs
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form main_window = new Form();
            main_window.Width = 640; main_window.Height = 480;
            main_window.BackgroundImageLayout = ImageLayout.Stretch; 
            main_window.Text = "Pixel Buffer in C# with no C++";
            framebuffer = new Bitmap(main_window.Width, main_window.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            main_window.Paint += main_window_Paint;

            Application.Run(main_window);
        }

        static Bitmap framebuffer; //note this had to be carfully placed, if it was in (declared/created in) the paint event handler it causes low FPS and flicker

        static void main_window_Paint(object sender, PaintEventArgs e)
        {
            Form main_window = (Form)sender;
            main_window.BackgroundImage = framebuffer;
            for (int y = 0; y < 480; ++y)
            {
                for (int x = 0; x < 640; ++x)
                {
                    if (y % 8 == 0 || x % 8 == 0) { framebuffer.SetPixel(x, y, Color.Black); continue; }
                    float r = 0f, g = 0f, b = 0f;
                    //note stupid double casts
                    float px = (float)x / 640f;
                    float py = (float)y / 480f;
                    //it helps to have a vector library
                    r = px;
                    g = py;
                    b = Math.Abs((float)Math.Sin(px*4f)); //note crappy Math static class
                    framebuffer.SetPixel(x, y, Color.FromArgb(255, (int)(r * 255), (int)(g * 255), (int)(b * 255)));
                }
            }
        }
    }
}

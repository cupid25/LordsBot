using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Drawing;
//using System.Drawing.Imaging;
using Emgu;
using Emgu.CV;
using Emgu.CV.Structure;

namespace LordsBot
{
   public class ImageProcessing
    {
        /*
        public struct RECT
        {
            public int Left, Top, Right, Bottom;

            public RECT(int left, int top, int right, int bottom)
            {
                Left = left;
                Top = top;
                Right = right;
                Bottom = bottom;
            }

            public RECT(System.Drawing.Rectangle r) : this(r.Left, r.Top, r.Right, r.Bottom) { }

            public int X
            {
                get { return Left; }
                set { Right -= (Left - value); Left = value; }
            }

            public int Y
            {
                get { return Top; }
                set { Bottom -= (Top - value); Top = value; }
            }

            public int Height
            {
                get { return Bottom - Top; }
                set { Bottom = value + Top; }
            }

            public int Width
            {
                get { return Right - Left; }
                set { Right = value + Left; }
            }

            public System.Drawing.Point Location
            {
                get { return new System.Drawing.Point(Left, Top); }
                set { X = value.X; Y = value.Y; }
            }

            public System.Drawing.Size Size
            {
                get { return new System.Drawing.Size(Width, Height); }
                set { Width = value.Width; Height = value.Height; }
            }

            public static implicit operator System.Drawing.Rectangle(RECT r)
            {
                return new System.Drawing.Rectangle(r.Left, r.Top, r.Width, r.Height);
            }

            public static implicit operator RECT(System.Drawing.Rectangle r)
            {
                return new RECT(r);
            }

            public static bool operator ==(RECT r1, RECT r2)
            {
                return r1.Equals(r2);
            }

            public static bool operator !=(RECT r1, RECT r2)
            {
                return !r1.Equals(r2);
            }

            public bool Equals(RECT r)
            {
                return r.Left == Left && r.Top == Top && r.Right == Right && r.Bottom == Bottom;
            }

            public override bool Equals(object obj)
            {
                if (obj is RECT)
                    return Equals((RECT)obj);
                else if (obj is System.Drawing.Rectangle)
                    return Equals(new RECT((System.Drawing.Rectangle)obj));
                return false;
            }

            public override int GetHashCode()
            {
                return ((System.Drawing.Rectangle)this).GetHashCode();
            }

            public override string ToString()
            {
                return string.Format(System.Globalization.CultureInfo.CurrentCulture, "{{Left={0},Top={1},Right={2},Bottom={3}}}", Left, Top, Right, Bottom);
            }
        }
        */



        public static List<Point> LocateImageMultiple(int handl , Bitmap bmpImage, double threshold) {

            Image<Bgr, byte> source = new Image<Bgr, byte>(ScreenshotWindow(handl));
            Image<Bgr, byte> template = new Image<Bgr, byte>(bmpImage);

            Image<Gray, float> result = source.MatchTemplate(template, Emgu.CV.CvEnum.TemplateMatchingType.CcoeffNormed);


            List<Point> pointList = new List<Point>();

            for (int y = 0; y < result.Data.GetLength(0); y++)
            {
                for (int x = 0; x < result.Data.GetLength(1); x++)
                {
                    if (result.Data[y, x, 0] >= threshold) //Check if its a valid match
                    {
                        //Point loc = new Point(x, y);

                        //Image2 found within Image1
                        //Rectangle match = new Rectangle(loc, template.Size);
                        //imageToShow.Draw(match, new Bgr(Color.Red), 3);

                        pointList.Add(new Point(x,y));

                    }
                }
            }


            
            return pointList;
        }


        public static Point LocateImageSingle(int handl, Bitmap bmpImage, double threshold)
        {

            Image<Bgr, byte> source = new Image<Bgr, byte>(ScreenshotWindow(handl));
            Image<Bgr, byte> template = new Image<Bgr, byte>(bmpImage);

            Image<Gray, float> result = source.MatchTemplate(template, Emgu.CV.CvEnum.TemplateMatchingType.CcoeffNormed);

            double[] minValues, maxValues;
            Point[] minLocations, maxLocations;
            result.MinMax(out minValues, out maxValues, out minLocations, out maxLocations);

            // You can try different values of the threshold. I guess somewhere between 0.75 and 0.95 would be good.
            if (maxValues[0] > threshold)
            {
                // This is a match. Do something with it, for example draw a rectangle around it.

                return maxLocations[0];

            }

           

            //Point pp = new Point(50,50);

            return Point.Empty;
        }






    





        public static Bitmap ScreenshotWindow(int handl) {
            RECT rc; //creates a rectangle 
            Win32.GetWindowRect(handl, out rc); //gets dimensions of the window
            // Bitmap bmp = new Bitmap(rc.Width, rc.Height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb); //creates a empty bitmap with the dimensions of the window
            Bitmap bmp = new Bitmap(rc.Width, rc.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            Graphics memoryGraphics = Graphics.FromImage(bmp);
            IntPtr dc = memoryGraphics.GetHdc();
            Win32.PrintWindow(handl, dc, 0);
            memoryGraphics.ReleaseHdc(dc);
            memoryGraphics.Dispose();
            return bmp;
        }





  

    }
}

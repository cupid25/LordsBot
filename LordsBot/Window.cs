using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Drawing;

namespace LordsBot
{
    class Window
    {

 
        public static void setWindowSize(int handl) {
            //handl = int.Parse(textBox1.Text, System.Globalization.NumberStyles.HexNumber);
            // MoveWindow(handl, 0, 0, 338, 220, true);
            Win32.SetWindowPos(handl, IntPtr.Zero, 20, 20, 837, 501, 0x0040 | 0x0020 | 0x0002 | 0x0004);
            Win32.UpdateWindow(handl);
            // RedrawWindow(handl, IntPtr.Zero, IntPtr.Zero, 0x0400/*RDW_FRAME*/ | 0x0100/*RDW_UPDATENOW*/
            // | 0x0001/*RDW_INVALIDATE*/);
        }


        public static int findWindow(string ClassName, string WindowName) {

            //EX: FindWindow("Qt5QWindowIcon", "bot1");

            int iHandle = Win32.FindWindow(ClassName, WindowName);

            return iHandle;

        }


        //returns the x,y coords of the handle on the screen.
        public static Point CoordOfHandle(int handl)
        {
            Point p = new Point();

            Win32.ClientToScreen(handl, ref p);

            return p;
        }


        // X = Width, Y= Height
        public static Point SizeOfWindow(int handl)
        {

            RECT rc; //creates a rectangle 
            Win32.GetWindowRect(handl, out rc); //gets dimensions of the window

            return new Point(rc.Width, rc.Height);

        }

    }
}

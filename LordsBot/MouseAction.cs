using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;
namespace LordsBot
{
    public class MouseAction
    {

        private const int dragSpeed = 2;


        //sends mouse action to a handle
        public static void SendMouseClick(int iHandle, int x, int y) {

            Win32.SendMessage(iHandle, Win32.WM_LBUTTONDOWN, 0x00000001, MAKELPARAM(x, y));
            Win32.SendMessage(iHandle, Win32.WM_LBUTTONUP, 0x00000000, MAKELPARAM(x, y));

        }

        public static void SendMouseDragUp(int iHandle, int windowWidth, int windowHeight)
        {
            int centerX = windowWidth / 2;
            int centerY = windowHeight / 2;

            Win32.SendMessage(iHandle, Win32.WM_LBUTTONDOWN, 0x00000001, MAKELPARAM(centerX, centerY));

            int i = 0;
            int dragCount = windowHeight / 2;
            while (i <= dragCount)
            {

                Win32.SendMessage(iHandle, Win32.WM_MOUSEMOVE, 0x0001, MAKELPARAM(centerX, centerY + i));


                i = i + dragSpeed;
                Thread.Sleep(1);
            }


            Win32.SendMessage(iHandle, Win32.WM_LBUTTONUP, 0x00000000, MAKELPARAM(centerX, centerY + i));

        }

        public static void SendMouseDragDown(int iHandle, int windowWidth, int windowHeight)
        {

            int centerX = windowWidth / 2;
            int centerY = windowHeight / 2;


            Win32.SendMessage(iHandle, Win32.WM_LBUTTONDOWN, 0x00000001, MAKELPARAM(centerX, centerY));

            int i = 0;
            int dragCount = windowHeight / 2;
            while (i <= dragCount)
            {

                Win32.SendMessage(iHandle, Win32.WM_MOUSEMOVE, 0x0001, MAKELPARAM(centerX, centerY - i));


                i = i + dragSpeed;
                Thread.Sleep(1);
            }


            Win32.SendMessage(iHandle, Win32.WM_LBUTTONUP, 0x00000000, MAKELPARAM(centerX, centerY - i));

        }

        public static void SendMouseDragDownSmall(int iHandle, int windowWidth, int windowHeight, int distance)
        {

            int centerX = windowWidth / 2;
            int centerY = windowHeight / 2;


            Win32.SendMessage(iHandle, Win32.WM_LBUTTONDOWN, 0x00000001, MAKELPARAM(centerX, centerY));

            int i = 0;
            int dragCount = distance;
            while (i <= dragCount)
            {

                Win32.SendMessage(iHandle, Win32.WM_MOUSEMOVE, 0x0001, MAKELPARAM(centerX, centerY - i));


                i = i + dragSpeed;
                Thread.Sleep(1);
            }


            Win32.SendMessage(iHandle, Win32.WM_LBUTTONUP, 0x00000000, MAKELPARAM(centerX, centerY - i));

        }
        public static void SendMouseDragUpSmall(int iHandle, int windowWidth, int windowHeight, int distance)
        {
            int centerX = windowWidth / 2;
            int centerY = windowHeight / 2;

            Win32.SendMessage(iHandle, Win32.WM_LBUTTONDOWN, 0x00000001, MAKELPARAM(centerX, centerY));

            int i = 0;
            int dragCount = distance;
            while (i <= dragCount)
            {

                Win32.SendMessage(iHandle, Win32.WM_MOUSEMOVE, 0x0001, MAKELPARAM(centerX, centerY + i));


                i = i + dragSpeed;
                Thread.Sleep(1);
            }


            Win32.SendMessage(iHandle, Win32.WM_LBUTTONUP, 0x00000000, MAKELPARAM(centerX, centerY + i));

        }

        public static void SendMouseDragRight(int iHandle, int windowWidth, int windowHeight)
        {

            int centerX = windowWidth / 2;
            int centerY = windowHeight / 2;

            Win32.SendMessage(iHandle, Win32.WM_LBUTTONDOWN, 0x00000001, MAKELPARAM(centerX, centerY));

            int i = 0;
            int dragCount = windowWidth / 2;
            while (i <= dragCount)
            {

                Win32.SendMessage(iHandle, Win32.WM_MOUSEMOVE, 0x0001, MAKELPARAM(centerX - i, centerY));


                i = i + dragSpeed;
                Thread.Sleep(1);
            }


            Win32.SendMessage(iHandle, Win32.WM_LBUTTONUP, 0x00000000, MAKELPARAM(centerX - i, centerY));

        }
        public static void SendMouseDragRightSmall(int iHandle, int windowWidth, int windowHeight, int distance)
        {

            int centerX = windowWidth / 2;
            int centerY = windowHeight / 2;

            Win32.SendMessage(iHandle, Win32.WM_LBUTTONDOWN, 0x00000001, MAKELPARAM(centerX, centerY));

            int i = 0;
            int dragCount = distance;
            while (i <= dragCount)
            {

                Win32.SendMessage(iHandle, Win32.WM_MOUSEMOVE, 0x0001, MAKELPARAM(centerX - i, centerY));


                i = i + dragSpeed;
                Thread.Sleep(1);
            }


            Win32.SendMessage(iHandle, Win32.WM_LBUTTONUP, 0x00000000, MAKELPARAM(centerX - i, centerY));

        }

        public static void SendMouseDragLeft(int iHandle, int windowWidth, int windowHeight)
        {

            int centerX = windowWidth / 2;
            int centerY = windowHeight / 2;

            Win32.SendMessage(iHandle, Win32.WM_LBUTTONDOWN, 0x00000001, MAKELPARAM(centerX, centerY));

            int i = 0;
            int dragCount = windowWidth / 2;
            while (i <= dragCount)
            {

                Win32.SendMessage(iHandle, Win32.WM_MOUSEMOVE, 0x0001, MAKELPARAM(centerX + i, centerY));


                i = i + dragSpeed;
                Thread.Sleep(1);
            }


            Win32.SendMessage(iHandle, Win32.WM_LBUTTONUP, 0x00000000, MAKELPARAM(centerX + i, centerY));

        }
        public static void SendMouseDragLeftSmall(int iHandle, int windowWidth, int windowHeight, int distance)
        {

            int centerX = windowWidth / 2;
            int centerY = windowHeight / 2;

            Win32.SendMessage(iHandle, Win32.WM_LBUTTONDOWN, 0x00000001, MAKELPARAM(centerX, centerY));

            int i = 0;
            int dragCount = distance;
            while (i <= dragCount)
            {

                Win32.SendMessage(iHandle, Win32.WM_MOUSEMOVE, 0x0001, MAKELPARAM(centerX + i, centerY));


                i = i + dragSpeed;
                Thread.Sleep(1);
            }


            Win32.SendMessage(iHandle, Win32.WM_LBUTTONUP, 0x00000000, MAKELPARAM(centerX + i, centerY));

        }


        public static void SendMouseDragDownSmall_AccountSwitch(int iHandle, int windowWidth, int windowHeight, int distance)
        {

            int centerX = windowWidth / 2;
            int centerY = windowHeight / 2;


            Win32.SendMessage(iHandle, Win32.WM_LBUTTONDOWN, 0x00000001, MAKELPARAM(centerX, centerY));

            int i = 0;
            int dragCount = distance;
            while (i <= dragCount)
            {

                Win32.SendMessage(iHandle, Win32.WM_MOUSEMOVE, 0x0001, MAKELPARAM(centerX, centerY - i));


                i = i + dragSpeed;
                Thread.Sleep(10);
            }


            Win32.SendMessage(iHandle, Win32.WM_LBUTTONUP, 0x00000000, MAKELPARAM(centerX, centerY - i));

        }
        public static void SendMouseDragUpSmall_AccountSwitch(int iHandle, int windowWidth, int windowHeight, int distance)
        {
            int centerX = windowWidth / 2;
            int centerY = windowHeight / 2;

            Win32.SendMessage(iHandle, Win32.WM_LBUTTONDOWN, 0x00000001, MAKELPARAM(centerX, centerY));

            int i = 0;
            int dragCount = distance;
            while (i <= dragCount)
            {

                Win32.SendMessage(iHandle, Win32.WM_MOUSEMOVE, 0x0001, MAKELPARAM(centerX, centerY + i));


                i = i + dragSpeed;
                Thread.Sleep(10);
            }


            Win32.SendMessage(iHandle, Win32.WM_LBUTTONUP, 0x00000000, MAKELPARAM(centerX, centerY + i));

        }
        public static void SendMouseDragLeftSmall_AccountSwitch(int iHandle, int windowWidth, int windowHeight, int distance)
        {

            int centerX = windowWidth / 2;
            int centerY = windowHeight / 2;

            Win32.SendMessage(iHandle, Win32.WM_LBUTTONDOWN, 0x00000001, MAKELPARAM(centerX, centerY));

            int i = 0;
            int dragCount = distance;
            while (i <= dragCount)
            {

                Win32.SendMessage(iHandle, Win32.WM_MOUSEMOVE, 0x0001, MAKELPARAM(centerX + i, centerY));


                i = i + dragSpeed;
                Thread.Sleep(10);
            }


            Win32.SendMessage(iHandle, Win32.WM_LBUTTONUP, 0x00000000, MAKELPARAM(centerX + i, centerY));

        }

        //mouse action to screen
        public static void ClickLeftButton(int x, int y)
        {

            Win32.SetCursorPos(x, y);
            Win32.mouse_event(Win32.MOUSEEVENTF_LEFTDOWN | Win32.MOUSEEVENTF_LEFTUP, x, y, 0, 0);

        }

        public static int MAKELPARAM(int x, int y)
        {
            return ((y << 16) | (x & 0xFFFF));
        }







    }
}

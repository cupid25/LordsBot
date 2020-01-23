using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace LordsBot
{
    class SendKeyPress
    {
        public const int VK_ESCAPE = 0x1B;

        public static void SendKey(int handl , int key)
        {
            int prevForegroundWindow;
            prevForegroundWindow = Win32.GetForegroundWindow();

            Win32.SetForegroundWindow(handl);

            Win32.PostMessage(handl, Win32.WM_KEYDOWN, key, 0x00010001);
            Win32.PostMessage(handl, Win32.WM_KEYUP, key, 0xC0010001);

            Thread.Sleep(2000);
            Win32.SetForegroundWindow(prevForegroundWindow);
        }



    }
}

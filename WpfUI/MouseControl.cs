using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfUI
{
    class MouseControl
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(long dwFlags, long dx, long dy, long cButtons, long dwExtraInfo);

        private const int LEFTDOWN = 0x02;
        private const int LEFTUP = 0x04;
        private const int RIGHTDOWN = 0x08;
        private const int RIGHTUP = 0x10;

        public void DoMouseClick(int x, int y)
        {
            mouse_event(LEFTDOWN | LEFTUP, x, y, 0, 0);
        }



        //[DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        //public static extern void mouse_event(long dwFlags, long dx, long dy, long cButtons, long dwExtraInfo);

        //private const int LEFTDOWN = 0x02;
        //private const int LEFTUP = 0x04;
        //private const int RIGHTDOWN = 0x08;
        //private const int RIGHTUP = 0x10;

        //public void DoMouseClick()
        //{
        //    //Call the imported function with the cursor's current position
        //    //Вызов импортированной функции с текущей позицией курсора
        //    int X = Cursor.Position.X;
        //    int Y = Cursor.Position.Y;
        //    mouse_event(LEFTDOWN | LEFTUP, X, Y, 0, 0);
        //}
    }
}

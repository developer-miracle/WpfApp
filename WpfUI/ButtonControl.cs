using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace WpfUI
{
    class ButtonControl
    {
        System.Threading.Timer timer;
        public string Button { get; set; }
        public ButtonControl(string button)
        {
            Button = button;
        }

        public void Press()
        {
            timer = new System.Threading.Timer((state) => { SendKeys.SendWait(Button); }, null, 0, 1000);
        }
        public void StopPress()
        {
            timer.Dispose();
        }

        
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Copyinfo.Forms.Controls.Buttons
{
    class TBButtonBase : Button
    {

        string textBuffor;

        public TBButtonBase() : base()
        {

        }

        public void InvokeIfRequired(Action action)
        {
            if (this.InvokeRequired)
                this.Invoke(action);
            else
                action();
        }

        public void InvokeIfRequired<T>(Action<T> action, T parameter)
        {
            if (this.InvokeRequired)
                this.Invoke(action, parameter);
            else
                action(parameter);
        }

        public void SetLoadingState()
        {
            InvokeIfRequired(new Action(() => { this.textBuffor = Text; this.Text = "Wczytuje"; this.Enabled = false; }));
        }

        public void SetNormalState()
        {
            InvokeIfRequired(new Action(() => { this.Text = textBuffor; this.Enabled = true; }));
        }
    }
    
}

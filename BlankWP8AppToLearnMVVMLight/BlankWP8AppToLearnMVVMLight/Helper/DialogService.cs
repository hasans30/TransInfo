using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WhyMvvm.Helpers
{
    public class DialogService : IDialogService
    {
        public object MessageBox { get; private set; }

        public void ShowMessage(string message)
        {
            //no impl
        }
    }
}

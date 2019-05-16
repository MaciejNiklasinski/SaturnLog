
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace SaturnMobile
{
    public partial class Shell : Xamarin.Forms.Shell
    {
        public Shell()
        {
            InitializeComponent();

            BindingContext = this;
        }

    }
}

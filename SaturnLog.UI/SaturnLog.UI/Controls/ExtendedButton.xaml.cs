using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SaturnLog.UI.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExtendedButton : ContentView
    {
        private static readonly Style _defaultEnabledStyle;
        private static readonly Style _defaultDisabledStyle;

        static ExtendedButton()
        {
            //
            Xamarin.Forms.Button defaultStylesSourceButton = new Button();

            //
            defaultStylesSourceButton.IsEnabled = true;

            //
            _defaultEnabledStyle = defaultStylesSourceButton.Style;

            //
            defaultStylesSourceButton.IsEnabled = false;

            //
            _defaultDisabledStyle = defaultStylesSourceButton.Style;

        }









        public event EventHandler Clicked;


        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(ExtendedButton), null);

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }



        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create(nameof(CommandParameter), typeof(Object), typeof(ExtendedButton), null);

        public Object CommandParameter
        {
            get { return (Object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }
        

        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), typeof(ExtendedButton), default(string));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }



        public static readonly BindableProperty FontProperty =
            BindableProperty.Create(nameof(Font), typeof(Font), typeof(ExtendedButton), default(Font));

        public Font Font
        {
            get { return (Font)GetValue(FontProperty); }
            set { SetValue(FontProperty, value); }
        }



        public static readonly BindableProperty FontAttributesProperty =
            BindableProperty.Create(nameof(FontAttributes), typeof(Font), typeof(ExtendedButton), default(Font));

        public Font FontAttributes
        {
            get { return (Font)GetValue(FontAttributesProperty); }
            set { SetValue(FontAttributesProperty, value); }
        }



        public static readonly BindableProperty FontFamilyProperty =
            BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(ExtendedButton), default(string));

        public string FontFamily
        {
            get { return (string)GetValue(FontFamilyProperty); }
            set { SetValue(FontFamilyProperty, value); }
        }



        public static readonly BindableProperty FontSizeProperty =
            BindableProperty.Create(nameof(FontSize), typeof(double), typeof(ExtendedButton), default(double));

        public double FontSize
        {
            get { return (double)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }






















        public static readonly BindableProperty EnabledStyleProperty =
            BindableProperty.Create(nameof(EnabledStyle), typeof(Style), typeof(ExtendedButton), ExtendedButton._defaultEnabledStyle);

        public Style EnabledStyle 
        {
            get { return (Style)GetValue(EnabledStyleProperty); }
            set { SetValue(EnabledStyleProperty, value); }
        }





        public static readonly BindableProperty DisabledStyleProperty =
            BindableProperty.Create(nameof(DisabledStyle), typeof(Style), typeof(ExtendedButton), ExtendedButton._defaultDisabledStyle);

        public Style DisabledStyle
        {
            get { return (Style)GetValue(DisabledStyleProperty); }
            set { SetValue(DisabledStyleProperty, value); }
        }




        public ExtendedButton()
        {
            this.InitializeComponent();

            if (this.EnabledStyle is null)
                this.EnabledStyle = ExtendedButton._defaultEnabledStyle;

            if (this.DisabledStyle is null)
                this.DisabledStyle = ExtendedButton._defaultDisabledStyle;

            this.GestureRecognizers.Add(new TapGestureRecognizer
            {
                // TapGestureRecognizer Command 
                Command = new Command(() => 
                {
                    if (this.IsEnabled)
                    {
                        this.Clicked?.Invoke(this, EventArgs.Empty);

                        if (this.Command?.CanExecute(this.CommandParameter) == true)
                           this.Command.Execute(this.CommandParameter);
                    }
                })
            });
        }
    }
}

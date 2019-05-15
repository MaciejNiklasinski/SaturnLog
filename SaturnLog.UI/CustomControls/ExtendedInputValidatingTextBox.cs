using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SaturnLog.UI
{
    public partial class ExtendedInputValidatingTextBox<EArgs, ExArgs> : TextBox where EArgs : EventArgs where ExArgs : class
    {
        #region Events
        // Occurs every time when the current text box getting enabled for user input.
        public event EventHandler InputEnabled;

        // Occurs every time when the current text box getting disabled for user input.
        public event EventHandler InputDisabled;

        // Occurs right before value provided by the user will get validated. 
        public event EventHandler<EArgs> InputValidationBegan;

        // Occurs right after validation of the value provided by the user failed.
        public event EventHandler<EArgs> InputValidationFailed;

        // Occurs right after validation of the value provided by the user get canceled.
        public event EventHandler<EArgs> InputValidationCanceled;

        // Event which getting invoked every time when user provided empty input and pressed enter key.
        public event EventHandler<EArgs> EmptyInputValueProvided;

        // Event which getting invoked every time when user provided invalid input and pressed enter key.
        public event EventHandler<EArgs> InvalidInputValueProvided;

        // Event which getting invoked every time when user provided valid input and pressed enter key.
        public event EventHandler<EArgs> ValidInputValueProvided;
        #endregion

        #region EventHandlers
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        // Assures that InputEnabled and InputDisabled events getting invoked when the
        // base Control.Enabled property changed its value (non virtual property, cannot be overridden)
        private void InputValidatingTextBox_EnabledChanged(object sender, EventArgs e)
        {
            // If text box is enabled...
            if (this.Enabled)
                // .. invoke InputEnabled event.
                this.InputEnabled?.Invoke(sender, e);
            // Otherwise
            else
                // .. invoke InputDisabled event.
                this.InputDisabled?.Invoke(sender, e);
        }

        // Occurs after the current text box has been enabled for input.
        private void InputValidatingTextBox_InputEnabled(object sender, EventArgs e)
        {
            if (this._validationFunc is null || this._inputProvidedEArgsCreationFunc is null || this._getOtherEArgsCreationParamFunc is null)
                throw new InvalidOperationException("This text box cannot be used before it will be supplied with validation function Func<string, bool> which will let it to determine whether current content of the text box is valid or not, and appropriate generic EArgs creation functions Func<string, EArgs> and Func<ExArgs>.");

            // If ClearOnEnable flag is true...
            if (this.ClearOnEnable)
                // ... clear the text box content.
                this.Clear();

            // If text box is empty or its content is equal with any of the watermarks....
            if (this.IsConsideredEmpty())
            {
                // Set the Text value of the text box its value to watermark text..
                this.Text = this._watermark;
                // Set text color to appropriate for displaying the watermark.
                this.ForeColor = this.ForegroundWatermarkColor;
                // Set flag indicating that the text box is in watermark mode to true.
                this._watermarkMode = true;
            }
            else
            {
                // Set text color to appropriate for displaying user input when text box is active.
                this.ForeColor = ForegroundActiveColor;
                // Set flag indicating that the text box is in watermark mode to false.
                this._watermarkMode = false;
            }

            // Set background color to the one appropriate for the text box when is active.
            this.BackColor = BackgroundActiveColor;

            // Focus on the text box;
            this.Focus();
        }

        private void InputValidatingTextBox_InputDisabled(object sender, EventArgs e)
        {
            // Set text color to appropriate for displaying text when text box is inactive.
            this.ForeColor = this.ForegroundInactiveColor;

            // Set background color to the one appropriate for the text box when is inactive.
            this.BackColor = BackgroundInactiveColor;
        }

        // TODO refactor into two separate methods
        private void InputValidatingTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (!this.Enabled)
                return;

            if (this._validationFunc is null || this._inputProvidedEArgsCreationFunc is null || this._getOtherEArgsCreationParamFunc is null)
                throw new InvalidOperationException("This text box cannot be used before it will be supplied with validation function Func<string, bool> which will let it to determine whether current content of the text box is valid or not, and appropriate generic EArgs creation functions Func<string, EArgs> and Func<ExArgs>.");

            // If button pressed is enter
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                // Declare generic EArgs
                EArgs args;

                // Obtain ExtendedInputArgument
                ExArgs exArgs = this._getOtherEArgsCreationParamFunc.Invoke();
                
                // If empty input is not allowed but provided input is empty.
                if (!this.AllowEmptyInput && this.IsConsideredEmpty())
                {
                    // Get EArgs based on an empty string.
                    args = this._inputProvidedEArgsCreationFunc.Invoke(exArgs, "");

                    // Invoke appropriate event prior to beginning process of validating provided value.
                    this.InputValidationBegan?.Invoke(sender, args);

                    // Invoke methods provided to be invoked when empty input value have been provided.
                    this.EmptyInputValueProvided?.Invoke(sender, args);

                    this._watermarkMode = false;
                    this._watermarkErrorMode = true;

                    this.Text = this._valueIsEmptyWatermark;
                    this.ForeColor = this.ForegroundErrorWatermarkColor;
                    return;
                }

                // Get EArgs based on the content of the text box.
                args = this._inputProvidedEArgsCreationFunc.Invoke(exArgs, this.Text);

                // Invoke appropriate event prior to beginning process of validating provided value.
                this.InputValidationBegan?.Invoke(sender, args);

                string nonWatermarkText = this.GetNonWatermarkText();

                Task<bool> validatingTask = Task.Run(() => { return this.ValidationFunc.Invoke(nonWatermarkText); });
                validatingTask.ContinueWith((t) =>
                {
                    // Declare dialog result variable used in case of failure.
                    DialogResult result;

                    // Depending on task being success/failure/cancellation.
                    switch (t.Status)
                    {
                        case TaskStatus.RanToCompletion:
                            // If provided input is invalid 
                            if (!t.Result)
                            {
                                // Invoke methods provided to be invoked when invalid input have been provided.
                                this.InvalidInputValueProvided?.Invoke(sender, this._inputProvidedEArgsCreationFunc.Invoke(exArgs, this.Text));

                                this._watermarkMode = false;
                                this._watermarkErrorMode = true;

                                this.Text = this._valueIsInvalidWatermark;
                                this.ForeColor = this.ForegroundErrorWatermarkColor;
                            }
                            // If provided input is valid
                            else
                            {
                                // Disable current text box for user input.
                                this.Enabled = false;

                                // Invoke methods provided to be invoked when valid input have been provided.
                                this.ValidInputValueProvided?.Invoke(sender, this._inputProvidedEArgsCreationFunc.Invoke(exArgs, this.Text));
                            }
                            break;
                        case TaskStatus.Canceled:
                            this.InputValidationCanceled?.Invoke(sender, args);
                            break;
                        case TaskStatus.Faulted:
                            result = MessageBox.Show("Failed to validate provided value. Would You like to retry?", "Failed to validate provided value", MessageBoxButtons.YesNo);
                            if (result == DialogResult.Yes)
                                this.InputValidatingTextBox_KeyDown(sender, e);
                            else
                                this.InputValidationFailed?.Invoke(sender, args);
                            break;
                    }
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }
            else if (this._watermarkMode || this._watermarkErrorMode)
            {
                this._watermarkMode = false;
                this._watermarkErrorMode = false;

                this.ForeColor = this.ForegroundActiveColor;
                this.Text = "";
            }
        }
        #endregion

        #region PrivateFields
        private bool _watermarkMode = false;
        private bool _watermarkErrorMode = false;

        private string _watermark;
        private string _valueIsEmptyWatermark;
        private string _valueIsInvalidWatermark;

        // The function used by the text box to determine whether the input provided by the user is valid.
        Func<string, bool> _validationFunc;

        // The function used to create appropriate type EArgs from input.
        Func<ExArgs, string, EArgs> _inputProvidedEArgsCreationFunc;

        // The function allowing to obtain other - than the one coming from the text box itself - Argument
        Func<ExArgs> _getOtherEArgsCreationParamFunc;
        #endregion

        #region Properties
        public bool AllowEmptyInput { get; set; } = false;

        public bool ClearOnEnable { get; set; } = true;

        public bool DisableOnValidInput { get; set; } = true;
        public bool DisableOnInvalidInput { get; set; } = false;
        public bool DisableOnEmptyInput { get; set; } = false;


        // Background
        public Color BackgroundInactiveColor { get; set; } = Color.White;
        public Color BackgroundActiveColor { get; set; } = Color.LightBlue;

        // Foreground
        public Color ForegroundInactiveColor { get; set; } = Color.Black;
        public Color ForegroundActiveColor { get; set; } = Color.Black;
        public Color ForegroundWatermarkColor { get; set; } = Color.Gray;
        public Color ForegroundErrorWatermarkColor { get; set; } = Color.Red;


        public string Watermark
        {
            get { return this._watermark; }
            set { this._watermark = value; }
        }
        public string ValueIsEmptyWatermark
        {
            get { return this._valueIsEmptyWatermark; }
            set { this._valueIsEmptyWatermark = value; }
        }
        public string ValueIsInvalidWatermark
        {
            get { return this._valueIsInvalidWatermark; }
            set { this._valueIsInvalidWatermark = value; }
        }

        public Func<string, bool> ValidationFunc
        {
            get { return this._validationFunc; }
            set { this._validationFunc = value; }
        }

        public Func<ExArgs, string, EArgs> InputProvidedEArgsCreationFunc
        {
            get { return this._inputProvidedEArgsCreationFunc; }
            set { this._inputProvidedEArgsCreationFunc = value; }
        }
        
        public Func<ExArgs> GetOtherEArgsCreationParamFunc
        {
            get { return this._getOtherEArgsCreationParamFunc; }
            set { this._getOtherEArgsCreationParamFunc = value; }
        }
        #endregion

        #region Constructors
        public ExtendedInputValidatingTextBox()
        {
            this.InitializeComponent();

            this.InitializeNonDesignerComponents();
        }
        #endregion

        #region Private Helpers
        private bool IsConsideredEmpty()
        {
            if (this.Text is null
                || this.Text == ""
                || this.Text == this._watermark
                || this.Text == this._valueIsEmptyWatermark
                || this.Text == this._valueIsInvalidWatermark)
                return true;
            else
                return false;
        }

        private string GetNonWatermarkText()
        {
            if (this.IsConsideredEmpty())
                return "";
            else
                return this.Text;
        }

        private void InitializeNonDesignerComponents()
        {
            this.SuspendLayout();
            // 
            // InputValidatingTextBox
            // 
            this.InputEnabled += new System.EventHandler(this.InputValidatingTextBox_InputEnabled);
            this.InputDisabled += new System.EventHandler(this.InputValidatingTextBox_InputDisabled);
            this.ResumeLayout(false);
        }
        #endregion
    }
}

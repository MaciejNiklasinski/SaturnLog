using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SaturnLog.UI.ControlsExtensions
{
    public static class RichTextBoxExtensions
    {
        public static void AppendText(this RichTextBox rtb, string text, Color color)
        {
            rtb.SelectionStart = rtb.TextLength;
            rtb.SelectionLength = 0;

            rtb.SelectionColor = color;
            rtb.AppendText(text);
            rtb.SelectionColor = rtb.ForeColor;
        }

        public static void AppendLog(this RichTextBox rtb, string text, Color color)
        {
            rtb.SelectionStart = rtb.TextLength;
            rtb.SelectionLength = 0;

            rtb.SelectionColor = color;
            rtb.AppendText(Environment.NewLine + text);
            rtb.SelectionColor = rtb.ForeColor;
        }
    }
}

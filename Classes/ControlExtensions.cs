using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WindowsFormsApplication1.Classes
{
    public static class ControlExtensions
    {
        /// <summary>
        /// Executes the Action asynchronously on the UI thread, does not block execution on the calling thread.
        /// </summary>
        /// <param name="control"></param>
        /// <param name="code"></param>
        public static void UIThread(this Control @this, Action code)
        {
            if (@this.InvokeRequired)
            {
                @this.BeginInvoke(code);
            }
            else
            {
                code.Invoke();
            }
        }
    }

    public static class Prompt
    {
        public static Tuple<location, string> ShowDialog(string text, string caption)
        {
            Form prompt = new Form();
            prompt.Width = 500;
            prompt.Height = 150;
            prompt.FormBorderStyle = FormBorderStyle.FixedDialog;
            prompt.Text = caption;
            prompt.StartPosition = FormStartPosition.CenterScreen;
            TextBox textBox = new TextBox() { Left = 50, Top=30, Width=200 };
            Button confirmation = new Button() { Text = "Ok", Left=350, Width=100, Top=70 };

            Dictionary<location, string> _dict = new Dictionary<location, string>();
            _dict.Add(location.BOTTOM, "Onderkant");
            _dict.Add(location.TOP, "Bovenkant");
            _dict.Add(location.LEFT, "Links");
            _dict.Add(location.RIGHT, "Rechts");
            ComboBox comboBox = new ComboBox() { DataSource= new BindingSource(_dict, null), DisplayMember = "Value", ValueMember = "Key", Left = 300, Top = 30 };

            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(comboBox);
            prompt.AcceptButton = confirmation;
            prompt.ShowDialog();

            location _loc = ((KeyValuePair<location, string>)(comboBox.SelectedItem)).Key;

            return new Tuple<location, string>(_loc, textBox.Text);
        }
    }
}
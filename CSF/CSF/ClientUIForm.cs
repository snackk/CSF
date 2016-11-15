using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientUI
{
    public partial class ClientUIForm : Form
    {
        private Engine.Engine engine = null;

        public ClientUIForm()
        {
            InitializeComponent();
        }

        private void decodeButton_Click(object sender, EventArgs e)
        {
            String text = null;

            text = this.textBoxEmailHeader.Text;
            if (!text.Equals(""))
            {
                engine = new Engine.Engine(text);
                outputTextBox.Text = engine.engineOutput;
            }
            else outputTextBox.Text = "";
        }

        private void textBoxEmailHeader_TextChanged(object sender, EventArgs e)
        {

        }

        private void textOutput_Click(object sender, EventArgs e)
        {

        }
    }
}

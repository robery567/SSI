using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SSI
{
    public partial class ColorWindow : UserControl
    {
        public ColorWindow()
        {
            InitializeComponent();
        }
        private Button[] cButtons;
        int orizontal = 10;
        int vertical = 25;
        private void ColorWindow_Load(object sender, EventArgs e)
        {
            EventColor evCol = new EventColor();
            cButtons = new Button[10];
            for (int i = 0; i < cButtons.Length; i++)
            {
                cButtons[i] = new Button();
                cButtons[i].Size = new Size(25, 25);
                cButtons[i].FlatStyle = System.Windows.Forms.FlatStyle.Standard;
                cButtons[i].Location = new Point(vertical, orizontal);
                cButtons[i].BackColor = evCol.colors[i];
                if ((i + 1) % 5 == 0)
                {
                    orizontal += 25;
                    vertical = 25;
                }
                else vertical += 25;

                this.Controls.Add(cButtons[i]);
                cButtons[i].Name = i.ToString();
                cButtons[i].Click += button_Click;
            }
        }
        private void button_Click(object sender, EventArgs e)
        { 

        }
    }
}

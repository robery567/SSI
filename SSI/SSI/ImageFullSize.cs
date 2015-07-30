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
    public partial class ImageFullSize : UserControl
    {
        public ImageFullSize()
        {
            InitializeComponent();
        }

        private void closeUc_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }
    }
}

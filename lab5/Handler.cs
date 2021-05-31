using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    class Handler 
    {
        public void Message(object sender, EventArgs e)
        {
            MessageBox.Show("Достигнут правый край");
        }
    }

}

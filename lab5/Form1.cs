using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Handler Handler1 = new Handler();// создаем экземпляр класса, реагирующего на событие 
            //Подписали метод  Message объекта Handler1 на событие RightBody
            myButton1.RightBody += new EventHandler(Handler1.Message);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void myButton1_Load(object sender, EventArgs e)
        {

        }
    }
}

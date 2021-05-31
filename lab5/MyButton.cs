using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class MyButton : UserControl
    {
        bool isDown; 
        bool isLeft;
        int dx = 3; 
        int inerc = 60;
        Timer tmr = new Timer() { Interval = 1 };
        int n;

        Point DownPoint;

        public MyButton()
        {
            InitializeComponent();
        }
        //    создание события на базе стандартного делегата EventHandler
        public event EventHandler RightBody;
        
        protected virtual void OnRightBody(EventArgs args)
        {
            if (RightBody != null) RightBody(this, args);// вызываем обработчик
        }
        // при нажатии клавиши мыши на кнопке
        private void button1_MouseDown_1(object sender, MouseEventArgs e)
        {
           
            DownPoint = e.Location;
            isDown = true;
        }
        // при отпускании клавиши мыши на кнопке

        private void button1_MouseUp_1(object sender, MouseEventArgs e)
        {
            isDown = false;
            n = 0;//обнуляем число тиков таймера
            tmr.Start();
            tmr.Tick += new EventHandler(tmr_Tick);
        }
        // при движении курсора мыши 
        private void button1_MouseMove_1(object sender, MouseEventArgs e)
        {
            if (isDown) 
            {
                Point p = e.Location; // определяем координаты курсора мыши
                                      //вычисляем разницу в координатах между положением курсора и "нулевой" точкой кнопки
                Point dp = new Point(p.X - DownPoint.X, 0);
                isLeft = (dp.X < 0);// если разница отрицательно двигаемся влево, иначе вправо
                                    // вычисляем конечное правое положение кнопки
                int konec = panel1.Location.X + panel1.Width - button1.Width;
                // если при сдвиге не выйдем за границы панели, двигаем
                if (button1.Location.X + dp.X >= 0 && button1.Location.X + dp.X <= konec)
                    button1.Location = new Point(button1.Location.X + dp.X, button1.Location.Y + dp.Y);
                // если достигли крайнего правого положения
                if (button1.Location.X >= konec)
                {
                    isDown = false; 
                    EventArgs args = new EventArgs();
                    OnRightBody(args);//запустим  событие RightBody, в условии когда достигли правой границы:
                }
                // если достигли крайнего левого положения
                if (button1.Location.X <= 0)
                {
                    isDown = false;

                }
            }
        }
        void tmr_Tick(object sender, EventArgs e)
        {
            // вычисляем гоизонтальную координату крайнего правого положения кнопки на панели 
            int konec = panel1.Location.X + panel1.Width - button1.Width;
            n++;// увеличиваем счетчик тиков на один
            if (n >= 1 && n <= inerc)// если двигаемся по инерции в том же направлении
            {
                if (isLeft) 
                {
                    if (button1.Location.X - dx >= 0)// если еще можно сместить влево, смещаем
                        button1.Location = new Point(button1.Location.X - dx, button1.Location.Y);
                    if (button1.Location.X <= 0)// если достигли левого края
                    {
                        n = 2 * inerc;// устанавливаем счетчик на максимум, больше движения по инерции не будет

                    }
                }
                else 
                {

                    if (button1.Location.X + dx <= konec) // если еще можно сместить вправо, смещаем
                        button1.Location = new Point(button1.Location.X + dx, button1.Location.Y);
                    if (button1.Location.X >= konec) // если достигли правого края
                    {
                        n = 2 * inerc;// устанавливаем счетчик на максимум, больше движения по инерции не будет
                        EventArgs args = new EventArgs();
                        OnRightBody(args);//запустим  событие RightBody
                    }
                }
            }
            
            if (n > 2 * inerc) tmr.Stop();// если движение по инерции закончено, останавливаем таймер
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        { }
        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}

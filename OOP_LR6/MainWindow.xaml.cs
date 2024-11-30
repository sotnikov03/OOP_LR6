using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OOP_LR6
{
   
    public partial class MainWindow : Window
    {
        private const int BallCount = 5; 
        private const double BallSize = 50; 
        private Random random = new Random();

        public MainWindow()
        {
            InitializeComponent();
            StartAnimation();
        }

        private void StartAnimation()
        {
            for (int i = 0; i < BallCount; i++)
            {
                // мячик
                Ellipse ball = new Ellipse
                {
                    Width = BallSize,
                    Height = BallSize,
                    Fill = new RadialGradientBrush(Colors.Yellow, Colors.Orange),
                };

                // начальное положение
                double startX = random.Next(0, Math.Max(0, (int)(AnimationCanvas.ActualWidth - BallSize)));
                double startY = random.Next(0, Math.Max(0, (int)(AnimationCanvas.ActualHeight - BallSize)));

                
                AnimationCanvas.Children.Add(ball);
                Canvas.SetLeft(ball, startX);
                Canvas.SetTop(ball, startY);

                
                Thread thread = new Thread(() => MoveBall(ball));
                thread.IsBackground = true;
                thread.Start();
            }
        }

        private void MoveBall(Ellipse ball)
        {
            // Генерируем случайную скорость
            double dx = random.NextDouble() * 4 - 2; // Скорость по X 
            double dy = random.NextDouble() * 4 - 2; // Скорость по Y 

            while (true)
            {
                Dispatcher.Invoke(() =>
                {
                    // Текущее положение
                    double x = Canvas.GetLeft(ball);
                    double y = Canvas.GetTop(ball);

                    // Новые координаты
                    double newX = x + dx;
                    double newY = y + dy;

                    // Проверка на столкновение со стенами
                    if (newX <= 0 || newX >= AnimationCanvas.ActualWidth - BallSize)
                        dx = -dx;

                    if (newY <= 0 || newY >= AnimationCanvas.ActualHeight - BallSize)
                        dy = -dy;

                    // Установка новых координат
                    Canvas.SetLeft(ball, x + dx);
                    Canvas.SetTop(ball, y + dy);
                });

                Thread.Sleep(20); // Скорость движения
            }
        }
    }
}

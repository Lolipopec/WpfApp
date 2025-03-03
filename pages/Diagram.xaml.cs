﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace WpfApp.pages
{
    /// <summary>
    /// Логика взаимодействия для Diagram.xaml
    /// </summary>
    public partial class Diagram : Page
    {
        public Diagram()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoadPages.MainFrame.Navigate(new login());
        }

        private void Diagram_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double maxX = Diagramm.ActualWidth;//получаем текущую ширину и высоту
            double maxY = Diagramm.ActualHeight;
            Diagramm.Children.Clear();//очишаем графическое поле
            Diagramm.Children.Add(line(maxX / 20, maxY / 20, maxX / 20, maxY - maxY / 20));//помещаем созданный объект на grid
            Diagramm.Children.Add(line(maxX / 20, maxY - maxY / 20, maxX - maxX / 20 - maxX / 200, maxY - maxY / 20));
            double razmer = Math.Sqrt(Math.Pow((maxX - maxX / 20) - (maxX / 20), 2) + Math.Pow((maxY - maxY / 20) - (maxY - maxY / 20), 2));
            double count = 20, stepX = (razmer - maxX / 100) / count, stepY = (maxY - maxY / 10) / count;
            int k = (int)count;
            for (int i = 1; i < count + 1; i++)
            {
                Line L = line(maxX / 20 + stepX * i, maxY - maxY / 20, maxX / 20 + stepX * i, maxY - maxY / 20 - stepY * i);
                L.Stroke = Brushes.Aqua;
                L.StrokeThickness = maxX / 100;
                Diagramm.Children.Add(L);
                TextBlock TB = new TextBlock();
                TB.Text = ((int)(maxY - maxY / 20 - stepY * k)).ToString();
                TB.Margin = new Thickness((maxX / 20 + stepX * i) - 7, maxY - maxY / 20 - stepY * i, 0, 0);
                Diagramm.Children.Add(TB);
                k--;
            }
        }


        private Line line(double x1, double y1, double x2, double y2)
        {
            Line L = new Line();
            L.X1 = x1;
            L.Y1 = y1;
            L.X2 = x2;
            L.Y2 = y2;
            L.Stroke = Brushes.Black;
            return L;
        }

        private Polygon polygon(double x1, double y1, double x2, double y2)
        {
            Polygon P = new Polygon();
            P.Points.Add(new Point(x1, y1));
            P.Points.Add(new Point(x1, y2));
            P.Points.Add(new Point(x2, y2));
            P.Points.Add(new Point(x2, y1));
            P.Stroke = Brushes.Aqua;
            return P;
        }
    }
}

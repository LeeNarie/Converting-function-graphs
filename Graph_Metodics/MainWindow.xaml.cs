using System;
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
using System.IO;


namespace Graph_Metodics
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double zoom = 0;
        public MainWindow()
        {
            InitializeComponent();
            Single_r.IsChecked = true;

        }

        void DrawCoordinate(double w)
        {

            //отрисовка координат


            double interv = 250 / w; //вычисление интервала

            grid1.Children.RemoveRange(1, 500); //очищаем грид

            PathGeometry pathGeom2 = new PathGeometry();
            System.Windows.Shapes.Path p2 = new System.Windows.Shapes.Path();

            if (is_draw_grid.IsChecked == true) //рисование сетки если галочка стоит
            {
                for (double i = 0; i <= 500; i = i + interv / 2)
                {
                    //по иксу
                    PathFigure pf = new PathFigure();
                    pf.StartPoint = new Point(i, 500);
                    LineSegment a = new LineSegment();
                    a.Point = new Point(i, 0);
                    pf.Segments.Add(a);
                    pathGeom2.Figures.Add(pf);

                    //по игреку
                    PathFigure pf2 = new PathFigure();
                    pf2.StartPoint = new Point(500, i);
                    LineSegment a2 = new LineSegment();
                    a2.Point = new Point(0, i);
                    pf2.Segments.Add(a2);
                    pathGeom2.Figures.Add(pf2);

                }
                p2.Data = pathGeom2;
                p2.Stroke = Brushes.LightSkyBlue;
                grid1.Children.Add(p2); 
            }
            // ось координат
            PathGeometry pathGeom = new PathGeometry();
            System.Windows.Shapes.Path p = new System.Windows.Shapes.Path();

            LineSegment vertLS = new LineSegment(); // вертикальная линия
            PathFigure vertPF = new PathFigure();
            vertPF.StartPoint = new Point(250, 500);
            vertLS.Point = new Point(250, 0);
            vertPF.Segments.Add(vertLS);
            pathGeom.Figures.Add(vertPF);

            LineSegment horLS = new LineSegment(); //горизонтальная линия
            PathFigure horPF = new PathFigure();
            horPF.StartPoint = new Point(0, 250);
            horLS.Point = new Point(500, 250);
            horPF.Segments.Add(horLS);
            pathGeom.Figures.Add(horPF);

            int c = 0;
            //деления и цифры 
            for (double i = 250; i <= 500; i = i + interv)
            {
                PathFigure pf = new PathFigure();
                pf.StartPoint = new Point(i, 254);
                LineSegment a = new LineSegment();
                a.Point = new Point(i, 246);
                pf.Segments.Add(a);

                //цифры
                Label l1 = new Label();
                l1.Content = c;
                l1.Margin = new Thickness(i - 6, 250, 3, 0);
                l1.FontSize = interv / 2;
                grid1.Children.Add(l1);
                pathGeom.Figures.Add(pf);
                c++;
            }

            c = 1;
            for (double i = 250 + interv; i <= 500; i = i + interv)
            {
                PathFigure pf = new PathFigure();
                pf.StartPoint = new Point(254, i);
                LineSegment a = new LineSegment();
                a.Point = new Point(246, i);
                pf.Segments.Add(a);
                pathGeom.Figures.Add(pf);

                Label l1 = new Label();
                l1.Content = c * -1;
                l1.Margin = new Thickness(252, i - 12, 3, 0);
                l1.FontSize = interv / 2;
                grid1.Children.Add(l1);
                pathGeom.Figures.Add(pf);
                c++;
            }
            c = 1;
            for (double i = 250 - interv; i >= 0; i = i - interv)
            {
                PathFigure pf = new PathFigure();
                pf.StartPoint = new Point(i, 254);
                LineSegment a = new LineSegment();
                a.Point = new Point(i, 246);
                pf.Segments.Add(a);
                pathGeom.Figures.Add(pf);

                Label l1 = new Label();
                l1.Content = c * -1;
                l1.Margin = new Thickness(i - 6, 250, 3, 0);
                l1.FontSize = interv / 2;
                grid1.Children.Add(l1);
                pathGeom.Figures.Add(pf);
                c++;

            }
            c = 1;
            for (double i = 250 - interv; i >= 0; i = i - interv)
            {
                PathFigure pf = new PathFigure();
                pf.StartPoint = new Point(254, i);
                LineSegment a = new LineSegment();
                a.Point = new Point(246, i);
                pf.Segments.Add(a);
                pathGeom.Figures.Add(pf);

                //цифры
                Label l1 = new Label();
                l1.Content = c;
                l1.Margin = new Thickness(252, i - 12, 3, 0);
                l1.FontSize = interv / 2;
                grid1.Children.Add(l1);
                pathGeom.Figures.Add(pf);
                c++;

            }

            PolyLineSegment vertArr = new PolyLineSegment();
            vertArr.Points = new PointCollection();
            vertArr.Points.Add(new Point(250, 0));
            vertArr.Points.Add(new Point(255, 10));
            PathFigure vertArrF = new PathFigure();
            vertArrF.StartPoint = new Point(245, 10);
            vertArrF.Segments.Add(vertArr);
            pathGeom.Figures.Add(vertArrF);

            PolyLineSegment horArr = new PolyLineSegment();
            horArr.Points = new PointCollection();
            horArr.Points.Add(new Point(495, 250));
            horArr.Points.Add(new Point(490, 245));
            PathFigure horArrF = new PathFigure();
            horArrF.StartPoint = new Point(490, 255);
            horArrF.Segments.Add(horArr);
            pathGeom.Figures.Add(horArrF);


            p.Data = pathGeom;
            p.Stroke = Brushes.Black;
            grid1.Children.Add(p);
        }

        bool isbuild = false;
        int graph_drawed = 0;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Single_r.IsChecked == true)
            {
                isbuild = true;

                try
                {

                    minX = -1 * Double.Parse(w_t.Text);
                    minY = -1 * Double.Parse(w_t.Text);

                    maxX = Double.Parse(w_t.Text);
                    maxY = Double.Parse(w_t.Text);

                    if (maxX < 150)
                    {


                        DrawCoordinate(Double.Parse(w_t.Text));

                        if (r1.IsChecked == true) { UpdateGraph(1); graph_drawed = 1; }
                        if (r2.IsChecked == true) { UpdateGraph(2); graph_drawed = 2; }
                        if (r4.IsChecked == true) { UpdateGraph(4); graph_drawed = 4; }
                        if (r5.IsChecked == true) { UpdateGraph(5); graph_drawed = 5; }
                        if (r6.IsChecked == true) { UpdateGraph(6); graph_drawed = 6; }
                        if (r8.IsChecked == true) { UpdateGraph(7); graph_drawed = 7; }
                        if (r9.IsChecked == true) { UpdateGraph(9); graph_drawed = 9; }
                        if (r10.IsChecked == true) { UpdateGraph(10); graph_drawed = 10; }

                        gr_info_t.Content = graph_info;
                        zoom = maxY;

                    }
                    else
                    {
                        w_t.Foreground = Brushes.Red;
                        MessageBox.Show("Некорректное значение!");
                    }
                }
                catch
                {
                    MessageBox.Show("Произошла ошибка! Проверьте правильность ввода данных.");
                }
            }

        }
        string graph_info = "";
        double func(double x, int k)
        {

            double function = 0;

            graph_info = "";

            string moduley = "";
            string modulex = "x";

            if (y_mod.IsChecked == true)
            {
                moduley = "|";
            }
            if (x_mod.IsChecked == true)
            {
                modulex = "|x|";
            }


            double x_d = Double.Parse(x_displace.Text);
            double y_d = Double.Parse(y_displace.Text);

            double x_s = Double.Parse(ox_stretch.Text);
            double y_s = Double.Parse(oy_stretch.Text);


            graph_info += "y =";

            if (x_mod.IsChecked == true)
            {
                x = Math.Abs(x);
            }
            if (Reverce.IsChecked == true)
            {
                x_s = x_s * -1;
            }
            graph_info += moduley + x_s.ToString() + " * ";


            switch (k)
            {
                case 1:
                    {
                        graph_info += modulex + " + " + x_d + moduley; function = x_s * (x + x_d);
                        break;
                    }
                case 2:
                    {
                        graph_info += "(" + y_s + " * " + modulex + " - " + x_d + ")² + " + y_d + moduley; function = x_s * Math.Pow((y_s * x) - x_d, 2) + y_d;
                        break;
                    }
                case 4:
                    {
                        graph_info += "(" + y_s + " * " + modulex + " - " + x_d + ")³ + " + y_d + moduley; function = x_s * Math.Pow((y_s * x) - x_d, 3) + y_d;
                        break;
                    }
                case 5:
                    {
                        graph_info += "Sin(" + y_s + " * " + modulex + " - " + x_d + ") + " + y_d + moduley; function = x_s * Math.Sin((y_s * x) - x_d) + y_d;
                        break;
                    }
                case 6:
                    {
                        graph_info += "Cos(" + y_s + " * " + modulex + " - " + x_d + ") + " + y_d + moduley; function = x_s * Math.Cos((y_s * x) - x_d) + y_d;
                        break;
                    }
                case 7:
                    {
                        graph_info += "Tg(" + y_s + " * " + modulex + " - " + x_d + ") + " + y_d + moduley; function = x_s * Math.Tan((y_s * x) - x_d) + y_d;
                        break;
                    }
                case 9:
                    {
                        graph_info += "1/x"; function =  x_s * (1 / (y_s * x) - x_d)  + y_d;
                        break;
                    }
                    case 10:
                    {
                        graph_info += "sqrt(x)"; function = x_s * (Math.Sqrt (y_s * x) - x_d) + y_d;
                        break;
                    }
                default:
                    {
                        break;
                    }

            }

            if (y_mod.IsChecked == true)
            {
                function = Math.Abs(function);
            }

            /* if (k == 1) { graph_info += "x + " + x_d + moduley; return x_s * (x + x_d); }
             if (k == 2) { graph_info += "(" + y_s + " * x - " + x_d + ")² + " + y_d + moduley; return x_s * Math.Pow((y_s * x) - x_d, 2) + y_d; }
             if (k == 4) { graph_info += "(" + y_s + " * x - " + x_d + ")³ + " + y_d + moduley; return x_s * Math.Pow((y_s * x) - x_d, 3) + y_d; }
             if (k == 5) { graph_info += "Sin(" + y_s + " * x - " + x_d + ") + " + y_d + moduley; return x_s * Math.Sin((y_s * x) - x_d) + y_d; }
             if (k == 6) { graph_info += "Cos(" + y_s + " * x - " + x_d + ") + " + y_d + moduley; return x_s * Math.Cos((y_s * x) - x_d) + y_d; }
             if (k == 7) { graph_info += "Tg(" + y_s + " * x - " + x_d + ") + " + y_d + moduley; return x_s * Math.Tan((y_s * x) - x_d) + y_d; }
             //if (k == 9) return 1 / x; */
            return function;

        }


        double minX = -25;
        double maxX = 25;

        double minY = -25;
        double maxY = 25;

        void UpdateGraph(int k)
        {

            var pixelWidth = 500;
            var pixelHeight = 500;
            PointCollection points = new PointCollection((int)pixelWidth + 1);
            for (int pixelX = 0; pixelX < pixelWidth; pixelX++)
            {
                if (k == 9 & pixelX == 250)
                {
                    var y = 200;
                    var pixelY = pixelHeight - MapToPixel(y, minY, maxY, pixelHeight);
                    points.Add(new Point(pixelX, pixelY));
                }
                else if (k == 10 & pixelX < 250 & x_mod.IsChecked==false)
                {

                }
                else
                {
                    var x = MapFromPixel(pixelX, pixelWidth, minX, maxX);
                    var y = func(x, k);
                    var pixelY = pixelHeight - MapToPixel(y, minY, maxY, pixelHeight);
                    points.Add(new Point(pixelX, pixelY));
                }
            }

           /* PointCollection points2 = new PointCollection((int)pixelWidth + 1);
            for (int pixelX = pX; pixelX < pixelWidth; pixelX++)
            {
                var x = MapFromPixel(pixelX, pixelWidth, minX, maxX);
                var y = func(x, 6);
                var pixelY = pixelHeight - MapToPixel(y, minY, maxY, pixelHeight);
                points2.Add(new Point(pixelX, pixelY));
            }

            PointCollection points3 = new PointCollection((int)pixelWidth + 1);
            for (int pixelX = pX; pixelX < pixelWidth; pixelX++)
            {
                var x = MapFromPixel(pixelX, pixelWidth, minX, maxX);
                var y = func(x, 4);
                var pixelY = pixelHeight - MapToPixel(y, minY, maxY, pixelHeight);
                points3.Add(new Point(pixelX, pixelY));
            }
           */
            Graph1.Points = points;
            //Graph2.Points = points2;
            //Graph3.Points = points3;
        }

        double MapFromPixel(double pixelV, double pixelMax, double minV, double maxV) =>
            minV + (pixelV / pixelMax) * (maxX - minX);

        double MapToPixel(double v, double minV, double maxV, double pixelMax) =>
            (v - minV) / (maxV - minV) * pixelMax;

        private void Graph1_MouseMove(object sender, MouseEventArgs e)
        {
            Point pos = Mouse.GetPosition(grid1);

            var pixelWidth = 500;
            var pixelHeight = 500;

            var x = MapFromPixel(pos.X, pixelWidth, minX, maxX);
            var y = MapFromPixel(pos.Y, pixelHeight, minY, maxY);



            xy_l.Content = "x: " + x.ToString("F3") + "; y: " + (y * -1).ToString("F3");

        }


        private void Graph1_MouseWheel(object sender, MouseWheelEventArgs e) //Приближение-уменьшение колесиком мыши
        {
            try
            {
                if (isbuild == true)
                {

                    if (e.Delta > 0)
                    {
                        if (zoom <= 30)
                        {
                            zoom += 0.5;


                            minX = -1 * zoom;
                            minY = -1 * zoom;

                            maxX = zoom;
                            maxY = zoom;


                            DrawCoordinate(zoom);

                            UpdateGraph(graph_drawed);
                        }
                    }

                    else if (e.Delta < 0)
                    {
                        if (zoom > 0.5 & zoom < 50)
                        {
                            zoom -= 0.5;


                            minX = -1 * zoom;
                            minY = -1 * zoom;

                            maxX = zoom;
                            maxY = zoom;


                            DrawCoordinate(zoom);

                            UpdateGraph(graph_drawed);
                        }
                    }
                }
            }
            catch
            { }

        }

        private void w_t_TextChanged(object sender, TextChangedEventArgs e)
        {

            try
            {
                zoom = Double.Parse(w_t.Text);
                if (Double.Parse(w_t.Text) > 150 || Double.Parse(w_t.Text)<1)
                {
                    w_t.Foreground = Brushes.Red;
                }
                else
                {
                    w_t.Foreground = Brushes.Black;
                }
            }
            catch
            {
                w_t.Foreground = Brushes.Red;
            }
        }

        private void r1_Checked(object sender, RoutedEventArgs e)
        {
            y_displace.IsEnabled = false;
            oy_stretch.IsEnabled = false;
        }

        private void r1_Unchecked(object sender, RoutedEventArgs e)
        {
            y_displace.IsEnabled = true;
            oy_stretch.IsEnabled = true;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e) //сохр как картинку
        {

            {

                var pSource = PresentationSource.FromVisual(Application.Current.MainWindow);
                Matrix m = pSource.CompositionTarget.TransformToDevice;
                double dpiX = m.M11 * 96;
                double dpiY = m.M22 * 96;


                var elementBitmap = new RenderTargetBitmap(500, 500, dpiX, dpiY, PixelFormats.Default);


                var drawingVisual = new DrawingVisual();
                using (DrawingContext drawingContext = drawingVisual.RenderOpen())
                {
                    var visualBrush = new VisualBrush(grid1);
                    drawingContext.DrawRectangle(
                        visualBrush,
                        null,
                        new Rect(new Point(0, 0), new Size(500 / m.M11, 500 / m.M22)));
                }


                elementBitmap.Render(drawingVisual);


                var encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(elementBitmap));


                using (var imageFile = new FileStream("graph_" + DateTime.Now.ToString("dd.MM.yyyy_hh.mm.ss") + ".png", FileMode.Create, FileAccess.Write))
                {
                    encoder.Save(imageFile);
                    imageFile.Flush();
                    imageFile.Close();
                }

                MessageBox.Show("Рисунок успешно сохранен в папке с программой!");
            }
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            PrintDialog p = new PrintDialog();
            if(p.ShowDialog() == true)
            {
                p.PrintVisual(grid1, "Печать");
            }
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            Button_Click(sender, e);
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {

            Graph1.Points.Clear();
            isbuild = false;

        }

        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("В разработке!");
        }

        private void Single_r_Copy_Checked(object sender, RoutedEventArgs e)
        {
            radio_buttons_grid.IsEnabled = false;
            tab_g2.IsEnabled = true;
            tab_g3.IsEnabled = true;
            g1_list.IsEnabled = true;
            g2_list.IsEnabled = true;
            g3_list.IsEnabled = true;
        }

        private void Single_r_Checked(object sender, RoutedEventArgs e)
        {
            radio_buttons_grid.IsEnabled = true;
            tab_g2.IsEnabled = false;
            tab_g3.IsEnabled = false;
            g1_list.IsEnabled = false;
            g2_list.IsEnabled = false;
            g3_list.IsEnabled = false;
        }
    }
}


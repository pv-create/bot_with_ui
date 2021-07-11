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
using System.Windows.Threading;
using System.Configuration;
using System.Data;
using System.Runtime.CompilerServices;
using System.Data.SqlClient;
using System.Data.OleDb;
using Telegram.Bot;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using Telegram.Bot.Args;

namespace bot
{
   
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       

        TextBox newTextBox = new TextBox();
        TextBox newLabel = new TextBox();
        TextBox newTextBox1 = new TextBox();
        TextBox newLabel1 = new TextBox();
        TextBox newTextBox2 = new TextBox();
        TextBox newLabel2 = new TextBox();
        TextBox newTextBox3 = new TextBox();
        TextBox newTextBox4 = new TextBox();
        TextBox newLabel3 = new TextBox();

        ListBox listbox = new ListBox();
        ListBox listbox1 = new ListBox();

        Button newButton = new Button();
        Button newButton1 = new Button();

        SqlConnection sqlConnection;
       
        DispatcherTimer timer;
        double panelwidh;
        bool hedden;

        
        public MainWindow()
        {
            string conectionstring = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\pavel\Desktop\тестовое задание\bot\bot\Database1.mdf;Integrated Security=True";
            InitializeComponent();
            timer = new DispatcherTimer();
            newButton1.Name = "sentbtn";
            timer.Interval = new TimeSpan(0,0,0,0,10);
            timer.Tick += Timer_Tick;
            panelwidh = sidepanel.Width;
            sqlConnection = new SqlConnection(conectionstring);
            //newsql = new SqlConnection(conectionstring);
            sqlConnection.Open();
            //newsql.Open();


        }

       

        private void Timer_Tick(object sender, EventArgs e)
        {
            if(hedden)
            {
                sidepanel.Width += 1;
                if(sidepanel.Width>=panelwidh)
                {
                    timer.Stop();
                    hedden = false;
                }
            }
            else
            {
                sidepanel.Width -= 1;
                if (sidepanel.Width <= 30)
                {
                    timer.Stop();
                    hedden = true;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            timer.Start();
        }

        private void panel_header_mousedown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton==MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            clear_all();
            double[] mas = {0, 0, 0, 0 };
            double[] mas1 = {350, 70, 0, 0 };
            double[] mas2 = { 0, 70, 0, 0 };
            double[] mas3 = {0, 300, 0, 0 };
            double[] mas4 = { 0, 0, 0, 0 };
            double[] mas5 = { 360, 0, 0, 0 };
            double[] mas6 = { 0, 120, 0, 0 };
            double[] mas7 = { 360, 120, 0, 0 };

            set_text_box_position(newTextBox, 270, 50, HorizontalAlignment.Right, VerticalAlignment.Top, 20, "", mas);
            set_text_box_position(newLabel, 270, 50, HorizontalAlignment.Left, VerticalAlignment.Top, 20, "Введите пункт отправления", mas);

            set_text_box_position(newTextBox1, 270, 50, HorizontalAlignment.Right, VerticalAlignment.Top, 20, "", mas1);
            set_text_box_position(newLabel1, 270, 50, HorizontalAlignment.Left, VerticalAlignment.Top, 20, "Введите пункт прибытия", mas2);

            set_text_box_position(newLabel2, 270, 50, HorizontalAlignment.Left, VerticalAlignment.Center, 20, "Введите цену билета", mas4);
            set_text_box_position(newTextBox2, 270, 50, HorizontalAlignment.Left, VerticalAlignment.Center, 20, "", mas5);

            set_text_box_position(newLabel3, 270, 50, HorizontalAlignment.Left, VerticalAlignment.Center, 20, "Введите код поезда", mas6);
            set_text_box_position(newTextBox3, 270, 50, HorizontalAlignment.Left, VerticalAlignment.Center, 20, "", mas7);

            set_button_position(newButton, 170, 50, HorizontalAlignment.Left, VerticalAlignment.Center, 20, "INSERT", mas3);

            newButton.Click += new RoutedEventHandler(newButton_Click);
        }

        private void newButton_Click(object sender, RoutedEventArgs e)
        {
           
            train tr = new train(newTextBox.Text, newTextBox1.Text, Convert.ToInt32(newTextBox2.Text), Convert.ToInt32(newTextBox3.Text));
            SqlCommand command = new SqlCommand($"INSERT INTO [trains] (Start, Finish, Cost, Number) VALUES (N'{tr.get_strart()}', N'{tr.get_finish()}', N'{tr.get_cost()}',N'{tr.get_code()}')", sqlConnection);
            command.ExecuteNonQuery();
            newTextBox.Text = string.Empty;
            newTextBox1.Text = string.Empty;
            newTextBox2.Text = string.Empty;
            newTextBox3.Text = string.Empty;
            newTextBox4.Text = string.Empty;

        }

        private  void set_text_box_position(TextBox a, double widh, double heigh, HorizontalAlignment horizontal, VerticalAlignment vertical, double Fsize, string text, double[] mas)
        {
            a.Width = widh;
            a.Height = heigh;
            a.HorizontalAlignment = horizontal;
            a.VerticalAlignment = vertical;
            a.FontSize = Fsize;
            a.Margin = new Thickness(mas[0], mas[1], mas[2], mas[3]);
            a.Text = text;
            newpanel.Children.Add(a);
        }

        private  void set_button_position(Button a, double widh, double heigh, HorizontalAlignment horizontal, VerticalAlignment vertical, double Fsize, string text, double[] mas)
        {
            a.Width = widh;
            a.Height = heigh;
            a.HorizontalAlignment = horizontal;
            a.VerticalAlignment = vertical;
            a.FontSize = Fsize;
            a.Margin = new Thickness(mas[0], mas[1], mas[2], mas[3]);
            a.Content = text;
            newpanel.Children.Add(a);
        }


        private  void set_lisbox_position(ListBox a, double widh, double heigh, HorizontalAlignment horizontal, VerticalAlignment vertical)
        {
            a.Width = widh;
            a.Height = heigh;
            a.HorizontalAlignment = horizontal;
            a.VerticalAlignment = vertical;
            newpanel.Children.Add(a);
        }


        private  void clear_all()
        {
            newpanel.Children.Remove(newTextBox);
            newpanel.Children.Remove(newTextBox1);
            newpanel.Children.Remove(newTextBox2); 
            newpanel.Children.Remove(newTextBox3);
            newpanel.Children.Remove(newTextBox4);

            newpanel.Children.Remove(newLabel);
            newpanel.Children.Remove(newLabel1);
            newpanel.Children.Remove(newLabel2);
            newpanel.Children.Remove(newLabel3);

            newpanel.Children.Remove(newButton);
            newpanel.Children.Remove(newButton1);

            newpanel.Children.Remove(listbox);
            newpanel.Children.Remove(listbox1);
        }


        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var DataGR = new DataGrid();


            newpanel.Children.Add(DataGR);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Window1 window1 = new Window1();
            window1.Show();
            
        }



        
    }
   
   
}

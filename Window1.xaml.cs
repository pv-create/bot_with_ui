using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Configuration;
using System.Data;
using System.Runtime.CompilerServices;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Telegram.Bot;
using Telegram.Bot.Args;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text.RegularExpressions;

namespace bot
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
   

    public partial class Window1 : Window
    {
        TelegramBotClient bot;
        ObservableCollection<TelegramUser> Users;//список пользователей
        string start;
        string finish;
        string full;
        SqlConnection sqlConnection;
        SqlConnection newsql;
        public Window1()
        {
            InitializeComponent();
            string conectionstring = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\pavel\Desktop\тестовое задание\bot\bot\Database1.mdf;Integrated Security=True";
            sqlConnection = new SqlConnection(conectionstring);
            //newsql = new SqlConnection(conectionstring);
            sqlConnection.Open();
            //newsql.Open();
            Users = new ObservableCollection<TelegramUser>();
            users.ItemsSource = Users; 
            string token = "1840149361:AAF3A8XrmhF-6dIkx9gmi86VPafKkIBvjPg";
            bot = new TelegramBotClient(token); 
            bot.OnMessage +=  delegate (object sender, Telegram.Bot.Args.MessageEventArgs e)
            {  
                string msg = $"{DateTime.Now}: {e.Message.Chat.FirstName} {e.Message.Chat.Id} {e.Message.Text}";
                this.Dispatcher.Invoke(() =>
                {
                    var person = new TelegramUser(e.Message.Chat.FirstName, e.Message.Chat.Id);
                    if (!Users.Contains(person)) Users.Add(person);
                    Users[Users.IndexOf(person)].AddMessage($"{person.Nick}: {e.Message.Text}");
                    string[] mass = e.Message.Text.Split(' ');
                    if(mass.Length==1)
                    {
                        start = mass[0];
                        start.ToLower();
                        string query1 = $"SELECT * FROM trains WHERE Start LIKE N'{start}'";
                        using (SqlConnection newsql = new SqlConnection(conectionstring))
                        {
                            SqlCommand command = new SqlCommand(query1, newsql);
                            newsql.Open();
                            SqlDataReader reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                string answer = $"{reader[1].ToString()} {reader[2].ToString()} {reader[3].ToString()} {reader[4].ToString()}";
                                bot.SendTextMessageAsync(e.Message.Chat.Id, answer);
                                reader[0].ToString();
                            }
                            reader.Close();
                        }
                    }
                    if (mass.Length == 2)
                    {
                        start = mass[0];
                        finish = mass[1];
                        start.ToLower();
                        finish.ToLower();
                        string query1 = $"SELECT * FROM trains WHERE Start LIKE N'{start}' AND Finish LIKE N'{finish}'";
                        using (SqlConnection newsql=new SqlConnection(conectionstring))
                        {
                            SqlCommand command =new SqlCommand(query1, newsql);
                            newsql.Open();
                            SqlDataReader reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                string answer = $"{reader[1].ToString()} {reader[2].ToString()} {reader[3].ToString()} {reader[4].ToString()}";
                                bot.SendTextMessageAsync(e.Message.Chat.Id, answer);
                               reader[0].ToString();
                            }
                            reader.Close();
                        }
                           
                    }
                });
            };

            bot.StartReceiving();
            bot.StartReceiving();
            sendMsg.Click += delegate { SendMsg(); };
            txtMsg.KeyDown += (s, e) => { if (e.Key == Key.Return) { SendMsg(); } };
        }

        public void SendMsg()
        {
            //SqlDataAdapter sqlDataAdapter = new SqlDataAdapter($"SELECT * FROM trains WHERE Start LIKE {start} AND Finish LIKE {finish}", sqlConnection);

            var concretUser = Users[Users.IndexOf(users.SelectedItem as TelegramUser)];
            
            string responceMsg = $"Support: {txtMsg.Text}";
            concretUser.Messages.Add(responceMsg);
            bot.SendTextMessageAsync(concretUser.Id, txtMsg.Text);
            txtMsg.Text = string.Empty;
        }
    }
}

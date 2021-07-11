using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text.RegularExpressions;

namespace bot
{
    /// <summary>
    /// Модель пользователя
    /// </summary>
    class TelegramUser : INotifyPropertyChanged, IEquatable<TelegramUser>
    {
        ///<summary>
        ///Инициализация
        ///<param name="Nickname">Firstname</param>
        ///<param name="ChatId">Id</param>
        public TelegramUser(string Nickname, long ChatId)
        {
            this.nick = Nickname;
            this.id = ChatId;
            Messages = new ObservableCollection <string>();
        }

        private string nick;
        public string Nick
        {
            get { return this.nick;}
            set
            {
                this.nick = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Nick)));
            }
        }
        private long id;
        public long Id
        {
            get { return this.id; }
            set
            {
                this.id = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Id)));

            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        ///<summary>
        ///метод сравнениния
        ///</summary>
        /// </param> name="other"</param>
        /// <returns></returns> 

        public bool Equals(TelegramUser other) => other.Id == this.id;

        public ObservableCollection <string> Messages { get; set; }

        ///<summary>
        ///все сообщения
        ///</summary>
        ///<param name="Text"> текст сообщения </param>
        public void AddMessage(string Text) => Messages.Add(Text);


    }
}

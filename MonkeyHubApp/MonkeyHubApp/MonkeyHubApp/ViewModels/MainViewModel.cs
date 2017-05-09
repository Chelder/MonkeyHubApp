using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyHubApp.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Descricao { get; set; }

        public MainViewModel()
        {
            Descricao = "Ola mundo! Eu estou aqui!";

            Task.Delay(3000).ContinueWith(t =>
            {
                Descricao = "Meu texto Mudou";
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Descricao)));
            });
        }
    }
}

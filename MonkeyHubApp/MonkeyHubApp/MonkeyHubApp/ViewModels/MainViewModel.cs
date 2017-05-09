using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyHubApp.ViewModels
{
    public class MainViewModel
    {
        public string Descricao { get; set; }

        public MainViewModel()
        {
            Descricao = "Ola mundo! Eu estou aqui!";
        }
    }
}

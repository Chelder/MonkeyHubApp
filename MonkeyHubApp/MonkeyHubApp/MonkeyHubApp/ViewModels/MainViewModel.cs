
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MonkeyHubApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private string _searchTerm;

        public string SearchTerm
        {
            get { return _searchTerm; }
            set
            {
                if (SetProperty(ref _searchTerm, value))
                {
                    SearhCommand.ChangeCanExecute();
                }
            }
        }

        public ObservableCollection<string> Resultados { get; }

        public Command SearhCommand { get; }

        public MainViewModel()
        {
            SearhCommand = new Command(ExecuteSearchCommand, CanExecuteSearchCommand);

            Resultados = new ObservableCollection<string>(new[] { "abc", "cde", "1", "2", "3", "4" });
        }

        async void ExecuteSearchCommand()
        {
            //await Task.Delay(2000);

            //Forma errada de utilizar o DisplayAlert
            bool res = await App.Current.MainPage.DisplayAlert("MonkeyHubApp", $"Você pesquisou por '{SearchTerm}'?", "Sim", "Não");

            if (res)
            {
                //Forma errada de utilizar o DisplayAlert
                await App.Current.MainPage.DisplayAlert("MonkeyHubApp", "Obrigado", "Ok");
                Resultados.Add(SearchTerm);
            }
            else
            {
                //Forma errada de utilizar o DisplayAlert
                await App.Current.MainPage.DisplayAlert("MonkeyHubApp", "De nada", "Ok");
            }
        }

        private bool CanExecuteSearchCommand()
        {
            return !string.IsNullOrWhiteSpace(SearchTerm);
        }
    }
}


using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Net.Http;
using MonkeyHubApp.Services;
using MonkeyHubApp.Models;
using System;

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

        public ObservableCollection<Tag> Resultados { get; }

        public Command SearhCommand { get; }
        public Command AboutCommand { get; }
        public Command<Tag> ShowCategoriaCommand { get; }

        private readonly IMonkeyHubApiService _monkeyHubApiService;

        public MainViewModel(IMonkeyHubApiService monkeyHubApiService)
        {
            _monkeyHubApiService = monkeyHubApiService;

            SearhCommand = new Command(ExecuteSearchCommand, CanExecuteSearchCommand);

            AboutCommand = new Command(ExecuteAboutCommand);

            ShowCategoriaCommand = new Command<Tag>(ExecuteShowCategoriaCommand);

            Resultados = new ObservableCollection<Tag>();
        }

        private async void ExecuteShowCategoriaCommand(Tag tag)
        {
            await PushAsync<CategoriaViewModel>(_monkeyHubApiService, tag);
        }

        async void ExecuteAboutCommand()
        {
            await PushAsync<AboutViewModel>();
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

                var tagsRetornadasDoServico = await _monkeyHubApiService.GetTagsAsync();

                Resultados.Clear();

                if (tagsRetornadasDoServico != null)
                {
                    foreach (var tag in tagsRetornadasDoServico)
                    {
                        Resultados.Add(tag);
                    }
                }
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

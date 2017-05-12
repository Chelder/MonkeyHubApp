
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Collections.Generic;
using MonkeyHubApp.Model;

namespace MonkeyHubApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private const string BaseUrl = "https://monkey-hub-api.azurewebsites.net/api/";

        public async Task<List<Tag>> GetTagsAsync()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await httpClient.GetAsync($"{BaseUrl}Tags").ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                {
                    return JsonConvert.DeserializeObject<List<Tag>>(
                        await new StreamReader(responseStream)
                            .ReadToEndAsync().ConfigureAwait(false));
                }
            }

            return null;
        }

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

        public MainViewModel()
        {
            SearhCommand = new Command(ExecuteSearchCommand, CanExecuteSearchCommand);

            Resultados = new ObservableCollection<Tag>();
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

                var tagsRetornadasDoServico = await GetTagsAsync();

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

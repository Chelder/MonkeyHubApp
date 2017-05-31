
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Net.Http;
using MonkeyHubApp.Services;
using MonkeyHubApp.Models;
using System;
using System.Threading.Tasks;

namespace MonkeyHubApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IMonkeyHubApiService _monkeyHubApiService;
        public ObservableCollection<Tag> Tags { get; }

        public Command AboutCommand { get; }

        public Command SearchCommand { get; }

        public Command<Tag> ShowCategoriaCommand { get; }

        public MainViewModel(IMonkeyHubApiService monkeyHubApiService)
        {
            _monkeyHubApiService = monkeyHubApiService;
            Tags = new ObservableCollection<Tag>();
            //AboutCommand = new Command(ExecuteAboutCommand);
            //SearchCommand = new Command(ExecuteSearchCommand);
            ShowCategoriaCommand = new Command<Tag>(ExecuteShowCategoriaCommand);
        }

        //private async void ExecuteSearchCommand()
        //{
        //    //await PushAsync<SearchViewModel>();
        //}

        private async void ExecuteShowCategoriaCommand(Tag tag)
        {
            await PushAsync<CategoriaViewModel>(tag);
        }

        private async void ExecuteAboutCommand()
        {
            await PushAsync<AboutViewModel>();
        }

        public async Task LoadAsync()
        {
            var tags = await _monkeyHubApiService.GetTagsAsync();

            Tags.Clear();
            foreach (var tag in tags)
            {
                Tags.Add(tag);
            }
        }
    }
}

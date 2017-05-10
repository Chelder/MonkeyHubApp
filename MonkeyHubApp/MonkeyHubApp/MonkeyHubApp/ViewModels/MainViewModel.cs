
using System;
using System.Diagnostics;
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

        public Command SearhCommand { get; }

        public MainViewModel()
        {
            SearhCommand = new Command(ExecuteSearchCommand, CanExecuteSearchCommand);
        }

        private bool CanExecuteSearchCommand()
        {
            return !string.IsNullOrWhiteSpace(SearchTerm);
        }

        void ExecuteSearchCommand()
        {
            Debug.WriteLine($"Clicou no botão! {DateTime.Now}");
        }
    }
}

using GoogleAutoCompleteApp.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;

namespace GoogleAutoCompleteApp
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _searchText;
        private string _selectedItem;

        public MainViewModel()
        {

        }
        public IList<string> SuggestionsList { get; private set; } = new ObservableCollection<string>();
        public string SelectedItem { get => _selectedItem; set { _selectedItem = value; OnPropertyChanged(); SuggestionSelected(); } }
        public string SearchText { get => _searchText; set { _searchText = value; OnPropertyChanged(); UpdateSuggestions(); } }

        private void SuggestionSelected()
        {
            if (SelectedItem == null) return;
            SearchText = SelectedItem;
            SelectedItem = null;
        }

        public async void UpdateSuggestions()
        {
            var response = await SearchHelper.Search(SearchText, CultureInfo.CurrentCulture.TwoLetterISOLanguageName);
            if (response != null)
            {
                SuggestionsList.Clear();
                foreach (var item in response)
                    SuggestionsList.Add(item);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName]string propName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}

using GoogleAutoCompleteApp.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace GoogleAutoCompleteApp
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _searchText;
        private string _selectedItem;
        private IList<string> _suggestionsList;

        public MainViewModel()
        {

        }
        public IList<string> SuggestionsList { get => _suggestionsList; private set { _suggestionsList = value; OnPropertyChanged(); } }
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
            var suggestions = await SearchHelper.Search(SearchText, CultureInfo.CurrentCulture.TwoLetterISOLanguageName);
            if (suggestions != null)
            {
                SuggestionsList = suggestions.ToList();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName]string propName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}

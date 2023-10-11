using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Globalization;

namespace Mytaskapp.ViewModel
{
    [QueryProperty("Text","Text")]
    public partial class DetailViewModel : ObservableObject
    {
        [ObservableProperty]
        string text;

        [RelayCommand]
        async Task GoBack() 
        {
            await Shell.Current.GoToAsync("..");        
        }
    }
     
}

using Mytaskapp.ViewModel;

namespace Mytaskapp;

public partial class DetailPage : ContentPage
{
	public DetailPage(DetailViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}
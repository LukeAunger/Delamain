using delamain_maui.ViewModel;

namespace delamain_maui.Views;

public partial class UserPreferences : ContentPage
{
	public UserPreferences()
	{
		InitializeComponent();
        BindingContext = new UserPreferencesViewModel();
    }

    //Ageslider
    void Slider_ValueChanged_1(System.Object sender, Microsoft.Maui.Controls.ValueChangedEventArgs e)
    {
        int num = (int)ageslider.Value;
        agesliderlabel.Text = num.ToString();
    }

    //BMI slider
    void Slider_ValueChanged(System.Object sender, Microsoft.Maui.Controls.ValueChangedEventArgs e)
    {
        int num = (int)bmislider.Value;
        bmisliderlabel.Text = num.ToString();
    }
}

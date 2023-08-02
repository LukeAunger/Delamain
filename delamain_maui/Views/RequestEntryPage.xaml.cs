using delamain_maui.Models;
using delamain_maui.Data;
using System;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Maui;
using delamain_maui.ViewModel;

namespace delamain_maui.Views;

public partial class RequestEntryPage : ContentPage
{
    //runs when the view is opened which instantiates the database and sets bindingContext to new patient_call class item
    public RequestEntryPage()
    {
        InitializeComponent();
        BindingContext = new RequestEntryPageViewModel();
    }

    void RespiratorySlider_ValueChanged(System.Object sender, Microsoft.Maui.Controls.ValueChangedEventArgs e)
    {
        int num = (int)RespiratorySlider.Value;
        Respiratoryvalue.Text = num.ToString();
    }

    void TempSlider_ValueChanged(System.Object sender, Microsoft.Maui.Controls.ValueChangedEventArgs e)
    {
        int num = (int)TempSlider.Value;
        Tempvalue.Text = num.ToString();
    }

    void heartSlider_ValueChanged_1(System.Object sender, Microsoft.Maui.Controls.ValueChangedEventArgs e)
    {
        int num = (int)heartSlider.Value;
        heartvalue.Text = num.ToString();
    }
}

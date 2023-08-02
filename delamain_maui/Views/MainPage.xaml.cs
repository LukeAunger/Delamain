using System.Collections.ObjectModel;
using delamain_maui.Data;
using delamain_maui.Models;
using delamain_maui.ViewModel;
using delamain_maui.Views;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Maui.Controls;

namespace delamain_maui.Views;

public partial class MainPage : ContentPage
{

    //setting database set to detailsDatabase type
    detailsDatabase database;

    public ObservableCollection<patient_call> Items { get; set; } = new();

    //creates a new instance of database
    public MainPage(detailsDatabase DetailsDatabase)
	{
        InitializeComponent();
        database = DetailsDatabase;
        BindingContext = this;


    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var list = await database.GetItemsAsync();
        list.Reverse();
        display.ItemsSource = list;
    }

    //Sends user to status view with the parameter set to the binding context of the page item
    private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection != null)
        {
            // Navigate to the NoteEntryPage, passing the ID as a query parameter.
            patient_call note = (patient_call)e.CurrentSelection.FirstOrDefault();

            await Shell.Current.GoToAsync($"{nameof(StatusPage)}?{nameof(StatusPage.ItemId)}={note.Id.ToString()}");
        }
    }

    //when the add button method is called function directs user to the RequestEntryPage
    async void OnAddClicked(object sender, EventArgs e)
    {
        // Navigate to the NoteEntryPage, without passing any data.
        await Shell.Current.GoToAsync(nameof(RequestEntryPage));
    }
}



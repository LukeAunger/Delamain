using Newtonsoft.Json;

namespace delamain_maui.Views;
[QueryProperty(nameof(ItemId), nameof(ItemId))]
//This code stores a patient_call instance, which represents a single patient_call, in the BindingContext of the page.
//The class is decorated with a QueryPropertyAttribute that enables data to be passed into the page, during navigation,
//via query parameters.The first argument for the QueryPropertyAttribute specifies the name of the property that will receive the data,
//with the second argument specifying the query parameter id.
//Therefore, the QueryParameterAttribute in the above code specifies that the ItemId property will receive
//the data passed in the ItemId query parameter from the URI specified in a GoToAsync method call.
//The ItemId property then calls the LoadCall method to create a Note object from the file on the device,
//and sets the BindingContext of the page to the Note object.

public partial class StatusPage : ContentPage
{
    //Injecting the local db
    detailsDatabase database;
    //Injecting the HubConnetion libraries
    ServerConnection _server;

    public int ItemId
    {
        set
        {
            LoadCase(value);
            traffic(value);
        }
    }

    public StatusPage(detailsDatabase detailsDatabase, ServerConnection serverConnection)
	{
		InitializeComponent();

        BindingContext = new patient_call();
        var call = BindingContext;
        database = detailsDatabase;
        _server = serverConnection;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        GetLocation();
        PinHospitals();
    }

    public async void GetLocation()
    {
        var georesult = await Geolocation.GetLocationAsync(new
        GeolocationRequest(GeolocationAccuracy.Default, TimeSpan.FromMinutes(1)));

        if(georesult != null)
        {
            Position place = new Position(georesult.Latitude, georesult.Longitude);
            MapSpan mapSpan = MapSpan.FromCenterAndRadius(place, Distance.FromKilometers(.444));
            myMap.MoveToRegion(mapSpan);
        }
    }

    public void PinHospitals()
    {
        var hosp = _server.gethosptials();
        if (hosp != "Connection to internet failed backout and try again")
        {
            List<HospitalLocations> hospitalLocations = JsonConvert.DeserializeObject<List<HospitalLocations>>(hosp);

            foreach (var item in hospitalLocations)
            {
                myMap.Pins.Add(new Pin
                {
                    Label = item.name,
                    Position = new Position(item.lat, item.lng)
                });
            }
        }
        //else Console.WriteLine("FailedConnection");
    }

    async void LoadCase(int itemId)
    {
        try
        {
            // Retrieve the note and set it as the BindingContext of the page.
            patient_call note = await database.GetItemAsync(itemId);
            BindingContext = note;
        }
        catch (Exception)
        {
            Console.WriteLine("Failed to load note.");
        }
    }

    async void traffic(int itemId)
    {
        patient_call note = await database.GetItemAsync(itemId);
        var testconnection = _server.testConnect();
        if(testconnection == "running")
        {
            var queue = await _server.Connect(note.userReqid);
            if(queue == "")
            {
                order.Text = "You may have been removed from the queue. This is either because either youve been seen or there was an error. If you have just made the request give it time and reload the page";
            } else
            {
                var num = int.Parse(queue);
                if (num <= 0)
                {
                    Trafficlight.TextColor = Colors.Green;
                }
                else if (num <= 10)
                {
                    Trafficlight.TextColor = Colors.Yellow;
                }
                else if (num <= 25)
                {
                    Trafficlight.TextColor = Colors.Red;
                }
                else if (queue == null)
                {
                    Trafficlight.Text = "No value available at this time.";
                }
            }
        } else
        {
            order.Text = "No value available at this time.";
        }
    }
}

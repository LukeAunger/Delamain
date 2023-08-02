using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Controls;

namespace delamain_maui.ViewModel
{
	public class RequestEntryPageViewModel : INotifyPropertyChanged
	{
        public string Symptoms { get; set; }
		public string heartvalue { get; set; }
		public string Respiratoryvalue { get; set; }
		public string Tempvalue { get; set; }

        public RequestEntryPageViewModel()
		{
            SaveRequestCommand = new Command(() =>
			{
                create();
				OnPropertyChanged(nameof(patient_call));
            });
		}

        public string Error { get; set; }

        public async Task<patient_call> make()
        {
            patient_call call = new patient_call();

            //Code that uses the geolocation to add an entry that contains the patients location
            var georesult = await Geolocation.GetLocationAsync(new
            GeolocationRequest(GeolocationAccuracy.Default, TimeSpan.FromMinutes(1)));//creating new instance of Geolocation
            var resultLocation = $"lat: {georesult.Latitude}, lng: {georesult.Longitude}";

            //Using Geocoding to change the location display from coordinates to an address.
            var result = await Geocoding.GetPlacemarksAsync(georesult.Latitude, georesult.Longitude);
            resultLocation = result.FirstOrDefault()?.FeatureName;



            if (heartvalue == null)
            {
                heartvalue = "60";
            }
            if (Respiratoryvalue == null)
            {
                Respiratoryvalue = "12";
            }
            if (Tempvalue == null)
            {
                Tempvalue = "37";
            }

            var bmi = int.Parse(Preferences.Get("bmisliderlabel", "25"));
            var age = int.Parse(Preferences.Get("agesliderlabel", "0"));
            var male = Preferences.Get("sexmale", false);
            var female = Preferences.Get("sexfemale", false);
            if (male == true)
            {
                call.gender = "male";
            }
            else if (female == true)
            {
                call.gender = "female";
            }

            call.Symptoms = Symptoms;
            call.name = Preferences.Get("username", "No name");
            call.phone = Preferences.Get("phone", "No number");
            call.BMI = bmi;
            call.age = age;
            call.heart_rate = int.Parse(heartvalue);
            call.respitory_rate = int.Parse(Respiratoryvalue);
            call.tempurature = int.Parse(Tempvalue);
            call.diabetes = Preferences.Get("diabetes", false);
            call.deficiencyanemias = Preferences.Get("lowiron", false);
            call.hypertensive = Preferences.Get("highblood", false);
            call.hyperlipemia = Preferences.Get("cholestoral", false);
            call.atrialfibrilation = Preferences.Get("atrial", false);
            call.CHD_with_no_MI = Preferences.Get("CHD", false);
            call.COPD = Preferences.Get("lungdisease", false);
            call.depression = Preferences.Get("depression", false);
            call.date = DateTime.UtcNow;
            call.geoloc = resultLocation;
            return (call);
        }

        public async void create()
		{
            Error = "";
            detailsDatabase _database = new detailsDatabase();
            ServerConnection _Server = new ServerConnection();
            var call = await make();
            if (!string.IsNullOrWhiteSpace(call.Symptoms))
            {
                if (call.name != "No name" && !string.IsNullOrWhiteSpace(call.name))
                {
                    if (call.phone != "No number" && !string.IsNullOrWhiteSpace(call.phone))
                    {
                        var key = await _Server.call(call);
                        if (key != null)
                        {
                            call.userReqid = key;
                            await _database.SaveItemAsync(call);
                            await Shell.Current.GoToAsync("..");
                        }
                        else await Shell.Current.GoToAsync("..");
                    }
                    else Error += ("Fill in your Phone number in preferences so your identifiable on arrival! " + Environment.NewLine);
                    OnPropertyChanged(nameof(Error));
                }
                else
                {
                    Error += ("Fill in your name in preferences so your identifiable on arrival! " + Environment.NewLine);
                    OnPropertyChanged(nameof(Error));
                }
            }
        }

        public Command SaveRequestCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

		public void OnPropertyChanged([CallerMemberName] string name = null) =>
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}


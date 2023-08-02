using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Android.Telecom;

namespace delamain_maui.ViewModel
{
	public class UserPreferencesViewModel : INotifyPropertyChanged
    {
        public Command Button_Clicked { get; set; }
        public Command Button_Clicked_1 { get; set; }

        public UserPreferencesViewModel()
		{
            Button_Clicked_1 = new Command(() =>
            {
                clearPreferences();
                OnPropertyChanged(nameof(username));
                OnPropertyChanged(nameof(agesliderlabel));
                OnPropertyChanged(nameof(sexmale));
                OnPropertyChanged(nameof(sexfemale));
                OnPropertyChanged(nameof(bmisliderlabel));
                OnPropertyChanged(nameof(diabetes));
                OnPropertyChanged(nameof(lowiron));
                OnPropertyChanged(nameof(atrial));
                OnPropertyChanged(nameof(highblood));
                OnPropertyChanged(nameof(cholestoral));
                OnPropertyChanged(nameof(CHD));
                OnPropertyChanged(nameof(lungdisease));
                OnPropertyChanged(nameof(depression));
                OnPropertyChanged(nameof(phone));
            });
            getPreferences();
            Button_Clicked = new Command(() =>
            {
                OnPropertyChanged(nameof(username));
                OnPropertyChanged(nameof(agesliderlabel));
                OnPropertyChanged(nameof(bmisliderlabel));
                OnPropertyChanged(nameof(phone));
                OnPropertyChanged(nameof(Error));
                setPreferences();
            });
        }

        public string agesliderlabel { get; set; }
        public string username { get; set; }
        public bool sexmale { get; set; }
        public bool sexfemale { get; set; }
        public string bmisliderlabel { get; set; }
        public bool diabetes { get; set; }
        public bool lowiron { get; set; }
        public bool atrial { get; set; }
        public bool highblood { get; set; }
        public bool cholestoral { get; set; }
        public bool CHD { get; set; }
        public bool lungdisease { get; set; }
        public bool depression { get; set; }
        public string phone { get; set; }

        public string Error { get; set; }

        public void getPreferences()
		{
            username = Preferences.Get("username", string.Empty);
            sexmale = Preferences.Get("sexmale", false);
            sexfemale = Preferences.Get("sexfemale", false);
            agesliderlabel = Preferences.Get("agesliderlabel", string.Empty);
            bmisliderlabel = Preferences.Get("bmisliderlabel", string.Empty);
            diabetes = Preferences.Get("diabetes", false);
            lowiron = Preferences.Get("lowiron", false);
            atrial = Preferences.Get("atrial", false);
            highblood = Preferences.Get("highblood", false);
            cholestoral = Preferences.Get("cholestoral", false);
            CHD = Preferences.Get("CHD", false);
            lungdisease = Preferences.Get("lungdisease", false);
            depression = Preferences.Get("depression", false);
            phone = Preferences.Get("phone", string.Empty);
        }

        public void setPreferences()
        {
            var containsInt = false;
            Error = "";
            if (!string.IsNullOrWhiteSpace(username))
            {
                containsInt = username.Any(char.IsDigit);
                if (containsInt == true)
                {
                    username = null;
                    Error += ("username cannot have numbers" + Environment.NewLine);
                }
                else
                {
                    Preferences.Set("username", username);
                }
            }
            else
            {
                username = null;
                Error += ("fill in username" + Environment.NewLine);
            }
            if(sexmale == true && sexfemale == true)
            {
                sexmale = false;
                sexfemale = false;
                Error += ("Pick your biological sex." + Environment.NewLine);
                OnPropertyChanged(nameof(Error));
                OnPropertyChanged(nameof(sexfemale));
                OnPropertyChanged(nameof(sexmale));
            }
            else
            {
                Preferences.Set("sexmale", sexmale);
                Preferences.Set("sexfemal", sexfemale);
            }
            if (!string.IsNullOrWhiteSpace(agesliderlabel))
            {
                containsInt = agesliderlabel.Any(char.IsLetter);
                if (containsInt == true)
                {
                    agesliderlabel = null;
                    Error += ("age cannot have letters" + Environment.NewLine);
                }
                else
                {
                    Preferences.Set("agesliderlabel", agesliderlabel);
                }
            }
            else
            {
                agesliderlabel = null;
                Error += ("fill in age" + Environment.NewLine);
                OnPropertyChanged(nameof(Error));
            }


            if (!string.IsNullOrWhiteSpace(bmisliderlabel))
            {
                containsInt = bmisliderlabel.Any(char.IsLetter);
                if (containsInt == true)
                {
                    bmisliderlabel = null;
                    Error += ("bmi cannot have letters" + Environment.NewLine);
                    OnPropertyChanged(nameof(Error));
                }
                else
                {
                    Preferences.Set("bmisliderlabel", bmisliderlabel);
                }
            }
            else
            {
                bmisliderlabel = null;
                Error += ("fill in bmi" + Environment.NewLine);
                OnPropertyChanged(nameof(Error));
            }
            Preferences.Set("diabetes", diabetes);
            Preferences.Set("lowiron", lowiron);
            Preferences.Set("atrial", atrial);
            Preferences.Set("highblood", highblood);
            Preferences.Set("cholestoral", cholestoral);
            Preferences.Set("CHD", CHD);
            Preferences.Set("lungdisease", lungdisease);
            Preferences.Set("depression", depression);


            if (!string.IsNullOrWhiteSpace(phone))
            {
                containsInt = phone.Any(char.IsLetter);
                if (containsInt == true)
                {
                    phone = null;
                    Error += ("phone cannot have letters" + Environment.NewLine);
                }
                else
                {
                    Preferences.Set("phone", phone);
                }
            }
            else
            {
                Error += ("fill in phone" + Environment.NewLine);
                OnPropertyChanged(nameof(Error));
                phone = null;
            }
        }
        public void clearPreferences()
        {
            Preferences.Clear();
            username = string.Empty;
            sexmale = false;
            sexfemale = false;
            agesliderlabel = string.Empty;
            bmisliderlabel = string.Empty;
            diabetes = false;
            lowiron = false;
            atrial = false;
            highblood = false;
            cholestoral = false;
            CHD = false;
            lungdisease = false;
            depression = false;
            phone = string.Empty;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}


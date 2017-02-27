using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace XamarinWebAPISample
{
    public partial class DetailPage : ContentPage
    {
        private bool _insertMode;
        private readonly Person _person;

        public DetailPage()
        {
            InitializeComponent();

            DeleteButton.Clicked += DeleteButton_Clicked;
            OkButton.Clicked += OkButton_Clicked;

            _insertMode = true;
            DeleteButton.IsVisible = false;
            _person = null;
        }

        public DetailPage(Person person) : this()
        {
            Id.Text = person.Id.ToString();
            Name.Text = person.Name;
            Age.Text = person.Age == null ? null : person.Age.ToString();

            _insertMode = false;
            DeleteButton.IsVisible = true;
            _person = person;
        }

        private void OkButton_Clicked(object sender, EventArgs e)
        {
            if (_insertMode)
            {
                Person person = new Person();
                person.Name = Name.Text;
                person.Age = string.IsNullOrWhiteSpace(Age.Text) ? null : (int?) Convert.ToInt32(Age.Text);

                string jsonString = JsonConvert.SerializeObject(person);

                HttpClient client = new HttpClient();
                HttpContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                client.PostAsync(App.BaseApiUrl, content);
            }
            else
            {
                _person.Name = Name.Text;
                _person.Age = string.IsNullOrWhiteSpace(Age.Text) ? null : (int?)Convert.ToInt32(Age.Text);

                string jsonString = JsonConvert.SerializeObject(_person);

                HttpClient client = new HttpClient();
                HttpContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                client.PutAsync(App.BaseApiUrl + "/" + _person.Id, content);
            }

            ((NavigationPage)App.Current.MainPage).PopAsync();

        }

        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert(
                "Conferma eliminazione",
                "Conferma l'eliminazione dell'entità",
                "Sì",
                "No"))
            {
                HttpClient client = new HttpClient();
                await client.DeleteAsync(App.BaseApiUrl + "/" + _person.Id);
                ((NavigationPage)App.Current.MainPage).PopAsync();
            }
        }


    }
}

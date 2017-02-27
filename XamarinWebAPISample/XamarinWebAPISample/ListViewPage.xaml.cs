using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace XamarinWebAPISample
{
    public partial class ListViewPage : ContentPage
    {
        public ListViewPage()
        {
            InitializeComponent();

            MyListView.ItemTapped += MyListView_ItemTapped;
            MyListView.Refreshing += MyListView_Refreshing;
            InsertButton.Clicked += InsertButton_Clicked;
        }

        private async void MyListView_Refreshing(object sender, EventArgs e)
        {
            await RefreshList();
            MyListView.IsRefreshing = false;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            MyListView.IsRefreshing = true;
            await RefreshList();
            MyListView.IsRefreshing = false;
        }

        private async Task RefreshList()
        {
            HttpClient client = new HttpClient();
            string jsonString = await client.GetStringAsync(App.BaseApiUrl);
            List<Person> list = JsonConvert.DeserializeObject<List<Person>>(jsonString);
            client.Dispose();

            MyListView.ItemsSource = list;

        }

        private void MyListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ((NavigationPage)App.Current.MainPage).PushAsync(new DetailPage((Person)e.Item));
        }

        private void InsertButton_Clicked(object sender, EventArgs e)
        {
            ((NavigationPage)App.Current.MainPage).PushAsync(new DetailPage());
        }


    }
}

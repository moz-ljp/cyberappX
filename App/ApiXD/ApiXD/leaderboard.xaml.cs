using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Firebase.Database;
using Firebase.Database.Query;

namespace ApiXD
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class leaderboard : ContentPage
    {

        FirebaseHelper firebaseHelper = new FirebaseHelper();
        public leaderboard()
        {
            InitializeComponent();

            initalise();
        }

        public async void initalise()
        {
            base.OnAppearing();
            var allPersons = await firebaseHelper.GetAllPersons();
            var ordered = allPersons.OrderBy(f => f.Score);
            var orderedProper = ordered.Reverse();
            lstPersons.ItemsSource = orderedProper;
        }

        public async void sortList(List<Person> allPersons)
        {

        }

        public async void backBTN_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

    }
}
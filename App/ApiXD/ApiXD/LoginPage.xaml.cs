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
    public partial class Page1 : ContentPage
    {

        FirebaseHelper firebaseHelper = new FirebaseHelper();

        string username;

        public Page1()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            
            base.OnAppearing();
            var allPersons = await firebaseHelper.GetAllPersons();
            //lstPersons.ItemsSource = allPersons;
            
        }

        private void submitBTNClicked(object sender, EventArgs e)

        {
        }

        private async void BtnRetrieve_Clicked(object sender, EventArgs e)
        {
            var person = await firebaseHelper.GetPerson(txtName.Text, password.Text);
            if (person != null)
            {
                if (person.Password == password.Text)
                {
                    //txtId.Text = person.PersonId.ToString();
                    txtName.Text = person.Name;
                    int score = person.Score;
                    username = txtName.Text.ToString();
                    await DisplayAlert("Success", "Logged in Successfully", "OK");
                    await Navigation.PushModalAsync(new MainPage(username, true, person.Password, score));

                }
                else
                {
                    await DisplayAlert("Failed", "Incorrect Login Information", "Try Again");
                    txtName.Text = string.Empty;
                    password.Text = string.Empty;
                }
            }
            else
            {
                await DisplayAlert("Failed", "Account not found", "Try Again");
                txtName.Text = string.Empty;
                password.Text = string.Empty;
            }

        }

        private async void BtnUpdate_Clicked(object sender, EventArgs e)
        {
            await firebaseHelper.UpdatePerson(txtName.Text, 0, "");
            //txtId.Text = string.Empty;
            txtName.Text = string.Empty;
            await DisplayAlert("Success", "Person Updated Successfully", "OK");
            //var allPersons = await firebaseHelper.GetAllPersons();
            //lstPersons.ItemsSource = allPersons;
        }

        private async void BtnDelete_Clicked(object sender, EventArgs e)
        {
            await firebaseHelper.DeletePerson(txtName.Text);
            await DisplayAlert("Success", "Person Deleted Successfully", "OK");
            //var allPersons = await firebaseHelper.GetAllPersons();
            //lstPersons.ItemsSource = allPersons;
        }

        private async void BtnAdd_Clicked(object sender, EventArgs e)
        {
            await firebaseHelper.AddPerson(txtName.Text, password.Text, 0);
            //txtId.Text = string.Empty;
            txtName.Text = string.Empty;
            password.Text = string.Empty;
            await DisplayAlert("Success", "Account Created", "OK");
            //var allPersons = await firebaseHelper.GetAllPersons();
            //lstPersons.ItemsSource = allPersons;

        }

        private async void backBTN_Clicked(object sender, EventArgs e)
        {

            await Navigation.PopModalAsync();

        }

        private void TextCell_Tapped(object sender, EventArgs e)
        {

        }
    }

}
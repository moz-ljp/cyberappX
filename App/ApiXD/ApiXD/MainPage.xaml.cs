using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json;
using System.Net.Http;
using Firebase.Database;
using Firebase.Database.Query;

namespace ApiXD
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]

    public partial class MainPage : ContentPage
    {

        FirebaseHelper firebaseHelper = new FirebaseHelper();

        string correctAnswer;

        int prev;

        int total;

        int score;

        JArray releasesArray = null;

        bool solved = false;

        bool deuter = false;

        bool trit = false;

        bool prot = false;

        string[] usernames = new string[] { "Color.blue",  };

        int type = 0;

        int count = 0;

        bool masterLogged = false;

        string masterPass = "";

        public string thisusername = "";

        public MainPage(string username, bool loggedIn, string password, int initscore)
        {
            InitializeComponent();
            if(username != null)
            {
                setUser(username);
            }

            masterLogged = loggedIn;
            masterPass = password;
            score = initscore;
            counterLabel.Text = initscore.ToString();
            
        }

        public JArray requestAPI()
        {

            //var request = (HttpWebRequest)WebRequest.Create("https://raw.githubusercontent.com/moz-ljp/cyberappX/master/questionsApi.json"); -request  from github
            var request = (HttpWebRequest)WebRequest.Create("https://firebasestorage.googleapis.com/v0/b/cyberapp-b2823.appspot.com/o/questionsApi.json?alt=media&token=8f4ce7b8-ec37-44e3-8c4d-297001f3b644"); //request from firebase
            //var request = (HttpWebRequest)WebRequest.Create("https://moz-ljp.github.io/cyberappX/questionsApi.json"); -request from github website
            request.Method = "GET";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36";
            request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            var response = (HttpWebResponse)request.GetResponse();
            string content = string.Empty;
            using (var stream = response.GetResponseStream())
            {
                using (var sr = new StreamReader(stream))
                {
                    content = sr.ReadToEnd();
                }
                        
            }

            resultLabel.Text = "";

            var releases = JArray.Parse(content);
            string responsetext = releases.ToString();

            foreach (var x in releases)
            {
                if (x.ToString().Contains("question"))
                {
                    total += 1;
                }
            }


            //responselabel.Text = responsetext;
            return releases;
            

        }

        /*
        private void callBTNClicked(object sender, EventArgs e)
        {
            var request = (HttpWebRequest)WebRequest.Create("https://foaas.com/operations");
            request.Method = "GET";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36";
            request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            var response = (HttpWebResponse)request.GetResponse();
            string content = string.Empty;
            using (var stream = response.GetResponseStream())
            {
                using (var sr = new StreamReader(stream))
                {
                    content = sr.ReadToEnd();
                }

            }
            var releases = JArray.Parse(content);
            string responsetext = releases.ToString();

    */

        async void btnOneClicked(object sender, EventArgs e)
        {
            string thisanswer = btnOne.Text;

            if (solved == false)
            {

                if (thisanswer == correctAnswer)
                {
                    resultLabel.Text = "Correct";
                    if (deuter == true)
                    {
                        resultLabel.TextColor = Color.Cyan;
                    }
                    else
                    {
                        resultLabel.TextColor = Color.Lime;

                    }
                    score += 1;
                    incScore();
                    counterLabel.Text = score.ToString();
                    await counterLabel.TranslateTo(0, -20, 1000, Easing.BounceIn);
                    await counterLabel.TranslateTo(0, 0, 1000, Easing.Linear);
                }
                else
                {
                    counterLabel.Text = score.ToString();
                    resultLabel.Text = "Incorrect";
                    resultLabel.TextColor = Color.Red;
                }
            }

            solved = true;

        }

        async void btnTwoClicked(object sender, EventArgs e)
        {

            string thisanswer = btnTwo.Text;

            if(solved == false) { 

                if (thisanswer == correctAnswer)
                {
                    resultLabel.Text = "Correct";
                    if(deuter == true)
                    {
                        resultLabel.TextColor = Color.Cyan;
                    }
                    else
                    {
                        resultLabel.TextColor = Color.Lime;

                    }
                    score += 1;
                    incScore();
                    counterLabel.Text = score.ToString();
                    await counterLabel.TranslateTo(0, -20, 1000, Easing.BounceIn);
                    await counterLabel.TranslateTo(0, 0, 1000, Easing.Linear);
                }
                else
                {
                    
                    resultLabel.Text = "Incorrect";
                    resultLabel.TextColor = Color.Red;
                }
            }

            solved = true;

        }

        async void incScore()
        {
            Console.WriteLine(masterLogged);
            if(masterLogged == true)
            {
            await firebaseHelper.UpdatePerson(usernameLabel.Text, score, masterPass);
            //await DisplayAlert("Success", "Person Updated Successfully", "OK");
            }
        }

        async void leaderboardButton(object sender, EventArgs e)
        {
            var newPage = new leaderboard();
            await Navigation.PushModalAsync(newPage);
        }


        async void loginButton(object sender, EventArgs e)
        {
            loadpage();
        }
        async void loadpage()
        {
            
            var newPage = new Page1();
            await Navigation.PushModalAsync(newPage);

        }

        private void deuterchecked(object sender, EventArgs e)
        {

            if(deuter == true)
            {
                deuter = false;
                counterLabel.TextColor = Color.Lime;
            }
            else
            {
                deuter = true;
                counterLabel.TextColor = Color.Cyan;
                
            }
        }

        private void protachecked(object sender, EventArgs e)
        {

            if (prot == true)
            {
                prot = false;
                callBTN.BackgroundColor = Color.Red;
                callBTN.TextColor = Color.White;
            }
            else
            {
                prot = true;
                callBTN.BackgroundColor = Color.Cyan;
                callBTN.TextColor = Color.Black;
            }
        }

        private void tritchecked(object sender, EventArgs e)
        {

            if (trit == true)
            {
                trit = false;
            }
            else
            {
                trit = true;
            }
        }

        public void setUser(string username)
        {
            usernameLabel.Text = username;
        }

        async void nextQuestion()
        {
            if (count == 0)
            {
                releasesArray = requestAPI();
                count = 1;
            }

            var releases = releasesArray;

            Console.WriteLine("Total Questions: ", total);

            //Console.WriteLine(releases.ToString());

            Random rnd = new Random();
            int randommessage = rnd.Next(0, total);

            Console.WriteLine(randommessage.ToString());
            
            if(prev == randommessage)
            {
                if(randommessage < (total-1))
                {
                    randommessage += 1;
                }
                else
                {
                    randommessage -= 1;
                }
                
            }
            

            Console.WriteLine(randommessage);

            string question = releases[randommessage]["question"].ToString();

            string answer = releases[randommessage]["answers"][0].ToString();

            string incorrect = releases[randommessage]["answers"][1].ToString();

            solved = false;

            int rndPos = rnd.Next(0, 2);

            if(rndPos == 0)
            {
                btnOne.Text = answer;
                btnTwo.Text = incorrect;
            }
            else
            {
                btnOne.Text = incorrect;
                btnTwo.Text = answer;
            }


            correctAnswer = answer;

            responselabel.Text = question;

            resultLabel.Text = "";

            prev = randommessage;

        }

        async void callBTNClicked(object sender, EventArgs e)
        {
            nextQuestion();
        }

        async void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {

        }

        private void Button_Clicked(object sender, EventArgs e)
        {

        }

        private async void logoutButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
            await Navigation.PushModalAsync(new MainPage("", false, "", 0));
        }
    }
}

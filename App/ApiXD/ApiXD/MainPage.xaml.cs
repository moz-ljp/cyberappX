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

namespace ApiXD
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]

    public partial class MainPage : ContentPage
    {

        string correctAnswer;

        int prev;

        int total;

        int score;

        JArray releasesArray = null;

        bool solved = false;

        bool deuter = false;

        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        public JArray requestAPI()
        {
            var request = (HttpWebRequest)WebRequest.Create("https://raw.githubusercontent.com/moz-ljp/cyberappX/master/questionsApi.json");
            //var request = (HttpWebRequest)WebRequest.Create("view-source:http://icyber.ml/questionsApi.json"); 
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

        private void btnOneClicked(object sender, EventArgs e)
        {
            string thisanswer = btnOne.Text;

            if (solved == false)
            {

                if (thisanswer == correctAnswer)
                {
                    resultLabel.Text = "Correct";
                    if (deuter == true)
                    {
                        resultLabel.TextColor = Color.Blue;
                    }
                    else
                    {
                        resultLabel.TextColor = Color.Green;

                    }
                    score += 1;
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

        private void btnTwoClicked(object sender, EventArgs e)
        {

            string thisanswer = btnTwo.Text;

            if(solved == false) { 

                if (thisanswer == correctAnswer)
                {
                    resultLabel.Text = "Correct";
                    if(deuter == true)
                    {
                        resultLabel.TextColor = Color.Blue;
                    }
                    else
                    {
                        resultLabel.TextColor = Color.Green;

                    }
                    score += 1;
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

        private void deuterchecked(object sender, EventArgs e)
        {
            if(deuter == true)
            {
                deuter = false;
            }
            else
            {
                deuter = true;
            }
        }

        private void nextQuestion()
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

        private void callBTNClicked(object sender, EventArgs e)
        {
            nextQuestion();
        }

        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {

        }
    }
}

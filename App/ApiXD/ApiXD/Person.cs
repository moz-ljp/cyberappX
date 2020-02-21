using System;
using System.Collections.Generic;
using System.Text;
using Firebase.Database;
using Firebase.Database.Query;

namespace ApiXD
{
    public class Person
    {
        FirebaseClient firebase = new FirebaseClient("https://cyberapp-b2823.firebaseio.com/");
        public string Password { get; set; }
        public string Name { get; set; }

        public int Score { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;


namespace ApiXD
{
    class FirebaseHelper
    {

        FirebaseClient firebase = new FirebaseClient("https://cyberapp-b2823.firebaseio.com/");

        public async Task<List<Person>> GetAllPersons()
        {
            
            return (await firebase
              .Child("Persons")
              .OnceAsync<Person>()).Select(item => new Person
              {
                  Name = item.Object.Name,
                  Password = item.Object.Password,
                  Score = item.Object.Score
              }).ToList();
        }

        public async Task AddPerson(string name, string password, int score)
        {

            await firebase
              .Child("Persons")
              .PostAsync(new Person() { Name = name, Password = password , Score = score});
        }

        public async Task<Person> GetPerson(string name, string password)
        {
            var allPersons = await GetAllPersons();
            await firebase
              .Child("Persons")
              .OnceAsync<Person>();
            return allPersons.Where(a => a.Name == name).FirstOrDefault();
        }

        public async Task UpdatePerson(string name, int score, string password)
        {
            var toUpdatePerson = (await firebase
              .Child("Persons")
              .OnceAsync<Person>()).Where(a => a.Object.Name == name).FirstOrDefault();

            await firebase
              .Child("Persons")
              .Child(toUpdatePerson.Key)
              .PutAsync(new Person() { Name = name , Score = score, Password = password});
        }

        public async Task DeletePerson(string name)
        {
            var toDeletePerson = (await firebase
              .Child("Persons")
              .OnceAsync<Person>()).Where(a => a.Object.Name == name).FirstOrDefault();
            await firebase.Child("Persons").Child(toDeletePerson.Key).DeleteAsync();

        }



    }

    


}

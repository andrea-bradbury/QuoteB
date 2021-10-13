using System;
using PCLStorage;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using QuoteB.Model;

namespace QuoteB
{
    /// This class manages all use of the Quotes objects.
    /// It collects the list of Quotes objects
    /// Generated a random Quotes object from the list
    /// And also handles reading and writing to json files.

    public class ManagingQuotes
    {

        public string FileName { get; set; }

        public string FolderName { get; set; }

        string fileName = "QuotesAppBContentsJson";
        string folderName = "QuotesBApp";


        // Collects all Quotes objects saved as list

        List<Quotes> listOfQuoteObjects = new List<Quotes>();
        
        


        ///Takes input of a Quote object and adds to a list.

        public void AddToList(Quotes quoteObject)
        {
            Console.WriteLine("Checking making it to Add to List function");

            if (listOfQuoteObjects == null)
            {
                List<Quotes> listOfQuoteObjects = new List<Quotes>();

                //Add quote to list
                listOfQuoteObjects.Add(quoteObject);
            }
            else
            {
                //Add quote to list
                listOfQuoteObjects.Add(quoteObject);
            }
            

            Console.WriteLine("Checking adding to list function");


        }


        ///Gets a random quotes object from the list

        public Quotes GetRandomQuote()
        {
            Console.WriteLine("Checking making it to random function");

            Random rnd = new Random();

            Console.WriteLine("Checking making random object");


            Quotes randomQuote = listOfQuoteObjects[rnd.Next(0, listOfQuoteObjects.Count)];

            Console.WriteLine("Checking random quote object is being picked from list" + randomQuote.ToString());


            return randomQuote;

        }


        /// Saving the lists of quotes objects into a file

        public async Task<bool> saveToFile()
        {
            try
            {

                //Serialize the list of Quotes objects

                string jsonFile = JsonConvert.SerializeObject(listOfQuoteObjects);


                // Open the folder

                IFolder folder = FileSystem.Current.LocalStorage;

                folder = await folder.CreateFolderAsync(folderName, CreationCollisionOption.OpenIfExists);


                //Open file

                IFile file = await folder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);


                //Write to file

                await file.WriteAllTextAsync(jsonFile);




                return true;

            }
            catch
            {
                ///Needs a directory not found exception

                return false;
            }


        }




        ///Reading from the json file 

        public async Task<bool> readFromFile()
        {
            try
            {

                //Open or create a folder

                IFolder folder = FileSystem.Current.LocalStorage;

                folder = await folder.CreateFolderAsync(folderName, CreationCollisionOption.OpenIfExists);

                Console.WriteLine("Checking open or create the folder");



                //Open file

                IFile file = await folder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);

                Console.WriteLine("Checking open or create the file");


                //Gets content from file

                string loadedContent = await file.ReadAllTextAsync();

                Console.WriteLine("Checking loaded content");


                //Turns file content into Quotes objects and puts them in list

                listOfQuoteObjects = JsonConvert.DeserializeObject<List<Model.Quotes>>(loadedContent);

                Console.WriteLine("Checking deserialising into objects");

               

                return true;
            }
            catch
            {
                ///Needs an exception for if a file doesn't exist

                return false;
            }

        }
    }
}

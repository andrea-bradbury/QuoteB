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


        // Collects all 'favourited' Quotes objects saved as list

        List<Quotes> listOfFavouriteQuoteObjects = new List<Quotes>();




        ///Takes input of a Quote object and adds to a list.

        public void AddToList(Quotes quoteObject)
        {
            

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
           


        }


        ///Gets a random quotes object from the list

        public Quotes GetRandomQuote()
        {

            Random rnd = new Random();

            Quotes randomQuote = listOfQuoteObjects[rnd.Next(0, listOfQuoteObjects.Count)];


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

                if (loadedContent == "null")
                {
                    List<Quotes> listOfQuoteObjects = new List<Quotes>();

                }
                else
                {
                    listOfQuoteObjects = JsonConvert.DeserializeObject<List<Model.Quotes>>(loadedContent);

                    Console.WriteLine("Checking deserialising into objects");
                    
                }

                

                return true;
            }
            catch
            {
                ///Needs an exception for if a file doesn't exist

                return false;
            }

        }


        /// For keeping a list of favourites current in the app
        public void UpdateFavourites(Quotes quote)
        {
            Console.WriteLine("Checking making it to Favourites function");

            
            if (quote.Favourite == true)
            {
                //Add quote to favourites list
                listOfFavouriteQuoteObjects.Add(quote);
            }

            Console.WriteLine("Checking adding to favourites list function");
        }




        public async Task<List<Model.Quotes>> ReturnFavouriteQuotes()
        {
            return listOfFavouriteQuoteObjects;
        }
    }
}

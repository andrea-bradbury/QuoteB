using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using PCLStorage;
using Newtonsoft.Json;
using QuoteB.Model;
using Xamarin.Essentials;

namespace QuoteB
{ 

    public partial class MainPage : ContentPage
    {

        ManagingQuotes managingQuotes = new ManagingQuotes();

        Quotes displayedQuote;

        public MainPage()
        {
            

            InitializeComponent();

            App app = (App)Application.Current;

            managingQuotes = app.managingQuotes;



            //Display a random quote on open if there is one
            try
            {
                Quotes quote = managingQuotes.GetRandomQuote();


                labelQuoteText.Text = quote.Saying;


                labelQuoteAuthor.Text = quote.Author;


                switchFavourite.IsToggled = quote.Favourite;

            }
            catch
            {
                switchFavourite.IsVisible = false;
                labelFavourites.IsVisible = false;

            }

            managingQuotes.UpdateFavourites();



        }

        


        ///Collects a random quote from the list of quote objects and displays in UI.
        
        void buttonRandomGenerate_Clicked(System.Object sender, System.EventArgs e)
        {

            try
            {
                displayedQuote = managingQuotes.GetRandomQuote();


                labelQuoteText.Text = displayedQuote.Saying;


                labelQuoteAuthor.Text = displayedQuote.Author;

                switchFavourite.IsVisible = true;
                labelFavourites.IsVisible = true;


                switchFavourite.IsToggled = displayedQuote.Favourite;

                

            }
            catch
            {
                DisplayAlert("Try adding a quote first.", " ", "OK");
            }

            
        }


        ///Take the quote and author input and create Quotes object.
        
        async void buttonSaveQuote_Clicked(System.Object sender, System.EventArgs e)
        {
            string quoteInput = entryQuote.Text;

            string authorInput = entryAuthor.Text;

            

            Console.WriteLine("Checking inputs are being stored to variables");



            Quotes Model = new Quotes(quoteInput, authorInput, false);

            Console.WriteLine("Checking object instantiate");


            managingQuotes.AddToList(Model);

            await DisplayAlert($"{Model.Saying}, {Model.Author}", "This quote has been added.", "OK");


            //Clear the entry fields

            entryQuote.Text = " ";
            entryAuthor.Text = " ";

            await managingQuotes.saveToFile();
            await managingQuotes.readFromFile();

           

        }


        /// Make a quote a favourite
        
        void switchFavourite_Toggled(System.Object sender, Xamarin.Forms.ToggledEventArgs e)
        {
            //Note for Andrea: this function randomly fires sometimes when generate quote button is clicked??

            if (displayedQuote.Favourite == false)
            {
                displayedQuote.Favourite = true;
            }
            else
            {
                displayedQuote.Favourite = false;
            }

            managingQuotes.UpdateFavourites();
        }


        /// Open new page for viewing favourited quotes
        
        async void buttonViewFavourites_Clicked(System.Object sender, System.EventArgs e)
        {

            FavouritesPage favouritesPage = new FavouritesPage();

            await Navigation.PushModalAsync(favouritesPage);

            List<Model.Quotes> listOfFavourites = await managingQuotes.ReturnFavouriteQuotes();

            await favouritesPage.populateFavouritesUI(listOfFavourites);

           
        }

    }
}

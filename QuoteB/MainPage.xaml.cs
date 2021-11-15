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
        ManagingQuotes managingQuotes;

       



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

                return;
            }

        }


        ///Collects a random quote from the list of quote objects and displays in UI.
        
        void buttonRandomGenerate_Clicked(System.Object sender, System.EventArgs e)
        {

            try
            {
                Quotes quote = managingQuotes.GetRandomQuote();


                labelQuoteText.Text = quote.Saying;


                labelQuoteAuthor.Text = quote.Author;

                switchFavourite.IsVisible = true;
                labelFavourites.IsVisible = true;


                switchFavourite.IsToggled = quote.Favourite;

            }
            catch
            {
                DisplayAlert("Try adding a quote first.", " ", "OK");
            }

            
        }


        ///Take the quote and author input and create Quotes object.
        
        void buttonSaveQuote_Clicked(System.Object sender, System.EventArgs e)
        {
            string quoteInput = entryQuote.Text;

            string authorInput = entryAuthor.Text;

            bool isFavourite = switchFavourite.IsToggled;

            Console.WriteLine("Checking inputs are being stored to variables");



            Quotes Model = new Quotes(quoteInput, authorInput, isFavourite);

            Console.WriteLine("Checking object instantiate");


            managingQuotes.AddToList(Model);

            managingQuotes.UpdateFavourites(Model);


            DisplayAlert($"{Model.Saying}, {Model.Author}, Favourited [{Model.Favourite}]", "This quote has been added.", "OK");


            //Need to add in clears for the entry fields

        }

        /// Make a quote a favourite
        
        void switchFavourite_Toggled(System.Object sender, Xamarin.Forms.ToggledEventArgs e)
        {
            

            //quotes(labelQuoteText.Text, labelQuoteAuthor.Text, switchFavourite.IsToggled);

            //managingQuotes.UpdateFavourites();
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

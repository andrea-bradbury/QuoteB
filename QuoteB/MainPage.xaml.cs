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
        }


        ///Collects a random quote from the list of quote objects and displays in UI.
        
        void buttonRandomGenerate_Clicked(System.Object sender, System.EventArgs e)
        {

            try
            {
                Quotes quote = managingQuotes.GetRandomQuote();


                labelQuoteText.Text = quote.Saying;


                labelQuoteAuthor.Text = quote.Author;

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

            Console.WriteLine("Checking inputs are being stored to variables");


            Quotes Model = new Quotes(quoteInput, authorInput);

            Console.WriteLine("Checking object instantiate");


            managingQuotes.AddToList(Model);


            DisplayAlert($"{Model.Saying}, {Model.Author}", "This quote has been added.", "OK");


            //Need to add in clears for the entry fields

        }

    }
}

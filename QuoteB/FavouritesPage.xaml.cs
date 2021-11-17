using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;


using Xamarin.Forms;

namespace QuoteB
{
    public partial class FavouritesPage : ContentPage
    {
        public FavouritesPage()
        {
            InitializeComponent();
        }

        async void buttonBack_Clicked(System.Object sender, System.EventArgs e)
        {
            try
            {
                await Navigation.PopModalAsync();
            }
            catch
            {

            }
        }

        //Filling the stack of favourites

        public async Task<bool> populateFavouritesUI(List<Model.Quotes> listOfFavourites)
        {
            

            try
            {
              
               
                if (listOfFavourites.Count == 0)
                {
                    //Create vertical stack
                    StackLayout stack = new StackLayout()
                    {
                        Orientation = StackOrientation.Horizontal,
                    };


                    //Set up smaller stack for data

                    Label labelNone = new Label()
                    {
                        Text = "No favourites to show",
                        HorizontalOptions = LayoutOptions.EndAndExpand,
                        TextColor = Color.MediumPurple,
                    };
                    stack.Children.Add(labelNone);

                    await DisplayAlert("No favourites to display yet", " ", "OK");

                }
                else
                {
                    


                    foreach (Model.Quotes quote in listOfFavourites)
                    {
                        //Create vertical stack
                        StackLayout stack = new StackLayout()
                        {
                            Orientation = StackOrientation.Horizontal,
                        };


                        //Set up smaller stack for data

                        Label labelSaying = new Label()
                        {
                            Text = quote.Saying,
                            HorizontalOptions = LayoutOptions.EndAndExpand,
                            TextColor = Color.MediumPurple,
                        };
                        stack.Children.Add(labelSaying);

                        Label labelAuthor = new Label()
                        {
                            Text = quote.Author,
                            HorizontalOptions = LayoutOptions.EndAndExpand,
                            TextColor = Color.DarkOrchid,
                        };
                        stack.Children.Add(labelAuthor);

                        //For each object in the list create the horizontal stack and add to main vertical stack
                        stackFavouriteQuotes.Children.Add(stack);

                    }


                }
                

                return true;
                
            }
            catch
            {

                await DisplayAlert("Oops something went wrong.", " ", "OK");

                return false;
            }




        }

    }
}

using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Threading.Tasks;
using QuoteB.Model;

namespace QuoteB
{
    public partial class App : Application
    {
        public ManagingQuotes managingQuotes { get; private set; } = new ManagingQuotes();

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            //Try open the file of Quotes and save contents to objects

            managingQuotes.readFromFile();
        }

        protected override void OnSleep()
        {
            //Save all Quote objects back to file

            managingQuotes.saveToFile();
        }

        protected override void OnResume()
        {

        }
    }
}

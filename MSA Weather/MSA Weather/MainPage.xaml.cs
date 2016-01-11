using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MSA_Weather
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }


        private void goButton_Click(object sender, RoutedEventArgs e)
        {
            string city = weatherInput.Text.ToString();
            executeWeather(city);
        }

        private async void executeWeather(string city)
        {
            // Let the user know something is happening so that they don't think the
            progressBar.Visibility = Visibility.Visible;

            // Creating a new Weather Object to bind the results from our API call.
            Weather_Object.RootObject rootObject =  await getWeather(city);

            if(rootObject.main != null)
            {
                progressBar.Visibility = Visibility.Collapsed;
                UpdateTextBlock(rootObject);
                UpdateStackPanel(rootObject);
            }
            else
            {
                progressBar.Visibility = Visibility.Collapsed;
                MessageDialog msgDialog = new MessageDialog("Enter a proper city!", "Oops!");
                await msgDialog.ShowAsync();
               
            }


        }

        private void UpdateTextBlock(Weather_Object.RootObject rootObject)
        {
            string outputString = "";
            outputString += rootObject.name + " " + rootObject.main.temp + " C" + "\n";
            outputString += "Wind Speed: " + rootObject.wind.speed + " m/s" + "\n";
            outputString += "Pressure: " + rootObject.main.pressure + " hPa" + "\n";
            outputString += "Humidity: " + rootObject.main.humidity + " %" + "\n";
            outputString += rootObject.weather[0].description;

            outputBox.Text = outputString;
        }


        // What mistake have I done here?
        // HINT: It wont impact the runtime in any way, but professional devs don't like this....
        private void UpdateStackPanel(Weather_Object.RootObject rootObject)
        {

            textCity.Text = rootObject.name;
            textTemp.Text = rootObject.main.temp + " C" + "\n";
            textPressure.Text = "Pressure: " + rootObject.main.pressure + " hPa" + "\n";
            textHumidity.Text = "Humidity: " + rootObject.main.humidity + " %" + "\n";
            textWindDir.Text = "Wind Speed: " + rootObject.wind.speed + " m/s" + "\n";
            textDescription.Text = rootObject.weather[0].description;
            
        }



        // This method must have the 'async' tag for our HTTPClient to work when calling
        // the weather API. This is because the method used to call our API is done asynchronously,
        // requiring the 'getWeather' method to wait for our HTTPClient to finish pulling the data
        // before continuing with the rest of the code.
        private async Task<Weather_Object.RootObject> getWeather(string city)
        {


            // We need a try/catch wrapped around our API resquest just incase an error occurs
            // while calling the weather API. If an error does occur the code inside the catch
            // statement is called, otherwise the app skips it and continues with the code.
            // Without a try/catch, if an error does occur the app would not know how to handle it
            // resulting in the app crashing. A try/catch prevents the app from crashing and can be
            // used to inform the user what went wrong.
            try
            {
                // Initializing HTTPClient.
                HttpClient client = new HttpClient();

                // Creating a new Weather Object to bind the results from our API call.
                Weather_Object.RootObject rootObject;

                // Calling our weather API, passing the string 'city' so we're getting the correct weather returned.
                // The 'await' tag tells the computer to wait for the results to be returned before continuing with
                // the rest of the code. The results are then assigned to string 'x' to be used later in the code.
                string x = await client.GetStringAsync(new Uri("http://api.openweathermap.org/data/2.5/weather?q=" + city + "&units=metric&APPID=440e3d0ee33a977c5e2fff6bc12448ee"));

                // We're now binding the returned data assigned to string 'x' to the object 'rootObject'.
                rootObject = JsonConvert.DeserializeObject<Weather_Object.RootObject>(x);
                return rootObject;
           
            }
            // Only called if an error occurs.
            catch (Exception ex)
            {
                return null;
            }

        }

    }
}

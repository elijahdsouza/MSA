using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace BMIme_App
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
          
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
          
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                double weight = Convert.ToDouble(input_weight.Text.ToString());
                double height = Convert.ToDouble(input_height.Text.ToString()) * 0.01;

                if (weight > 0 & height > 0)
                {
                    double bmi = weight / (height * height);

                    displayBMI(bmi);
                }
                else
                {
                    outputBMI.Text = "";
                    result.Text = "";
                    errorMess.Text = "Enter a valid height and weight.";

                }
            }
            catch (FormatException)
            {
                outputBMI.Text = "";
                result.Text = "";
                errorMess.Text = "Enter a valid height and weight.";
            }
            
        }

        private void displayBMI(double bmi)
        {

           outputBMI.Text = Convert.ToString(bmi);
           errorMess.Text = "";

            if (bmi < 18.5)
            {
                result.Text = "You are under weight.";

            }
            else if (bmi >= 18.5 & bmi <= 24.9)
            {

                result.Text = "You have a normal weight.";
            }
            else if (bmi >= 25 & bmi <= 29.9)
            {

                result.Text = "You are overweight.";
            }
            else
            {
                result.Text = "You are obese.";
            }
        } 
        private void TextBlock_SelectionChanged_1(object sender, RoutedEventArgs e)
        {
           
        }

        private void Result_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private void invalidInput_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }
    }
}

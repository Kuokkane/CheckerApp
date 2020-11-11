using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CheckerApp
{

    public partial class MainWindow : Window
    {
        static HttpClient client = new HttpClient();
        public MainWindow()
        {
            InitializeComponent();


        }



        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            //Send input to API and save result of the query to Answer object
            Answer result = await GetAPIAsync("https://localhost:44320/api/Fibonacci/" + input.Text);

            MessageBox.Show(result.answer);

        }

        //Data structure
        public class Answer

        {
            public string answer { get; set; }
        }

        //Calls API in the url given on Button_Click and retrieves JSON-data
        static async Task<Answer> GetAPIAsync(string path)

        {
            Answer result = null;

            HttpResponseMessage response = await client.GetAsync(path);

            if (response.IsSuccessStatusCode)

            {
                result = await response.Content.ReadAsAsync<Answer>();
            }

            return result;

        }

        //Allow only numeric in input field
        private void input_previewtextinput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }
    }
}

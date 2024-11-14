using Newtonsoft.Json;
using Marathon.Models;
namespace Marathon;

public partial class MainPage : ContentPage
{
    
    private RaceCollection RaceObject;
    public MainPage()
    {
        InitializeComponent();
        FillPicker();
    }

    public void FillPicker()
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri("https://joewetzel.com/fvtc/marathon/");
        var response = client.GetAsync("races").Result;
        var wsJson = response.Content.ReadAsStringAsync().Result;

        RaceObject = JsonConvert.DeserializeObject<RaceCollection>(wsJson);

        RacePicker.ItemsSource = RaceObject.races;
    }

    private void RacePicker_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        var SelectedRace = ((Picker)sender).SelectedIndex;

        var RaceID = RaceObject.races[SelectedRace].id;
        
        var client = new HttpClient();
        client.BaseAddress = new Uri("https://joewetzel.com/fvtc/marathon/");
        var response = client.GetAsync("results/" + RaceID).Result;
        var wsJson = response.Content.ReadAsStringAsync().Result;

        var ResultObject = JsonConvert.DeserializeObject<ResultCollection>(wsJson);

        var CellTemplate = new DataTemplate(typeof(TextCell));
        
        CellTemplate.SetBinding(TextCell.TextProperty, "name");
        CellTemplate.SetBinding(TextCell.DetailProperty, "detail");

        lstResults.ItemTemplate = CellTemplate;
        lstResults.ItemsSource = ResultObject.results;

    }
}
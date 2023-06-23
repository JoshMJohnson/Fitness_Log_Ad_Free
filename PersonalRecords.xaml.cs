namespace WorkoutLog;

public partial class PersonalRecords : ContentPage
{
	public PersonalRecords()
	{
		InitializeComponent();

        /* display when PR list is empty */
        VerticalStackLayout vertical_layout = new VerticalStackLayout();

        vertical_layout.VerticalOptions = LayoutOptions.Center;
        vertical_layout.HorizontalOptions = LayoutOptions.Center;

        vertical_layout.Add(new Label
        {
            Text = "No PRs Saved!",
            FontSize = 20,
            FontAttributes = FontAttributes.Italic,
            HorizontalOptions = LayoutOptions.Center
        });

        vertical_layout.Add(new Image
        {
            Source = "empty_goal_list.png",
            HeightRequest = 200
        });

        Grid goal_layout = pr_layout;
        Grid.SetRow(vertical_layout, 0);
        goal_layout.Add(vertical_layout);
    }

    /* executed when PR plus button clicked */
    private async void Add_PR(object sender, EventArgs e)
    {
        await DisplayAlert("Personal Record", "yaa", "Save");
    }
}
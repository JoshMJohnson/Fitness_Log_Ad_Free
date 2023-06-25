using CommunityToolkit.Maui.Views;
using WorkoutLog.Popup;
using WorkoutLog.Model;

namespace WorkoutLog;

public partial class BodyProgression : ContentPage
{
    private VerticalStackLayout vertical_layout_body_progression_empty;

    public BodyProgression()
	{
		InitializeComponent();

        /* display when PR list is empty */
        vertical_layout_body_progression_empty = new VerticalStackLayout();

        vertical_layout_body_progression_empty.VerticalOptions = LayoutOptions.Center;
        vertical_layout_body_progression_empty.HorizontalOptions = LayoutOptions.Center;

        vertical_layout_body_progression_empty.Add(new Label
        {
            Text = "No Progressions Saved!",
            FontSize = 20,
            FontAttributes = FontAttributes.Italic,
            HorizontalOptions = LayoutOptions.Center
        });

        vertical_layout_body_progression_empty.Add(new Image
        {
            Source = "empty_list.png",
            HeightRequest = 200
        });

        Grid goal_layout = progression_layout;
        Grid.SetRow(vertical_layout_body_progression_empty, 0);
        goal_layout.Add(vertical_layout_body_progression_empty);

        Refresh_Progression();
    }

    /* executed when PR plus button clicked */
    private async void Add_Body_Progression(object sender, EventArgs e)
    {
        object result = await this.ShowPopupAsync(new BodyProgressionPopup());
    }

    /* refreshes the PR list being displayed on UI */
    public async void Refresh_Progression()
    {
        /* TESTING START */
        List<Progression> progression_list = new List<Progression>();

        Progression temp1 = new Progression();
        Progression temp2 = new Progression();
        Progression temp3 = new Progression();

        temp1.Id = 1;
        temp2.Id = 2;
        temp3.Id = 3;

        temp1.Image_Full_Path = "image1.jpg";
        temp2.Image_Full_Path = "image2.png";
        temp3.Image_Full_Path = "image3.jpg";

        //progression_list.Add(temp1);
        //progression_list.Add(temp2);
        //progression_list.Add(temp3);
        /* TESTING END */

        progression_display.ItemsSource = progression_list;

        if (progression_list.Count == 0)  /* if pr list is empty - no pr's set */
        {
            vertical_layout_body_progression_empty.IsVisible = true;
        }
        else  /* else pr list is not empty */
        {
            vertical_layout_body_progression_empty.IsVisible = false;
        }
    }
}
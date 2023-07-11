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
        Empty_Progression_Display();
        Refresh_Progression();
    }

    /* todo executes help button is clicked */
    private void Help_Window(object sender, EventArgs e)
    {

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

        temp1.id = 1;
        temp2.id = 2;
        temp3.id = 3;

        temp1.image_full_path = "image1.jpg";
        temp2.image_full_path = "image2.png";
        temp3.image_full_path = "image3.jpg";

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

    /* creates message for empty categories */
    private void Empty_Progression_Display()
    {
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
    }
}
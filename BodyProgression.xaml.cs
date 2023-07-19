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

    /* executed when PR plus button clicked */
    private async void Add_Body_Progression(object sender, EventArgs e)
    {
        object result = await this.ShowPopupAsync(new BodyProgressionPopup());
    }

    /* refreshes the PR list being displayed on UI */
    public async void Refresh_Progression()
    {
        List<Progression> progression_list = new List<Progression>();

        progression_list_display.ItemsSource = progression_list;

        if (progression_list.Count == 0)  /* if body progression list is empty - no body progression's set */
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

    /* todo button clicked to create a workout category */
    private async void Progression_Selected(object sender, EventArgs e)
    {

    }
}
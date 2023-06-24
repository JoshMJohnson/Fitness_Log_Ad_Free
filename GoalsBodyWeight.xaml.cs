using CommunityToolkit.Maui.Views;
using WorkoutLog.Popup;
using WorkoutLog.Model;

namespace WorkoutLog;

public partial class GoalsBodyWeight : ContentPage
{
	public GoalsBodyWeight()
	{
		InitializeComponent();

		/* display when body weight goal list is empty */
		VerticalStackLayout vertical_layout = new VerticalStackLayout();

		vertical_layout.VerticalOptions = LayoutOptions.Center;
		vertical_layout.HorizontalOptions = LayoutOptions.Center;

        vertical_layout.Add(new Label
        {
            Text = "No body weight goals!",
            FontSize = 20,
            FontAttributes = FontAttributes.Italic,
            HorizontalOptions = LayoutOptions.Center
        });

        vertical_layout.Add(new Image
        {
            Source = "empty_list.png",
            HeightRequest = 200
        });

        Grid goal_layout = goal_bw_layout;
        Grid.SetRow(vertical_layout,0);
        goal_layout.Add(vertical_layout);
    }

    private async void Add_Body_Weight_Goal(object sender, EventArgs e)
    {
        object result = await this.ShowPopupAsync(new GoalBodyWeightPopup());
    }
}
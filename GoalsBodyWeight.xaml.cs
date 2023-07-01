using CommunityToolkit.Maui.Views;
using WorkoutLog.Popup;
using WorkoutLog.Model;

namespace WorkoutLog;

public partial class GoalsBodyWeight : ContentPage
{
    private VerticalStackLayout vertical_layout_empty_bw_goal_list;

    public GoalsBodyWeight()
	{
		InitializeComponent();

        /* display when body weight goal list is empty */
        vertical_layout_empty_bw_goal_list = new VerticalStackLayout();

        vertical_layout_empty_bw_goal_list.VerticalOptions = LayoutOptions.Center;
        vertical_layout_empty_bw_goal_list.HorizontalOptions = LayoutOptions.Center;

        vertical_layout_empty_bw_goal_list.Add(new Label
        {
            Text = "No body weight goals!",
            FontSize = 20,
            FontAttributes = FontAttributes.Italic,
            HorizontalOptions = LayoutOptions.Center
        });

        vertical_layout_empty_bw_goal_list.Add(new Image
        {
            Source = "empty_list.png",
            HeightRequest = 200
        });

        Grid goal_layout = goal_bw_layout;
        Grid.SetRow(vertical_layout_empty_bw_goal_list, 0);
        goal_layout.Add(vertical_layout_empty_bw_goal_list);

        Refresh_BW_Goal_List();
    }

    private async void Add_Body_Weight_Goal(object sender, EventArgs e)
    {
        await this.ShowPopupAsync(new GoalBodyWeightPopup());
        Refresh_BW_Goal_List();
    }

    /* todo swipe remove BW goal */
    private async void Remove_Body_Weight_Goal(object sender, EventArgs e)
    {
        SwipeItem remove_pr = (SwipeItem) sender;
        string pr_name = remove_pr.Text;

        //await App.RecordRepo.Remove_Goal_Body_Weight(0);
        Refresh_BW_Goal_List();
    }

    /* todo swipe edit BW goal */
    private async void Update_Body_Weight_Goal(object sender, EventArgs e)
    {
        SwipeItem remove_pr = (SwipeItem) sender;
        string pr_name = remove_pr.Text;

        //await App.RecordRepo.Update_Goal_PR();

        Refresh_BW_Goal_List();
    }

    /* refreshes the BW goal list being displayed on UI */
    public async void Refresh_BW_Goal_List()
    {
        List<GoalBW> pr_goal_list = await App.RecordRepo.Get_Body_Weight_Goal_List();

        body_weight_goals_display.ItemsSource = pr_goal_list;

        if (pr_goal_list.Count == 0)  /* if pr list is empty - no pr's set */
        {
            vertical_layout_empty_bw_goal_list.IsVisible = true;
        }
        else  /* else pr list is not empty */
        {
            vertical_layout_empty_bw_goal_list.IsVisible = false;
        }
    }
}
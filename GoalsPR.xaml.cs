using CommunityToolkit.Maui.Views;
using WorkoutLog.Popup;
using WorkoutLog.Model;

namespace WorkoutLog;

public partial class GoalsPR : ContentPage
{
    private VerticalStackLayout vertical_layout_empty_pr_goal_list;

    public GoalsPR()
    {
        InitializeComponent();

        /* display when PR goal list is empty */
        vertical_layout_empty_pr_goal_list = new VerticalStackLayout();

        vertical_layout_empty_pr_goal_list.VerticalOptions = LayoutOptions.Center;
        vertical_layout_empty_pr_goal_list.HorizontalOptions = LayoutOptions.Center;

        vertical_layout_empty_pr_goal_list.Add(new Label
        {
            Text = "No PR goals!",
            FontSize = 20,
            FontAttributes = FontAttributes.Italic,
            HorizontalOptions = LayoutOptions.Center
        });

        vertical_layout_empty_pr_goal_list.Add(new Image
        {
            Source = "empty_list.png",
            HeightRequest = 200
        });

        Grid goal_layout = goal_pr_layout;
        Grid.SetRow(vertical_layout_empty_pr_goal_list, 0);
        goal_layout.Add(vertical_layout_empty_pr_goal_list);

        Refresh_PR_Goal_List();
    }

    private async void Add_PR_Goal(object sender, EventArgs e)
    {
        await this.ShowPopupAsync(new GoalPRPopup());
        Refresh_PR_Goal_List();
    }

    /* swipe remove pr goal */
    private async void Remove_PR_Goal(object sender, EventArgs e)
    {
        SwipeItem remove_pr = (SwipeItem)sender;
        string pr_name = remove_pr.Text;

        await App.RecordRepo.Remove_Goal_PR(pr_name);
        Refresh_PR_Goal_List();
    }

    /* ? swipe edit pr goal */
    private async void Update_PR_Goal(object sender, EventArgs e)
    {
        SwipeItem update_pr = (SwipeItem) sender;
        string pr_name = update_pr.Text;

        await App.RecordRepo.Update_Goal_PR(pr_name);
        Refresh_PR_Goal_List();
    }

    /* refreshes the PR goal list being displayed on UI */
    public async void Refresh_PR_Goal_List()
    {
        List<GoalPR> pr_goal_list = await App.RecordRepo.Get_Goal_PR_List();

        pr_goals_display.ItemsSource = pr_goal_list;

        if (pr_goal_list.Count == 0)  /* if pr list is empty - no pr's set */
        {
            vertical_layout_empty_pr_goal_list.IsVisible = true;
        }
        else  /* else pr list is not empty */
        {
            vertical_layout_empty_pr_goal_list.IsVisible = false;
        }
    }
}
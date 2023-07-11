using CommunityToolkit.Maui.Views;
using WorkoutLog.Popup;
using WorkoutLog.Model;
using CommunityToolkit.Maui.Converters;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.Maui.Controls;

namespace WorkoutLog;

public partial class PersonalRecords : ContentPage
{
    private VerticalStackLayout vertical_layout_empty_pr_list;

    public PersonalRecords()
	{
		InitializeComponent();
        Empty_PR_Display();
        Refresh_PR_List();
    }

    /* todo executes help button is clicked */
    private void Help_Window(object sender, EventArgs e)
    {

    }

    /* executed when PR plus button clicked */
    private async void Add_PR(object sender, EventArgs e)
    {
        await this.ShowPopupAsync(new PersonalRecordAddPopup());
        Refresh_PR_List();
    }

    /* swipe remove pr */
    private async void Remove_PR(object sender, EventArgs e)
    {
        SwipeItem remove_pr = (SwipeItem) sender;
        string pr_name = remove_pr.Text;

        await App.RecordRepo.Remove_PR(pr_name);
        Refresh_PR_List();
    }

    /* swipe edit pr */
    private async void Update_PR(object sender, EventArgs e)
    {
        SwipeItem update_pr = (SwipeItem) sender;
        string pr_name = update_pr.Text;

        await this.ShowPopupAsync(new UpdatePR(pr_name));
        Refresh_PR_List();
    }

    /* refreshes the PR list being displayed on UI */
    public async void Refresh_PR_List()
    {
        List<PR> pr_list = await App.RecordRepo.Get_PR_List();

        pr_display.ItemsSource = pr_list;

        if (pr_list.Count == 0)  /* if pr list is empty - no pr's set */
        {
            vertical_layout_empty_pr_list.IsVisible = true;
        }
        else  /* else pr list is not empty */
        {
            vertical_layout_empty_pr_list.IsVisible = false;
        }
    }

    /* creates message for empty categories */
    private void Empty_PR_Display()
    {
        /* display when PR list is empty */
        vertical_layout_empty_pr_list = new VerticalStackLayout();

        vertical_layout_empty_pr_list.VerticalOptions = LayoutOptions.Center;
        vertical_layout_empty_pr_list.HorizontalOptions = LayoutOptions.Center;

        vertical_layout_empty_pr_list.Add(new Label
        {
            Text = "No PRs Saved!",
            FontSize = 20,
            FontAttributes = FontAttributes.Italic,
            HorizontalOptions = LayoutOptions.Center
        });

        vertical_layout_empty_pr_list.Add(new Image
        {
            Source = "empty_list.png",
            HeightRequest = 200
        });

        Grid goal_layout = pr_layout;
        Grid.SetRow(vertical_layout_empty_pr_list, 0);
        goal_layout.Add(vertical_layout_empty_pr_list);
    }
}
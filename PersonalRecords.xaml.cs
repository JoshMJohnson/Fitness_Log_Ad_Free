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

        Refresh_PR_List();
    }

    /* executed when PR plus button clicked */
    private async void Add_PR(object sender, EventArgs e)
    {
        object result = await this.ShowPopupAsync(new PersonalRecordAddPopup());
    }

    /* todo refreshes the PR list being displayed on UI */
    public async void Refresh_PR_List()
    {
        List<PR> pr_list = new List<PR>();

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
}
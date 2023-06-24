using CommunityToolkit.Maui.Views;
using WorkoutLog.Popup;
using WorkoutLog.Model;
using CommunityToolkit.Maui.Converters;

namespace WorkoutLog;

public partial class PersonalRecords : ContentPage
{
    VerticalStackLayout vertical_layout_empty_pr_list;

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
            Source = "empty_goal_list.png",
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

    /* refreshes the PR list being displayed on UI */
    public async void Refresh_PR_List()
    {
        /* TESTING START */
        List<PR> pr_list = new List<PR>();

        PR temp1 = new PR();
        PR temp2 = new PR();
        PR temp3 = new PR();
        PR temp4 = new PR();
        PR temp5 = new PR();

        temp1.Id = 1;
        temp2.Id = 2;
        temp3.Id = 3;
        temp4.Id = 4;
        temp5.Id = 5;

        temp1.Date_Achieved = DateTime.Today;
        temp2.Date_Achieved = DateTime.Now;
        temp3.Date_Achieved = DateTime.Now;
        temp4.Date_Achieved = DateTime.Now;
        temp5.Date_Achieved = DateTime.Now;

        temp1.Exercise_Name = "Bench Press";
        temp2.Exercise_Name = "Leg Press";
        temp3.Exercise_Name = "Squat";
        temp4.Exercise_Name = "Bar Curl";
        temp5.Exercise_Name = "Dumbbell Curl";

        temp1.Weight = 150;
        temp2.Weight = 42;
        temp4.Weight = 71;

        temp3.Time_Hours = 0;
        temp3.Time_Min = 35;
        temp3.Time_Sec = 55;

        temp5.Time_Hours = 1;
        temp5.Time_Min = 57;
        temp5.Time_Sec = 11;

        pr_list.Add(temp1);
        pr_list.Add(temp2);
        pr_list.Add(temp3);
        pr_list.Add(temp4);
        pr_list.Add(temp5);
        /* TESTING END */

        pr_display.ItemsSource = pr_list;

        if (pr_list.Count == 0)  /* if pr list is empty - no pr's set */
        {
            vertical_layout_empty_pr_list.IsVisible = true;
        }
        else  /* else pr list is not empty */
        {
            vertical_layout_empty_pr_list.IsVisible = false;

            for (int i = 0; i < pr_list.Count; i++) /* loop through list of pr's */
            {
                /* alternate background colors of items on pr list */
                if (i % 2 == 0)
                {

                }
                else
                {

                }

                /* swap from weight pr -> time pr display */
                if (pr_list[i].Weight == 0) /* if exercise type is a timed event */
                {

                }
            }
        }
    }
}
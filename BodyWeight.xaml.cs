using WorkoutLog.Popup;
using WorkoutLog.Model;
using CommunityToolkit.Maui.Views;

namespace WorkoutLog;

public partial class BodyWeight : ContentPage
{
    public BodyWeight()
	{
		InitializeComponent();
        Refresh_Table_Data();
    }

    /* executed when body weight plus button clicked to record an update in body weight */
    private async void Record_Body_Weight(object sender, EventArgs e)
    {
        object result = await this.ShowPopupAsync(new BodyWeightAddPopup());

        if (result != null) /* if body weight entry was made */
        {
            Refresh_Table_Data();
        }
    }

    /* refreshes the Body Weight table data */
    private async void Refresh_Table_Data()
    {
        List<BodyWeightEntry> entries = await App.RecordRepo.Get_Body_Weight_List();

        chart_data_display.ItemsSource = entries; /* updates the chart data */

        /* 'Actual' cell */
        actual_weight_display.Text = entries[0].weight.ToString();

        /* 'Change' cell */
        int most_recent_weight_entry = entries[0].weight;
        int second_most_recent_weight_entry = entries[1].weight;
        int weight_change = most_recent_weight_entry - second_most_recent_weight_entry;

        change_weight_display.Text = weight_change.ToString();

        /* 'Closest Goal' cell */
        List<GoalBW> body_weight_goals = await App.RecordRepo.Get_Body_Weight_Goal_List();

        if (body_weight_goals.Count == 0) /* if no body weight goals */
        {
            goal_display.Text = "----";
        }
        else /* else; at least one body weight goal set */
        {
            for (int i = 0; i < body_weight_goals.Count; i++) /* loop throught the body weight goals */
            {
                if (body_weight_goals[i].date_desired) /* if current goal has a desired achieve by date */
                {
                    goal_display.Text = body_weight_goals[i].weight.ToString();
                    break;
                }

                if (i == body_weight_goals.Count - 1) /* if no body weight goals have a desired date */
                {
                    goal_display.Text = "----";
                }
            }
        }

        /* todo 'This Week' cell */

        //week_change_display.Text = week_change.ToString();


        /* todo 'This Month' cell */
        //month_change_display.Text = month_change.ToString();


        /* 'Total' cell */
        int num_entries = entries.Count;

        if (num_entries <= 2) /* if; 0 or 1 body weight entries */ 
        {
            total_change_display.Text = "----";
        }
        else /* else more than 1 recorded body weight entries */
        {
            double oldest_recorded_weight = entries[num_entries - 1].weight;
            double newest_recorded_weight = entries[0].weight;
            double diff_weight = newest_recorded_weight - oldest_recorded_weight;

            total_change_display.Text = diff_weight.ToString();
        }
    }
}
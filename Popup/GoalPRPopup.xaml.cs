namespace WorkoutLog.Popup;

public partial class GoalPRPopup
{
	public GoalPRPopup()
	{
		InitializeComponent();
    }

    /* creates an exercise along with the PR */
    private async void Submit_Goal(object sender, EventArgs e)
    {
        string name = exercise_name_entry.Text;
        DateTime date;
        bool has_desired;

        if (date_included_toggle.IsToggled) /* if pr goal date included */
        {
            date = achieve_by_date.Date;
            has_desired = true;
        }
        else /* else pr goal date not included */
        {
            date = DateTime.Now;
            has_desired = false;
        }

        if (exercise_type_toggle.IsToggled) /* if time pr */
        {
            string min_update_string = min_pr.Text.ToString();
            int min_update = int.Parse(min_update_string);

            string sec_update_string = sec_pr.Text.ToString();
            int sec_update = int.Parse(sec_update_string);

            int hours = min_update / 60;
            int mins = min_update % 60;

            await App.RecordRepo.Add_Goal_PR(name, date, has_desired, false, -1, hours, mins, sec_update);
        }
        else /* else weight pr */
        {
            string weight_update_string = weight_pr.Text.ToString();
            int weight_update = int.Parse(weight_update_string);

            await App.RecordRepo.Add_Goal_PR(name, date, has_desired, true, weight_update, -1, -1, -1);
        }

        Close();
    }

    /* closes popup for creating an exercise */
    private void Cancel_Goal(object sender, EventArgs e)
    {
        Close();
    }

    /* handles event when toggled between weight and time PR type */
    private void exercise_type_change(object sender, EventArgs e)
    {
        /* if changed to toggle on weight PR */
        if (time_display.IsVisible)
        {
            weight_display.IsVisible = true;
            time_display.IsVisible = false;
        }
        else /* else changed to toggle on time PR */
        {
            weight_display.IsVisible = false;
            time_display.IsVisible = true;
        }
    }

    /* handles event when toggled between goal date and no date */
    private void date_included_change(object sender, EventArgs e)
    {
        if (date_included_toggle.IsToggled) /* if date desired */
        {
            record_date_display.IsVisible = true;
            no_date_display.IsVisible = false;
        }
        else /* else no date desired */
        {
            record_date_display.IsVisible = false;
            no_date_display.IsVisible = true;
        }
    }
}

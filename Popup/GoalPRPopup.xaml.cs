namespace WorkoutLog.Popup;

public partial class GoalPRPopup
{
	public GoalPRPopup()
	{
		InitializeComponent();
        achieve_by_date.MinimumDate = DateTime.Now;
    }

    /* creates an exercise along with the PR */
    private async void Submit_Goal(object sender, EventArgs e)
    {
        string name = exercise_name_entry.Text;

        if (name != null && name.Length != 0) /* if name field is not empty */
        {
            name = name.Trim(); /* removes leading and trailing whitespace */

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
                string min_update_string = min_pr.Text;
                string sec_update_string = sec_pr.Text;

                if (min_update_string != null && sec_update_string != null
                        && min_update_string.Length != 0 && sec_update_string.Length != 0) /* if time fields are not empty */
                {
                    min_update_string = min_update_string.ToString();
                    sec_update_string = sec_update_string.ToString();

                    int min_update = int.Parse(min_update_string);
                    int sec_update = int.Parse(sec_update_string);

                    int hours = min_update / 60;
                    int mins = min_update % 60;

                    await App.RecordRepo.Add_Goal_PR(name, date, has_desired, false, -1, hours, mins, sec_update);

                    error_prompt.IsVisible = false;
                    Close();
                }
                else /* else; time fields are empty */
                {
                    error_prompt.Text = "Goal time values cannot be empty";
                    error_prompt.IsVisible = true;
                }
            }
            else /* else weight pr */
            {
                string weight_update_string = weight_pr.Text;

                if (weight_update_string != null && weight_update_string.Length != 0)
                {
                    weight_update_string = weight_update_string.ToString();

                    int weight_update = int.Parse(weight_update_string);
                    await App.RecordRepo.Add_Goal_PR(name, date, has_desired, true, weight_update, -1, -1, -1);

                    error_prompt.IsVisible = false;
                    Close();
                }
                else
                {
                    error_prompt.Text = "Goal weight value cannot be empty";
                    error_prompt.IsVisible = true;
                }
            }
        }
        else /* else; name field is empty */
        {
            error_prompt.Text = "Goal name cannot be empty";
            error_prompt.IsVisible = true;
        }
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

namespace WorkoutLog.Popup;

public partial class GoalBodyWeightPopup
{
	public GoalBodyWeightPopup()
	{
		InitializeComponent();
    }

    /* creates an exercise along with the PR */
    private async void Submit_Goal(object sender, EventArgs e)
    {
        string goal_name = goal_name_entry.Text;
        goal_name = goal_name.Trim(); /* removes leading and trailing whitespace */

        string weight_update_string = weight_entry.Text.ToString();
        int weight_update = int.Parse(weight_update_string);

        DateTime date;
        bool has_desired;

        if (date_included_toggle.IsToggled) /* if pr goal date included */
        {
            date = weight_date.Date;
            has_desired = true;
        }
        else /* else pr goal date not included */
        {
            date = DateTime.Now;
            has_desired = false;
        }

        await App.RecordRepo.Add_Goal_Body_Weight(goal_name, date, has_desired, weight_update);

        Close();
    }

    /* closes popup for creating an exercise */
    private void Cancel_Goal(object sender, EventArgs e)
    {
        Close();
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

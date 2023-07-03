using WorkoutLog.Model;

namespace WorkoutLog.Popup;

public partial class UpdatePR
{
    private PR updating_pr;

	public UpdatePR(string pr_name)
	{
		InitializeComponent();
        Get_PR_Data(pr_name);
    }

    /* creates an exercise along with the PR */
    private async void Submit_PR_Update(object sender, EventArgs e)
    {
        string name = updating_pr.exercise_name;
        DateTime date = record_date.Date;

        if (updating_pr.is_weight_pr == false) /* if time pr */
        {
            string min_update_string = min_pr.Text.ToString();
            int min_update = int.Parse(min_update_string);

            string sec_update_string = sec_pr.Text.ToString();
            int sec_update = int.Parse(sec_update_string);

            int hours = min_update / 60;
            int mins = min_update % 60;

            await App.RecordRepo.Update_PR(name, date, -1, hours, mins, sec_update);
        }
        else /* else weight pr */
        {
            string weight_update_string = weight_pr.Text.ToString();
            int weight_update = int.Parse(weight_update_string);

            await App.RecordRepo.Update_PR(name, date, weight_update, -1, -1, -1);
        }

        Close();
    }

    /* closes popup for creating an exercise */
    private void Cancel_PR_Update(object sender, EventArgs e)
    {
        Close();
    }

    /* gets pr data from database */
    private async void Get_PR_Data(string pr_name)
    {
        update_pr_exercise_name.Text = pr_name;
        updating_pr = await App.RecordRepo.Get_PR(pr_name);

        if (updating_pr.is_weight_pr) /* if updating pr is a weight pr */
        {
            time_display.IsVisible = false;
            weight_display.IsVisible = true;
        }
        else /* else updating pr is a time pr */
        {
            time_display.IsVisible = true;
            weight_display.IsVisible = false;
        }
    }
}
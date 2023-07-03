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
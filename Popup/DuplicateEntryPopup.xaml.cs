using Android.Media.TV;

namespace WorkoutLog.Popup;

public partial class DuplicateEntryPopup
{
	public DuplicateEntryPopup(string table_name)
	{
		InitializeComponent();

		/* if duplicate entry is a body progression entry */
		if (table_name == "Body Progression")
		{
			Display_Body_Progression(table_name);
        }
		else if (table_name == "Body Weight") /* else if duplicate entry is a body weight entry */
        {
            Display_Body_Weight(table_name);
        }
        else if (table_name == "Body Weight Goal") /* else if duplicate entry is a body weight goal entry */
        {
            Display_Body_Weight_Goal(table_name);
        }
        else if (table_name == "PR Goal") /* else if duplicate entry is a pr goal entry */
        {
            Display_PR_Goal(table_name);
        }
        else if (table_name == "PR") /* else if duplicate entry is a PR entry */
        {
            Display_PR(table_name);
        }
        else if (table_name == "Category") /* else if duplicate entry is a calendar category entry */
        {
            Display_Category(table_name);
        }
        else /* else duplicate entry is a notes entry */
        {
            Display_Notes(table_name);
        }
    }

	/* displays body progression duplicate info */
	private void Display_Body_Progression(string table_name)
	{
        duplicate_table_display.Text = table_name;

        string prompt = $"The {table_name} entry is a duplicate.";
        string prompt2 = $"The {table_name} entry was not added as a duplicate.";

        duplicate_table_prompt_display.Text = prompt;
        duplicate_table_prompt2_display.Text = prompt2;
    }

    /* todo displays body weight duplicate info */
    private void Display_Body_Weight(string table_name)
    {
        
    }

    /* todo displays body weight goal duplicate info */
    private void Display_Body_Weight_Goal(string table_name)
    {

    }

    /* todo displays PR goal duplicate info */
    private void Display_PR_Goal(string table_name)
    {

    }

    /* todo displays PR duplicate info */
    private void Display_PR(string table_name)
    {

    }

    /* todo displays category duplicate info */
    private void Display_Category(string table_name)
    {

    }

    /* todo displays notes duplicate info */
    private void Display_Notes(string table_name)
    {

    }

    /* closes the popup alert for duplicate */
    private void Close_Popup(object sender, EventArgs e)
	{
		Close();
	}
}
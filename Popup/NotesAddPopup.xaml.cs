using WorkoutLog.Model;

namespace WorkoutLog.Popup;

public partial class NotesAddPopup
{
	private int max_char_value { get; set; }
	private int current_char_value { get; set; }

	public NotesAddPopup()
	{
		InitializeComponent();
		       
		current_char_value = 0;
        max_char_value = 300;

        current_char_value_display.Text = current_char_value.ToString();
        max_char_value_display.Text = max_char_value.ToString();
    }

	/* refreshes the current char value of the note text box */
	private void Refresh_Word_Count(object sender, EventArgs e)
	{
		current_char_value = text_box_display.Text.Length;
        current_char_value_display.Text = current_char_value.ToString();
    }

    /* saves the note into the database */
    private async void Submit_Record(object sender, EventArgs e)
    {
        string name = note_title_content.Text;
        
        if (name != null && name.Length != 0) /* if name field is not empty */
        {
            /* name appearance tweaking */
            name = name.Trim();
            name = string.Concat(char.ToUpper(name[0]), name.Substring(1));

            if (name != null && name.Length != 0) /* if name field is not empty after trim */
            {
                string note_content = text_box_display.Text;

                if (note_content == null || note_content.Length == 0) /* if no note content */
                {
                    note_content = "";
                }

                List<Note> notes_list_before = await App.RecordRepo.Get_All_Notes();
                await App.RecordRepo.Add_Note(name, note_content);
                List<Note> notes_list_after = await App.RecordRepo.Get_All_Notes();

                error_prompt.IsVisible = false;

                if (notes_list_before.Count == notes_list_after.Count) /* if duplicate entry */
                {
                    Close(false);
                }
                else /* else; valid entry; not a duplicate */
                {
                    Close(true);
                }
            }
            else /* else; name field is empty after trim */
            {
                error_prompt.Text = "Note title cannot be empty";
                error_prompt.IsVisible = true;
            }
        }
        else /* else; name field is empty */
        {
            error_prompt.Text = "Note title cannot be empty";
            error_prompt.IsVisible = true;
        }
    }

    /* closes the note popup */
    private void Cancel_Record(object sender, EventArgs e)
    {
        Close();
    }
}
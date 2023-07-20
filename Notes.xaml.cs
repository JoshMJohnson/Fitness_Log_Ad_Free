using CommunityToolkit.Maui.Views;
using WorkoutLog.Popup;
using WorkoutLog.Model;

namespace WorkoutLog;

public partial class Notes : ContentPage
{
    private VerticalStackLayout vertical_layout_empty_notes_list;

    public Notes()
	{
		InitializeComponent();
        Empty_Notes_Display();

    }

    /* creates message for empty categories */
    private void Empty_Notes_Display()
    {
        /* display when PR list is empty */
        vertical_layout_empty_notes_list = new VerticalStackLayout();

        vertical_layout_empty_notes_list.VerticalOptions = LayoutOptions.Center;
        vertical_layout_empty_notes_list.HorizontalOptions = LayoutOptions.Center;

        vertical_layout_empty_notes_list.Add(new Label
        {
            Text = "No Notes!",
            FontSize = 20,
            FontAttributes = FontAttributes.Italic,
            HorizontalOptions = LayoutOptions.Center
        });

        vertical_layout_empty_notes_list.Add(new Image
        {
            Source = "empty_list.png",
            HeightRequest = 200
        });

        Grid empty_display = notes_layout;
        Grid.SetRow(vertical_layout_empty_notes_list, 0);
        empty_display.Add(vertical_layout_empty_notes_list);
    }

    /* todo executed when the Notes plus button clicked */
    private async void Add_Note(object sender, EventArgs e)
    {
        await this.ShowPopupAsync(new NotesAddPopup());
        Refresh_Notes();
    }

    /* todo executed when a Note is selected to be removed */
    private async void Remove_Note(object sender, EventArgs e)
    {

    }

    /* todo updates a Note */
    private async void Update_Note(object sender, EventArgs e)
    {

    }

    /* todo refreshes the display of notes */
    public async void Refresh_Notes()
    {

    }
}
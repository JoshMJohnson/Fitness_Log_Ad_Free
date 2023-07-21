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
        Refresh_Notes();

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

    /* executed when the Notes plus button clicked */
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

    /* ? refreshes the display of notes */
    public async void Refresh_Notes()
    {
        List<Note> notes_list = await App.RecordRepo.Get_All_Notes();

        notes_display.ItemsSource = notes_list;

        if (notes_list.Count == 0)  /* if pr list is empty - no pr's set */
        {
            vertical_layout_empty_notes_list.IsVisible = true;
        }
        else  /* else pr list is not empty */
        {
            vertical_layout_empty_notes_list.IsVisible = false;
        }


    }
}
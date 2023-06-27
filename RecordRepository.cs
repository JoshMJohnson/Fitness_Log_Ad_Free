using SQLite;
using System.Net;
using WorkoutLog.Model;

namespace WorkoutLog;

public class RecordRepository
{
	private static SQLiteAsyncConnection conn;

	/* constructor - creates a database */
	public RecordRepository(string db_path)
	{
		conn = new SQLiteAsyncConnection(db_path);
	}

    /* * body progression section */
    /* todo adds an entry to the body progression table within the database */
    public async Task Add_Progression()
	{

	}

    /* todo removes an entry in the body progression table within the database */
    public async Task Remove_Progression()
    {

    }

    /* todo returns a list of progressions from the database */
    public async Task Get_Progression_List()
    {

    }

    /* * body weight section */
    /* todo adds an entry to the body weight table within the database */
    public async Task Add_Body_Weight()
    {

    }

    /* todo edits an entry in the body weight table within the database */
    public async Task Edit_Body_Weight()
    {

    }

    /* todo removes an entry in the body weight table within the database */
    public async Task Remove_Body_Weight()
    {

    }

    /* todo returns a list of body weight entries from the database */
    public async Task Get_Body_Weight_List()
    {

    }

    /* * body weight goals section */
    /* todo adds a body weight entry to the goal table within the database */
    public async Task Add_Goal_Body_Weight()
    {

    }

    /* todo edits a body weight entry in the goal table within the database */
    public async Task Edit_Goal_Body_Weight()
    {

    }

    /* todo removes a body weight entry in the goal table within the database */
    public async Task Remove_Goal_Body_Weight()
    {

    }

    /* todo returns a list of body weight goals from the database */
    public async Task Get_Body_Weight_Goal_List()
    {

    }

    /* * pr goals section */
    /* todo adds a pr entry to the goal table within the database */
    public async Task Add_Goal_PR()
    {

    }

    /* todo edits a pr entry in the goal table within the database */
    public async Task Edit_Goal_PR()
    {

    }

    /* todo removes a pr entry in the goal table within the database */
    public async Task Remove_Goal_PR()
    {

    }

    /* todo returns a list of pr goals from the database */
    public async Task Get_Goal_PR_List()
    {

    }

    /* * personal records section */
    /* todo adds a pr entry to the pr table within the database */
    public async Task Add_PR()
    {

    }

    /* todo edits a pr entry in the pr table within the database */
    public async Task Edit_PR()
    {

    }

    /* todo removes a pr entry in the pr table within the database */
    public async Task Remove_PR()
    {

    }

    /* todo returns a list of PR's from the database */
    public async Task Get_PR_List()
    {

    }

    /* * workout calendar section*/
    /* todo adds an entry to the workout calendar table within the database */
    public async Task Add_Calendar_Entry()
    {

    }

    /* todo edits an entry in the workout calendar table within the database */
    public async Task Edit_Calendar_Entry()
    {

    }

    /* todo removes an entry in the workout calendar table within the database */
    public async Task Remove_Calendar_Entry()
    {

    }

    /* todo returns a list of calendar entries from the database */
    public async Task Get_Calendar_Entries_List()
    {

    }

    /* todo adds a category to the categories table within the database */
    public async Task Add_Calendar_Category()
    {

    }

    /* todo removes a category in the categories table within the database */
    public async Task Remove_Calendar_Category()
    {

    }

    /* todo returns a list of calendar categories from the database */
    public async Task Get_Calendar_Category_List()
    {

    }
}
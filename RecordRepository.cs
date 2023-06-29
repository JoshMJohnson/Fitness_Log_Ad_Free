using SQLite;
using System.Net;
using System.Xml.Linq;
using WorkoutLog.Model;
using static SQLite.SQLite3;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WorkoutLog;

public class RecordRepository
{
	private static SQLiteAsyncConnection conn;
    private string database_path;
    public string status_message { get; set; }

	/* constructor - creates a database */
	public RecordRepository(string db_path)
	{
        database_path = db_path;
	}

    /* initializes database */
    private async Task Init_Database()
    {
        if (conn != null)
        {
            return;
        }

        conn = new SQLiteAsyncConnection(database_path); /* create database */

        /* create database tables */
        await conn.CreateTableAsync<BodyWeight>();
        await conn.CreateTableAsync<Calendar>();
        await conn.CreateTableAsync<Category>();
        await conn.CreateTableAsync<Goal>();
        await conn.CreateTableAsync<PR>();
        await conn.CreateTableAsync<Progression>();
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
    /* ? adds a pr entry to the goal table within the database */
    public async Task Add_Goal_PR(string goal_name, DateTime date, bool is_weight, bool is_pr, bool is_body_weight,
                                    int goal_weight, int hours, int mins, int sec)
    {
        ArgumentNullException.ThrowIfNull(goal_name, nameof(goal_name));
        ArgumentNullException.ThrowIfNull(date, nameof(date));
        ArgumentNullException.ThrowIfNull(is_weight, nameof(is_weight));
        ArgumentNullException.ThrowIfNull(is_pr, nameof(is_pr));
        ArgumentNullException.ThrowIfNull(is_body_weight, nameof(is_body_weight));
        ArgumentNullException.ThrowIfNull(goal_weight, nameof(goal_weight));
        ArgumentNullException.ThrowIfNull(hours, nameof(hours));
        ArgumentNullException.ThrowIfNull(mins, nameof(mins));
        ArgumentNullException.ThrowIfNull(sec, nameof(sec));

        try
        {
            await Init_Database();

            Goal new_goal = new Goal
            {
                name = goal_name,
                goal_archieve_by_date = date,
                is_weight_goal = is_weight,
                is_pr_goal = is_pr,
                is_body_weight_goal = is_body_weight,
                weight = goal_weight,
                time_hours = hours,
                time_min = mins,
                time_sec = sec
            };

            int result = await conn.InsertAsync(new_goal);

            status_message = string.Format("{0} goal added (Goal name: {1})", result, goal_name);
        }
        catch (Exception e)
        {
            status_message = string.Format("Failed to add goal: {0}. Error: {1}", goal_name, e.Message);
        }
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
    /* ? adds a pr entry to the pr table within the database */
    public async Task Add_PR(string name, DateTime date, bool is_weight_pr_type,
                                int weight_par, int hours, int min, int sec)
    {
        ArgumentNullException.ThrowIfNull(name, nameof(name));
        ArgumentNullException.ThrowIfNull(date, nameof(date));
        ArgumentNullException.ThrowIfNull(is_weight_pr_type, nameof(is_weight_pr_type));
        ArgumentNullException.ThrowIfNull(weight_par, nameof(weight_par));
        ArgumentNullException.ThrowIfNull(hours, nameof(hours));
        ArgumentNullException.ThrowIfNull(min, nameof(min));
        ArgumentNullException.ThrowIfNull(sec, nameof(sec));

        try
        {
            await Init_Database();

            PR new_pr = new PR
            {
                exercise_name = name,
                date_achieved = date,
                is_weight_pr = is_weight_pr_type,
                weight = weight_par,
                time_hours = hours,
                time_min = min,
                time_sec = sec
            };

            int result = await conn.InsertAsync(new_pr);

            status_message = string.Format("{0} PR added (PR Name: {1})", result, name);
        }
        catch (Exception e)
        {
            status_message = string.Format("Failed to add {0}. Error: {1}", name, e.Message);
        }
    }

    /* todo edits a pr entry in the pr table within the database */
    public async Task Edit_PR()
    {

    }

    /* ? removes a pr entry in the pr table within the database */
    public async Task Remove_PR(string exercise_name)
    {
        ArgumentNullException.ThrowIfNull(exercise_name, nameof(exercise_name));

        try
        {
            await Init_Database();
            PR removing_pr = await conn.FindAsync<PR>(exercise_name);
            await conn.DeleteAsync(removing_pr);
        }
        catch (Exception e)
        {
            status_message = string.Format("Failed to remove {0}. Error: {1}", exercise_name, e.Message);
        }
    }

    /* todo returns a list of PR's from the database */
    public async Task Get_PR_List()
    {

    }

    /* * workout calendar section*/
    /* ? adds an entry to the workout calendar table within the database */
    public async Task Add_Calendar_Entry(DateTime date, Category category)
    {
        ArgumentNullException.ThrowIfNull(date, nameof(date));
        ArgumentNullException.ThrowIfNull(category, nameof(category));

        try
        {
            await Init_Database();

            Calendar calendar_entry = new Calendar
            {
                entry_date = date,
                calendar_category = category
            };

            int result = await conn.InsertAsync(calendar_entry);

            status_message = string.Format("{0} calendar entry made for {1})", result, date);
        }
        catch (Exception e)
        {
            status_message = string.Format("Failed to add calendar entry on {0}. Error: {1}", date, e.Message);
        }
    }

    /* todo edits an entry in the workout calendar table within the database */
    public async Task Edit_Calendar_Entry()
    {

    }

    /* todo removes an entry in the workout calendar table within the database */
    public async Task Remove_Calendar_Entry(DateTime date, Category category)
    {
        ArgumentNullException.ThrowIfNull(date, nameof(date));
        ArgumentNullException.ThrowIfNull(category, nameof(category));

        try
        {
            await Init_Database();





            int result = 1;

            status_message = string.Format("{0} calendar entry removed (Date: {1})", result, date);
        }
        catch (Exception e)
        {
            status_message = string.Format("Failed to remove calendar entry on {0}. Error: {1}", date, e.Message);
        }
    }

    /* todo returns a list of calendar entries from the database */
    public async Task Get_Calendar_Entries_List()
    {

    }

    /* ? adds a category to the categories table within the database */
    public async Task Add_Calendar_Category(string category_name, Color category_color)
    {
        ArgumentNullException.ThrowIfNull(category_name, nameof(category_name));
        ArgumentNullException.ThrowIfNull(category_color, nameof(category_color));

        try
        {
            await Init_Database();

            Category new_category = new Category
            {
                name = category_name,
                color = category_color
            };

            int result = await conn.InsertAsync(new_category);

            status_message = string.Format("{0} category added (Category Name: {1})", result, category_name);
        }
        catch (Exception e)
        {
            status_message = string.Format("Failed to add category {0}. Error: {1}", category_name, e.Message);
        }
    }
    
    /* ? removes a category in the categories table within the database */
    public async Task Remove_Calendar_Category(string category_name)
    {
        ArgumentNullException.ThrowIfNull(category_name, nameof(category_name));

        try
        {
            await Init_Database();

            Category removing_category = await conn.FindAsync<Category>(category_name);
            int result = await conn.DeleteAsync(removing_category);

            status_message = string.Format("{0} category removed (Category Name: {1})", result, category_name);
        }
        catch (Exception e)
        {
            status_message = string.Format("Failed to remove category {0}. Error: {1}", category_name, e.Message);
        }
    }

    /* todo returns a list of calendar categories from the database */
    public async Task Get_Calendar_Category_List()
    {

    }
}
using Newtonsoft.Json;
using Questao2;
using System.Web;

public class Program
{

    private static string _url = "https://jsonmock.hackerrank.com/api/football_matches";
    public static void Main()
    {
        string teamName = "Paris Saint-Germain";
        int year = 2013;
        int totalGoals = getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        teamName = "Chelsea";
        year = 2014;
        totalGoals = getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        // Output expected:
        // Team Paris Saint - Germain scored 109 goals in 2013
        // Team Chelsea scored 92 goals in 2014
    }

    public static int getTotalScoredGoals(string team, int year)
    {
        var totalScoredGoals = 0;
        var page = 1;
        for (int teamId = 1; teamId < 3; teamId++)
        {
            while (page > 0)
            {
                NextGoals apiResponse = getNextGoals(team, year, page, teamId);
                if (apiResponse != null)
                {
                    totalScoredGoals += apiResponse.Goals;
                    page = apiResponse.NextPage;
                }
                else
                {
                    page = -1;
                }
            }
            page = 1;
        }

        return totalScoredGoals;
    }


    public static NextGoals getNextGoals(string team, int year, int pageId, int teamid)
    {
        var builder = new UriBuilder(_url);
        var query = HttpUtility.ParseQueryString(builder.Query);
        query["year"] = year.ToString();
        query[$"team{teamid}"] = team.ToString();
        query["page"] = pageId.ToString();

        builder.Query = query.ToString();
        var url = builder.ToString();

        try
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(url).Result;
            response.EnsureSuccessStatusCode();
            string responseBody = response.Content.ReadAsStringAsync().Result;

            var apiResponse = JsonConvert.DeserializeObject<ApiResponse> (responseBody);
            if (apiResponse != null)
            {
                int goals = 0;
                if (teamid == 1)
                {
                    goals = apiResponse.Data.Sum(m => int.Parse(m.Team1Goals));
                }
                else
                {
                    goals = apiResponse.Data.Sum(m => int.Parse(m.Team2Goals));
                }

                int nextpage = apiResponse.Page + 1;
                if (nextpage > apiResponse.TotalPages)
                {
                    nextpage = -1;
                }
                return new NextGoals { Goals = goals, NextPage = nextpage };
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }
        return null;
    }

}
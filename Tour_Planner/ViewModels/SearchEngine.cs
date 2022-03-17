namespace Tour_Planner
{
    public class SearchEngine : ISearchEngine
    {
        public string[] SearchFor(string searchText)
        {
            return new[] { $"Search Result 1 for {searchText}", $"Search Result 2 for {searchText}", $"Search Result 3 for {searchText}" };
        }
    }
}
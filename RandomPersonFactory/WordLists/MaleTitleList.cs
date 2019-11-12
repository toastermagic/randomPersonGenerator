namespace easygoingsoftware.People
{
    public static partial class WordLists
    {
        public static WeightedList<string, double> MaleTitles {
            get {
                return new WeightedList<string, double> {
                    { "Mr", 0.00 },
                    { "Dr", 0.75 },
                    { "Prof", 0.85 },
                    { "Rev", 0.95 },
                };
            }
        }
    }
}
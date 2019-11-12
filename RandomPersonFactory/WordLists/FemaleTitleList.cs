namespace easygoingsoftware.People
{
    public static partial class WordLists
    {
        /// <summary>
        /// List of 'female' titles ie. Mrs, Dr, Rev...
        /// </summary>
        public static WeightedList<string, double> FemaleTitles {
            get {
                return new WeightedList<string, double> {
                    { "Miss", 0.00 },
                    { "Ms", 0.20},
                    { "Mrs", 0.40 },
                    { "Dr", 0.75 },
                    { "Prof", 0.85 },
                    { "Rev", 0.95 },
                };
            }
        }
    }
}
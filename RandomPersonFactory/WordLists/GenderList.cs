namespace easygoingsoftware.People
{
    public static partial class WordLists
    {
        /// <summary>
        /// Binary list of genders ie. Male, Female...
        /// </summary>
        public static WeightedList<string, double> Genders {
            get {
                return new WeightedList<string, double> {
                    { "Male", 0.00 },
                    { "Female", 0.50 },
                };
            }
        }
    }
}
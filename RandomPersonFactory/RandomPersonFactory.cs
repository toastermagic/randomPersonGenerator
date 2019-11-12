using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace easygoingsoftware.People
{
    public class RandomPersonFactory
    {
        /// <summary>
        /// Generates random company name in one of three formats
        /// </summary>
        /// <returns>Company name</returns>
        private static string GetCompanyName()
        {
            var nameType = RNG.R.NextDouble();

            if (nameType < 0.3)
            {
                return $"{WordLists.Adjectives.GetRandom()} {WordLists.Nouns.GetRandom()} {WordLists.IndustryList.GetRandom()}";
            }
            else if (nameType < 0.6)
            {
                return $"{WordLists.Adjectives.GetRandom()} {WordLists.IndustryList.GetRandom()} of {WordLists.CityList.GetRandom()}";
            }
            else
            {
                return $"{WordLists.LastNameList.GetRandom()} {WordLists.Adjectives.GetRandom()} {WordLists.Nouns.GetRandom()}s";
            }
        }

        /// <summary>
        /// Generates random postcode in British format (excluding armed forces postcodes)
        /// </summary>
        /// <param name="city">Can generate more 'realistic' postcode if city is provided</param>
        /// <returns>City name</returns>
        private static string GeneratePostcode(string city = null)
        {
            string part1 = null;

            if (city == null)
                part1 = RNG.GetRandomLetter() + RNG.GetRandomNumberString(2);
            else
                part1 = city.Substring(0, 1) + RNG.GetRandomNumberString(2);

            var part2 = $"{RNG.GetRandomNumberString(1):0}{RNG.GetRandomLetter()}{RNG.GetRandomLetter()}";

            return $"{part1} {part2}".Trim();
        }

        /// <summary>
        /// Generates random phone numbers in UK landline or mobile format
        /// </summary>
        /// <param name="isMobile">Whether the number should start '07' (for UK mobile)</param>
        /// <returns>Phone number</returns>
        private static string GeneratePhoneNumber(bool isMobile = false)
        {
            if (isMobile)
                return $"07{RNG.GetRandomNumberString(3)} {RNG.GetRandomNumberString(6)}";
            else
                return $"0{RNG.R.Next(1, 6):0}{RNG.GetRandomNumberString(3)} {RNG.GetRandomNumberString(6)}";
        }

        /// <summary>
        /// Returns a random job title from the ISO list of job titles
        /// </summary>
        /// <returns>Job title</returns>
        private static string GetJobTitle()
        {
            return WordLists.Professions.GetRandom();
        }

        /// <summary>
        /// Returns 'gender appropriate' first name (this does not necessarily reflect recent culture and is currently Western centric).
        /// </summary>
        /// <param name="gender">M or F or neither (neither will pick at random)</param>
        /// <returns>First name</returns>
        private static string GetFirstName(string gender)
        {
            string[] nameList;

            switch (gender.Substring(0, 1).ToUpper())
            {
                case "M":
                    nameList = WordLists.MaleNameList;
                    break;
                case "F":
                    nameList = WordLists.FemaleNameList;
                    break;
                default:
                    nameList = (RNG.R.NextDouble() > 0.5) ? WordLists.MaleNameList : WordLists.FemaleNameList;
                    break;
            }

            return nameList.GetRandom();
        }

        /// <summary>
        /// Generates Western/UK style surnames.
        /// Small chance of returning double barrelled or Irish "O'Malley" style names
        /// </summary>
        /// <returns>Surname</returns>
        private static string GetLastName()
        {
            var lastName = WordLists.LastNameList.GetRandom();

            // 1% O'Prefix
            if (RNG.R.NextDouble() > 0.99)
                lastName = "O'" + lastName;

            //  2% double barrelled
            if (RNG.R.NextDouble() > 0.98)
                lastName = lastName + "-" + WordLists.LastNameList.GetRandom();

            return lastName;
        }

        /// <summary>
        /// Returns title picked from lists of gendered pronouns (probably very unpopular these days)
        /// </summary>
        /// <param name="gender">M or F or neither (neither will pick at random)</param>
        /// <returns>Title</returns>
        private static string GetTitle(string gender)
        {
            switch (gender.Substring(0, 1).ToUpper())
            {
                case "M":
                    return WordLists.MaleTitles.GetItem();
                case "F":
                    return WordLists.FemaleTitles.GetItem();
                default:
                    return RNG.R.NextDouble() > 0.5 ? WordLists.MaleTitles.GetItem() : WordLists.FemaleTitles.GetItem();
            }
        }

        /// <summary>
        /// Returns random date of birth given minimum and maximum ages
        /// </summary>
        /// <param name="minAge">Minimum age (years)</param>
        /// <param name="maxAge">Maximum age (years)</param>
        /// <returns>Date of birth</returns>
        private static DateTime GetDateOfBirth(int minAge = 20, int maxAge = 70)
        {
            return DateTime.Today.AddYears(-1 * (RNG.R.Next(0, maxAge - minAge) + minAge)).AddDays(RNG.R.Next(0, 365));
        }

        /// <summary>
        /// Returns random gender (M or F)
        /// </summary>
        /// <returns>Gender</returns>
        private static string GetGender()
        {
            return WordLists.Genders.GetItem();
        }

        /// <summary>
        /// Generates flat or apartment number prefix for address
        /// </summary>
        /// <returns>Flat or apartment number</returns>
        private static string GenerateFlatApartmentNumber() {
            var sb = new StringBuilder();

            sb.Append(RNG.R.NextDouble() > 0.5 ? "Flat " : "Apt ");

            sb.Append(RNG.R.Next(1, 20));

            if (RNG.R.NextDouble() > 0.7) {
                var aptLetter = RNG.GetRandomLetter('F');
                if (RNG.R.NextDouble() > 0.5) {
                    aptLetter = aptLetter.ToLower();
                }
                sb.Append(aptLetter);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Generates random PersonalDetails object
        /// </summary>
        /// <returns>PersonalDetails</returns>
        public static PersonalDetails GetRandomPerson()
        {
            var gender = GetGender();
            var city = WordLists.CityList.GetRandom();

            var p = new PersonalDetails()
            {
                Birthday = GetDateOfBirth(),
                Gender = gender,
                Title = GetTitle(gender),
                FirstName = GetFirstName(gender),
                LastName = GetLastName(),
                ResidenceNumber = RNG.R.Next(10, 1500).ToString(),
                StreetName = WordLists.StreetList.GetRandom(),
                Town = WordLists.TownList.GetRandom(),
                City = city,
                PostCode = GeneratePostcode(city),
                HomeTelephoneNumber = GeneratePhoneNumber(isMobile: false),
                MobileTelephoneNumber = GeneratePhoneNumber(isMobile: true),
                JobTitle = GetJobTitle(),
                CompanyName = GetCompanyName()
            };

            if (RNG.R.NextDouble() > 0.7) {
                p.FlatApartmentNumber = GenerateFlatApartmentNumber();
            }

            return p;
        }

        /// <summary>
        /// Generates PersonalDetails object from random seed, if the seed remains the same, the person generated will always have the same details
        /// </summary>
        /// <param name="seed">Any integer, string, Guid or byte array</param>
        /// <returns>PersonalDetails</returns>
        public static PersonalDetails GetRandomPerson(object seed)
        {
            switch(seed)
            {
                case int i: RNG.SetSeed(i);
                    break;
                case string s: RNG.SetSeed(s);
                    break;
                case Guid g: RNG.SetSeed(g);
                    break;
                case byte[] b: RNG.SetSeed(b);
                    break;
                default:
                    throw new ArgumentException("Unsupported seed type");
            }

            return GetRandomPerson();
        }

        /// <summary>
        /// Generates list of randomised people, either seeded or entirely new each time
        /// </summary>
        /// <param name="int">Number of people</param>
        /// <param name="seed">Any integer, string, Guid or byte array</param>
        /// <returns>PersonalDetails</returns>
        public static List<PersonalDetails> GetPeople(int numberOfPeople, bool random = false) {
            var people = new List<PersonalDetails>();
            for (var i = 0; i < 1000; i++)
            {
                if (random) {
                    people.Add(RandomPersonFactory.GetRandomPerson());
                } else {
                    people.Add(RandomPersonFactory.GetRandomPerson(i));
                }
            }
            return people;
        }
    }
}
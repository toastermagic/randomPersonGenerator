using System;
using System.Collections.Generic;
using System.Linq;

namespace easygoingsoftware.People
{
    /// <summary>
    /// Contains properties suitable for describing somebody's basic personal details, such as first name, last name, town, city and postcode
    /// </summary>
    public class PersonalDetails
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime Birthday { get; set; }
        public string HomeTelephoneNumber { get; set; }
        public string MobileTelephoneNumber { get; set; }
        public string FlatApartmentNumber { get; set; }
        public string ResidenceNumber { get; set; }
        public string StreetName { get; set; }
        public string Town { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public string JobTitle { get; set; }
        public string CompanyName { get; set; }
        public string Email => $"{FirstName.ToLower()}.{LastName.Replace("'", "").ToLower()}@invalidemail.com";
        public string Username => $"{FirstName.Substring(0, 1).ToLower()}{LastName.Replace("'","").ToLower()}";
        public string StreetAddress => string.Join(", ", new List<string> {
            FlatApartmentNumber,
            ResidenceNumber,
            StreetName,
            Town,
            City,
            PostCode
        }.Where(e => !string.IsNullOrEmpty(e)));
        
        public string HeaderRow
        {
            get
            {
                return string.Join(",", new List<string>
                {
                    "Title",
                    "FirstName",
                    "LastName",
                    "Username",
                    "Email",
                    "Gender",
                    "Birthday",
                    "HomeTelephoneNumber",
                    "MobileTelephoneNumber",
                    "FlatApartmentNumber",
                    "ResidenceNumber",
                    "StreetName",
                    "Town",
                    "City",
                    "PostCode",
                    "JobTitle",
                    "CompanyName"
                });
            }
        }

        public string ToCSV()
        {
            return string.Join(",", new List<string>{
                $"\"{this.Title}\"",
                $"\"{this.FirstName}\"",
                $"\"{this.LastName}\"",
                $"\"{this.Username}\"",
                $"\"{this.Email}\"",
                $"\"{this.Gender}\"",
                $"\"{this.Birthday.ToString("yyyy-MM-dd")}\"",
                $"\"{this.HomeTelephoneNumber}\"",
                $"\"{this.MobileTelephoneNumber}\"",
                $"\"{this.FlatApartmentNumber}\"",
                $"\"{this.ResidenceNumber}\"",
                $"\"{this.StreetName}\"",
                $"\"{this.Town}\"",
                $"\"{this.City}\"",
                $"\"{this.PostCode}\"",
                $"\"{this.JobTitle}\"",
                $"\"{this.CompanyName}\""
            });
        }
    }
}
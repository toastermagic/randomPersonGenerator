using System;
using System.Collections.Generic;
using easygoingsoftware.People;
using McMaster.Extensions.CommandLineUtils;
using Newtonsoft.Json;

namespace Whatever
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var app = new CommandLineApplication
            {
                Name = "Random People Generator",
                Description = "Creates lists of randomised people for testing purposes"
            };

            app.HelpOption();

            var formatOption = app.Option("-f|--format <Format>", "Output format (csv|json)", CommandOptionType.SingleValue);
            var outputOption = app.Option("-o|--output <Filename>", "Output filename", CommandOptionType.SingleValue);
            var numberOption = app.Option<int>("-n|--number <Count>", "Number of people to generate", CommandOptionType.SingleValue);
            var randomOption = app.Option("-r|--random", "Creates random list, or seeded list (same each time)", CommandOptionType.NoValue);

            app.OnExecuteAsync(async (token) =>
            {
                var numPeople = numberOption.HasValue() ? numberOption.ParsedValue : 10;
                var format = formatOption.HasValue() ? formatOption.Value() : "json";
                var random = randomOption.HasValue() ? true : false;
                var filename = outputOption.HasValue() ? outputOption.Value() : $"{AppDomain.CurrentDomain.BaseDirectory}people.{format}";

                using (System.IO.StreamWriter file = new System.IO.StreamWriter($"{filename}"))
                {
                    if (format == "csv")
                    {
                        file.WriteLine(new PersonalDetails().HeaderRow);
                    }
                    var people = new List<PersonalDetails>();
                    for (var i = 0; i < numPeople; i++)
                    {
                        var p = RandomPersonFactory.GetRandomPerson(random ? null : i);
                        if (format == "json")
                        {
                            people.Add(p);
                        }
                        else
                        {
                            file.WriteLine(p.ToCSV());
                            await file.FlushAsync();
                        }
                    }

                    if (format == "json")
                    {
                        await file.WriteAsync(JsonConvert.SerializeObject(people));
                    }
                }

                Console.WriteLine($"Wrote {numPeople} {(random ? "random" : "seeded")} people to {filename}");

                return 0;
            });

            app.Execute(args);
        }
    }
}

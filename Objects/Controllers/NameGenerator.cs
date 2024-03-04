using System;
using System.Collections.Generic;

namespace superautodungeon.Objects.Controllers;

public class NameGenerator
{
    public List<string> MaleFirstNames, FemaleFirstNames, LastNames;
    public NameGenerator()
    {
        MaleFirstNames = new() {
            "Gerald",
            "Alexander",
            "Archibald",
            "Cecil",
            "Frederick",
            "Henry",
            "Abraham",
            "Barnaby",
            "Bartholomew",
            "Calvin",
            "Chester",
            "Elias",
            "Oliver",
            "Samuel",
            "Theodore",
            "Alvin",
            "Paul",
            "Karl",
            "Aurelius",
            "Gregory",
            "Dennis",
            "Timothy"
        };

        FemaleFirstNames = new() {
            "Olivia",
            "Gwendolyn",
            "Genevieve",
            "Abigail",
            "Eleanor",
            "Suzanne",
            "Elspeth",
            "Maeve",
            "Candace",
            "Celeste",
            "Evelyn",
            "Florence",
            "Sabrina",
            "Vivienne",
            "Sonya",
            "Ada",
            "Clara",
            "Yvette"
        };

        LastNames = new() {
            "Abernathy",
            "Davenport",
            "Ellsworth",
            "Fairchild",
            "Garrison",
            "Hawthorne",
            "Ashwood",
            "Armstrong",
            "Dalton",
            "Clifford",
            "Thornton",
            "Ferdinand",
            "Lawrence",
            "Montgomery",
            "Seymour",
            "Wilbur"
        };
    }

    public string CreateMaleName()
    {
        Random random = new();
        return $"{MaleFirstNames[random.Next(MaleFirstNames.Count)]} {LastNames[random.Next(LastNames.Count)]}";
    }

    public string CreateFemaleName()
    {
        Random random = new();
        return $"{FemaleFirstNames[random.Next(FemaleFirstNames.Count)]} {LastNames[random.Next(LastNames.Count)]}";
    }
}
namespace Gdn.Web.Models.Report
{
    public class WorldModel
    {
        public string Territory { get; set; }

        public string Country { get; set; }

        public string Year { get; set; }

        public string Stats { get; set; }

        public WorldModel() { }

        public WorldModel(string territory, string country, string year, string stats)
        {
            Territory = territory;
            Year = year;
            Country = country;
            Stats = stats;
        }
    }
}
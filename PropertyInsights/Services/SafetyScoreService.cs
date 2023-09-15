using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PropertyInsights.Models;

namespace PropertyInsights.Services
{
    public class SafetyScoreService
    {
        private readonly HttpClient httpClient;
        private const string BaseDataEndpoint = "https://data.cityofchicago.org/resource/crimes.json";

        public SafetyScoreService()
        {
            httpClient = new HttpClient();
        }


        public async Task<double> GetSafetyScore(double latitude, double longitude)
        {
            var baseUri = "https://data.cityofchicago.org/resource/crimes.json?$where=year%3E=2019";

            var formattedUri = new Uri(baseUri + " AND within_circle(Location," + latitude.ToString() + "," + longitude.ToString() + ",300)");

            var httpRequest = new HttpRequestMessage(HttpMethod.Get, formattedUri);

            var response = await httpClient.SendAsync(httpRequest);

            var jsonText = await response.Content.ReadAsStringAsync();


            // var deserialized = JsonConvert.DeserializeObject<IEnumerable<Crimes>>(jsonText)?.ToList(); // IEnumerable is useless now
           //  var ward11Crimes = deserialized.Where(x => x.Ward == 11);
            // var numCrimes = deserialized.Count;
            // return numCrimes;


            // var numCrimesInCity = await GetNumCrimesInCity();

            var medianNumberOfCrimesPerWard = await GetNumWardCrimes();

            // var counter = deserialized?.Count();


            // Comparing inputted location with median
            var ward11Crimes = await GetCountOfCrimes(11);
            double doubleWard11Crimes = Convert.ToDouble(ward11Crimes);
            double doubleMedianNumberOfCrimesPerWard = Convert.ToDouble(medianNumberOfCrimesPerWard);
            double percentageDifference = CompareWithMedian(medianNumberOfCrimesPerWard, ward11Crimes);

            return percentageDifference;
        }

        private async Task<int> GetNumCrimesInCity()
        {
            var offset = 0;

            var cityCrimes = new List<Crimes>();

            while (true)
            {
                var formattedEndpoint = new Uri(BaseDataEndpoint + "?$where=year%3E=2023&$offset= " + offset + "&$order=:id");
                Console.WriteLine(formattedEndpoint);
                var response  = await GetCrimes(formattedEndpoint);
                cityCrimes.AddRange(response);

                offset += 1000;
                if (response.Count < 1000)
                {
                    break;
                }
            }
            return cityCrimes.Count;
        }

        private async Task<int> GetNumWardCrimes()
        {
            // var ward = 0;
            var crimesInWard = new List<int>(); // we will try to find median using this list of integers, which are # of crimes in each ward
            // var formattedEndpoint = new Uri(BaseDataEndpoint + "?$where=year%3E=2023&$offset= " + offset + "&$order=:id" + "&ward=" + ward);

            for (int wardNumber = 1; wardNumber < 51; wardNumber++)
            {
                /*var numCrimesInWard = 0;
                numCrimesInWard = await GetCountOfCrimes(wardNumber);*/

                crimesInWard.Add(await GetCountOfCrimes(wardNumber));
            }

            crimesInWard.Sort();
            return crimesInWard[crimesInWard.Count / 2];

            // Need to get a function that returns count of each ward then add to "crimesInWard" list
            // Can borrow code from "GetNumCrimesInCity" to get the loop for offset. Can borrow from "GetCrimes" for the JSON text
            // "GetNumCrimesInCity" already calls GetCrimes. Therefore only borrow from "GetNumCrimesInCity"

        }

        private async Task<List<Crimes>> GetCrimes(Uri endpoint)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, endpoint);
            httpRequest.Headers.Add("X-App-Token", "5FObvBiV6fJ47Ev42z45abccQ");

            var response = await httpClient.SendAsync(httpRequest);

            var jsonText = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<IEnumerable<Crimes>>(jsonText)?.ToList(); // IEnumerable is useless now
        }

        private async Task<int> GetCountOfCrimes(int ward)
        {
            var offset = 0;

            var cityCrimes = new List<Crimes>();

            while (true)
            {
                var formattedEndpoint = new Uri(BaseDataEndpoint + "?$where=year%3E=2023&$offset= " + offset + "&$order=:id" + "&ward=" + ward);
                // Console.WriteLine(formattedEndpoint); not sure why this is here
                var response = await GetCrimes(formattedEndpoint);
                cityCrimes.AddRange(response);

                offset += 1000;
                if (response.Count < 1000)
                {
                    break;
                }
            }
            return cityCrimes.Count;
        }

        private static double CompareWithMedian(double allWardMedian, double wardCrimes)
        {
            double percentageDifference = ((wardCrimes - allWardMedian) / allWardMedian) * 100;
            return percentageDifference;
        }
    }
}

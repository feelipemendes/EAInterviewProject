using CustomerSegmentationProcessor.Services.Responses;
using Newtonsoft.Json;

namespace CustomerSegmentationProcessor.Services
{
    public class PrizmApiService
	{
		private readonly HttpClient _httpClient;

        public PrizmApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<int> GetSegmentCodeAsync(string postalCode)
		{
			try
			{
                var response = await _httpClient.GetAsync($"api/pcode/get_segment?postal_code={postalCode}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<ApiResponse>(content);

                    if (result.Format == "multi" && result.Data != null)
                    {
                        var dataList = JsonConvert.DeserializeObject<List<ApiDataItem>>(result.Data.ToString());
                        return dataList.FirstOrDefault()?.PrizmId ?? 0;
                    }
                    else if (result.Format == "unique")
                    {
                        var resultToInt = int.Parse(result.Data.ToString());
                        return resultToInt;
                    }
                    else if (result.Format == "non_residential_zoning")
                    {
                        Console.WriteLine("No segment code assigned (non-residential zoning).");
                        return 0;
                    }
                    else
                    {
                        Console.WriteLine("Unknown format or no data.");
                        return 0;
                    }
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                    return 0;
                }
            }
			catch (Exception ex)
			{
                Console.WriteLine($"Error during the API request: {ex.Message}");
                return 0;
            }
		}


	}
}

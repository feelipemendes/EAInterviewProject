using CustomerSegmentationProcessor.Data;
using CustomerSegmentationProcessor.Services;

string path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "InputFiles", "CodingInterviewTestCustomerFileCSV_200.csv");


if (!File.Exists(path))
{
	Console.WriteLine("File cannot be found.");
	return;
}

try
{
	List<Customer> customers = CustomerFileParser.Parse(path);

    var client = new HttpClient()
    {
        BaseAddress = new Uri("https://prizm.environicsanalytics.com/"),
        Timeout = TimeSpan.FromSeconds(30) 
    };

    var prizmApiService = new PrizmApiService(client);

    var groupOne = new CustomerGroup("Group_1");
    var groupTwo = new CustomerGroup("Group_2");

    foreach (var customer in customers)
    {

        var segmentCode = await prizmApiService.GetSegmentCodeAsync(customer.PostalCode);

        if (segmentCode >= 1 && segmentCode <= 30)
        {
            groupOne.Customers.Add(customer);
        } else if (segmentCode >= 31 && segmentCode <= 67)
        {
            groupTwo.Customers.Add(customer);   
        }
    }

    Console.WriteLine(groupOne.ToString());
    Console.WriteLine(groupTwo.ToString());

}
catch (Exception ex)
{
	Console.WriteLine($"Error in file processor:{ex.Message}");
}
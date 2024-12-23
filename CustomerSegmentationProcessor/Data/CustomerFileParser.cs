namespace CustomerSegmentationProcessor.Data
{
	public class CustomerFileParser
	{
		public static List<Customer> Parse(string path)
		{
			var customers = new List<Customer>();
			var lines = File.ReadAllLines(path);

			foreach (var line in lines.Skip(1)) //skip the header
			{
				var col = line.Split(',');

				customers.Add(new Customer
				{
					StoreId = int.Parse(col[0]),
					CustomerId = col[1],
					PostalCode = col[2],
					TotalVisits = int.Parse(col[3]),
				});
			}

			return customers;
		}
	}
}

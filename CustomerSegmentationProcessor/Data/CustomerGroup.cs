namespace CustomerSegmentationProcessor.Data
{
	public class CustomerGroup
	{
        public CustomerGroup(string name)
        {
            Name = name;
            Customers = new List<Customer>();
        }

        public string Name { get; set; }
		public List<Customer> Customers { get; set; }
		public int TotalVisits => Customers.Sum(x => x.TotalVisits);

        public override string ToString()
        {
            return $"Target group name: {Name}, Total visits:{TotalVisits}";
        }
    }
}

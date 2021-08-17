namespace API.Entities
{
    public class Feature
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Explanation { get; set; }
        public string GroupName { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
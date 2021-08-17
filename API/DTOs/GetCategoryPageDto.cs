namespace API.DTOs
{
    public class GetCategoryPageDto
    {
        public int CategoryId { get; set; }
        public int PageNubmer { get; set; }
        public int ItemsPerPage { get; set; }
        public string SortBy { get; set; }
        public bool IsAscending { get; set; }

    }
}
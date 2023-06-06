namespace SampleRestApi.Application.Models
{
    public class DataResponseModel<T> where T : class
    {
        public List<T> Items { get; set; } = new List<T>();
    }
}

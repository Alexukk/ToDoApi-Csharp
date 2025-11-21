namespace ToDoApi.DTOs
{
    public class GetTaskDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public string CreatedAt { get; set; }
    }
}

namespace API.Models.Entities
{
    public class ResponseContent
    {
        public Status Status { get; set; }
        public object Content { get; set; } = new { };
    }
    public enum Status
    {
        Erro = 0,
        Ok = 1,
    }
}

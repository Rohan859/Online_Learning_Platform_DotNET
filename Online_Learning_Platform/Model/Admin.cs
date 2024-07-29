namespace Online_Learning_Platform.Model
{
    public class Admin
    {
        public Guid AdminId { get;} = Guid.NewGuid();
        public string? AdminName { get; set; }
        public string? AdminPassword { get; set; }
        public string? AdminEmail { get; set; }
    }
}

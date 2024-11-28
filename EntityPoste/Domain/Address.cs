namespace EntityPoste.Domain
{
    public record Address(int Id, string Street, string City, string Country, int UserId)
    {
        public User? User { get; private set; }
    }
}

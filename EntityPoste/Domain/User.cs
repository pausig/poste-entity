namespace EntityPoste.Domain;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public ICollection<Address> Addresses { get; set; }

    public override string ToString() => $"Id: {Id}, Name: {Name}, Email: {Email}";
}
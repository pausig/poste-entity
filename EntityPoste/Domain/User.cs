using System.Net.Mail;

namespace EntityPoste.Domain;

public record User
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Email { get; private set; }

    public ICollection<Address> Addresses;

    public User(int id, string name, string email, ICollection<Address>? addresses = default)
    {
        Id = id;
        Name = name;
        Email = IsValidEmail(email) ? email : string.Empty;
        Addresses = addresses ?? [];
    }

    public User(string name, string email, int id = default) : this(id, name, email, []) { }

    public void UpdateEmail(string email)
    {
        if (!IsValidEmail(email))
            return;

        Email = email;
    }

    private static bool IsValidEmail(string email)
    {
        try
        {
            var _ = new MailAddress(email);
            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }
}
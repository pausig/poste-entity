using EntityPoste.Domain;

namespace EntityPoste.SeedWork;

public interface IUserRepository : IDisposable
{
    public void Insert(string name, string email);
    public void Update(int id, string email);
    public void Delete(int id);
    public IEnumerable<User> GetUsers();

    public IEnumerable<User> GetUsersByProvider(string provider);

    public IEnumerable<string> GetProviders();
}
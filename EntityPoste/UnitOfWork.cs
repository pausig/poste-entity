using EntityPoste.Domain;
using EntityPoste.SeedWork;
using Microsoft.Data.SqlClient;
using static System.Console;

namespace EntityPoste;

public class UnitOfWork(IUserRepository userRepository) : IDisposable
{
    public void Dispose()
    {
        userRepository.Dispose();
    }

    public void Work()
    {
        try
        {
            InternalWork();
        }
        catch (SqlException exception)
        {
            WriteLine("An error occurred while executing the database operation. {0}", exception);
        }
        catch (Exception e)
        {
            WriteLine("An error occurred while executing the operation. {0}", e);
            throw;
        }

        return;

        void InternalWork()
        {
            while (true)
            {
                WriteLine(
                    "Choose an operation: 1) Read All 2) Insert User 3) Update User 4) Delete User 5) Select for Email 6) Exit");
                var choice = ReadLine();

                switch (choice)
                {
                    case "1":
                        Read();
                        break;
                    case "2":
                        Insert();
                        break;
                    case "3":
                        Update();
                        break;
                    case "4":
                        Delete();
                        break;
                    case "5":
                        SelectForEmail();
                        break;
                    case "6":
                        return;
                    default:
                        WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }


    private void Read()
    {
        foreach (var user in userRepository.GetUsers()) WriteLine(user.ToString());
    }

    private void Insert()
    {
        Write("Insert UserName: ");
        var userName = ReadLine();
        Write("Insert Email: ");
        var email = ReadLine();

        if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(email))
        {
            WriteLine("Please enter a valid user name or email");
            return;
        }

        userRepository.Insert(userName, email);
    }

    private void Update()
    {
        Write("Insert the Id to Update: ");
        var id = ReadLine();

        Write("Insert the Email to Update: ");
        var email = ReadLine();


        if (int.TryParse(id, out var userId) && !string.IsNullOrEmpty(email)) userRepository.Update(userId, email);
    }

    private void Delete()
    {
        Write("Insert the Id to Delete: ");
        var id = ReadLine();

        if (int.TryParse(id, out var userId))
            userRepository.Delete(userId);
        else
            WriteLine("Id is not valid");
    }


    private void SelectForEmail()
    {
        WriteLine("Select Provider: ");

        var providers = userRepository.GetProviders();

        var providersMat = providers.ToList();
        if (providersMat.Count == 0)
        {
            WriteLine("No Providers Found");
            return;
        }

        for (var i = 0; i < providersMat.Count; i++) WriteLine($"{i} - provider: {providersMat[i]}");

        Write("Select Provider: ");
        var selectedProvider = ReadLine();

        if (!int.TryParse(selectedProvider, out var providerIndex)) return;
        var users = userRepository.GetUsersByProvider(providersMat[providerIndex]);

        var materialized = users as User[] ?? users.ToArray();
        if (materialized.Length == 0)
        {
            WriteLine("No users found");
            return;
        }

        foreach (var user in materialized)
        {
            WriteLine(user.ToString());
            foreach (var address in user.Addresses)
            {
                WriteLine(" - {0}", address.ToString());
            }
        }
    }
}
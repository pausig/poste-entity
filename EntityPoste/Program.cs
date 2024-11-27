using EntityPoste;
using EntityPoste.Repository;
using EntityPoste.SeedWork;
using Microsoft.Extensions.Logging;

var appDbContext = new AppDbContext();
IUserRepository userRepository = new UserRepository(appDbContext);
using var unitOfWork = new UnitOfWork(userRepository);

unitOfWork.Work();


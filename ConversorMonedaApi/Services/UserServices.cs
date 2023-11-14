using ConversorMonedaApi.Data;
using ConversorMonedaApi.Data.Models.Dtos;
using ConversorMonedaApi.Entities;

namespace ConversorMonedaApi.Services
{
    public class UserServices
    {
        private readonly ConversorContext _context;
        public UserServices(ConversorContext context)
        {
            _context = context;
        }

        public User? ValidateUser(AuthenticationRequestDto authRequestBody)
        {
            return _context.Users.FirstOrDefault(p => p.UserName == authRequestBody.UserName && p.Password == authRequestBody.Password);
        }
        public User CreateUser(UserForCreation userToCreate)
        {
            var user = new User
            {
                UserName = userToCreate.UserName,
                Password = userToCreate.Password,
                TypeUser = userToCreate.TypeUser,


            };

            if (userToCreate.UserName==null || userToCreate.Password==null)
            {
                return null;
            }

            if (userToCreate.TypeUser == null)
            {
                user.TypeUser = "free";
            }

            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

        private int GetRemainingRequestsForTypeUser(string typeUser)
        {
            var remainingRequest = _context.Subscriptions
                .FirstOrDefault(r => r.TypeUser == typeUser);

            if (remainingRequest != null)
            {
                return remainingRequest.Value;
            }

            // En caso de que no se encuentre un valor válido, puedes establecer un valor predeterminado o manejarlo como desees.
            return 0; // Valor predeterminado
        }

        public List<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public User GetUserById(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return null;
            }
            _context.Users.Remove(user);
            _context.SaveChanges();
            return user;
        }

        public User? UpdateUser(int Userid, UserForUpdate userToUpdate)
        {
            User userEntity = _context.Users.First(u => u.UserId == Userid);

            if (userEntity == null)
            {
                return null;
            }
            if (userToUpdate.UserName == null)
            {
                userEntity.UserName = userEntity.UserName;
            }
            userEntity.UserName = userToUpdate.UserName;
            userEntity.TypeUser = userToUpdate.TypeUser;
            

            _context.SaveChanges();

            return userEntity;

        }
        
    }
}
/*
 Este servicio gestionaría las operaciones relacionadas con la gestión de usuarios, como 
el registro, inicio de sesión, recuperación de contraseñas, verificación de límites
de solicitudes y cualquier operación relacionada con la autenticación y gestión de cuentas de usuario.
 */
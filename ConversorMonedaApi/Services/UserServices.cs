using ConversorMonedaApi.Data;
using ConversorMonedaApi.Data.Models.Dtos;
using ConversorMonedaApi.Data.Models.Enum;
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
            if (userToCreate.UserName == null || userToCreate.Password == null)
            {
                return null;
            }

            if (userToCreate.TypeUser == null)
            {
                userToCreate.TypeUser = "free";
            }

            
            if (userToCreate.Roleid == null)
            {
                userToCreate.Roleid = (int)Role.User;
            }
             var userExist = _context.Users.FirstOrDefault(p => p.UserName == userToCreate.UserName);

            if (userExist==null)
            { 

            var user = new User
            {
                UserName = userToCreate.UserName,
                Password = userToCreate.Password,
                TypeUser = userToCreate.TypeUser,
                Role = (Role)userToCreate.Roleid
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
            }
            else
            {
                throw new Exception("El usuario ya existe");
            }
        }



        public List<UserforFront> GetUsers()
        {
            var users = _context.Users.ToList();
            var usersForFront = new List<UserforFront>();
            foreach (var user in users)
            {
                var subscription = _context.Subscriptions.FirstOrDefault(r => r.TypeUser == user.TypeUser);
                var userForFront = new UserforFront
                {
                    userId = user.UserId,
                    UserName = user.UserName,
                    TypeUser = user.TypeUser,
                    ConversionCounter = subscription.Value - user.ConversionCounter,
                    Roleid = (int)user.Role
                    
                };
                usersForFront.Add(userForFront);
            }
            return usersForFront;
            
        }

        public User DeleteUserById(int id)
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

        public UserforFront GetUserById(int id)
        {
            User userEntity= _context.Users.Find(id);
            if (userEntity == null)
            {
                return null;
            }
            var user = new UserforFront
            {
                UserName = userEntity.UserName,
                TypeUser = userEntity.TypeUser,
            };
            return user;
        }
        public User? UpdateUser(int Userid, UserForUpdate userToUpdate)
        {
            var userEntity = _context.Users.Find(Userid);

            if (userEntity == null)
            {
                return null;
            }
            if (userToUpdate.UserName == null || userToUpdate.UserName == "");
            {
                userEntity.UserName = userEntity.UserName;
            }
            userEntity.UserName = userToUpdate.UserName;
            userEntity.TypeUser = userToUpdate.TypeUser;
            userEntity.Role = (Role)userToUpdate.Roleid;
            

            _context.SaveChanges();

            return userEntity;

        }
        
    }
}

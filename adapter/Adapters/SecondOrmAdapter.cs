using Example_04.Homework.SecondOrmLibrary;
using Example_04.Homework.Models.Interfaces;
using Example_04.Homework.Models;
using System.Linq;

namespace Example_04.Homework.Adapters
{
    class SecondOrmAdapter : SecondOrm, ITarget
    {
        public void Create(IDbEntity entity) {
            switch (entity.GetType().Name)
            {
                case "DbUserEntity":
                    Context.Users.Add(entity as DbUserEntity);
                    break;
                case "DbUserInfoEntity":
                    Context.UserInfos.Add(entity as DbUserInfoEntity);
                    break;
            }
        }

        public void Delete(IDbEntity entity) {
            switch (entity.GetType().Name)
            {
                case "DbUserEntity":
                    Context.Users.Remove(Read(entity.Id) as DbUserEntity);
                    break;
                case "DbUserInfoEntity":
                    Context.UserInfos.Remove(Read(entity.Id) as DbUserInfoEntity);
                    break;
            }
        }

        public IDbEntity Read(int id) {

            var user = Context.Users.FirstOrDefault(e => e.Id == id);
            if (user != null)
            {
                return user;
            }

            var info = Context.UserInfos.FirstOrDefault(e => e.Id == id);
            if (info != null)
            {
                return info;
            }

            return null;
        }

        public void Update(IDbEntity entity) {
            Delete(entity);
            Create(entity);
        }
    }
}

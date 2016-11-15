using System;
using Example_04.Homework.Models.Interfaces;

namespace Example_04.Homework.FirstOrmLibrary
{
    class FirstOrm : IFirstOrm<IDbEntity>
    {
        public void Create(IDbEntity entity) {
            throw new NotImplementedException();
        }

        public void Delete(IDbEntity entity) {
            throw new NotImplementedException();
        }

        public IDbEntity Read(int id) {
            throw new NotImplementedException();
        }

        public void Update(IDbEntity entity) {
            throw new NotImplementedException();
        }
    }
}

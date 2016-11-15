using Example_04.Homework.Models.Interfaces;

namespace Example_04.Homework.FirstOrmLibrary
{
    public interface IFirstOrm<TDbEntity> where TDbEntity : IDbEntity
    {
        void Create(TDbEntity entity);
        TDbEntity Read(int id);
        void Update(TDbEntity entity);
        void Delete(TDbEntity entity);
    }
}

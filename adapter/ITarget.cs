using Example_04.Homework.Models.Interfaces;

namespace Example_04.Homework
{
    interface ITarget
    {
        void Create(IDbEntity entity);
        IDbEntity Read(int id);
        void Update(IDbEntity entity);
        void Delete(IDbEntity entity);
    }
}

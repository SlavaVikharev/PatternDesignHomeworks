using System;

namespace Example_04.Homework.SecondOrmLibrary
{
    class SecondOrm : ISecondOrm
    {
        public ISecondOrmContext Context
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}

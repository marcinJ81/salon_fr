using salonfr;
using System;
using System.Collections.Generic;
using System.Text;

namespace salonfrSource.UpdateObjectInBase
{
    public interface IUpdateObject<T>
    {
        bool UpdateObject(T dataobjectForChange, int id);
    }

    public class UpdateClient : IUpdateObject<Client>
    {

        public bool UpdateObject(Client dataobjectForChange, int id)
        {

            throw new NotImplementedException();
        }
    }
}

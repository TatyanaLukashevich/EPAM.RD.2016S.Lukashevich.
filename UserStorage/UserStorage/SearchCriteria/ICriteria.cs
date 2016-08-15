using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorage.SearchCriteria
{
    public interface ICriteria<T>
    {
        bool MeetCriteria(T entity);
    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAL
{
    class BookConfiguration : DbConfiguration
    {
        public BookConfiguration()
        {
            DbInterception.Add(new BookInterceptorTransientErrors());
            DbInterception.Add(new BookInterceptorLogging());
        }
    }
}

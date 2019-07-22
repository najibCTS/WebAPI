using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopsWebAPI.Models
{
    public interface IPODbEntities: IDisposable
    {
        DbSet<ITEM> ITEMs { get; }
        DbSet<PODETAIL> PODETAILs { get; }
        DbSet<POMASTER> POMASTERs { get; }
        DbSet<SUPPLIER> SUPPLIERs { get; }
        void MarkAsModified(object entity);
        int SaveChanges();
    }
}

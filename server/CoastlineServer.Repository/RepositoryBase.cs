using System;
using CoastlineServer.DAL.Context;

namespace CoastlineServer.Repository
{
    public class RepositoryBase
    {
        protected readonly CoastlineContext Context;

        protected RepositoryBase(CoastlineContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }
    }
}
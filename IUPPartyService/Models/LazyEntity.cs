using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace IUPPartyService.Models
{
    public class LazyEntity
    {
        protected ILazyLoader lazyLoader;

        public LazyEntity() { }

        public LazyEntity(ILazyLoader lazyLoader)
        {
            this.lazyLoader = lazyLoader;
        }
    }
}
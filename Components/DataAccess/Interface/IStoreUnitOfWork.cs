﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interface
{
    public interface IStoreUnitOfWork
        : IDisposable
    {
        //IUsersRepository Users { get; }
        int Complete();
    }
}

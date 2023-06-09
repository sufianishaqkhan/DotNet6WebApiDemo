﻿using Common.Interface;
using DataAccess.Context;
using DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class StoreUnitOfWork : IStoreUnitOfWork
    {
        private readonly DemoDbContext? _context;
        private readonly ICustomLogger _logger;
        public StoreUnitOfWork(DemoDbContext context, ICustomLogger logger)
        {
            _context = context;
            _logger = logger;

            Users = new UsersRepository(_context, _logger);

        }
        public IUsersRepository Users { get; private set; }
        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

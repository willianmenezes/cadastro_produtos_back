using CadastroProduto.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace CadastroProduto.Data.Repository
{
    public class BaseRepository : IDisposable
    {
        protected EntityContext _context { get; set; }
        protected readonly TimeSpan DefaultTimeout = new TimeSpan(0, 1, 30);

        public BaseRepository(EntityContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            if (this._context != null)
            {
                this._context.Dispose();
            }
        }

        protected TransactionScope CreateTransactionScopeWithIsolationLevel(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted, TimeSpan? timeout = null)
        {
            return new TransactionScope
            (
                TransactionScopeOption.Required,
                new TransactionOptions
                {
                    IsolationLevel = isolationLevel,
                    Timeout = timeout ?? DefaultTimeout
                },
                TransactionScopeAsyncFlowOption.Enabled
            );
        }
    }
}

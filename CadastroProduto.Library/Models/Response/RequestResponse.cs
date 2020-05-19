using System;
using System.Collections.Generic;
using System.Text;

namespace CadastroProduto.Library.Models.Response
{
    public class RequestResponse
    {
        public string Message { get; set; }
    }

    public class RequestResponse<TEntity> : RequestResponse
    {
        public TEntity Response { get; set; }
    }
}

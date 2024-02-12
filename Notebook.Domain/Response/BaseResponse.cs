﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Notebook.Domain.Enums;

namespace Notebook.Domain.Responce
{
    public class BaseResponse<T> : IBaseResponse<T>
    {
        public string Description { get; set; }
        public StatusCode StatusCode { get; set; }
        public T Data { get; set; }
    }
    public interface IBaseResponse<T>
    { }

}

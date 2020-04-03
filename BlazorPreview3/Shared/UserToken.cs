using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorPreview3.Shared
{
    public class UserToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public bool IsSuccess { get; set; }
    }
}

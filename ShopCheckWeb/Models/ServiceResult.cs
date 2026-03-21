using System;
using System.Collections.Generic;
using System.Text;

namespace ShopCheckWeb
{
    public class ServiceResult
    {
        public bool IsValid => !ValidationErrors.Any();
        public Dictionary<string, string> ValidationErrors { get; } = new();
    }
}

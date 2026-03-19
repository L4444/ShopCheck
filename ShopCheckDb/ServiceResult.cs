using System;
using System.Collections.Generic;
using System.Text;

namespace ShopCheckDb
{
    public class ServiceResult
    {
        public bool IsValid => !ValidationErrors.Any();
        public Dictionary<string, string> ValidationErrors { get; } = new();
    }
}

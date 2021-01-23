using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Core
{
    public class ResourceNotFoundResult
    {
        public void AddMessage(string resourceName, string keyFieldName, object keyValue)
        {
            string msg = $"Resource: {resourceName} | {keyFieldName}: {keyValue}";
            ResourceNotFoundList.Add(msg);
        }

        public List<string> ResourceNotFoundList { get; } = new List<string>();
    }
}

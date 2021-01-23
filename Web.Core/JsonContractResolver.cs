using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Core.DataAnnotations;
using Newtonsoft.Json.Serialization;

namespace Web.Core
{
    public class JsonContractResolver : DefaultContractResolver
    {
        protected override List<MemberInfo> GetSerializableMembers(Type objectType)
        {
            var serializableMembers = base.GetSerializableMembers(objectType);

            /*                           
             * Remove any members that have the [ResourceRequired] attribute.
             * These are not meant to be exposed to outside callers (for example, in swagger).
             * They are just used internally. 
            */
            var jsonExclude =
                objectType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                    .Where(prop => Attribute.IsDefined(prop, typeof(JsonExcludeAttribute)))
                    .Select(rr => rr.Name);

            foreach (var member in serializableMembers.ToList())
            {
                if (jsonExclude.Contains(member.Name))
                {
                    serializableMembers.Remove(member);
                }
            }
            return serializableMembers;
        }
    }
}

using System;

namespace Core.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property)]

    public class JsonExcludeAttribute : Attribute
    {
    }
}
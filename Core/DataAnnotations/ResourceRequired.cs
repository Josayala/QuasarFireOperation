using System;

namespace Core.DataAnnotations
{
    public class ResourceRequiredAttribute : Attribute
    {
        public ResourceRequiredAttribute(string keyPropertyName)
        {
            if (string.IsNullOrEmpty(keyPropertyName))
                throw new ArgumentException("Argument cannot be null or empty", "keyPropertyName");

            KeyPropertyName = keyPropertyName;
            UsePropertyNameAsResourceName = true;
        }

        public ResourceRequiredAttribute(string keyPropertyName, string resourceName)
        {
            if (string.IsNullOrEmpty(resourceName))
                throw new ArgumentException("Argument cannot be null or empty", "resourceName");

            if (string.IsNullOrEmpty(keyPropertyName))
                throw new ArgumentException("Argument cannot be null or empty", "keyPropertyName");

            ResourceName = resourceName;
            KeyPropertyName = keyPropertyName;
            UsePropertyNameAsResourceName = false;
        }

        public string ResourceName { get; }
        public string KeyPropertyName { get; }
        public bool UsePropertyNameAsResourceName { get; }
    }
}
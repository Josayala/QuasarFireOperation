using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public enum Severity
    {
        Fatal,
        Warning,
        Informational
    }

    public class StatusMessage
    {
        public StatusMessage(Severity severity, string message)
        {
            if (string.IsNullOrEmpty(message)) throw new ArgumentException("A message must be provided", "message");

            Severity = severity;
            Message = message;
            FieldNames = new List<string>();
        }
        
        public StatusMessage(Severity severity, string message, params string[] fieldNames) : this(severity, message)
        {
            foreach (string fieldName in fieldNames)
            {
                FieldNames.Add(fieldName);
            }
        }

        public string Message { get; }

        public Severity Severity { get; }

        public List<string> FieldNames { get; set; }

        public override string ToString()
        {
            if (Message == string.Empty || Message == null)
                return string.Empty;

            StringBuilder builder = new StringBuilder(Message);
            builder.Append(" (");
            builder.Append(Severity.ToString());
            builder.Append(")");
            return builder.ToString();
        }

    }
}

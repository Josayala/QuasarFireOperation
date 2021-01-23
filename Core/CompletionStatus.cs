using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Core
{
    public class CompletionStatus
    {
        public CompletionStatus()
        {
            IsSuccessful = true;
            _MessageList = new List<StatusMessage>();
        }

        public CompletionStatus(StatusMessage statusMessage) : this()
        {
            AddMessage(statusMessage);
        }

        public CompletionStatus(Severity severity, string message) : this()
        {
            AddMessage(new StatusMessage(severity, message));
        }

        private readonly IList<StatusMessage> _MessageList;
        public IReadOnlyCollection<StatusMessage> MessageList
        {
            get { return new ReadOnlyCollection<StatusMessage>(_MessageList); }
        }

        public bool IsSuccessful { get; private set; }

        public bool HasMessages()
        {
            return _MessageList.Count > 0;
        }

        public bool HasMessages(Severity ofSeverity)
        {
            return _MessageList.Any(m => m.Severity == ofSeverity);
        }

        public void ClearMessages()
        {
            _MessageList.Clear();
        }

        public StatusMessage AddMessage(StatusMessage message)
        {
            _MessageList.Add(message);

            if (_MessageList.Any(m => m.Severity == Severity.Fatal))
            {
                IsSuccessful = false;
            }

            return message;
        }

        public StatusMessage AddValidationMessage(string message)
        {
            return AddMessage(new StatusMessage(Severity.Fatal, message));
        }

        public StatusMessage AddValidationMessage(string message, params string[] fieldNames)
        {
            return AddMessage(new StatusMessage(Severity.Fatal, message, fieldNames));
        }

        public void AddMessages(IEnumerable<StatusMessage> messageList)
        {
            foreach (var message in messageList)
            {
                AddMessage(message);
            }
        }

        public string ToString(string messageSeparator)
        {
            if (_MessageList == null)
            {
                return "";
            }

            string tmp = "";
            foreach (StatusMessage msg in _MessageList)
            {
                tmp += messageSeparator;
                tmp += msg.ToString();
            }
            return tmp;
        }

    }
}

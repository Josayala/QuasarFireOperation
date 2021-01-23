using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Responses
{
    public class StatusResponse
    {
        public StatusResponse() { }
        public StatusResponse(CompletionStatus status)
        {
            Status = status;
        }

        public CompletionStatus Status { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Mediator.Core;
using QuasarFireOperation.Domain.CommandModel.Repositories;

namespace QuasarFireOperation.Domain.CommandModel.Requests
{
    public class PublishMessageRequest : StatusRequest<IUnitOfWork>
    {
        public string Message { get; set; }

        protected override void ExpandResources(IUnitOfWork unitOfWork)
        {
        }
    }
}

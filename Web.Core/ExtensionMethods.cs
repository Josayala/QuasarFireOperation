using System;
using System.Collections.Generic;
using System.Text;
using Core;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Web.Core
{
    public static class ExtensionMethods
    {
        public static void AddToModelState(this CompletionStatus completionStatus, ModelStateDictionary modelState)
        {
            foreach (var validationError in completionStatus.MessageList)
            {
                if (validationError.FieldNames.Count == 0)
                {
                    modelState.AddModelError("Validation Errors", validationError.Message);
                }
                else
                {
                    foreach (var fieldName in validationError.FieldNames)
                    {
                        modelState.AddModelError(fieldName, validationError.Message);
                    }
                }
            }
        }
    }
}

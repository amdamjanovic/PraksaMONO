using DReporting.Model.Common;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Text;

namespace DReporting.Model
{
    public class ModelStateWrapper : IValidationDictionary
    {
        private ModelStateDictionary modelState;

        public ModelStateWrapper(ModelStateDictionary _modelState)
        {
            modelState = _modelState;
        }


        public bool IsValid
        {
            get { return modelState.IsValid; }
        }

        public void AddError(string key, string errorMessage)
        {
            modelState.AddModelError(key, errorMessage);
        }
    }
}

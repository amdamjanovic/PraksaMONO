using DReporting.Model.Common;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Text;

namespace DReporting.Model
/* 
 IValidationDictionary interface and the ModelStateWrapper class enables to 
 completely isolate service layer from controller layer. 
 The service layer is no longer dependent on model state.
 */
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

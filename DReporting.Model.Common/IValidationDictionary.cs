using System;
using System.Collections.Generic;
using System.Text;

namespace DReporting.Model
{
    public interface IValidationDictionary
    {
        void AddError(string key, string errorMessage);
        bool IsValid { get; }
    }
}

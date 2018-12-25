using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Mvvm;

namespace RegistroPropiedad.Modelos
{
    public class ModeloBase : BindableBase
    {
        public List<string> Errors { get; set; }

        public bool HasErrors()
        {
            return Errors != null && Errors.Any();
        }

        public void SetError(string errorMessage)
        {
            if (Errors == null)
            {
                Errors = new List<string>();
            }

            Errors.Add(errorMessage);
        }

        public void SetErrors(List<string> errors)
        {
            if (Errors == null)
            {
                Errors = new List<string>();
            }

            if (errors != null)
            {
                Errors.AddRange(errors);
            }
        }
    }
}

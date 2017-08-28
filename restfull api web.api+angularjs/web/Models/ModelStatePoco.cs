using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP.Web.Models
{
    public class ModelStatePoco
    {
        public String FieldName { get; set; }
        public String FieldValue { get; set; }
        public ICollection<string> Errors { get; set; }

        public static ICollection<ModelStatePoco> MapFromModelState(ModelStateDictionary modelStateDictionary)
        {
            var listModelStatePoco = new List<ModelStatePoco>();
            foreach (var modelState in modelStateDictionary)
            {
                var errors = new List<string>();
                foreach (var error in modelState.Value.Errors)
                {
                    errors.Add(error.ErrorMessage);
                }
                listModelStatePoco.Add(new ModelStatePoco {
                    FieldName = modelState.Key,
                    FieldValue = (modelState.Value.Value != null) ? modelState.Value.Value.AttemptedValue : null,
                    Errors = errors
                });
            }
            return listModelStatePoco;
        }
    }
}
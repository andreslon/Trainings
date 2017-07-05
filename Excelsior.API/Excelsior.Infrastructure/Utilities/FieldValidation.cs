using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excelsior.Infrastructure.Utilities
{
    public class FieldValidation : IDisposable
    {
        private string Fields { get; set; }
        private List<string> FieldList { get; set; }
        public FieldValidation(string fields)
        {
            this.Fields = fields;
            this.FieldList = new List<string>();
            if (fields != null)
                FieldList.AddRange(fields.ToLower().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries));
        }
        public bool this[string fieldName]
        {
            get
            {
                return FieldList.Count == 0 || FieldList.Contains(fieldName?.ToLower());
            } 
        } 
        public void Dispose()
        { 
            GC.SuppressFinalize(this);
        }
    }

}

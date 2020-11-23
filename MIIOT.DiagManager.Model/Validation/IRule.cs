using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.DiagManager.Model.Validation
{
    public interface IRule
    {
        string Name { get; set; }
        ModelValidationResult Validate(object value);
    }
}

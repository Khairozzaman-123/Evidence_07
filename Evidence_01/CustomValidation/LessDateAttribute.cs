using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Evidence_01.CustomValidation
{
    //LessDate
    public class LessDateAttribute:ValidationAttribute
    {
        public LessDateAttribute():base("{0} Date Should Less Then Current Date.")
        {

        }

        public override bool IsValid(object value)
        {
            DateTime propValue = Convert.ToDateTime(value);
            if (propValue < DateTime.Now)
                return true;
            else
                return false;
        }
    }
}
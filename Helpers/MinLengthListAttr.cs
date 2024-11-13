using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Helpers;

[AttributeUsage(AttributeTargets.Property | 
                AttributeTargets.Field, AllowMultiple = false)]
public class MinLengthListAttr : ValidationAttribute
{
    private readonly int _minItems;
    public string ErrorMessage;
    
    public MinLengthListAttr(int minItems)
    {
        _minItems = minItems;
        ErrorMessage = $"The list must contain at least {_minItems} items.";
    }

    public override bool IsValid(object value)
    {
        if (value is ICollection collection && collection.Count >= _minItems)
        {
            return true;
        }

        return false;
    }
}
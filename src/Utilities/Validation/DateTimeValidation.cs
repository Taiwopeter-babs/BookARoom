using System.ComponentModel.DataAnnotations;

namespace BookARoom.Utilities;

public class DateTimeValidation : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is null)
            return true;

        bool isParsed = DateTime.TryParse((string)value, out DateTime dateTime);

        if (!isParsed)
            return false;

        return true;
    }
}

using System.ComponentModel.DataAnnotations;
using BookARoom.Extensions;

namespace BookARoom.Utilities;

public class DateTimeValidation : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is null)
            return true;

        string dateTimeString = (string)value;
        DateTime converted;

        try
        {
            converted = dateTimeString.GetUtcDate();
        }
        catch (FormatException)
        {
            return false;
        }

        bool isParsed = DateTime.TryParse((string)value, out DateTime dateTime);

        if (!isParsed)
            return false;

        return true;
    }
}

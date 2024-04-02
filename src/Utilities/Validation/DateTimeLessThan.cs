using System.ComponentModel.DataAnnotations;

namespace BookARoom.Utilities;


/// <summary>
/// A custom validation attribute to check that the decorated datetime
/// is less than or equal to the property value passed
/// </summary>
public class DateTimeLessThan : ValidationAttribute
{
    private enum DateTimeComparisonResult
    {
        Earlier = -1,
        Later = 1,
        TheSame = 0
    };
    private readonly string _datetimeToCompare;

    public DateTimeLessThan(string dateTimeToCompare)
    {
        _datetimeToCompare = dateTimeToCompare;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        bool isParsed;
        ErrorMessage = ErrorMessageString;

        if (value is null)
        {
            return ValidationResult.Success;
        }

        isParsed = DateTime.TryParse((string)value, out DateTime currentDateTimeValue);
        if (!isParsed)
        {
            return new ValidationResult(ErrorMessage);
        }

        var propertyToCompare = validationContext.ObjectType.GetProperty(_datetimeToCompare);
        if (propertyToCompare == null)
        {
            throw new ArgumentException("Property not found");
        }

        var propertyToCompareDateString = propertyToCompare
            .GetValue(validationContext.ObjectInstance);

        isParsed = DateTime.TryParse((string)propertyToCompareDateString!,
            out DateTime propertyToCompareDateTime);

        if (!isParsed)
        {
            return new ValidationResult(ErrorMessage);
        }

        var comparison = (DateTimeComparisonResult)currentDateTimeValue
            .CompareTo(propertyToCompareDateTime);


        string compareResult = comparison.ToString();
        if (compareResult == "TheSame" || compareResult == "Earlier")
        {
            return ValidationResult.Success;
        }

        // date is greater than the compared property datetime
        return new ValidationResult(ErrorMessage);

    }

}

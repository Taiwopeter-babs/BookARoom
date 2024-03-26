using System.ComponentModel.DataAnnotations;

namespace BookARoom.Utilities;

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
        ErrorMessage = ErrorMessageString;
        string nullValueMessage = "DateTime value is null";

        if (value is null)
            ErrorMessage = nullValueMessage;

        var currentDateTimeValue = (DateTime)value;

        var propertyToCompare = validationContext.ObjectType.GetProperty(_datetimeToCompare);
        if (propertyToCompare == null)
        {
            throw new ArgumentException("Property not found");
        }

        var propertyToCompareDateTimeValue = (DateTime)propertyToCompare
            .GetValue(validationContext.ObjectInstance);

        var comparison = (DateTimeComparisonResult)currentDateTimeValue
            .CompareTo(propertyToCompareDateTimeValue);

        int comparisonInteger = (int)comparison;

        // Check if date is less than or equal to the comparedProperty
        if (comparisonInteger <= 0)
        {
            return ValidationResult.Success;
        }

        // date is greater than the compared property datetime
        return new ValidationResult(ErrorMessage);

    }

}

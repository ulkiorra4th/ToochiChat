namespace ToochiChat.Domain.Extensions;

public static class DateTimeExtensions
{
    public static int CalculateAge(this DateTime birthDate)
    {
        return (DateTime.Now.Day >= birthDate.Day && DateTime.Now.Month >= birthDate.Month) ?
            DateTime.Now.Year - birthDate.Year : DateTime.Now.Year - birthDate.Year - 1;
    }
}
namespace SharedKernel;

public static class GetDate
{
    public static DateTime GetRandomDate(DateTime startDate, DateTime endDate, Random random)
    {
        var range = (endDate - startDate).Days;
        return startDate.AddDays(random.Next(range)).AddHours(random.Next(0, 24)).AddMinutes(random.Next(0, 60));
    }
}

namespace Database.Extensions;

public static class TeeBookingNamesToString
{
    public static string NamesToString(this TeeBooking booking)
    {
        string result = booking.Golfers.Any() == true ? booking.Golfers.First().ToString() : string.Empty;
        for (int i = 1; i < booking.Golfers.Count; i++)
        {
            result = result + "," + booking.Golfers[i].ToString();
        }

        return result;
    }
}
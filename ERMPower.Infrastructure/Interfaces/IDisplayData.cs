namespace ERMPower.Infrastructure.Interfaces
{
    public interface IMedianDisplayData
    {
        string FileName { get; set; }
        string DateTime { get; set; }
        decimal Value { get; set; }
        decimal MedianValue { get; set; }
    }


}

namespace ERMPower.Infrastructure.Interfaces
{
    public interface IDisplayData<T> where T:class
    {
        void ShowDisplayContent(T objectToDisplay);
    }
}

namespace ERMPower.ConsoleApp
{
    public class Result<T>
    {
        private readonly T _result;
        private readonly string _message;


        public Result(T result, string message = null)
        {
            _result = result;
            _message = message;
        }
    }
}
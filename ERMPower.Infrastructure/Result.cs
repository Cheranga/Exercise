namespace ERMPower.Infrastructure
{
    public class Result<T>
    {
        private readonly T _data;
        public virtual  ResultStatus Status { get; set; }

        public T Data
        {
            get { return _data; }
        }

        public Result(T data)
        {   
            _data = data;
        }
    }
}
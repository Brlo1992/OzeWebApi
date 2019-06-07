namespace OzeContract.Communication
{
    public class Response<T> where T : class
    {
        public Response(T data = null)
        {
            this.Data = data;
        }

        public T Data { get; private set; }
    }
}
namespace Lanre.Tests.Core
{
    public class ResultEntity<TResult>
    {
        public ResultEntity(TResult result) => Result = result;

        public TResult Result { get; set; }
    }
}
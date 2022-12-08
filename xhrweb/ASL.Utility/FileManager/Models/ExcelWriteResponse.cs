namespace ASL.Utility.FileManager.Models
{
    public class ExcelWriteResponse<T>
    {
        public int Code { get; set; }

        public string Msg { get; set; }

        public T Data { get; set; }

        public static ExcelWriteResponse<T> GetResult(int code, string msg, T data = default)
        {
            return new ExcelWriteResponse<T>
            {
                Code = code,
                Msg = msg,
                Data = data
            };
        }
    }
}

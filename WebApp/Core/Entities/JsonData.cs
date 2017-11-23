using Core.Enum;

namespace Core.Entities
{
    public class JsonData
    {
        public ResultCode Code { get; set; }
        public object Data { get; set; }
        public string Message { get; set; }
    }
}

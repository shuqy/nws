using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.my3w
{

    /// <summary>
    /// 题库序列返回结果
    /// </summary>
    public class SequenceResult
    {
        public int[] data { get; set; }
        public int errorCode { get; set; }
        public string message { get; set; }
        public string success { get; set; }
    }
}

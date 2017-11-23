using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.my3w
{
    /// <summary>
    /// 问题返回结果
    /// </summary>
    public class QuestionJsonResult
    {
        public List<QuestionResult> data { get; set; }
        public int errorCode { get; set; }
        public string message { get; set; }
        public string success { get; set; }
    }

    /// <summary>
    /// 问题
    /// </summary>
    public class QuestionResult
    {
        public int id { get; set; }
        /// <summary>
        /// 科目类别 1科目一3科目三
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// 答案 a16 b32 c64 d128 ab48 ac80 ad144 bc96 bd160 abc112 bcd224 abcd240
        /// </summary>
        public int answer { get; set; }
        public int chapterId { get; set; }
        /// <summary>
        /// 难度
        /// </summary>
        public int difficulty { get; set; }
        /// <summary>
        /// 答案解释
        /// </summary>
        public string explain { get; set; }
        public string label { get; set; }
        /// <summary>
        /// 多媒体高度
        /// </summary>
        public int mediaHeight { get; set; }
        /// <summary>
        /// 多媒体类型 0无 1图片 2mp4
        /// </summary>
        public int mediaType { get; set; }
        /// <summary>
        /// 多媒体视频宽度
        /// </summary>
        public int mediaWidth { get; set; }
        /// <summary>
        /// 答案a
        /// </summary>
        public string optionA { get; set; }
        public string optionB { get; set; }
        public string optionC { get; set; }
        public string optionD { get; set; }
        public string optionE { get; set; }
        public string optionF { get; set; }
        public string optionG { get; set; }
        public string optionH { get; set; }
        /// <summary>
        /// 选项类别 0判断题 1单选选择题 2多选题
        /// </summary>
        public int optionType { get; set; }
        /// <summary>
        /// 问题
        /// </summary>
        public string question { get; set; }
        /// <summary>
        /// 问题id
        /// </summary>
        public int questionId { get; set; }
        /// <summary>
        /// 多媒体内容
        /// </summary>
        public string mediaContent { get; set; }
        /// <summary>
        /// 错误数
        /// </summary>
        public int falseCount { get; set; }
        /// <summary>
        /// 正确数
        /// </summary>
        public int trueCount { get; set; }
        /// <summary>
        /// 错误率
        /// </summary>
        public decimal wrongRate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Entities;
using Core.Helper;
using Model.my3w;

namespace WebApp.Controllers
{
    public class JKController : Controller
    {
        // GET: JK
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 更新题库
        /// </summary>
        /// <returns></returns>
        public ActionResult UpQuestions()
        {
            return View();
        }

        /// <summary>
        /// 更新题库
        /// </summary>
        /// <param name="type">科目类别 1科目一 3科目三</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UpdateQuestions(int type)
        {
            if (type != 1 && type != 3)
                return Json(new JsonData { Code = Core.Enum.ResultCode.Fail, Message = "更新失败" }, JsonRequestBehavior.DenyGet);
            HttpHelper httpHelper = new HttpHelper();
            HttpItem sequenceItem = new HttpItem
            {
                URL = "http://api2.jiakaobaodian.com/api/open/exercise/sequence.htm?_r=18556060022096025072&cityCode=110000&page=1&limit=25&course=kemu"
                    + type + "&carType=car&_=0.1590286446735263",
            };
            HttpResult seqResult = httpHelper.GetHtml(sequenceItem);
            SequenceResult sequenceResult = Newtonsoft.Json.JsonConvert.DeserializeObject<SequenceResult>(seqResult.Html);
            string codes = "";
            int succesCount = 0;
            DapperHelper dapperHelper = new DapperHelper(Core.Enum.DbConnEnum.my3w);
            IEnumerable<QuestionResult> qlist = dapperHelper.Query<QuestionResult>("select questionId from QuestionResult");
            for (int i = 1; i < sequenceResult.data.Length; i++)
            {
                if (qlist.Any(q => q.questionId == sequenceResult.data[i])) continue;
                codes += sequenceResult.data[i] + ",";
                if (i % 10 == 0)
                {
                    codes = codes.TrimEnd(',');
                    succesCount += addQuestion(codes, type);
                    codes = "";
                }
            }
            if (!string.IsNullOrEmpty(codes))
            {
                succesCount += addQuestion(codes, type);
            }
            return Json(new JsonData { Code = Core.Enum.ResultCode.OK, Data = succesCount }, JsonRequestBehavior.DenyGet);
        }

        private int addQuestion(string codes, int type)
        {
            HttpHelper httpHelper = new HttpHelper();
            DapperHelper dapperHelper = new DapperHelper(Core.Enum.DbConnEnum.my3w);
            codes = codes.TrimEnd(',');
            HttpItem item = new HttpItem
            {
                URL = "http://api2.jiakaobaodian.com/api/open/question/question-list.htm?_r=110230801350704736067&page=1&limit=25&questionIds=" + codes,
            };
            HttpResult qResult = httpHelper.GetHtml(item);
            string text = StringHelper.UnicodeToGB(qResult.Html);
            QuestionJsonResult questionResult = Newtonsoft.Json.JsonConvert.DeserializeObject<QuestionJsonResult>(text);
            int succesCount = dapperHelper.InsertMultiple("insert into QuestionResult (type,answer,chapterId,difficulty,explain,label,mediaHeight,mediaType,mediaWidth,optionA,optionB,optionC,optionD,optionE,optionF,optionG,optionH,optionType,question,questionId,mediaContent,falseCount,trueCount,wrongRate) values (" + type + ",@answer,@chapterId,@difficulty,@explain,@label,@mediaHeight,@mediaType,@mediaWidth,@optionA,@optionB,@optionC,@optionD,@optionE,@optionF,@optionG,@optionH,@optionType,@question,@questionId,@mediaContent,@falseCount,@trueCount,@wrongRate)", questionResult.data);
            return succesCount;
        }

        /// <summary>
        /// 练习
        /// </summary>
        /// <returns></returns>
        public ActionResult Exercise(int km, int id)
        {
            return View();
        }

        /// <summary>
        /// 问题列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult QuestionList(string questionIds)
        {
            if (string.IsNullOrEmpty(questionIds))
            {
                return Json(new JsonData { Code = Core.Enum.ResultCode.Error, Message = "参数错误" }, JsonRequestBehavior.DenyGet);
            }
            questionIds = questionIds.TrimEnd(',');
            if (questionIds.Split(',').Length > 10)
            {
                return Json(new JsonData { Code = Core.Enum.ResultCode.Error, Message = "参数错误" }, JsonRequestBehavior.DenyGet);
            }
            DapperHelper dapperHelper = new DapperHelper(Core.Enum.DbConnEnum.my3w);
            var list = dapperHelper.Query<QuestionResult>(string.Format("select * from QuestionResult where questionId in ({0})", questionIds));
            return Json(new JsonData { Code = Core.Enum.ResultCode.OK, Data = list }, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 问题列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Sequenec(int km = 0)
        {
            DapperHelper dapperHelper = new DapperHelper(Core.Enum.DbConnEnum.my3w);
            var list = dapperHelper.Query<QuestionResult>(string.Format("select questionId from QuestionResult where type={0}", km));
            List<int> idList = list.Select(l => l.questionId).ToList();
            return Json(new JsonData { Code = Core.Enum.ResultCode.OK, Data = idList }, JsonRequestBehavior.DenyGet);
        }
    }
}
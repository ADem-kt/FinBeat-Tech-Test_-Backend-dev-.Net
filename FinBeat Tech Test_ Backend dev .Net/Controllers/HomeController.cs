using FinBeat_Tech_Test__Backend_dev_.Net.ef;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text.Json.Nodes;
using System.Text.Json;
using Humanizer;
using Microsoft.VisualBasic;
using System.Numerics;
using Newtonsoft.Json.Linq;

namespace FinBeat_Tech_Test__Backend_dev_.Net.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [HttpPost]
        public ActionResult PostCodeValue(string json)
        {
            try
            {
                _logger.Log(LogLevel.Information, "Запрос {0}: {1}", System.Reflection.MethodInfo.GetCurrentMethod().Name, json);
                var dicArray = System.Text.Json.JsonSerializer.Deserialize<Dictionary<int, string>[]>(json);
                List<CodeValueClass> сodeValueList=new();
                foreach (var dic in dicArray)
                {
                    foreach (var j in dic)
                    {
                        сodeValueList.Add(new CodeValueClass() { Code = j.Key, Value = j.Value });
                    }
                }
                _logger.Log(LogLevel.Information, "Преобразовали {0} в {1}", json, System.Text.Json.JsonSerializer.Serialize(сodeValueList));
                if (сodeValueList.Count > 0)
                {
                    using (ApplicationContext db = new ApplicationContext())
                    {
                        db.CodeValueDbSet.RemoveRange(db.CodeValueDbSet);
                        var orderedсodeValueList = сodeValueList.OrderBy(p => p.Code);
                        foreach (var сodeValue in orderedсodeValueList)
                        {
                            if (сodeValue != null)
                            {
                                db.CodeValueDbSet.Add(сodeValue);
                            }
                            db.SaveChanges();
                        }
                    }
                    _logger.Log(LogLevel.Information, "Ответ {0}: {1}", System.Reflection.MethodInfo.GetCurrentMethod().Name, "Ok");
                    return Ok();
                }
                _logger.Log(LogLevel.Information, "Ответ {0}: {1}", System.Reflection.MethodInfo.GetCurrentMethod().Name, "400 Bad Request");
                return StatusCode(400);
            }
            catch (Exception ex) {
                _logger.Log(LogLevel.Information, "Ответ {0}: {1}", System.Reflection.MethodInfo.GetCurrentMethod().Name, "500 Internal Server Error"); 
                return StatusCode(500, ex.ToString()); }
        }

        [HttpGet]
        public ActionResult GetConfig(int? code, string? value)
        {
            try
            {
                _logger.Log(LogLevel.Information, "Запрос {0}: {1}", System.Reflection.MethodInfo.GetCurrentMethod().Name,$"Фильтрация по code={code}, value={value}");
                using (ApplicationContext db = new ApplicationContext())
                {
                    IEnumerable<CodeValueClass> codeValueList = db.CodeValueDbSet.ToList();
                    if (code != null && code != 0)
                    {
                        codeValueList = codeValueList.Where(p => p.Code == code);

                        if (!String.IsNullOrEmpty(value))
                        {
                            codeValueList = codeValueList.Where(p => p.Value == value);
                        }
                    }
                    var resultJson = System.Text.Json.JsonSerializer.Serialize(codeValueList);
                    _logger.Log(LogLevel.Information, "Ответ {0}: {1}", System.Reflection.MethodInfo.GetCurrentMethod().Name, resultJson);
                    return Ok(resultJson);
                }
            }
            catch (Exception ex) {
                _logger.Log(LogLevel.Information, "Ответ {0}: {1}", System.Reflection.MethodInfo.GetCurrentMethod().Name, "500 Internal Server Error");
                return StatusCode(500, ex.ToString()); }
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moduit.Interview.Models;
using Moduit.Interview.Params;
using Moduit.Interview.Requests;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Moduit.Interview
{
    [Route("backend/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        IEnumerable<QuestionsModel.Data> list = new List<QuestionsModel.Data>();
        public QuestionController()
        {
            list = this.zero();
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("zero")]
        public List<QuestionsModel.Data> zero()
        {
            var result = new List<String>();

            try
            {
                using (StreamReader file = new StreamReader(Directory.GetCurrentDirectory() + @"/init.json"))
                {
                    List<QuestionsModel.Data> list = (List<QuestionsModel.Data>)JsonSerializer.Deserialize(file.ReadToEnd(), typeof(List<QuestionsModel.Data>));
                    return list;
                }
            }
            catch (Exception ex)
            {
                throw ex; 
            }
        }

        [HttpGet("one")]
        public ActionResult one()
        {
            try
            {
                return Ok(list.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("two")]
        public ActionResult two([FromQuery] QuestionsParam param)
        {
            try
            {
                if (!string.IsNullOrEmpty(param.keyword))
                {
                    list = list.Where(x => (x.title.ToLower().Contains(param.keyword.ToLower()) || x.description.ToLower().Contains(param.keyword.ToLower())));
                }
                if (!string.IsNullOrEmpty(param.tag))
                {
                    list = list.Where(x => (x.title.ToLower().Contains(param.tag.ToLower()) || x.description.ToLower().Contains(param.tag.ToLower())));
                }
                return Ok(list.OrderByDescending(x => x.id).Take(3));
            }
            catch (Exception ex)
            {
                return BadRequest(list);
            }
        }

        [HttpPost("three")]
        public ActionResult three([FromBody] QuestionRequest.Data param)
        {
            var result = new List<QuestionsModel.Data>();

            try
            {
                foreach (var item in param.items)
                {
                    var data = new QuestionsModel.Data()
                    {
                        id = param.id,
                        category = param.category,
                        createdAt = param.createdAt,
                        title = item.title,
                        description = item.description,
                        footer = item.footer
                    };
                    result.Add(data);
                }
                List<QuestionsModel.Data> listData = new List<QuestionsModel.Data>();
                using (StreamReader file = new StreamReader(Directory.GetCurrentDirectory() + @"/init.json"))
                {
                    listData = (List<QuestionsModel.Data>)JsonSerializer.Deserialize(file.ReadToEnd(), typeof(List<QuestionsModel.Data>));
                    listData.AddRange(result);
                }

                using (StreamWriter file = new StreamWriter(Directory.GetCurrentDirectory() + @"/init.json"))
                {
                    file.Write(JsonSerializer.Serialize(listData));
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(result);
            }
        }

    }
}

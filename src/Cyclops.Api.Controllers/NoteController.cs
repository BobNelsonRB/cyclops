using Cyclops.Api.Input;
using Cyclops.Data.Abstractions;
using Cyclops.Data.Common;
using Cyclops.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cyclops.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/notes")]
    public class NoteController : Controller
    {

        private INoteDataProvider _DataProvider = null;

        public NoteController(INoteDataProvider dataProvider)
        {
            _DataProvider = dataProvider ?? throw new NullReferenceException(nameof(dataProvider));
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            return Json(new { message = "done" });
        }

        [HttpGet]
        public IActionResult Get()
        {
            Parameters p = Parameters.SetStrategy("all");
            var list = _DataProvider.Get(p);
            return Json(list);
        }

        [HttpPost]
        public IActionResult Post([FromBody] NoteInput input)
        {
            input.Id = Guid.NewGuid().ToString().TrimStart('{').TrimEnd('}');
            if (Validator.TryValidate(input, out Note model, out string errorMessage))
            {
                var createdModel = _DataProvider.Post(model);
                if (createdModel != null)
                {
                    return Json(createdModel);
                }
            }
            else
            {
                return BadRequest(errorMessage);
            }
            return BadRequest();
        }

        [HttpPut]
        public IActionResult Put([FromBody] NoteInput input)
        {
            if (Validator.TryValidate(input, out Note model, out string errorMessage))
            {
                var createdModel = _DataProvider.Put(model);
                if (createdModel != null)
                {
                    return Json(createdModel);
                }
            }
            else
            {
                return BadRequest(errorMessage);
            }
            return BadRequest();
        }




    }
}

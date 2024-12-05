using IMSOFTWARE.Models.SharedModels;
using IMSOFTWARE.WebApi.Models.DbModels;
using IMSOFTWARE.WebApi.Models.SharedModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace IMSOFTWARE.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        public readonly IConfiguration _configuration;

        public PersonController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IEnumerable<CatPerson> Get()
        {
            using (var context = new OGMDBIMSOFTWAREContext())
            {
                return context.CatPerson.ToList();
            }
        }

        [HttpPost]
        public IActionResult Post(PersonBindingModel model)
        {
            ResponseModel responseModel = new ResponseModel();
            if(model is null)
            {
                return BadRequest();
            }
            else if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                try
                {
                    using (var context = new OGMDBIMSOFTWAREContext())
                    {
                        int.TryParse(model.Edad, out int Edad);
                        CatPerson person = new CatPerson()
                        {
                            Name = model.Nombre,
                            Age = Edad,
                            Email = model.Email
                        };
                        
                        context.CatPerson.Add(person);
                        context.SaveChanges();
                    }

                    responseModel.Code = 0;
                    responseModel.Message = "Respuesta correcta";
                }
                catch (Exception ex)
                {
                    responseModel.Code = 1; // Error
                    responseModel.Message = ex.Message;
                }                
            }

            return Ok(responseModel);
        }
    }
}

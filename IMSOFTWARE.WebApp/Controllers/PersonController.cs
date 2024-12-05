using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using IMSOFTWARE.Models.DbModels;
using IMSOFTWARE.Models.ViewModels;
using IMSOFTWARE.Models.SharedModels;

namespace IMSOFTWARE.WebApp.Controllers
{
    public class PersonController : Controller
    {
        // GET: Person
        public async Task<ActionResult> Index()
        {

            var options = new RestClientOptions("https://localhost:7221/")
            {
                MaxTimeout = 30000,
            };
            
            var client = new RestClient(options);
            var request = new RestRequest("https://localhost:7221/api/Person", Method.Get);
            RestResponse response = await client.ExecuteAsync(request);
            List<CatPerson> People = new List<CatPerson>();

            if (response.ErrorException is null)
                People = JsonConvert.DeserializeObject<List<CatPerson>>(response.Content);


            CatPersonViewModel viewModel = new CatPersonViewModel();
            viewModel.People = People;

            return View(viewModel);
        }

        public async Task<ActionResult> Add()
        {
            PersonBindingModel bindingModel = new PersonBindingModel();

            return View(bindingModel);
        }

        [HttpPost]
        public async Task<ActionResult> Add(PersonBindingModel bindingModel)
        {
            if (bindingModel == null)
            {
                TempData["ErrorMessage"] = "El modelo está vacío";
                return View(bindingModel);
            }
            else if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "El modelo contiene errores";
                return View(bindingModel);
            }
            else
            {
                try
                {
                    var options = new RestClientOptions("https://localhost:7221/")
                    {
                        MaxTimeout = 30000,
                    };
                    var client = new RestClient(options);
                    var request = new RestRequest("https://localhost:7221/api/Person", Method.Post);
                    request.AddHeader("Content-Type", "application/json");
                    var body = JsonConvert.SerializeObject(bindingModel);

                    request.AddStringBody(body, DataFormat.Json);
                    RestResponse response = await client.ExecuteAsync(request);
                    
                    if(response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        TempData["SuccessMessage"] = "Se agregó correctamente la persona";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Error al registrar la persona - " + response.ErrorMessage;
                    }

                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = ex.Message;
                    return View(bindingModel);
                }
            }

            return RedirectToAction("Add");
        }
    }
}
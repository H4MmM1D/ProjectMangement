using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectMangement.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMangement.Web.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public ProjectController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetProject()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:54867/api/project/" + 1);
            var client = _clientFactory.CreateClient();


            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var project = JsonConvert.DeserializeObject<Project>(jsonString);
                return View(project);
            }
            else
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> Add(Project project)
        {
            if (ModelState.IsValid)
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:54867/api/project");
                var client = _clientFactory.CreateClient();
                request.Content = new StringContent(JsonConvert.SerializeObject(project), Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.SendAsync(request);
                if (response.StatusCode == HttpStatusCode.Created)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View("Index");
                }
                
            }
            else
            {
                return View("Index");
            }

        }
    }
}

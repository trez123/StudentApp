using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using StudentAppFrontend.Models;
using System.Text;
using System.Net.Http.Headers;

namespace StudentAppFrontend.Controllers;

public class StudentController : Controller
{
        private readonly IHttpClientFactory _clientHandler;
        public StudentController(IHttpClientFactory clientHandler)
        {
            this._clientHandler = clientHandler;
        }

        public async Task<IActionResult> Index()
        {
            var httpClient = _clientHandler.CreateClient("StudentAPI");

            var response = await httpClient.GetAsync("");

            List<Student> productList = new();

            if(response != null){

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                productList = JsonConvert.DeserializeObject<List<Student>>(content)!;
            }
            else
                return Problem("Error in Api response");

            return View(productList);
            }
            else 
                return View(productList);
        }



        [HttpPost]
        public IActionResult Upsert(Student studentvm)
        {
           if (!ModelState.IsValid) return View(studentvm);

           var student = new Student
           {
               StudentName = studentvm.StudentName,
               EmailAddress = studentvm.EmailAddress,
               PhoneNumber = studentvm.PhoneNumber,
           };

           var json = JsonConvert.SerializeObject(student);

           var data = new StringContent(json, Encoding.UTF8, "application/json");

           if (student.Id == 0)
           {
               var response = _clientHandler.CreateClient("StudentAPI").PostAsync("", data).Result;

               if (response.IsSuccessStatusCode)
               {
                   return RedirectToAction("Index");
               }
               else
               {
                   ModelState.AddModelError(string.Empty, "Product creation failed");
                   return View(studentvm);
               }
           }
           else
           {
               var response = _clientHandler.CreateClient("StudentAPI").PutAsync($"{student.Id}", data).Result;

               if (response.IsSuccessStatusCode)
               {
                   return RedirectToAction("Index");
               }
               else
               {
                   ModelState.AddModelError(string.Empty, "Product creation failed");
                   return View(studentvm);
               }
           }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var httpClient = _clientHandler.CreateClient("StudentAPI");
            var response = await httpClient.DeleteAsync($"{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Student Delete failed");
                return RedirectToAction("Index");
            }
        }


}
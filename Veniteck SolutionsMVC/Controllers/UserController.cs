using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using VeniteckSolutionsMVC.Models;

namespace Veniteck_SolutionsMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly HttpClient _httpClient;

        public UserController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult InsertUser()
        {
            return View();
        }



        // For creating a new user (Insert)
        [HttpPost]
        public async Task<IActionResult> InsertUser(UserDTO user)
        {
            if (!ModelState.IsValid)
                return View("InsertUser");

            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://localhost:7074/api/User/Insert", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("IndexUser"); // Redirect to Index after successful insertion
            }

            ModelState.AddModelError(string.Empty, "An error occurred while adding the user.");
            return View("IndexUser");
        }


        [HttpGet]
        public IActionResult UpdateUser()
        {
            return View();
        }

        // For updating an existing user (Update)
        [HttpPost]
        public async Task<IActionResult> UpdateUser(UserDTO user)
        {
            if (!ModelState.IsValid)
                return View("Index");

            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync("https://localhost:7074/api/User/Update", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("IndexUser"); // Redirect to Index after successful update
            }

            ModelState.AddModelError(string.Empty, "An error occurred while updating the user.");
        
            return View("IndexUser");
        }


        [HttpGet]
        public async Task<IActionResult> IndexUser()
        {
            // Fetch users from API
            var response = await _httpClient.GetAsync("https://localhost:7074/api/User/GetAll");

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var users = JsonConvert.DeserializeObject<List<UserDTO>>(jsonResponse);

                return View(users); // Pass the user list to the view
            }

            ModelState.AddModelError(string.Empty, "An error occurred while retrieving users.");
            return View(new List<UserDTO>()); // Return an empty list in case of an error
        }


        [HttpPost]
        public async Task<IActionResult> IndexUser(UserDTO user)
        {
            if (!ModelState.IsValid)
                return View("IndexUser"); // Redirect to IndexUser if the model state is invalid

            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync("https://localhost:7074/api/User/Update", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("IndexUser"); // Redirect to IndexUser after successful update
            }

            ModelState.AddModelError(string.Empty, "An error occurred while updating the user.");
            return View("IndexUser");
        }
        [HttpGet]
        public async Task<IActionResult> GetUserById(int userId)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7074/api/User/GetUserById/{userId}");

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<UserDTO>(jsonResponse);
                return View(user); // Return the user details to the view
            }

            ModelState.AddModelError(string.Empty, "An error occurred while retrieving the user.");
            return View("IndexUser");
        }
    }
}

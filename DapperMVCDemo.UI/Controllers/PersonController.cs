using Microsoft.AspNetCore.Mvc;
using DapperMVCDemo.Data.Models.Domain;
using DapperMVCDemo.Data.Repository;

namespace DapperMVCDemo.UI.Controllers
{
    public class PersonController : Controller
    {
        private readonly IPersonRepository _personRepository;

        public PersonController(IPersonRepository personRepository)
        {
                _personRepository = personRepository;
        }
        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Person person)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(person);
                bool addPerson = await _personRepository.AddAsync(person);
                if (addPerson) 
                {
                    TempData["msg"] = "Sucessfully Added";
                }
                else
                {
                    TempData["msg"] = "Could not add";
                }
            }
            catch (Exception ex) 
            {
                TempData["msg"] = "Hebana!! Something went wrong!!!";
            }
            return RedirectToAction(nameof(DisplayAll));
        }


        public async Task<IActionResult>Edit(int id)
        {
            var person=await _personRepository.GetByIdAsync(id);
            return View(person);
        }

        [HttpPost]
        public async Task<IActionResult>Edit (Person person)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(person);
                bool updateRecord = await _personRepository.UpdateAsync(person);

                if (updateRecord)
                    TempData["msg"] = "Successfully Added";
                else
                    TempData["msg"] = "Oh Hell Nah";
            }

             catch (Exception ex) 
            {
                TempData["msg"] = "Seriously!!!!!";
            }
            return RedirectToAction(nameof(DisplayAll));
        }

        public async Task<IActionResult> DisplayAll()
        {
            var people=await _personRepository.GetAllAsync();
            return View(people);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var deleteResult = await _personRepository.DeleteAsync(id);
            return RedirectToAction(nameof(DisplayAll));
        }




    }
}

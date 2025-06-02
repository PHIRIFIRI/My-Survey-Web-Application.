using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurveyApp.Data;
using SurveyApp.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

//this controller navigates the user through the survey process
//Survey controoller handles the survey filling and viewing results

namespace SurveyApp.Controllers
{
    public class SurveyController : Controller
    {
        private readonly SurveyContext _context;

        public SurveyController(SurveyContext context)
        {
            _context = context;
        }

        // GET: FillOutSurvey
        //this is the action that returns the view for filling out the survey

        public IActionResult FillOutSurvey()
        {
            return View();
        }

        // POST: FillOutSurvey
        //using [HttpPost] to handle form submission
        //this is the action that handles the form submission for filling out the survey
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FillOutSurvey(SurveyResponse model)
        {
            if (ModelState.IsValid)
            {
                var age = CalculateAge(model.DateOfBirth);
                if (age < 5 || age > 120)
                {
                    ModelState.AddModelError("DateOfBirth", "Age must be between 5 and 120.");
                    return View(model);
                }

                _context.SurveyResponses.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(FillOutSurvey));
            }
            return View(model);
        }

        // GET: ViewSurveyResults
        //this is the action that returns the view for viewing survey results
        //using async to fetch survey responses from the database
        //asychronously retrieves survey responses from the database
        public async Task<IActionResult> ViewSurveyResults()
        {
            var responses = await _context.SurveyResponses.ToListAsync();
            if (responses.Count == 0)
            {
                ViewBag.Message = "No Surveys Available.";
                return View();
            }

            var totalSurveys = responses.Count;
            var ages = responses.Select(r => CalculateAge(r.DateOfBirth)).ToList();
            var viewModel = new SurveyResultsViewModel
            {
                TotalSurveys = totalSurveys,
                AverageAge = Math.Round(ages.Average(), 1),
                Oldest = ages.Max(),
                Youngest = ages.Min(),
                PizzaPercentage = Math.Round((double)responses.Count(r => r.LikesPizza) / totalSurveys * 100, 1),
                PastaPercentage = Math.Round((double)responses.Count(r => r.LikesPasta) / totalSurveys * 100, 1),
                PapAndWorsPercentage = Math.Round((double)responses.Count(r => r.LikesPapAndWors) / totalSurveys * 100, 1),
                AvgRatingMovies = Math.Round(responses.Average(r => r.RatingMovies), 1),
                AvgRatingRadio = Math.Round(responses.Average(r => r.RatingRadio), 1),
                AvgRatingEatOut = Math.Round(responses.Average(r => r.RatingEatOut), 1),
                AvgRatingTV = Math.Round(responses.Average(r => r.RatingTV), 1)
            };

            //esnsure that the view model is not null before passing it to the view
            //ensuring view results are displayed correctly
            return View(viewModel);
            return View();
            return View("ViewSurveyResults");
        }
        //calculate age based on date of birth
        //this method calculates the age based on the date of birth provided by the user
        //it takes a DateTime parameter and returns an integer representing the age
        //it is set private to encapsulate the logic within the controller
        //it is set to private to encapsulate the logic within the controller
        private int CalculateAge(DateTime dateOfBirth)
        {
            var today = DateTime.Today;
            var age = today.Year - dateOfBirth.Year;
            if (dateOfBirth.Date > today.AddYears(-age)) age--;
            return age;
        }
    }
}
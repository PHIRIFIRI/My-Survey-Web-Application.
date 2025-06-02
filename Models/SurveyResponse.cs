using System.ComponentModel.DataAnnotations;
//class that represents a survey response in the application
//this get all the properties that are required for the survey response
//this class also sets validation attributes for each property to ensure that the data entered by the user meets the required criteria

namespace SurveyApp.Models
{
    public class SurveyResponse
    {
   public int Id { get; set; }
        //applying required validation
        
        /// /attributes to ensure that the user provides the necessary information
        /// </summary>
        [Required(ErrorMessage = "Full Name is required.")]
    public string FullName { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid Email Address.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Date of Birth is required.")]
    public DateTime DateOfBirth { get; set; }

    [Required(ErrorMessage = "Contact Number is required.")]
    public string ContactNumber { get; set; }

    public bool LikesPizza { get; set; }
    public bool LikesPasta { get; set; }
    public bool LikesPapAndWors { get; set; }
    public bool LikesOther { get; set; }

        //using Range attribute to ensure that the rating is between 1 and 5
    [Range(1, 5, ErrorMessage = "Please select a rating for movies.")]
    public int RatingMovies { get; set; }

    [Range(1, 5, ErrorMessage = "Please select a rating for radio.")]
    public int RatingRadio { get; set; }

    [Range(1, 5, ErrorMessage = "Please select a rating for eating out.")]
    public int RatingEatOut { get; set; }

    [Range(1, 5, ErrorMessage = "Please select a rating for TV.")]
    public int RatingTV { get; set; }
}
}

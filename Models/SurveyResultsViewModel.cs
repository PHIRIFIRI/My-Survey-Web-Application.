namespace SurveyApp.Models
{
    public class SurveyResultsViewModel
    {
        public int TotalSurveys { get; set; }
        public double AverageAge { get; set; }
        public int Oldest { get; set; }
        public int Youngest { get; set; }
        public double PizzaPercentage { get; set; }
        public double PastaPercentage { get; set; }
        public double PapAndWorsPercentage { get; set; }
        public double AvgRatingMovies { get; set; }
        public double AvgRatingRadio { get; set; }
        public double AvgRatingEatOut { get; set; }
        public double AvgRatingTV { get; set; }
    }
}
using Microsoft.EntityFrameworkCore;
using SurveyApp.Models;

namespace SurveyApp.Data
{
    public class SurveyContext : DbContext
    {
        public SurveyContext(DbContextOptions<SurveyContext> options) : base(options) { }

        public DbSet<SurveyResponse> SurveyResponses { get; set; }
    }
}
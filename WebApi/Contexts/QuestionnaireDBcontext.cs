using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Models
{
    public class QuestionnaireDBContext : DbContext
    {
        public QuestionnaireDBContext(DbContextOptions<QuestionnaireDBContext> options) : base(options) { }
        public DbSet<WebApi.Models.Questionnaire> Questionnaire { get; set; }
    }
}

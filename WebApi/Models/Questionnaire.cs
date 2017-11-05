using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class Questionnaire
    {
        public int QuestionnaireID { get; set; }
        public string name { get; set; }
        public string question { get; set; }
    }
}

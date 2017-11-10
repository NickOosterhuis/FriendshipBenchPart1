using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.ViewModels.Questionnaires
{
    public class QuestionnaireWithAnswersViewModel
    {
        public DateTime Time { get; set; }

        public string Client_id { get; set; }

        public List<AnswerGetViewModel> Answers { get; set; }
    }
}

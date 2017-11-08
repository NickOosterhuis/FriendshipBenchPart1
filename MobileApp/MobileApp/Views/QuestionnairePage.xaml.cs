using MobileApp.Models;
using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json.Linq;
using System.IO;
using Newtonsoft.Json;
using System.Diagnostics;

namespace MobileApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class QuestionnairePage : ContentPage
	{
        private List<Question> allQuestions;
        private List<Answers> allAnswers = new List<Answers>();
        private int questionNumber = 0;

        public QuestionnairePage ()
		{
			InitializeComponent ();
            FetchQuestions();

        }

        private void button_yes(object sender, EventArgs e)
        {
            SaveAnswer("Yes");
            questionNumber++;
            ShowQuestion();
        }

        private void button_no(object sender, EventArgs e)
        {
            SaveAnswer("No");
            questionNumber++;
            ShowQuestion();
        }

        // Fetch all the appointments and make a list of it.
        private async Task FetchQuestions()
        {
            // Send a GET request to the API.
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(Constants.questionsUrl);

            // If the request was succesfull, add appointments to the list. If not, let the user know.
            if (response.IsSuccessStatusCode)
            {
                // Convert the API response into a JSON object.
                allQuestions = new List<Question>();
                String json = await response.Content.ReadAsStringAsync();
                dynamic convertedJson = JsonConvert.DeserializeObject(json);

                // Loop through all the appointments in the JSON object and create appointment objects.
                foreach (var question in convertedJson)
                {
                    allQuestions.Add(new Question
                    {
                        Id = (int)question.id,
                        QuestionString = (string)question.question
                    });
                }

                ShowQuestion();
            }
            else
            {
                DisplayAlert("Error", "Sorry, something went wrong. Please try again later.", "Okay");
            }
        }

        private void ShowQuestion()
        {

            if (questionNumber < allQuestions.Count)
            {
                QuestionLabel.Text = allQuestions[questionNumber].QuestionString;
            }
            else
            {
                writeQuestionnaire();
            }
            

        }

        private void SaveAnswer(string answer)
        {
            allAnswers.Add(new Answers {

                Answer = answer,
                Question_id = allQuestions[questionNumber].Id
            });
        }

        private async void writeQuestionnaire()
        {
            Debug.WriteLine("Beunhaas dat ding in elkaar!" + DateTime.Now);
            Questionnaire questionnaire = new Questionnaire
            {
                Client_id = "1",
                Time = DateTime.Now
            };

            var client = new HttpClient();
            string json = JsonConvert.SerializeObject(questionnaire);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = new HttpResponseMessage();
            response = await client.PostAsync(Constants.questionnaireUrl, content);

            if (response.IsSuccessStatusCode)
            {
                //Questionnaire aangemaakt
                //Questionnaire ID uit response halen, antwoorden posten.
            }
            else
            {
                DisplayAlert("Error", "Sorry, something went wrong. Please try again later.", "Okay");
            }

        }
    }
}
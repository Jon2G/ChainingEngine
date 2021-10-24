using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ChainingEngine.Interfaces;
using ChainingEngine.Models.Adelante;
using ChainingEngine.Views;
using Kit.Extensions;
using Kit.Model;

namespace ChainingEngine.ViewModels
{
    public class QuestionViewModel : ModelBase
    {
        public IQuestion Question { get; }
        public ICommand AnswerCommand { get; set; }
        public QuestionViewModel(MainView window, ChainingEngine.Models.Adelante.Hipotesis question)
        {
            this.Question = question;
            this.AnswerCommand = new Command<bool>((b) => Answer(b, window, question));
        }

        private void Answer(bool answer, MainView window, ChainingEngine.Models.Adelante.Hipotesis hipotesis)
        {
            this.Question.Answer = answer;
            if (answer)
            {
                hipotesis.Verdadero.Run(window);
                return;
            }
            hipotesis.Falso.Run(window);
        }
    }
}

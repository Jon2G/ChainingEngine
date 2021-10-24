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
        public QuestionViewModel(MainView window, BaseHipotesis question)
        {
            this.Question = question;
            this.AnswerCommand = new Command<bool>((b) => Answer(b, window, question));
        }

        private void Answer(bool answer, MainView window, BaseHipotesis hipotesis)
        {
            this.Question.Answer = answer;
            switch (hipotesis)
            {
                case Hipotesis<HechoInferido> inferido:
                    if (answer) //PREGUNTA
                    {
                        inferido.Verdadero.Run(window); //HECHO VERDADERO
                        break;
                    }
                    inferido.Falso.Run(window);//HECHO FALSO
                    break;
                case Hipotesis<Conclusion> conclusion:
                    if (answer)
                    {
                        conclusion.Verdadero.Run(window);
                        break;
                    }

                    conclusion.Falso.Run(window);
                    break;

            }

        }
    }
}

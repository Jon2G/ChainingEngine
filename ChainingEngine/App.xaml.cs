using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ChainingEngine.Models;
using ChainingEngine.Models.Adelante;

namespace ChainingEngine
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {


            Hecho hecho = Hecho.New("Es una CocaCola")
                .SetHipotesis(Hipotesis<HechoInferido>.New("Es color marrón")
                    .Set(
                        verdadero: (HechoInferido)HechoInferido.New("Es color marrón").SetHipotesis(Hipotesis<Conclusion>.New("La etiqueta es roja").Set(Conclusion.New("Cumple con los estándares"), Conclusion.New("No cumple con los estándares"))),
                        falso: HechoInferido.New("No es color marrón").SetConclusion(Conclusion.New("No es CocaCola"))));






        }
    }
}

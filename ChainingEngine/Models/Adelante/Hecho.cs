namespace ChainingEngine.Models.Adelante
{
   public class Hecho : ObjectBase
    {
        public string Descripcion { get; set; }
        public BaseHipotesis Hipotesis { get; set; }

        public Hecho(string descripcion)
        {
            this.Descripcion = descripcion;
        }

        public static Hecho New(string descripcion) => new Hecho(descripcion);

        public Hecho SetHipotesis(BaseHipotesis hipotesis)
        {
            Hipotesis = hipotesis;
            return this;
        }
    }
}

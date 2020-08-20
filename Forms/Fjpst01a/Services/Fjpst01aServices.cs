using Alio.Forms.Fjpst01a.Model;
using Foundations.Core.AppSupportLib.Composition;

namespace Alio.Forms.Fjpst01a.Services
{
    public class Fjpst01aServices : AbstractServices<Fjpst01aModel>
    {

        public Fjpst01aServices(Fjpst01aModel model) : base(model)
        {
        }

        public new Fjpst01aTask Task
        {
            get { return (Fjpst01aTask)base.Task; }
        }



    }
}

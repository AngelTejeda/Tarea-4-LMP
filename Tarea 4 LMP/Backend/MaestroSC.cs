using System.Linq;
using Tarea_4_LMP.Data_Access;

namespace Tarea_4_LMP.Backend
{
    class MaestroSC : BaseSC
    {
        public IQueryable<Maestro> GetAllMaestros()
        {
            return dbContext.Maestro;
        }

        public Maestro GetMaestroWithName(string name)
        {
            return GetAllMaestros().FirstOrDefault(m => m.nombre_maestro + " " + m.apellido_paterno_maestro + " " + m.apellido_materno_maestro == name);
            //return GetAllMaestros().FirstOrDefault(m => GetFullName(m) == name);
        }

        public string GetFullName(Maestro maestro)
        {
            return maestro.nombre_maestro + " " + maestro.apellido_paterno_maestro + " " + maestro.apellido_materno_maestro;
        }
    }
}

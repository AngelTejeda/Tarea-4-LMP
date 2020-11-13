using System.Linq;
using Tarea_4_LMP.Data_Access;

namespace Tarea_4_LMP.Backend
{
    class AlumnoSC :  BaseSC
    {
        public IQueryable<Alumno> GetAllAlumnos()
        {
            return dbContext.Alumno;
        }

        public IQueryable<IGrouping<string, Alumno>> GroupAlumosByCarrera()
        {
            return GetAllAlumnos().GroupBy(a => a.carrera);
        }
    }
}

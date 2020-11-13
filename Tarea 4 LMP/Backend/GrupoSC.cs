using System;
using System.Linq;
using Tarea_4_LMP.Data_Access;

namespace Tarea_4_LMP.Backend
{
    class GrupoSC : BaseSC
    {
        public IQueryable<Grupo> GetAllGrupos()
        {
            return dbContext.Grupo;
        }

        public Grupo GetGroup(String subject, int groupNumber)
        {
            return GetAllGrupos().FirstOrDefault(g => g.Materia.nombre_materia == subject && g.num_grupo == groupNumber);
        }
    }
}

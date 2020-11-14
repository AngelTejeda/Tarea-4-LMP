using System;
using System.Linq;
using Tarea_4_LMP.Data_Access;

namespace Tarea_4_LMP.Backend
{
    class MateriaSC : BaseSC
    {
        public IQueryable<Materia> GetAllMaterias()
        {
            return dbContext.Materia;
        }

        public Materia GetMateriaWithName(string name)
        {
            return GetAllMaterias().FirstOrDefault(m => m.nombre_materia == name);
        }

        public void AddMateria(Materia materia)
        {
            dbContext.Materia.Add(materia);
            dbContext.SaveChanges();

            Console.WriteLine(" Se agregó la materia " + materia.nombre_materia);
        }

        public void RemoveMateria(Materia materia)
        {
            try
            {
                dbContext.Materia.Remove(materia);
                dbContext.SaveChanges();

                Console.WriteLine(" Se eliminó la materia " + materia.nombre_materia);
            }
            catch(Exception)
            {
                Console.WriteLine(" Ha ocurrido un error al intentar eliminar la materia " + materia.nombre_materia);
            }
        }

        public void WriteAllMaterias()
        {
            Console.WriteLine(" ---Materias Registradas---");
            GetAllMaterias()
            .ToList().ForEach(m =>
            {
                Console.WriteLine(" - " + m.nombre_materia);
            });
        }
    }
}

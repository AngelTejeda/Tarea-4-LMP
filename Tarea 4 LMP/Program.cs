using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tarea_4_LMP.Backend;
using Tarea_4_LMP.Data_Access;
using Tarea_4_LMP.Models;

namespace Tarea_4_LMP
{
    class Program
    {
        static void Main(string[] args)
        {
            String opcion_menu;
            String opcion_submenu1;
            
            do
            {
                Console.Clear();
                Console.WriteLine("\n  TAREA 4 Lenguajes Modernos de Programacion\n\n");
                Console.WriteLine(" MENU: \n\n   1) SELECT FROM TABLE AS\n   2) WHERE FROM TABLE \n   3) GROUP BY FIELD\n      ORDER BY FIELD\n   4) INSERT INTO TABLE\n      DELETE FROM TABLE\n   5) Salir");
                Console.Write("\n Seleccione su opcion: ");
                opcion_menu = Console.ReadLine();

                Console.Clear();
                switch (opcion_menu)
                {
                    case "1":
                        Console.WriteLine("\n\tSELECT FROM TABLE\n");
                        Program.Select_alumnos();
                        break;
                    case "2":
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("\n\tWHERE FROM TABLE\n\n MENU: \n\n   1)Integrantes del equipo\n   2)Materias que imparte el profesor Rolando \n   3)Salir");
                            Console.Write("\nSeleccione su opcion: ");
                            opcion_submenu1 = Console.ReadLine();

                            Console.Clear();
                            switch (opcion_submenu1)
                            {
                                case "1":
                                    Program.Where_equipo();
                                    break;
                                case "2":
                                    Program.Where_materias();
                                    break;
                                case "3":
                                    Console.Clear();
                                    break;
                                default:
                                    Console.WriteLine("\n!!!\tOpcion Invalida");
                                    break;
                            }
                            if (opcion_submenu1 != "3")
                            {
                                Presiona();
                            }
                        } while (opcion_submenu1 != "3");
                        break;
                    case "3":
                        Console.WriteLine("\n\tGROUP BY FIELD\n\tORDER BY FIELD\n");
                        Program.GroupBy_OrderBy_cantidad();
                        break;
                    case "4":
                        Console.WriteLine("\n\tINSERT INTO TABLE\n\tDELETE FROM TABLE\n");
                        Program.Insert_Delete_materias();
                        break;
                    case "5":
                        Console.WriteLine("\n\tProfe, ponganos 100 :)");
                        break;
                    default:
                        Console.WriteLine("\n!!!\tOpcion Invalida");
                        break;
                }
                if(opcion_menu!="2" && opcion_menu != "5")
                {
                    Presiona();
                }
            } while (opcion_menu != "5");

            Presiona();
        }

        public static void Where_equipo()
        {
            Grupo grupoLMP = new GrupoSC().GetGroup("Lenguajes Modernos de Programación", 11);

            List<AlumnoDTO> integrantes = grupoLMP
                .Alumno
                .Select(a => new AlumnoDTO
                {
                    Matricula = a.matricula_alumno,
                    Nombre = a.nombre_alumno,
                    ApellidoPaterno = a.apellido_paterno_alumno,
                    ApellidoMaterno = a.apellido_materno_alumno
                }).ToList();

            Console.WriteLine("\n\n Integrantes del equipo: ");
            integrantes.ForEach(a =>
            {
                Console.WriteLine(" - " + a.Nombre + " " + a.ApellidoPaterno + " " + a.ApellidoMaterno);
            });
        }

        public static void Select_alumnos()
        {
            Console.WriteLine("\n\n SELECT FROM TABLE AS");
            Console.WriteLine(" Alumnos Registrados:\n\n");

            List<AlumnoDTO> alumnos = new AlumnoSC()
                .GetAllAlumnos()
                .Select(a => new AlumnoDTO
                {
                    Matricula = a.matricula_alumno,
                    Nombre = a.nombre_alumno,
                    ApellidoPaterno = a.apellido_paterno_alumno,
                    ApellidoMaterno = a.apellido_materno_alumno
                }).ToList();

            StringBuilder datos = new StringBuilder();
            datos.Append(String.Format("{0,-51} {1,11}\n", " Nombre", "Matricula"));
            datos.Append("\n");

            alumnos.ForEach(a =>
            {
                string name = " " + a.Nombre + " " + a.ApellidoPaterno + " " + a.ApellidoMaterno;
                datos.Append(String.Format("{0,-51} {1,11}\n", name, a.Matricula));
            });

            Console.WriteLine(datos);
        }

        public static void Where_materias()
        {
            Console.WriteLine("\n\n WHERE FROM TABLE");
            Console.WriteLine(" Materias que imparte el maestro Rolando:");

            Maestro maestro = new MaestroSC().GetMaestroWithName("Rolando Valdemar Domínguez Lozano");

            List<string> materias = new GrupoSC()
                .GetAllGrupos()
                .Where(g => g.Maestro.matricula_maestro == maestro.matricula_maestro)
                .Select(g => g.Materia.nombre_materia)
                .Distinct()
                .ToList();

            materias.ForEach(m =>
            {
                Console.WriteLine(" - " + m);
            });
        }

        public static void GroupBy_OrderBy_cantidad()
        {
            Console.WriteLine(" GROUP BY FIELD");
            Console.WriteLine(" Cantidad de alumnos por carrera");

            var alumnosPorCarrera = new AlumnoSC()
                .GroupAlumosByCarrera()
                .Select(g => new
                {
                    carrera = g.Key,
                    total = g.Count()
                }).ToList();

            alumnosPorCarrera.ForEach(a =>
            {
                Console.WriteLine(" - " + a.carrera + ": " + a.total);
            });
            Console.WriteLine();


            Console.WriteLine(" ORDER BY FIELD");
            Console.WriteLine(" Ordenar la consulta anterior de mayor a menor cantidad");

            alumnosPorCarrera
                .OrderByDescending(a => a.total)
                .ToList().ForEach(g =>
                {
                    Console.WriteLine(" - " + g.carrera + ": " + g.total);
                });
        }

        public static void Insert_Delete_materias()
        {
            MateriaSC materiaSC = new MateriaSC();

            Console.WriteLine(" INSERT INTO TABLE");
            Console.WriteLine(" Insertar una nueva materia");
            Console.WriteLine();

            Materia materia = new Materia
            {
                nombre_materia = "Metodología de la Programación",
                semestre_materia = 1
            };

            materiaSC.AddMateria(materia);
            materiaSC.WriteAllMaterias();
            Console.WriteLine("\n");


            Console.WriteLine(" DELETE FROM TABLE");
            Console.WriteLine(" Borrar un registro");
            Console.WriteLine();

            Materia materiaRemove = materiaSC.GetMateriaWithName("Metodología de la Programación");
            materiaSC.RemoveMateria(materiaRemove);
            materiaSC.WriteAllMaterias();
        }

        public static void Presiona()
        {
            Console.WriteLine();
            Console.WriteLine("\n\nPresione la tecla ENTER para continuar...");
            Console.ReadLine();
        }
    }
}

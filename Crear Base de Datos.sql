DROP DATABASE IF EXISTS LMP
GO

CREATE DATABASE LMP
GO

USE LMP
GO

DROP TABLE IF EXISTS dbo.Maestro
CREATE TABLE Maestro (
	matricula_maestro int,
	nombre_maestro varchar(40) NOT NULL,
	apellido_paterno_maestro varchar(40) NOT NULL,
	apellido_materno_maestro varchar(40) NOT NULL

	PRIMARY KEY(matricula_maestro),

	CHECK(LEN(matricula_maestro) = 7),
	CHECK(LEN(nombre_maestro) > 0),
	CHECK(LEN(apellido_paterno_maestro) > 0),
	CHECK(LEN(apellido_materno_maestro) > 0)
)

DROP TABLE IF EXISTS dbo.Materia
CREATE TABLE Materia (
	id_materia int IDENTITY(1, 1),
	nombre_materia varchar(100) NOT NULL,
	semestre_materia int NOT NULL

	PRIMARY KEY(id_materia),

	CHECK(LEN(nombre_materia) > 0),
	CHECK(semestre_materia >= 1 AND semestre_materia <= 9)
)

DROP TABLE IF EXISTS dbo.Alumno
CREATE TABLE Alumno (
	matricula_alumno int,
	nombre_alumno varchar(40) NOT NULL,
	apellido_paterno_alumno varchar(40) NOT NULL,
	apellido_materno_alumno varchar(40) NOT NULL,
	carrera varchar(4) NOT NULL,
	semestre_alumno int NOT NULL

	PRIMARY KEY(matricula_alumno),

	CHECK(LEN(matricula_alumno) = 7),
	CHECK(LEN(nombre_alumno) > 0),
	CHECK(LEN(apellido_paterno_alumno) > 0),
	CHECK(LEN(apellido_materno_alumno) > 0),
	CHECK(carrera IN ('LCC', 'LSTI', 'LMAD', 'LM', 'LF', 'LA')),
	CHECK(semestre_alumno >= 1 AND semestre_alumno <= 9)
)

DROP TABLE IF EXISTS dbo.Grupo
CREATE TABLE Grupo (
	num_grupo int,
	dias_semana varchar(40) NOT NULL,
	horario varchar(11) NOT NULL,
	matricula_maestro int NOT NULL,
	id_materia int NOT NULL

	PRIMARY KEY(num_grupo, id_materia),
	FOREIGN KEY(matricula_maestro) REFERENCES Maestro ON UPDATE NO ACTION
														ON DELETE NO ACTION,
	FOREIGN KEY(id_materia) REFERENCES Materia ON UPDATE NO ACTION
												ON DELETE NO ACTION,

	CHECK(num_grupo > 0),
	CHECK(LEN(dias_semana) > 0),
	CHECK(horario LIKE '[0-9][0-9]:[0-9][0-9]-[0-9][0-9]:[0-9][0-9]')
)

DROP TABLE IF EXISTS dbo.Alumno_Grupo
CREATE TABLE Alumno_Grupo (
	matricula_alumno int NOT NULL,
	num_grupo int NOT NULL,
	id_materia int NOT NULL

	PRIMARY KEY(matricula_alumno, num_grupo, id_materia),
	FOREIGN KEY(matricula_alumno) REFERENCES Alumno ON UPDATE NO ACTION
														ON DELETE NO ACTION,										
	FOREIGN KEY(num_grupo, id_materia) REFERENCES Grupo(num_grupo, id_materia) ON UPDATE NO ACTION
																				ON DELETE NO ACTION
)
GO

INSERT INTO Maestro Values(1111111, 'Rolando Valdemar', 'Domínguez', 'Lozano')
INSERT INTO Maestro Values(2222222, 'Yazmany', 'Guerrero', 'Ceja')
INSERT INTO Maestro Values(3333333, 'Eduardo', 'Valdes', 'García')
INSERT INTO Maestro Values(4444444, 'Azucena Yoloxóchitl', 'Ríos', 'Mercado')
INSERT INTO Maestro Values(5555555, 'Ernesto Jesús', 'Solís', 'Valenzuela')

INSERT INTO Materia Values('Aplicaciones Móviles', 4)
INSERT INTO Materia Values('Lenguajes Modernos de Programación', 5)
INSERT INTO Materia Values('Teoría de Autómatas', 4)
INSERT INTO Materia Values('Programación Lineal', 5)
INSERT INTO Materia Values('Probabilidad', 4)
INSERT INTO Materia Values('Estructura de Datos', 3)

INSERT INTO Grupo Values(11, 'SAB', '13:00-16:00', 1111111, 2)			/* LMP Rolando */
INSERT INTO Grupo Values(10, 'LUN,MIE,VIE', '19:00-20:00', 1111111, 2)	/* LMP Rolando */
INSERT INTO Grupo Values(11, 'MAR,JUE', '20:00-21:30', 1111111, 1)		/* App Mov Rolando */
INSERT INTO Grupo Values(11, 'LUN-VIE', '12:00-13:00', 2222222, 3)		/* Teoría de Autómatas Yazmany */
INSERT INTO Grupo Values(11, 'LUN-VIE', '11:00-12:00', 3333333, 4)		/* Programación Lineal Eduardo */
INSERT INTO Grupo Values(9, 'LUN-VIE', '09:00-10:00', 3333333, 4)		/* Programación Lineal Eduardo */
INSERT INTO Grupo Values(6, 'LUN-VIE', '09:00-10:00', 4444444, 5)		/* Probabilidad Azucena */
INSERT INTO Grupo Values(4, 'LUN-JUE', '08:00-09:00', 5555555, 6)		/* Estructura de Datos Solís */

INSERT INTO Alumno Values(1851388, 'Ángel', 'Tejeda', 'Tiscareño', 'LCC', 5)
INSERT INTO Alumno Values(1234567, 'José Santos', 'Flores', 'Silva', 'LCC', 5)
INSERT INTO Alumno Values(2234456, 'José Raúl', 'Evangelista', 'Mendoza', 'LCC', 5)
INSERT INTO Alumno Values(1122334, 'Sofía Alejandra', 'Gaytán', 'Díaz', 'LCC', 5)
INSERT INTO Alumno Values(5544321, 'Silvia Ivonne', 'Algo', 'Algo', 'LCC', 5)
INSERT INTO Alumno Values(1011223, 'Edson Yael', 'García', 'Silva', 'LCC', 5)
INSERT INTO Alumno Values(1123434, 'Ricardo', 'López', 'Gutierrez', 'LA', 3)
INSERT INTO Alumno Values(2314234, 'Hugo Javier', 'Martínez', 'Moreno', 'LF', 3)
INSERT INTO Alumno Values(4248642, 'Andrea Natalia', 'Morales', 'Guerra', 'LM', 4)
INSERT INTO Alumno Values(8852162, 'Deno', 'Manzo', 'Bargas', 'LSTI', 4)
INSERT INTO Alumno Values(7321453, 'Andrés Eduardo', 'Garza', 'Solís', 'LMAD', 3)
INSERT INTO Alumno Values(1552133, 'Rebeca', 'Rodriguez', 'Rodriguez', 'LSTI', 5)
INSERT INTO Alumno Values(1234127, 'Jorge Andrés', 'Sólís', 'Sánchez', 'LM', 5)

INSERT INTO Alumno_Grupo Values(1851388, 11, 2)
INSERT INTO Alumno_Grupo Values(1851388, 11, 1)
INSERT INTO Alumno_Grupo Values(1851388, 11, 4)
INSERT INTO Alumno_Grupo Values(1234567, 11, 2)
INSERT INTO Alumno_Grupo Values(1234567, 11, 4)
INSERT INTO Alumno_Grupo Values(2234456, 11, 2)
INSERT INTO Alumno_Grupo Values(2234456, 11, 4)
INSERT INTO Alumno_Grupo Values(1122334, 11, 2)
INSERT INTO Alumno_Grupo Values(1122334, 11, 1)
INSERT INTO Alumno_Grupo Values(1122334, 11, 4)
INSERT INTO Alumno_Grupo Values(5544321, 10, 2)
INSERT INTO Alumno_Grupo Values(5544321, 11, 1)
INSERT INTO Alumno_Grupo Values(1011223, 10, 2)
INSERT INTO Alumno_Grupo Values(1123434, 6, 5)
INSERT INTO Alumno_Grupo Values(2314234, 6, 5)
INSERT INTO Alumno_Grupo Values(2314234, 11, 3)
INSERT INTO Alumno_Grupo Values(4248642, 6, 5)
INSERT INTO Alumno_Grupo Values(8852162, 6, 5)
INSERT INTO Alumno_Grupo Values(8852162, 11, 3)
INSERT INTO Alumno_Grupo Values(7321453, 6, 5)
INSERT INTO Alumno_Grupo Values(7321453, 4, 6)
INSERT INTO Alumno_Grupo Values(1552133, 6, 5)
INSERT INTO Alumno_Grupo Values(1234127, 6, 5)

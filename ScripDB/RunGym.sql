CREATE DATABASE RunGym

USE RunGym
use master

-- tabla Usuarios --
CREATE TABLE Usuarios(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Apellido NVARCHAR(100) NOT NULL,
	TipoDocumento NVARCHAR(20) NOT NULL,
	Documento NVARCHAR(20) NOT NULL UNIQUE,
	Correo NVARCHAR(255) NOT NULL UNIQUE,
	Contraseña NVARCHAR(200) NOT NULL,
	Celular NVARCHAR(20) NOT NULL, 
    Genero NVARCHAR(10) NOT NULL CHECK (Genero IN ('Masculino', 'Femenino', 'Otro')),
    FechaNacimiento DATE NOT NULL,
    Peso INT NOT NULL,
    Altura DECIMAL(4,2) NOT NULL,
    HorasSueno TINYINT NOT NULL CHECK (HorasSueno BETWEEN 0 AND 24),
    ConsumoAgua NVARCHAR(100) NOT NULL,
	FechaRegistro DATETIME DEFAULT GETDATE(),
	CodigoVerificacion NVARCHAR(100) NULL,
	CodigoExpira DATETIME NULL,

	IdRol INT NOT NULL,
	FOREIGN KEY (IdRol) REFERENCES Rol(Id)
);

CREATE TABLE Rol(
Id INT IDENTITY(1,1) PRIMARY KEY,
Rol NVARCHAR(50)
);

-- tabla metas --
CREATE TABLE Metas(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    IdUsuario INT FOREIGN KEY REFERENCES Usuarios(Id) NOT NULL,
    MetaPrincipal NVARCHAR(100) NOT NULL,
    CuerpoActual NVARCHAR(100) NOT NULL,
    CuerpoDeseado NVARCHAR(100)NOT NULL,
    Descripción NVARCHAR(MAX)NOT NULL,
	FechaObjetivo DATETIME NOT NULL,
	FechaCreacion DATETIME DEFAULT GETDATE()
);

CREATE TABLE Ejercicios(
    Id INT PRIMARY KEY IDENTITY,
    Nombre NVARCHAR(100) NOT NULL,
	Categoria NVARCHAR(100),
    ImagenURL NVARCHAR(255),
);


CREATE TABLE DetallesEjercicio(
    Id INT PRIMARY KEY IDENTITY ,
    EjercicioId INT,
    Descripcion TEXT,
    Repeticiones NVARCHAR(100),
    Descanso NVARCHAR(100),
    Cuidados TEXT,
	URLVideo NVARCHAR(255),
    FOREIGN KEY (EjercicioId) REFERENCES Ejercicios(Id)
);

CREATE TABLE PasosEjercicio(
    Id INT PRIMARY KEY IDENTITY,
    EjercicioId INT,
    NumeroPaso INT,
    DescripcionPaso TEXT,
    FOREIGN KEY (EjercicioId) REFERENCES Ejercicios(Id)
);




//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

CREATE TABLE TipoDieta (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(50) NOT NULL -- e.g. Hipercalórica, Equilibrada, Hipocalórica
);

CREATE TABLE Dieta (
    Id INT PRIMARY KEY IDENTITY(1,1),
    TipoDietaId INT FOREIGN KEY REFERENCES TipoDieta(Id),
    Desayuno NVARCHAR(500) NOT NULL,
    Almuerzo NVARCHAR(500) NOT NULL,
    Cena NVARCHAR(500) NOT NULL,
    Snacks NVARCHAR(500) NOT NULL
);

///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

-- Tabla Contactos --
CREATE TABLE ErroresReportados(
Id INT IDENTITY (1,1),
Nombre NVARCHAR(100) NOT NULL,
Correo NVARCHAR(255) NOT NULL,
Celular NVARCHAR(10) NOT NULL,
Asunto NVARCHAR(500) NOT NULL,
Mensaje NVARCHAR(1000) NOT NULL,
FechaReporte DATETIME DEFAULT GETDATE(),
);

select * from Usuarios
select * from Rol
select * from Metas
select * from Dieta
select * from TipoDieta
select * from DetallesEjercicio
select * from Ejercicios
select * from PasosEjercicio
select * from ErroresReportados


DELETE from Usuarios
delete from metas
delete from ErroresReportados
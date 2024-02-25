Create Database PruebaTecnica02FJMAV2

use PruebaTecnica02FJMAV2

--Tabla que contiene las marcas
create table Marcas(
MarcaId int primary key identity(1,1),
Nombre nvarchar(100) not null
)

--Tabla que contiene los modelos de celular
create table Celulares(
CelularId int primary key identity(1,1),
Nombre nvarchar(100) not null,
Precio decimal(10,2) not null,
Descripcion nvarchar(max),
Imagen image,
MarcaId int foreign key references Marcas(MarcaId) not null
)
Create database GIGEROA_DB
go
use GIGEROA_DB
go
Create table Tipos (
	IDTipo bigint identity(1,1) primary key,
	Identificador bigint unique not null,
	Nombre varchar(150) default 'No cargado',
	Activo bit default 1
)
go
Create table Provincias (
	IDProvincia bigint identity(1,1) primary key,
	Identificador bigint unique not null,
	Nombre varchar(150) default 'No cargado',
	Activo bit default 1
)
go
Create table Domicilios (
	IDDomicilio bigint identity(1,1) primary key,
	IDProvincia bigint foreign key references Provincias(IDProvincia),
	Ciudad varchar(150) default 'No cargado',
	Calle varchar(150) default 'No cargado',
	Numero bigint not null,
	Piso varchar (50) null,
	Depto varchar (50) null,
	Referencia varchar(150) default 'No cargado'
)
go
Create table Usuarios (
	IDUsuario bigint identity(1,1) primary key,
	Email varchar(150) unique not null,
	Contra varchar(150) not null,
	FechaNac date not null,
	Nombres varchar(150) default 'No cargado',
	Apellidos varchar(150) default 'No cargado',
	DNI bigint check (DNI > 10000000) not null,
	IDDomicilio bigint foreign key references Domicilios(IDDomicilio),
	IDTipo bigint foreign key references Tipos(IDTipo),
	Telefono bigint null,
	Activo bit default 1,
)
go
Create table Marcas (
	IDMarca bigint identity(1,1) primary key,
	Identificador bigint unique not null,
	Nombre varchar(50) default 'No cargado',
	Activo bit default 1
)
go
Create table Categorias (
	IDCategoria bigint identity(1,1) primary key,
	Identificador bigint unique not null,
	Nombre varchar(50) default 'No cargado',
	Activo bit default 1
)
go
Create table Articulos (
	IDArticulo bigint identity(1,1) primary key,
	Identificador bigint unique not null,
	IDMarca bigint foreign key references Marcas(IDMarca),
	Nombre varchar(150) default 'No cargado',
	Descripcion varchar(150) default 'No cargado',
	EsMateriaPrima bit not null,
	ImagenURL varchar(300) not null,
	Activo bit default 1,
	Precio decimal(18,2) not null,
)
go
Create table Articulos_x_Categoria (
	IDArticulo bigint foreign key references Articulos(IDArticulo),
	IDCategoria bigint foreign key references Categorias(IDCategoria),
)
go
Create table Favoritos_x_Usuario (
	IDFavorito bigint identity (1,1) primary key,
	IDUsuario bigint foreign key references Usuarios(IDUsuario),
	IDArticulo bigint foreign key references Articulos(IDArticulo)
)
go
insert into Provincias (Nombre, Identificador)
values ('Buenos Aires',1)
go
insert into Provincias (Nombre, Identificador)
values ('Catamarca',2)
go
insert into Provincias (Nombre, Identificador)
values ('Chaco',3)
go
insert into Provincias (Nombre, Identificador)
values ('Chubut',4)
go
insert into Provincias (Nombre, Identificador)
values ('Córdoba',5)
go
insert into Provincias (Nombre, Identificador)
values ('Corrientes',6)
go
insert into Provincias (Nombre, Identificador)
values ('Entre Ríos',7)
go
insert into Provincias (Nombre, Identificador)
values ('Formosa',8)
go
insert into Provincias (Nombre, Identificador)
values ('Jujuy',9)
go
insert into Provincias (Nombre, Identificador)
values ('La Pampa',10)
go
insert into Provincias (Nombre, Identificador)
values ('La Rioja',11)
go
insert into Provincias (Nombre, Identificador)
values ('Mendoza',12)
go
insert into Provincias (Nombre, Identificador)
values ('Misiones',13)
go
insert into Provincias (Nombre, Identificador)
values ('Neuquén',14)
go
insert into Provincias (Nombre, Identificador)
values ('Río Negro',15)
go
insert into Provincias (Nombre, Identificador)
values ('Salta',16)
go
insert into Provincias (Nombre, Identificador)
values ('San Juan',17)
go
insert into Provincias (Nombre, Identificador)
values ('Santa Cruz',18)
go
insert into Provincias (Nombre, Identificador)
values ('Santa Fe',19)
go
insert into Provincias (Nombre, Identificador)
values ('Santiago del Estero',20)
go
insert into Provincias (Nombre, Identificador)
values ('Tierra del Fuego',21)
go
insert into Provincias (Nombre, Identificador)
values ('Tucumán',22)
go
insert into Tipos (Nombre, Identificador)
values ('Administrador',1)
go
insert into Tipos (Nombre, Identificador)
values ('Vendedor',2)
go
insert into Tipos (Nombre, Identificador)
values ('Cliente',3)
go
insert into Domicilios (IDProvincia, Ciudad, Calle, Numero, Piso, Depto, Referencia)
values (1,'Belén de escobar','Rivadavia',631,'PA','E','Entre un negocio de cosas de bebés y un local de videojuegos')
go
insert into Usuarios (Email, Contra, FechaNac, Nombres, Apellidos, DNI, IDDomicilio, IDTipo, Telefono)
values ('guillermo.gigeroa@hotmail.com','queteimporta','15-09-1995','Guillermo Adrián', 'Gigeroa', 39112399, 1, 1, 1169221781)
go
insert into Marcas (Nombre, Identificador)
values ('Artesanal Catiana',1)
go
insert into Marcas (Nombre, Identificador)
values ('Reina Ramona',2)
go
insert into Marcas (Nombre, Identificador)
values ('Muaa',3)
go
insert into Marcas (Nombre, Identificador)
values ('Cheeky',4)
go
insert into Marcas (Nombre, Identificador)
values ('Zara',5)
go
insert into Marcas (Nombre, Identificador)
values ('Mimo & Co',6)
go
insert into Marcas (Nombre, Identificador)
values ('47 Street',7)
go
insert into Marcas (Nombre, Identificador)
values ('Nike',8)
go
insert into Marcas (Nombre, Identificador)
values ('Adidas',9)
go
insert into Marcas (Nombre, Identificador)
values ('Lacoste',10)
go
insert into Categorias(Nombre, Identificador)
values ('Lana',1)
go
insert into Categorias(Nombre, Identificador)
values ('Medias',2)
go
insert into Categorias(Nombre, Identificador)
values ('Pantalones',3)
go
insert into Categorias(Nombre, Identificador)
values ('Ropa interior',4)
go
insert into Categorias(Nombre, Identificador)
values ('Barbijos',5)
go
insert into Categorias(Nombre, Identificador)
values ('Abrigos',6)
go
insert into Categorias(Nombre, Identificador)
values ('Sweaters',7)
go
insert into Categorias(Nombre, Identificador)
values ('Bufandas',8)
go
insert into Categorias(Nombre, Identificador)
values ('Gorros',9)
go
insert into Categorias(Nombre, Identificador)
values ('Friselina',10)
go
insert into Categorias(Nombre, Identificador)
values ('Jeans',11)
go
insert into Categorias(Nombre, Identificador)
values ('Buzos',12)
go
insert into Categorias(Nombre, Identificador)
values ('Remeras',13)
go
insert into Categorias(Nombre, Identificador)
values ('Jogging',14)
go
insert into Articulos (IDMarca, Identificador, Nombre, Descripcion, EsMateriaPrima, ImagenURL, Precio)
values (1,1,'Medias de lana','Medias que son de lana',0,'https://farm8.staticflickr.com/7266/8160731619_d2a7b5304d_z.jpg','200.40')
go
insert into Articulos (IDMarca, Identificador, Nombre, Descripcion, EsMateriaPrima, ImagenURL, Precio)
values (1,2,'Chaleco de lana','Un chaleco de lana',0,'https://i.pinimg.com/originals/58/e4/a3/58e4a39f22dcfaf398951ee3588c8161.jpg','599.90')
go
insert into Articulos_x_Categoria (IDArticulo, IDCategoria)
values (1,1)
go
insert into Articulos_x_Categoria (IDArticulo, IDCategoria)
values (1,2)
go
insert into Favoritos_x_Usuario (IDUsuario, IDArticulo)
values (1,1)
go
-- Creo un Store Procedure en el cual listo todos los artículos con sus respectivas marcas, luego otro SP para ver las categorías de c/u 
create procedure SP_ListarArticulosSinCategoria
as
select A.Identificador as [ID_Articulo], A.Nombre, M.Identificador as [ID_Marca], M.Nombre as [Marca], A.Descripcion, A.EsMateriaPrima, A.ImagenURL as [URL_Imagen], A.Activo as [Estado], A.Precio
from Articulos as A
inner join Marcas as M on A.IDMarca = M.IDMarca
go
-- Busca con un SP, con el Identificador de Articulo, las categorías del mismo
create procedure SP_BuscarCategoriasDelArticulo (
	@Identificador bigint
)
as
select C.Identificador as [ID_Categoria], C.Nombre as [Categoria], C.Activo as [Activo]
from Articulos as A
inner join Marcas as M on A.IDMarca = M.IDMarca
inner join Articulos_x_Categoria as AXC on AXC.IDArticulo = A.IDArticulo
inner join Categorias as C on C.IDCategoria = AXC.IDCategoria
where A.Identificador = @Identificador
go
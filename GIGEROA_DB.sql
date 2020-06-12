Create database GIGEROA_DB
go
use GIGEROA_DB
go
Create table Tipos (
	IDTipo bigint identity(1,1) primary key,
	Nombre varchar(150) default 'No cargado',
	Activo bit default 1
)
go
Create table Provincias (
	IDProvincia bigint identity(1,1) primary key,
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
	Nombre varchar(50) default 'No cargado',
	Activo bit default 1
)
go
Create table Categorias (
	IDCategoria bigint identity(1,1) primary key,
	Nombre varchar(50) default 'No cargado',
	Activo bit default 1
)
go
Create table Articulos (
	IDArticulo bigint identity(1,1) primary key,
	IDMarca bigint foreign key references Marcas(IDMarca),
	Descripcion varchar(150) default 'No cargado',
	EsMateriaPrima bit not null,
	ImagenURL varchar(300) not null,
	Activo bit default 1,
	Precio decimal not null,
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
insert into Provincias (Nombre)
values ('Buenos Aires')
go
insert into Provincias (Nombre)
values ('Catamarca')
go
insert into Provincias (Nombre)
values ('Chaco')
go
insert into Provincias (Nombre)
values ('Chubut')
go
insert into Provincias (Nombre)
values ('Córdoba')
go
insert into Provincias (Nombre)
values ('Corrientes')
go
insert into Provincias (Nombre)
values ('Entre Ríos')
go
insert into Provincias (Nombre)
values ('Formosa')
go
insert into Provincias (Nombre)
values ('Jujuy')
go
insert into Provincias (Nombre)
values ('La Pampa')
go
insert into Provincias (Nombre)
values ('La Rioja')
go
insert into Provincias (Nombre)
values ('Mendoza')
go
insert into Provincias (Nombre)
values ('Misiones')
go
insert into Provincias (Nombre)
values ('Neuquén')
go
insert into Provincias (Nombre)
values ('Río Negro')
go
insert into Provincias (Nombre)
values ('Salta')
go
insert into Provincias (Nombre)
values ('San Juan')
go
insert into Provincias (Nombre)
values ('Santa Cruz')
go
insert into Provincias (Nombre)
values ('Santa Fe')
go
insert into Provincias (Nombre)
values ('Santiago del Estero')
go
insert into Provincias (Nombre)
values ('Tierra del Fuego')
go
insert into Provincias (Nombre)
values ('Tucumán')
go
insert into Tipos (Nombre)
values ('Administrador')
go
insert into Tipos (Nombre)
values ('Vendedor')
go
insert into Tipos (Nombre)
values ('Cliente')
go
insert into Domicilios (IDProvincia, Ciudad, Calle, Numero, Piso, Depto, Referencia)
values (1,'Belén de escobar','Rivadavia',631,'PA','E','Entre un negocio de cosas de bebés y un local de videojuegos')
go
insert into Usuarios (Email, Contra, FechaNac, Nombres, Apellidos, DNI, IDDomicilio, IDTipo, Telefono)
values ('guillermo.gigeroa@hotmail.com','queteimporta','15-09-1995','Guillermo Adrián', 'Gigeroa', 39112399, 1, 1, 1169221781)
go
insert into Marcas (Nombre)
values ('Muaa')
go
insert into Marcas (Nombre)
values ('Cheeky')
go
insert into Marcas (Nombre)
values ('Zara')
go
insert into Marcas (Nombre)
values ('Mimo & Co')
go
insert into Marcas (Nombre)
values ('Artesanal Catiana')
go
insert into Marcas (Nombre)
values ('Reina Ramona')
go
insert into Marcas (Nombre)
values ('47 Street')
go
insert into Marcas (Nombre)
values ('Nike')
go
insert into Marcas (Nombre)
values ('Adidas')
go
insert into Marcas (Nombre)
values ('Lacoste')
go
insert into Categorias(Nombre)
values ('Lana')
go
insert into Categorias(Nombre)
values ('Medias')
go
insert into Categorias(Nombre)
values ('Pantalones')
go
insert into Categorias(Nombre)
values ('Ropa interior')
go
insert into Categorias(Nombre)
values ('Barbijos')
go
insert into Categorias(Nombre)
values ('Abrigos')
go
insert into Categorias(Nombre)
values ('Sweaters')
go
insert into Categorias(Nombre)
values ('Bufandas')
go
insert into Categorias(Nombre)
values ('Gorros')
go
insert into Categorias(Nombre)
values ('Friselina')
go
insert into Categorias(Nombre)
values ('Jeans')
go
insert into Categorias(Nombre)
values ('Buzos')
go
insert into Categorias(Nombre)
values ('Remeras')
go
insert into Categorias(Nombre)
values ('Jogging')
go
insert into Articulos (IDMarca, Descripcion, EsMateriaPrima, ImagenURL, Precio)
values (5,'Medias de lana',0,'https://farm8.staticflickr.com/7266/8160731619_d2a7b5304d_z.jpg',200.50)
go
insert into Articulos_x_Categoria (IDArticulo, IDCategoria)
values (1,1)
go
insert into Articulos_x_Categoria (IDArticulo, IDCategoria)
values (1,2)
go
insert into Favoritos_x_Usuario (IDUsuario, IDArticulo)
values (1,1)
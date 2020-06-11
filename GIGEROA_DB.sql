Create database GIGEROA_DB
go
use GIGEROA_DB 
go
Create table Tipos (
	IDTipo bigint identity(1,1) primary key,
	Nombre varchar(50) default 'No cargado',
	Activo bit default 1
)
go
Create table Provincias (
	IDProvincia bigint identity(1,1) primary key,
	Nombre varchar(50) default 'No cargado',
	Activo bit default 1
)
go
Create table Domicilios (
	IDDomicilio bigint identity(1,1) primary key,
	IDProvincia bigint foreign key references Provincias(IDProvincia),
	Ciudad varchar(50) default 'No cargado',
	Calle varchar(50) default 'No cargado',
	Numero bigint default 0,
	Piso bigint null,
	Depto bigint null,
	Referencia varchar(150) default 'No cargado'
)
go
Create table Usuarios (
	IDUsuario bigint identity(1,1) primary key,
	Email varchar(50) unique not null,
	Contra varchar(50) not null,
	FechaNac date not null,
	Nombre varchar(50) default 'No cargado',
	Apellido varchar(50) default 'No cargado',
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
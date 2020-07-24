use master
go
drop database GIGEROA_DB
go
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
Create table Usuarios (
	IDUsuario bigint identity(1,1) primary key,
	Email varchar(150) unique not null,
	Contra varchar(150) not null,
	Nombres varchar(150) default 'No cargado',
	Apellidos varchar(150) default 'No cargado',
	DNI bigint check (DNI >= 10000) not null,
	IDTipo bigint foreign key references Tipos(IDTipo),
	Telefono bigint null,
	Activo bit default 1,
)
go
Create table Domicilios (
	IDDomicilio bigint identity(1,1) primary key,
	IDProvincia bigint foreign key references Provincias(IDProvincia),
	IDUsuario bigint foreign key references Usuarios(IDUsuario),
	Ciudad varchar(150) default 'No cargado',
	Calle varchar(150) default 'No cargado',
	Numero bigint not null,
	Piso varchar (50) null,
	Depto varchar (50) null,
	CP bigint not null,
	Referencia varchar(150) default 'No cargado'
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
	ImagenURL varchar(1000) not null,
	Activo bit default 1,
	Precio decimal(18,2) not null check (Precio >= 0),
	Stock bigint default 1 not null check (Stock >= 0),
)
go
Create table Articulos_x_Categoria (
	IDArticulo bigint foreign key references Articulos(IDArticulo),
	IDCategoria bigint foreign key references Categorias(IDCategoria),
)
go
Create table Favoritos_x_Usuario (
	IDUsuario bigint foreign key references Usuarios(IDUsuario),
	IDArticulo bigint foreign key references Articulos(IDArticulo)
)
go
Create table Facturas(
	Numero bigint identity(1,1) primary key,
	Pago bit default 1 not null,
	Cancelada bit default 0 not null,
)
go
Create table EstadosDeVenta(
	IDEstado bigint primary key identity(1,1),
	Nombre varchar(150) not null,
)
go
Create table Ventas(
IDVentas bigint not null primary key identity(1,1),
NumeroFactura bigint not null foreign key references Facturas(Numero),
IDUsuario bigint not null foreign key references Usuarios(IDUsuario),
IDEstado bigint foreign key references EstadosDeVenta(IDEstado),
Fecha datetime default (Getdate())
)
go
create table Articulos_x_ventas(
	IDVenta bigint not null foreign key references Ventas(IDVentas),
	IDArticulo bigint not null foreign key references Articulos(IDArticulo),
	Cantidad bigint not null check (Cantidad >= 0),
)
go
-- Creo un Store Procedure en el cual listo todos los artículos con sus respectivas marcas, luego otro SP para ver las categorías de c/u 
create procedure SP_ListarArticulosSinCategoria
as
select A.Identificador as [ID_Articulo], A.Nombre, M.Identificador as [ID_Marca], M.Nombre as [Marca], A.Descripcion, A.EsMateriaPrima, A.Stock , A.ImagenURL as [URL_Imagen], A.Activo as [Estado], A.Precio
from Articulos as A
inner join Marcas as M on A.IDMarca = M.IDMarca
go
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
create view VW_Marcas
as
select Identificador as [ID_Marca], Nombre as [Marca]from Marcas
where Activo = 1
go
create view VW_Categorias
as
select Identificador as [ID_Categorias], Nombre as [Categoria] from Categorias
where Activo = 1
go
create view VW_Provincias
as
select Nombre as [Provincia] from provincias
go
create view VW_UsuariosCompletos
as
select U.IDUsuario, U.Email, U.Contra, U.Activo, U.Nombres, U.Apellidos, U.DNI, U.Telefono, T.Identificador as ID_Tipo, T.Nombre as Tipo, P.Nombre as Provincia, D.Ciudad, D.Calle, D.Numero, D.Piso, D.Depto, D.CP, D.Referencia from Usuarios as U
left join Domicilios as D on U.IDUsuario = D.IDUsuario
left join Provincias as P on D.IDProvincia = P.IDProvincia
left join Tipos as T on U.IDTipo = T.IDTipo
where U.Activo = 1 
go
create procedure SP_AltaUsuario(
	@Email varchar(150),
	@Password varchar(150),
	@Nombres varchar(150),
	@Apellidos varchar(150),
	@DNI bigint,
	@IDProvincia bigint,
	@Ciudad varchar(150),
	@Calle varchar(150),
	@Numero bigint,
	@Piso varchar(150),
	@CP bigint,
	@Departamento varchar(150),
	@Referencia varchar(150),
	@IDTipo bigint,
	@Telefono bigint,
	@Activo bit
)
as
Begin
	Begin try
		BEGIN transaction
			Declare @IDUsuario bigint
			INSERT INTO Usuarios (Email,Contra,Nombres,Apellidos,DNI,Telefono,Activo,IDTipo)
			VALUES (@Email,@Password,@Nombres,@Apellidos,@DNI,@Telefono,@Activo,@IDTipo)
			Set @IDUsuario = (Select Top 1 IDUsuario From Usuarios Order by IDUsuario Desc)
			INSERT INTO Domicilios (IDProvincia,IDUsuario,Ciudad,Calle,Numero,Piso,Depto,CP,Referencia)
			VALUES (@IDProvincia,@IDUsuario,@Ciudad,@Calle,@Numero,@Piso,@Departamento,@CP,@Referencia)
		COMMIT transaction
	End try
	Begin catch  
		Rollback transaction
		Raiserror('Error al agregar el usuario',16,2)
	End catch
End
go
create procedure SP_ActualizarUsuario(
	@IDUsuario bigint,
	@Email varchar(150),
	@Password varchar(150),
	@Nombres varchar(150),
	@Apellidos varchar(150),
	@DNI bigint,
	@IDProvincia bigint,
	@Ciudad varchar(150),
	@Calle varchar(150),
	@Numero bigint,
	@Piso varchar(150),
	@CP bigint,
	@Departamento varchar(150),
	@Referencia varchar(150),
	@IDTipo bigint,
	@Telefono bigint,
	@Activo bit
)
as
Begin
	Begin try
		BEGIN transaction
			UPDATE Usuarios
			Set
				Email = @Email,
				Contra = @Password,
				Nombres = @Nombres,
				Apellidos = @Apellidos,
				DNI = @DNI,
				Telefono = @Telefono,
				Activo = @Activo,
				IDTipo = @IDTipo
			Where IDUsuario = @IDUsuario
			UPDATE Domicilios
			Set
				IDProvincia = @IDProvincia,
				Ciudad = @Ciudad,
				Calle = @Calle,
				Numero = @Numero,
				Piso = @Piso,
				Depto = @Departamento,
				CP = @CP,
				Referencia = @Referencia
			Where IDUsuario = @IDUsuario
		COMMIT transaction
	End try
	Begin catch  
		Rollback transaction
		Raiserror('Error al actualizar el usuario',16,2)
	End catch
End
go
create view VW_Tipos
as
Select Identificador as [ID_Tipo], Nombre from Tipos
where Activo = 1
go
create procedure SP_BajaUsuario(
	@IDUsuario bigint,
	@Activo bit
)
as
Begin
	Begin try
		BEGIN transaction
			UPDATE Usuarios
			Set Activo = @Activo
			Where IDUsuario = @IDUsuario
		COMMIT transaction
	End try
	Begin catch  
		Rollback transaction
		Raiserror('Error al eliminar el usuario',16,2)
	End catch
End
go
create procedure SP_AgregarArticulo(
	@IDMarca bigint,
	@IDCategoria bigint,
	@Nombre varchar(150),
	@Descripcion varchar(150),
	@EsMateriaPrima bit,
	@ImagenURL varchar(1000),
	@Precio decimal(18,2)
)
as
Begin
	Begin try
		BEGIN transaction
			insert into Articulos (IDMarca, Identificador, Nombre, Descripcion, EsMateriaPrima, ImagenURL, Precio)
			values (@IDMarca,(select count(*) from Articulos)+1,@Nombre,@Descripcion,@EsMateriaPrima,@ImagenURL,@Precio)
			insert into Articulos_x_Categoria (IDArticulo,IDCategoria)
			values ((select top 1 IDArticulo from Articulos order by IDArticulo desc),@IDCategoria)
		COMMIT transaction
	End try
	Begin catch  
		Rollback transaction
		Raiserror('Error al agregar el articulo',16,2)
	End catch
End
go
create procedure SP_AgregarCategoriaAlUltimoArticulo(
	@IDCategoria bigint
)
as
Begin
	Begin try
		BEGIN transaction
			insert into Articulos_x_Categoria (IDArticulo,IDCategoria)
			values ((select top 1 IDArticulo from Articulos order by IDArticulo desc),@IDCategoria)
		COMMIT transaction
	End try
	Begin catch  
		Rollback transaction
		Raiserror('Error al agregar categoria al articulo',16,2)
	End catch
End
go
create procedure SP_AgregarCategoria(
	@Nombre varchar(50)
)
as
Begin
	Begin try
		BEGIN transaction
			insert into Categorias(Nombre, Identificador)
			values (@Nombre, (select count(*) from Categorias)+1)
		COMMIT transaction
	End try
	Begin catch  
		Rollback transaction
		Raiserror('Error al agregar categoria nueva',16,2)
	End catch
End
go
create procedure SP_AgregarMarca(
	@Nombre varchar(50)
)
as
Begin
	Begin try
		BEGIN transaction
			insert into Marcas(Nombre, Identificador)
			values (@Nombre, (select count(*) from Marcas)+1)
		COMMIT transaction
	End try
	Begin catch  
		Rollback transaction
		Raiserror('Error al agregar marca nueva',16,2)
	End catch
End
go
create procedure SP_BajaCategoria(
	@IDCategoria bigint,
	@Activo bit
)
as
Begin
	Begin try
		BEGIN transaction
			Update Categorias
			Set Activo = @Activo
			Where Identificador = @IDCategoria
		COMMIT transaction
	End try
	Begin catch  
		Rollback transaction
		Raiserror('Error al dar de baja la categoria',16,2)
	End catch
End
go
create procedure SP_BajaMarca(
	@IDMarca bigint,
	@Activo bit
)
as
Begin
	Begin try	
		BEGIN transaction
			Update Marcas
			Set Activo = @Activo
			Where Identificador = @IDMarca
		COMMIT transaction
	End try
	Begin catch  
		Rollback transaction
		Raiserror('Error al dar de baja la marca',16,2)
	End catch
End
go
create procedure SP_ModificarArticulo(
	@IDArticulo bigint,
	@IDMarca bigint,
	@Nombre varchar(150),
	@Descripcion varchar(150),
	@EsMateriaPrima bit,
	@ImagenURL varchar(1000),
	@Precio decimal(18,2)
)
as
Begin
	Begin try
		BEGIN transaction
			Update Articulos
			Set IDMarca = @IDMarca, Nombre = @Nombre, Descripcion = @Descripcion, EsMateriaPrima = @EsMateriaPrima, ImagenURL = @ImagenURL, Precio = @Precio
			where Identificador = @IDArticulo
		COMMIT transaction
	End try
	Begin catch  
		Rollback transaction
		Raiserror('Error al modificar el articulo',16,2)
	End catch
End
go
create procedure SP_LimpiarCategorias(
	@IDArticulo bigint
)
as
Begin
	Begin try
		BEGIN transaction
			Delete from Articulos_x_Categoria
			Where IDArticulo = @IDArticulo
		COMMIT transaction
	End try
	Begin catch  
		Rollback transaction
		Raiserror('Error al limpiar categorías del articulo',16,2)
	End catch
End
go
create procedure SP_ActualizarCategorias(
	@IDArticulo bigint,
	@IDCategoria bigint
)
as
Begin
	Begin try
		BEGIN transaction
			insert into Articulos_x_Categoria (IDArticulo,IDCategoria)
			values (@IDArticulo,@IDCategoria)
		COMMIT transaction
	End try
	Begin catch  
		Rollback transaction
		Raiserror('Error al actualizar categorias del articulo',16,2)
	End catch
End
go
create view VW_UsuariosCompletosAdmin
as
select U.IDUsuario, U.Email, U.Contra, U.Activo, U.Nombres, U.Apellidos, U.DNI, U.Telefono, T.Identificador as ID_Tipo, T.Nombre as Tipo, P.Nombre as Provincia, D.Ciudad, D.Calle, D.Numero, D.Piso, D.Depto, D.CP, D.Referencia from Usuarios as U
left join Domicilios as D on U.IDUsuario = D.IDUsuario
left join Provincias as P on D.IDProvincia = P.IDProvincia
left join Tipos as T on U.IDTipo = T.IDTipo
go
create procedure SP_BajaArticulo(
	@IDArticulo bigint,
	@Activo bit
)
as
Begin
	Begin try
		BEGIN transaction
			Update Articulos
			Set Activo = @Activo
			Where Identificador = @IDArticulo
		COMMIT transaction
	End try
	Begin catch  
		Rollback transaction
		Raiserror('Error al dar de baja el articulo',16,2)
	End catch
End
go

create view VW_UltimaFactura
as
select top 1 Numero from Facturas
order by Numero desc
go
--Procedimiento almacenado para generar una factura
create procedure SP_CrearFactura
as
Begin
	Begin try
		BEGIN transaction
			insert into Facturas (Pago)
			values (1)
		COMMIT transaction
	End try
	Begin catch  
		Rollback transaction
		Raiserror('Error al generar la factura',16,2)
	End catch
End
go
--Procedimiento almacenado para generar una venta
create procedure SP_AgregarVenta(
	@NumeroFactura bigint,
	@IDUsuario bigint
)
as
Begin
	Begin try
		BEGIN transaction
			Insert into Ventas (NumeroFactura, IDUsuario, IDEstado)
			Values (@NumeroFactura,@IDUsuario,1)
		COMMIT transaction
	End try
	Begin catch  
		Rollback transaction
		Raiserror('Error al generar la venta',16,2)
	End catch
End
go
--Procedimiento almacenado para agregar datos a la tabla Articulos_x_ventas
create procedure SP_AgregarVentaAXV (
	@IDArticulo bigint,
	@Cantidad bigint
)
as
Begin
	Begin try
		BEGIN transaction
			Insert into Articulos_x_ventas (IDArticulo,IDVenta,Cantidad)
			Values (@IDArticulo,(Select top 1 IDVentas from Ventas order by IDVentas desc), @Cantidad)
		COMMIT transaction
	End try
	Begin catch  
		Rollback transaction
		Raiserror('Error al generar la venta AXV',16,2)
	End catch
End
go
--Procedimiento almacenado para modificar el stock
create procedure SP_ComprarArticulo(
	@IDArticulo bigint,
	@Stock bigint
)
as
Begin
	Begin try
		BEGIN transaction
			Update Articulos
			Set Stock = Stock-@Stock
			Where IDArticulo = @IDArticulo
		COMMIT transaction
	End try
	Begin catch  
		Rollback transaction
		Raiserror('Error al modificar stock',16,2)
	End catch
End
go
--Veo las transacciones de un usuario en específico
create procedure SP_VerTransaccionesDe(
	@IDUsuario bigint
)
as
select (select convert(varchar, V.Fecha, 103)) as Fecha, (select convert(varchar, V.Fecha, 24)) as Hora, V.NumeroFactura, AXV.IDArticulo, A.Nombre, AXV.Cantidad, U.IDUsuario, U.Email, U.Telefono, U.Nombres, U.Apellidos, U.DNI, P.Nombre as Provincia, D.Ciudad, D.Calle, D.Numero, D.Piso, D.Depto, D.CP, D.Referencia, V.IDEstado, E.Nombre as Estado from Ventas as V
join EstadosDeVenta as E on V.IDEstado = E.IDEstado
join Usuarios as U on V.IDUsuario = U.IDUsuario
join Articulos_x_ventas as AXV on V.IDVentas = AXV.IDVenta
join Articulos as A on AXV.IDArticulo = A.IDArticulo
join Domicilios as D on U.IDUsuario = D.IDUsuario
join Provincias as P on D.IDProvincia = P.IDProvincia
where U.IDUsuario = @IDUsuario
order by V.NumeroFactura desc
go
--Veo todas las transacciones
create procedure SP_VerTransacciones
as
select (select convert(varchar, V.Fecha, 103)) as Fecha, (select convert(varchar, V.Fecha, 24)) as Hora, V.NumeroFactura, AXV.IDArticulo, A.Nombre, AXV.Cantidad, U.IDUsuario, U.Email, U.Telefono, U.Nombres, U.Apellidos, U.DNI, P.Nombre as Provincia, D.Ciudad, D.Calle, D.Numero, D.Piso, D.Depto, D.CP, D.Referencia, V.IDEstado, E.Nombre as Estado from Ventas as V
join EstadosDeVenta as E on V.IDEstado = E.IDEstado
join Usuarios as U on V.IDUsuario = U.IDUsuario
join Articulos_x_ventas as AXV on V.IDVentas = AXV.IDVenta
join Articulos as A on AXV.IDArticulo = A.IDArticulo
join Domicilios as D on U.IDUsuario = D.IDUsuario
join Provincias as P on D.IDProvincia = P.IDProvincia
where E.IDEstado in (1,2)
order by V.NumeroFactura desc
go
create procedure SP_VerTransaccionesAdmin
as
select (select convert(varchar, V.Fecha, 103)) as Fecha, (select convert(varchar, V.Fecha, 24)) as Hora, V.NumeroFactura, AXV.IDArticulo, A.Nombre, AXV.Cantidad, U.IDUsuario, U.Email, U.Telefono, U.Nombres, U.Apellidos, U.DNI, P.Nombre as Provincia, D.Ciudad, D.Calle, D.Numero, D.Piso, D.Depto, D.CP, D.Referencia, V.IDEstado, E.Nombre as Estado from Ventas as V
join EstadosDeVenta as E on V.IDEstado = E.IDEstado
join Usuarios as U on V.IDUsuario = U.IDUsuario
join Articulos_x_ventas as AXV on V.IDVentas = AXV.IDVenta
join Articulos as A on AXV.IDArticulo = A.IDArticulo
join Domicilios as D on U.IDUsuario = D.IDUsuario
join Provincias as P on D.IDProvincia = P.IDProvincia
order by V.NumeroFactura desc
go
create procedure SP_ActualizarEstadoVenta(
	@NumeroFactura bigint,
	@IDEstado bigint
)
as
Begin
	Begin try
		BEGIN transaction
			Update Ventas
			Set IDEstado = @IDEstado
			Where NumeroFactura = @NumeroFactura
		COMMIT transaction
	End try
	Begin catch  
		Rollback transaction
		Raiserror('Error al actualizar estado de venta',16,2)
	End catch
End
go
create procedure SP_ActualizarStockArticulo(
	@IDArticulo bigint,
	@Stock bigint
)
as
Begin
	Begin try
		BEGIN transaction
			Update Articulos
			Set Stock = @Stock
			Where IDArticulo = @IDArticulo
		COMMIT transaction
	End try
	Begin catch  
		Rollback transaction
		Raiserror('Error al actualizar stock del artículo',16,2)
	End catch
End
go
--Agrego todos los estados de venta con sus nombres
insert into EstadosDeVenta (Nombre)
values ('Pendiente'),('En camino'),('Entregado'),('Error')
go
--Agrego todas las provincias de Argentina
insert into Provincias (Nombre, Identificador)
values ('Buenos Aires',1),('Catamarca',2),('Chaco',3),('Chubut',4),('Córdoba',5),('Corrientes',6),('Entre Ríos',7),('Formosa',8),('Jujuy',9),('La Pampa',10),('La Rioja',11),('Mendoza',12),('Misiones',13),('Neuquén',14),('Río Negro',15),('Salta',16),('San Juan',17),('Santa Cruz',18),('Santa Fe',19),('Santiago del Estero',20),('Tierra del Fuego',21),('Tucumán',22)
go
--Agrego los tipos de usuarios
insert into Tipos (Nombre, Identificador)
values ('Administrador',1),('Vendedor',2),('Cliente',3)
go
--Agreego las marcas
exec SP_AgregarMarca 'Artesanal Catiana'
go
exec SP_AgregarMarca 'Reina Ramona'
go
Exec SP_AgregarMarca 'Muaa'
go
Exec SP_AgregarMarca 'Cheeky'
go
Exec SP_AgregarMarca 'Zara'
go
Exec SP_AgregarMarca 'Mimo & Co'
go
Exec SP_AgregarMarca '47 Street'
go
Exec SP_AgregarMarca 'Nike'
go
Exec SP_AgregarMarca 'Adidas'
go
Exec SP_AgregarMarca 'Lacoste'
go
--Agrego las categorías
Exec SP_AgregarCategoria 'Lana'
go
Exec SP_AgregarCategoria 'Medias'
go
Exec SP_AgregarCategoria 'Pantalones'
go
Exec SP_AgregarCategoria 'Ropa interior'
go
Exec SP_AgregarCategoria 'Barbijos'
go
Exec SP_AgregarCategoria 'Abrigos'
go
Exec SP_AgregarCategoria 'Sweaters'
go
Exec SP_AgregarCategoria 'Bufandas'
go
Exec SP_AgregarCategoria 'Gorros'
go
Exec SP_AgregarCategoria 'Friselina'
go
Exec SP_AgregarCategoria 'Jeans'
go
Exec SP_AgregarCategoria 'Buzos'
go
Exec SP_AgregarCategoria 'Remeras'
go
Exec SP_AgregarCategoria 'Jogging'
go
Exec SP_AgregarCategoria 'Chalecos'
go
Exec SP_AgregarCategoria 'Calzado'
go
--Agrego los artículos
Exec SP_AgregarArticulo 1,1,'Medias de lana','Medias abrigadas de lana',0,'https://i.ibb.co/cDJ60p7/IMG-20200604-171022-151.jpg','350'
go
Exec SP_AgregarCategoriaAlUltimoArticulo 2
go
Exec SP_AgregarCategoriaAlUltimoArticulo 6
go
--Agrego stock al artículo
Exec SP_ComprarArticulo 1,-9
go
Exec SP_AgregarArticulo 1,1,'Chaleco de lana','Un chaleco de lana abrigado',0,'https://i.ibb.co/zrmT0Mh/FB-IMG-15930994178334057.jpg','600'
go
Exec SP_AgregarCategoriaAlUltimoArticulo 6
go
Exec SP_AgregarCategoriaAlUltimoArticulo 15
go
Exec SP_ComprarArticulo 2,-9
go
Exec SP_AgregarArticulo 2,13,'Remera bordada','Remera bordada manga larga',0,'https://i.ibb.co/pr01Whz/IMG-20200623-110121-111.jpg','400'
go
Exec SP_AgregarArticulo 2,11,'Jean','Jean london talle 36-46',0,'https://i.ibb.co/zhznZdc/26-06-2020-16-38-29-678-2064338717.jpg','1500'
go
Exec SP_AgregarArticulo 1,1,'Botas de lana','Botas de lana abrigadas',0,'https://i.ibb.co/R4ndtY1/IMG-20200606-151947-034.jpg','400'
go
Exec SP_AgregarCategoriaAlUltimoArticulo 2
go
Exec SP_AgregarCategoriaAlUltimoArticulo 6
go
Exec SP_AgregarCategoriaAlUltimoArticulo 16
go
Exec SP_ComprarArticulo 3,-9
go
Exec SP_AgregarArticulo 1,1,'Gorro unisex','Gorro verde y negro',0,'https://i.ibb.co/jGXrRjG/IMG-20200614-153305-390.jpg','350'
go
Exec SP_AgregarCategoriaAlUltimoArticulo 6
go
Exec SP_AgregarCategoriaAlUltimoArticulo 9
go
Exec SP_ComprarArticulo 4,-9
go
Exec SP_AgregarArticulo 1,1,'Gorro de lana blanco','Gorro blanco',0,'https://i.ibb.co/pbr3VLZ/IMG-20200614-153305-393.jpg','350'
go
Exec SP_AgregarCategoriaAlUltimoArticulo 6
go
Exec SP_AgregarCategoriaAlUltimoArticulo 9
go
Exec SP_ComprarArticulo 5,-9
go
Exec SP_AgregarArticulo 2,13,'Remera rayada','Remera de algodón rayada color blanco y negro',0,'https://i.ibb.co/KXwLy52/IMG-20200616-115842-132.jpg','350'
go
Exec SP_ComprarArticulo 6,-9
go
Exec SP_AgregarArticulo 2,13,'Remera negra','Remera de algodón negra estampada',0,'https://i.ibb.co/GWx70Cg/IMG-20200619-144917-491.jpg','350'
go
Exec SP_ComprarArticulo 7,-9
go
Exec SP_AgregarArticulo 1,1,'Medias crochet','Medias abrigadas de lana en crochet',0,'https://i.ibb.co/xm7dtks/26-06-2020-17-19-52-915-1614353750.jpg','300'
go
Exec SP_AgregarCategoriaAlUltimoArticulo 2
go
Exec SP_AgregarCategoriaAlUltimoArticulo 6
go
Exec SP_AgregarCategoriaAlUltimoArticulo 16
go
Exec SP_ComprarArticulo 8,-9
go
Exec SP_AgregarArticulo 1,1,'Gorro de lana niños','Gorro de lana con trama de colores para niños',0,'https://i.ibb.co/7SVpctv/IMG-20200609-172018-201.jpg','400'
go
Exec SP_AgregarCategoriaAlUltimoArticulo 6
go
Exec SP_AgregarCategoriaAlUltimoArticulo 9
go
Exec SP_ComprarArticulo 9,-9
go
--Agrego los usuarios iniciales para pruebas
Exec SP_AltaUsuario 'guillermo.gigeroa@hotmail.com','s','Guillermo Adrián','Gigeroa',39112399,1,'Belén de Escobar','Rivadavia',631,'PA',1625,'E','Entre un negocio de cosas de bebés y un local de videojuegos',1,1169221781,1
go
Exec SP_AltaUsuario 'vendedor@vendedor.com','s','NombreVendedor','ApellidoVendedor',10000,2,'A','A',0,'A',0,'A','No hay',2,1163695874,1
go
Exec SP_AltaUsuario 'cliente@cliente.com','s','NombreCliente','ApellidoCliente',10000,2,'A','A',0,'A',0,'A','No hay',3,1163695874,1
go
--Agrego un favorito (A usar más adelante en el proyecto)
insert into Favoritos_x_Usuario (IDUsuario, IDArticulo)
values (1,1)
go
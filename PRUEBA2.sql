create database RegistroVehiculos
go

use RegistroVehiculos
go

create table PERSONA (
rut int primary key not null,
nombre varchar(50),
apellido varchar(50),
calle varchar(50),
numero int,
comuna varchar(50)
)

select * from PERSONA
go

--insert into PERSONA values (1,' ',' ',' ',' ',' ') 
--go

delete PERSONA 
go

create proc InsDatosPersona
(
@rut int,
@nombre varchar(50),
@apellido varchar(50),
@calle varchar(50),
@numero int,
@comuna varchar(50)
)
as insert into PERSONA values(@rut,@nombre,@apellido,@calle,@numero,@comuna)
GO

create proc modDatosPersona
(
@rut int,
@nombre varchar(50),
@apellido varchar(50),
@calle varchar(50),
@numero int,
@comuna varchar(50)
)
as
begin
update PERSONA set nombre = @nombre ,apellido = @apellido,calle = @calle ,numero = @numero,comuna = @comuna 
where rut = @rut
end
GO

exec InsDatosPersona 2,'a','a','a',1,'a'
GO


drop procedure modDatosPersona
go

--USE [RegistroVehiculos]
--GO

--DECLARE	@return_value int

--EXEC	@return_value = modDatosPersona
--		@rut = 1,
--		@nombre = N'a',
--		@apellido = N'a',
--		@calle = N'a',
--		@numero = 1,
--		@comuna = N'a'

--SELECT	'Return Value' = @return_value

--GO

--FIN PERSONAS
--FIN PERSONAS

--COMIENZO VEHICULOS
--COMIENZO VEHICULOS

use RegistroVehiculos
go

create table VEHICULO (
patente varchar(6) primary key not null,
marca varchar(50),
modelo varchar(50),
annio int,
color varchar(50),
rut int FOREIGN KEY REFERENCES PERSONA(rut)
)
go

select * from VEHICULO
go

insert into VEHICULO values ('patent','marca','modelo',2012,'color',1)
go

delete VEHICULO
go

create proc InsDatosVehiculo
(
@patente varchar(6),
@marca varchar(50),
@modelo varchar(50),
@annio int,
@color varchar(50),
@rut int
)
as insert into VEHICULO values(@patente,@marca,@modelo,@annio,@color,@rut)
GO

create proc modDatosVehiculo
(
@patente varchar(6),
@marca varchar(50),
@modelo varchar(50),
@annio int,
@color varchar(50),
@rut int
)
as
begin
update VEHICULO set marca = @marca,modelo = @modelo ,annio = @annio,color = @color, rut = @rut 
where patente = @patente
end
GO

--create proc modDatosVehiculo
--(
--@patente varchar(6),
--@marca varchar(50),
--@modelo varchar(50),
--@annio int,
--@color varchar(50),
--@rut int
--)
--as
--begin
--update VEHICULO set marca = @marca,modelo = @modelo ,annio = @annio,color = @color, rut = @rut
--where patente = @patente
--end
--GO


SELECT * FROM Vehiculo where patente = '123123'
go

update VEHICULO set  marca = 'asd',modelo = 'asd' ,annio = 123,color = 'asd', rut = 1
where patente = '123123'
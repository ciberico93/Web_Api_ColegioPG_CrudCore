create database ColegioPG

use colegioPG

create table Cursos(
   Id_cursos int primary key identity (1,1),
   Nombre varchar(50) not null,
)


create table Alumnos (
   Id int primary key identity (1,1),
   Nombre varchar(50) not null,
   Apellido varchar(50) not null,
   Direccion varchar(50) not null,
   Telefono varchar(50) not null,
   Correo varchar(50) not null,
   Id_cursos int,
   foreign key (Id_cursos) references Cursos(Id_Cursos)
)

insert into Cursos values ('Matematica')
insert into Cursos values ('Lenguaje')
insert into Cursos values ('Religion')

Select * from Cursos

insert into Alumnos values ('Carlos','Iberico','Calle Luis Galvan 102','4634692','ciberico93@gmail.com',1);
insert into Alumnos values ('Anabelen','Iberico','Calle Luis Galvan 102','4634692','ciberico93@gmail.com',2);
insert into Alumnos values ('Jose','Iberico','Calle Luis Galvan 102','4634692','ciberico93@gmail.com',3);

Select * from Alumnos


Select a.Id,a.Nombre,a.Apellido,a.Correo,a.Direccion,a.Telefono,c.Nombre as Curso 
from Alumnos a inner join Cursos c on c.Id_cursos=a.Id_cursos


create <database/table> tableName (name1 type1, name2 type2 ....) [from databaseName]
create database earth
create table people (id int,name string,home string) from earth

drop <database/table> Name from databaseName
drop database earth
drop table people from earth

insert into databaseName.tableName (name1,name2...) values ([value1,value2...]) [,([value1,value2...]),([value1,value2...])...]
insert into Japan.people(id,name,nick) values(1,lisa,aho)
insert    into    earth.people ( id ,  name  , nick)   values  ( 1 , lisa , aho ),(2,aho,lisa)  ,  (3,haha,hehe)

delete from database.tableName [where Database.Table.Name1 = Value1 <and/or> Database.Table.Name2 > Value ....]
delete from ear.peo
delete from ear.peo where ear.peo.id = 1 and ear.peo.name = sa
delete from ear.peo where ear.peo.id < 1 or ear.peo.name = sa and ear.peo.st = ssssa
delete from ear.peo where ear.peo.address = "Mars" or ear.peo.id < 1 or ear.peo.name = sa and ear.peo.st = ssssa

update DatabaseName.tableName set Database.Table.Name1 = Value1 [,Database.Table.Name2 = Value2 ...] [where Database.Table.Name1 = Value1 [,Database.Table.Name2 = Value2 ...]]
update earth.peo set name = world
update earth.peo set name = world,id = 2 where ear.peo.id=1
update earth.peo set name = world,id = 2 where ear.peo.id=1 and ear.peo.name = world
update earth.peo set name = world,id = 2 where ear.peo.id=1 or ear.peo.name = world
update earth.peo set name = world,id = 2 where ear.peo.id=1 and ear.peo.name = world or ear.peo.address = Mars

select DatabaseName.tableName.Name1 [,DatabaseName.tableName.Name2 ...] where DatabaseName.tableName.Name3 = Value1 [<and/or> DatabaseName.tableName.Name4 >Value2] 
select ear.peo.id,ear.peo.name
select ear.peo.id,ear.peo.name where ear.peo.id > 5 and ear.peo.name = sss or ear.peo.sgender = 0
create <database/table> tableName (name1 type1, name2 type2 ....) [from databaseName]
create database earth
create table people (id int,name string,home string) from earth

drop <database/table> Name from databaseName
drop database earth
drop table people from earth

insert into databaseName.tableName [(name1,name2...)] values ([value1,value2...]) [,([value1,value2...]),([value1,value2...])...]
insert into USA.people values(1,lisa,aho)
insert into Japan.people(id,name,nick) values(1,lisa,aho)
insert    into    earth.people ( id ,  name  , nick)   values  ( 1 , lisa , aho ),(2,aho,lisa)  ,  (3,haha,hehe)

delete from database.tableName [where Name1 = Value1 <and/or> Name2 > Value ....]
delete from ear.peo
delete from ear.peo where id = 1 and name = sa
delete from ear.peo where id < 1 or name = sa and st = ssssa
delete from ear.peo where address = "Mars" or id < 1 or name = sa and st = ssssa

update DatabaseName.tableName set (Name1 = Value1 [,Name2 = Value2 ...]) [where Name1 = Value1 [,Name2 = Value2 ...]]
update earth.peo set (name = world)
update earth.peo set (name = world,id = 2) where id=1
update earth.peo set (name = world,id = 2) where id=1 and name = world or address = Mars
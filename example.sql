﻿create <database/table> tableName (name1 type1, name2 type2 ....) [from databaseName]
create database earth
create table people (id int,name string,home string) from earth

drop <database/table> <table/database>Name [from databaseName]
drop database earth
drop table people from earth

insert into tableName [(name1,name2...)] values ([value1,value2...]) [,([value1,value2...]),([value1,value2...])...] from databaseName
insert into people values(1,lisa,aho) from USA
insert into people(id,name,nick) values(1,lisa,aho) from Japan
insert    into    people ( id ,  name  , nick)   values  ( 1 , lisa , aho ),(2,aho,lisa)  ,  (3,haha,hehe) from earth

delete from database.tableName [where Name1 = Value1 <and/or> Name2 > Value ....]
delete from ear.peo
delete from ear.peo where id = 1 and name = sa
delete from ear.peo where id < 1 or name = sa and st = ssssa
delete from ear.peo where address = "Mars" or id < 1 or name = sa and st = ssssa
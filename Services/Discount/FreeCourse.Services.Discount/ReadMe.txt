
-- I did not do the method to create a database table. 

create table Discount(
Id serial primary key,
UserId varchar(200) not null,
Rate smallint not null,
Code varchar(50) not null,
CreateDate timestamp not null default CURRENT_TIMESTAMP)
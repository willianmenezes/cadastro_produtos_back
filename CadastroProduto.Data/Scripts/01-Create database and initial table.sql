create database mercado_teste;

create table Product(
ProductId					uniqueidentifier primary key not null,
Name						varchar(200) not null,
Price						float not null,
UrlImage					varchar(1000) not null,
Status						bit not null,
Created						datetime not null,
Updated						datetime not null
);
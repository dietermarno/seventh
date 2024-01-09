CREATE DATABASE "seventh";
USE "seventh";

CREATE TABLE "Users" (
	"Id" INT PRIMARY KEY IDENTITY,
	"Name" VARCHAR(100) DEFAULT '',
	"Login" VARCHAR(30) DEFAULT '',
	"Password" VARCHAR(30) DEFAULT '',
	"Email" VARCHAR(100) DEFAULT ''
);

INSERT INTO "Users" ("Name", "Login", "Password", "Email") VALUES ('Dieter Marno', 'dietermarno', '12345', 'dieter@customdev.com.br');

CREATE TABLE "Servers" (
	"Id" VARCHAR(200) UNIQUE,
	"Name" VARCHAR(200) DEFAULT '',
	"IP" VARCHAR(15) DEFAULT '',
	"Port" VARCHAR(15) DEFAULT ''
);

CREATE TABLE "Videos" (
	"Id" VARCHAR(200) UNIQUE,
	"ServerId" VARCHAR(200) KEY DEFAULT '',
	"Description" VARCHAR(200) DEFAULT '',
	"SizeInBytes" BIGINT NULL DEFAULT 0,
	"Recycled" INT NULL DEFAULT 0
);

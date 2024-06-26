CREATE DATABASE db
    WITH
    OWNER = postgres
    ENCODING = 'UTF8'
    LOCALE_PROVIDER = 'libc'
    TABLESPACE = pg_default
    CONNECTION LIMIT = -1
    IS_TEMPLATE = False;

CREATE TABLE public."Classes"
(
    name character varying
);

ALTER TABLE IF EXISTS public."Classes"
    OWNER to postgres;


INSERT INTO public."Classes"(
	name)
	VALUES ('Class1'),('Class2'),('Class3')

CREATE DATABASE skip_locked_sandbox ENCODING 'UTF8' TEMPLATE template0;

INSERT INTO public.task (id, code, created, status)
VALUES (-1, 'TEST', null, 'NOT_STARTED');
INSERT INTO public.task (id, code, created, status)
VALUES (-2, 'ANOTHER', null, 'NOT_STARTED');

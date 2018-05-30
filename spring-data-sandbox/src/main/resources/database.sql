CREATE TABLE role (
  id   INTEGER PRIMARY KEY,
  code VARCHAR(40),
  name VARCHAR(40)
);

CREATE TABLE privilege (
  id      INTEGER PRIMARY KEY,
  role_id INTEGER REFERENCES role,
  code    VARCHAR(40),
  name    VARCHAR(40)
);

DELETE FROM privilege;
DELETE FROM role;

INSERT INTO role (id, name) VALUES (2000, 'ROLE_ADMIN');
INSERT INTO privilege (id, role_id, name) VALUES (5000, 2000, 'PRIVILEGE_CREATE_USER');
INSERT INTO privilege (id, role_id, name) VALUES (5001, 2000, 'PRIVILEGE_DELETE_USER');

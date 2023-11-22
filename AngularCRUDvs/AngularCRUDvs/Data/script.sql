

INSERT INTO dbo.unidad (block, dpto, UserId)
values
    ('B1', '101',0),
    ('B1', '102',0),
    ('B1', '103',0),
    ('B1', '104',0),
    ('B1', '201',0),
    ('B1', '202',0),
    ('B1', '203',0),
    ('B1', '204',0),
    ('B1', '301',0),
    ('B1', '302',0),
    ('B1', '303',0),
    ('B1', '304',0),
    ('B1', '401',0),
    ('B1', '402',0),
    ('B1', '403',0),
    ('B1', '404',0),
    ('B1', '501',0),
    ('B1', '502',0),
    ('B1', '503',0),
    ('B1', '504',0),
    ('B1', '601',0),
    ('B1', '602',0),
    ('B1', '603',0),
    ('B1', '604',0),
    ('B1', '701',0),
    ('B1', '702',0),
    ('B1', '703',0),
    ('B1', '704',0),
    ('B1', '801',0),
    ('B1', '802',0),
    ('B1', '803',0),
    ('B1', '804',0);

INSERT INTO dbo.Persona (TipoDocumento, NroDocumento, Nombres, ApellidoPaterno, ApellidoMaterno, Email, Telefono, UnidadId)
VALUES
    (NULL, NULL, 'OLGA ESPERANZA', 'ALVAREZ', 'NUÑEZ', NULL, NULL, 1),
    (NULL, NULL, 'HUMBERTO FLORENCIO', 'MAURICIO', 'FLORES', NULL, NULL, 2),
    (NULL, NULL, 'AMPARO', 'VARGAS', 'TAPIA', NULL, NULL, 3),
    (NULL, NULL, 'CINTHIA', 'FARIAS', 'LAVALLE', NULL, NULL, 4),
    (NULL, NULL, 'JORGE ARMANDO', 'LACHAPELLE', 'CARNEIRO', NULL, NULL, 5),
    (NULL, NULL, 'FLORENTINO', 'CASTILLO', 'BERRU', NULL, NULL, 6),
    (NULL, NULL, 'RAUL ALBERTO', 'FLORES', 'ESCAJADILLO', NULL, NULL, 7),
    (NULL, NULL, 'ROBERTO MISAEL', 'TEJADA', 'VASQUEZ', NULL, NULL, 8),
    (NULL, NULL, 'EVA ENRIQUETA', 'MARAÑON', 'LOAYZA', NULL, NULL, 9),
    (NULL, NULL, 'FRANCISCO ALFONSO', 'BARRETO', 'CASTILLO', NULL, NULL, 10),
    (NULL, NULL, 'NATHALY GLORIA', 'SILVA', 'CUBA', NULL, NULL, 11),
    (NULL, NULL, 'COSME', 'SOTO', 'CORDOVA', NULL, NULL, 12),
    (NULL, NULL, 'FERNANDO AUGUSTO', 'BERRIOS', 'BUSTAMANTE', NULL, NULL, 13),
    (NULL, NULL, 'PERCY PEDRO', 'GONGORA', 'CERVANTES', NULL, NULL, 14),
    (NULL, NULL, 'PERCY', 'CELESTINO', 'BELLINA', NULL, NULL, 15),
    (NULL, NULL, 'MAXIMO', 'ANDAMAYO', 'ENCISO', NULL, NULL, 16),
    (NULL, NULL, 'ELICID AMINABAD', 'ANTAYHUA', 'POMA', NULL, NULL, 17),
    (NULL, NULL, 'CESAR AUGUSTO', 'VILCHERREZ', 'MENDOZA', NULL, NULL, 18),
    (NULL, NULL, 'PEDRO ELIAS', 'ROCA', 'HERMOSA', NULL, NULL, 19),
    (NULL, NULL, 'JOSE ANGEL', 'OLAZABAL', 'CASTILLO', NULL, NULL, 20),
    (NULL, NULL, 'GUILLERMO', 'VALLEJO', 'RIOS', NULL, NULL, 21),
    (NULL, NULL, 'CHAN PARK BYUNG/BECERRA FRESIA', 'CHAN PARK BYUNG', 'BECERRA FRESIA', NULL, NULL, 22),
    (NULL, NULL, 'BYUNG', 'CHAN', 'PARK', NULL, NULL, 23),
    (NULL, NULL, 'MAX VENA', 'FLORES', 'MARTINEZ', NULL, NULL, 24),
    (NULL, NULL, 'ALEX JOSE', 'CALDERON', 'JIMENEZ', NULL, NULL, 25),
    (NULL, NULL, 'ANA MARIA', 'CANO', 'MENDOZA', NULL, NULL, 26),
    (NULL, NULL, 'RONALD SMITH', 'YRUPAILLA', 'DIAZ', NULL, NULL, 27),
    (NULL, NULL, 'FIORELLA BELISA', 'ORE', 'SANCHEZ', NULL, NULL, 28),
    (NULL, NULL, 'JOSIMAR ENZO', 'ALIAGA', 'OSPINA', NULL, NULL, 29),
    (NULL, NULL, 'CARLA LEONOR', 'SALDIVAR', 'CHOY', NULL, NULL, 30),
    (NULL, NULL, 'LUIS', 'PALACIOS', 'RUBIO', NULL, NULL, 31),
    (NULL, NULL, 'MIGUEL ARTURO', 'FERNANDEZ', 'GARGUVERICH', NULL, NULL, 32);

   

    
INSERT INTO dbo.[User] (UserName, PasswordHash, Estado) VALUES ('webmaster', 'YQBzAGQAYQBzAGQA', 1);

INSERT INTO dbo.[User] (UserName, PasswordHash, Estado) VALUES ('B1101', 'YQBzAGQAYQBzAGQA', 1);
INSERT INTO dbo.[User] (UserName, PasswordHash, Estado) VALUES ('B1102', 'YQBzAGQAYQBzAGQA', 1);
INSERT INTO dbo.[User] (UserName, PasswordHash, Estado) VALUES ('B1103', 'YQBzAGQAYQBzAGQA', 1);
INSERT INTO dbo.[User] (UserName, PasswordHash, Estado) VALUES ('B1104', 'YQBzAGQAYQBzAGQA', 1);
INSERT INTO dbo.[User] (UserName, PasswordHash, Estado) VALUES ('B1201', 'YQBzAGQAYQBzAGQA', 1);
INSERT INTO dbo.[User] (UserName, PasswordHash, Estado) VALUES ('B1202', 'YQBzAGQAYQBzAGQA', 1);
INSERT INTO dbo.[User] (UserName, PasswordHash, Estado) VALUES ('B1203', 'YQBzAGQAYQBzAGQA', 1);
INSERT INTO dbo.[User] (UserName, PasswordHash, Estado) VALUES ('B1204', 'YQBzAGQAYQBzAGQA', 1);
INSERT INTO dbo.[User] (UserName, PasswordHash, Estado) VALUES ('B1301', 'YQBzAGQAYQBzAGQA', 1);
INSERT INTO dbo.[User] (UserName, PasswordHash, Estado) VALUES ('B1302', 'YQBzAGQAYQBzAGQA', 1);
INSERT INTO dbo.[User] (UserName, PasswordHash, Estado) VALUES ('B1303', 'YQBzAGQAYQBzAGQA', 1);
INSERT INTO dbo.[User] (UserName, PasswordHash, Estado) VALUES ('B1304', 'YQBzAGQAYQBzAGQA', 1);
INSERT INTO dbo.[User] (UserName, PasswordHash, Estado) VALUES ('B1401', 'YQBzAGQAYQBzAGQA', 1);
INSERT INTO dbo.[User] (UserName, PasswordHash, Estado) VALUES ('B1402', 'YQBzAGQAYQBzAGQA', 1);
INSERT INTO dbo.[User] (UserName, PasswordHash, Estado) VALUES ('B1403', 'YQBzAGQAYQBzAGQA', 1);
INSERT INTO dbo.[User] (UserName, PasswordHash, Estado) VALUES ('B1404', 'YQBzAGQAYQBzAGQA', 1);
INSERT INTO dbo.[User] (UserName, PasswordHash, Estado) VALUES ('B1501', 'YQBzAGQAYQBzAGQA', 1);
INSERT INTO dbo.[User] (UserName, PasswordHash, Estado) VALUES ('B1502', 'YQBzAGQAYQBzAGQA', 1);
INSERT INTO dbo.[User] (UserName, PasswordHash, Estado) VALUES ('B1503', 'YQBzAGQAYQBzAGQA', 1);
INSERT INTO dbo.[User] (UserName, PasswordHash, Estado) VALUES ('B1504', 'YQBzAGQAYQBzAGQA', 1);
INSERT INTO dbo.[User] (UserName, PasswordHash, Estado) VALUES ('B1601', 'YQBzAGQAYQBzAGQA', 1);
INSERT INTO dbo.[User] (UserName, PasswordHash, Estado) VALUES ('B1602', 'YQBzAGQAYQBzAGQA', 1);
INSERT INTO dbo.[User] (UserName, PasswordHash, Estado) VALUES ('B1603', 'YQBzAGQAYQBzAGQA', 1);
INSERT INTO dbo.[User] (UserName, PasswordHash, Estado) VALUES ('B1604', 'YQBzAGQAYQBzAGQA', 1);
INSERT INTO dbo.[User] (UserName, PasswordHash, Estado) VALUES ('B1701', 'YQBzAGQAYQBzAGQA', 1);
INSERT INTO dbo.[User] (UserName, PasswordHash, Estado) VALUES ('B1702', 'YQBzAGQAYQBzAGQA', 1);
INSERT INTO dbo.[User] (UserName, PasswordHash, Estado) VALUES ('B1703', 'YQBzAGQAYQBzAGQA', 1);
INSERT INTO dbo.[User] (UserName, PasswordHash, Estado) VALUES ('B1704', 'YQBzAGQAYQBzAGQA', 1);
INSERT INTO dbo.[User] (UserName, PasswordHash, Estado) VALUES ('B1801', 'YQBzAGQAYQBzAGQA', 1);
INSERT INTO dbo.[User] (UserName, PasswordHash, Estado) VALUES ('B1802', 'YQBzAGQAYQBzAGQA', 1);
INSERT INTO dbo.[User] (UserName, PasswordHash, Estado) VALUES ('B1803', 'YQBzAGQAYQBzAGQA', 1);
INSERT INTO dbo.[User] (UserName, PasswordHash, Estado) VALUES ('B1804', 'YQBzAGQAYQBzAGQA', 1);


UPDATE dbo.unidad
SET userid = (
    SELECT userid
    FROM dbo.[User]
    WHERE username = (block + dpto)
);

UPDATE dbo.Persona
SET UnidadId = (
    SELECT UnidadId
    FROM dbo.[unidad]
    WHERE userId -1 = PersonaId 
);


INSERT INTO dbo.role (Name,Estado) values ('WEBMASTER',1)
INSERT INTO dbo.role (Name,Estado) values ('PROPIETARIO',1)
INSERT INTO dbo.role (Name,Estado) values ('INQUILINO',1)
INSERT INTO dbo.role (Name,Estado) values ('ADMINISTRADOR',1)

insert into dbo.userrole (UserId,RoleId) 
select a.userId, 1 from dbo.[User] a where UserName in ('webmaster')

insert into dbo.userrole (UserId,RoleId) 
select a.userId, 2 from dbo.[User] a where UserName not in ('webmaster')



 
-- Insertar registros para todos los meses del año
INSERT INTO Recibo (Descripcion, FechaEmision, FechaVencimiento, Mes, Anio, Estado)
VALUES
    ('Mantenimiento del mes de enero', '2023-01-01', '2023-02-02', 1, 2023, 'Activo'),
    ('Mantenimiento del mes de febrero', '2023-02-01', '2023-03-02', 2, 2023, 'Activo'),
    ('Mantenimiento del mes de marzo', '2023-03-01', '2023-04-02', 3, 2023, 'Activo'),
    ('Mantenimiento del mes de abril', '2023-04-01', '2023-05-02', 4, 2023, 'Activo'),
    ('Mantenimiento del mes de mayo', '2023-05-01', '2023-06-02', 5, 2023, 'Activo'),
    ('Mantenimiento del mes de junio', '2023-06-01', '2023-07-01', 6, 2023, 'Activo'),
    ('Mantenimiento del mes de julio', '2023-07-01', '2023-08-02', 7, 2023, 'Activo'),
    ('Mantenimiento del mes de agosto', '2023-08-01', '2023-09-02', 8, 2023, 'Activo'),
    ('Mantenimiento del mes de septiembre', '2023-09-01', '2023-10-01', 9, 2023, 'Activo'),
    ('Mantenimiento del mes de octubre', '2023-10-01', '2023-11-01', 10, 2023, 'Activo'),
    ('Mantenimiento del mes de noviembre', '2023-11-01', '2023-12-01', 11, 2023, 'Activo'),
    ('Mantenimiento del mes de diciembre', '2023-12-01', '2024-01-02', 12, 2023, 'Activo');


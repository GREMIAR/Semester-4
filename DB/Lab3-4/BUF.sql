use journal;
SELECT * FROM professor;
SELECT * FROM dolg;
INSERT INTO dolg(name,zp)
	VALUES ("Доцент", 1000000.69);
UPDATE professor 
	SET dolg_id=(SELECT dolg_id FROM dolg WHERE name='Доцент');
UPDATE dolg 
	SET zp = 1000000.69;
DELETE FROM professor WHERE professor_id=9;

INSERT INTO professor(surname,name,patronymic, department_id, dolg_id)
VALUES('Фамилия5',"1","2", 3, 10);


INSERT INTO professor(surname,name,patronymic, department_id)
VALUES('Фамилия564',"1","2", 3);



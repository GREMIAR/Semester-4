ALTER TABLE professor ADD dolg_id INT NULL;
ALTER TABLE professor ADD FOREIGN KEY(dolg_id) REFERENCES dolg(dolg_id);
INSERT INTO dolg (name,zp)
VALUES ("Доцент", 1000000.69);
UPDATE professor 
	SET dolg_id=(SELECT dolg_id FROM dolg WHERE name='Доцент');
    
    

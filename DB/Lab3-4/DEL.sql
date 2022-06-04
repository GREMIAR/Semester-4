ALTER TABLE professor DROP FOREIGN KEY professor_ibfk_2;
ALTER TABLE professor DROP dolg_id;
DELETE FROM dolg; 
ALTER TABLE dolg AUTO_INCREMENT=1;
DELETE FROM professor WHERE professor_id>4; 
ALTER TABLE professor AUTO_INCREMENT=5;
SELECT SUM(attendance) Посетил,COUNT(attendance)-SUM(attendance) Прогулял, name FROM (SELECT * FROM class_student) as classStudent
	JOIN (SELECT * FROM (SELECT * FROM (SELECT * FROM student) as pupil) as uchenik WHERE name!='Имя1') s USING(student_id)
GROUP BY student_id

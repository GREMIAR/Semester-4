SELECT SUM(attendance) Посетил,COUNT(attendance)-SUM(attendance) Прогулял, name FROM class_student
	JOIN (SELECT * FROM student WHERE name!='Имя1') s USING(student_id)
GROUP BY student_id

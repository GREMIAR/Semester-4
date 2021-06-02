SELECT SUM(attendance) Посетил,COUNT(attendance)-SUM(attendance) Прогулял, name FROM (SELECT * FROM class_student LIMIT 1) as classStudent
	JOIN (SELECT * FROM (SELECT * FROM (SELECT * FROM student LIMIT 1) as scholar LIMIT 1) as pupil WHERE name='Имя1' LIMIT 1) as stud USING(student_id)
GROUP BY student_id
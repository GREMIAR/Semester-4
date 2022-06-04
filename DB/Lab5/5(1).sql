SELECT SUM(attendance) Посетил,COUNT(attendance)-SUM(attendance) Прогулял,  s.name, (SELECT name FROM journal.group WHERE group_id = g.group_id) Группа FROM class_student
	LEFT JOIN student s USING(student_id)
    JOIN journal.group g USING(group_id)
GROUP BY student_id
    
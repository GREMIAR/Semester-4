SELECT f.name Факультет, d.name Кафедра
FROM faculty f
	JOIN department d USING(faculty_id)
WHERE f.name = 'Иняз'
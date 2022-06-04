SELECT d.name Кафедра, s.name Специальность
FROM department d
	JOIN department_speciality ds USING(department_id)
    JOIN speciality s USING(speciality_id)
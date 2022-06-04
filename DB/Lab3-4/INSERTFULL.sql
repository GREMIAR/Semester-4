INSERT INTO faculty (name) VALUES
('ИПАИТ'),
('ФИЗМАТ'),
('ФФКС'),
('Иняз');

INSERT INTO department (faculty_id, name) VALUES
((SELECT faculty_id FROM faculty WHERE name='ИПАИТ'), 'Техническая физика и математика'),
((SELECT faculty_id FROM faculty WHERE name='ИПАИТ'), 'Информационные системы и технологии'),
((SELECT faculty_id FROM faculty WHERE name='ИПАИТ'), 'Программная инженерия'),
((SELECT faculty_id FROM faculty WHERE name='Иняз'), 'Ин. языкий коммуникации');

INSERT INTO speciality (name) VALUES
('ПГ'),
('ПИ'),
('ИВТ'),
('ИТ');

INSERT INTO department_speciality (department_id, speciality_id) VALUES
((SELECT department_id FROM department WHERE name='Программная инженерия'), (SELECT speciality_id FROM speciality WHERE name='ПГ')),
((SELECT department_id FROM department WHERE name='Информационные системы и технологии'), (SELECT speciality_id FROM speciality WHERE name='ПИ')),
((SELECT department_id FROM department WHERE name='Информационные системы и технологии'), (SELECT speciality_id FROM speciality WHERE name='ИВТ')),
((SELECT department_id FROM department WHERE name='Информационные системы и технологии'), (SELECT speciality_id FROM speciality WHERE name='ИТ'));

INSERT INTO professor (name, surname, patronymic, department_id) VALUES
('Имя1', 'Фамилия1', 'Отчество1', (SELECT department_id FROM department WHERE name='Программная инженерия')),
('Имя2', 'Фамилия2', 'Отчество2', (SELECT department_id FROM department WHERE name='Программная инженерия')),
('Имя3', 'Фамилия3', 'Отчество3', (SELECT department_id FROM department WHERE name='Программная инженерия')),
('Имя4', 'Фамилия4', 'Отчество4', (SELECT department_id FROM department WHERE name='Программная инженерия'));

INSERT INTO plan (plan_year_creation, name, department_id, speciality_id) VALUES
(2019, 'Учебный план ПГ', (SELECT department_id FROM department WHERE name='Программная инженерия'), (SELECT speciality_id FROM speciality WHERE name='ПГ')),
(1999, 'Учебный план ПИ', (SELECT department_id FROM department WHERE name='Информационные системы и технологии'), (SELECT speciality_id FROM speciality WHERE name='ПИ')),
(2009, 'Учебный план ИВТ', (SELECT department_id FROM department WHERE name='Информационные системы и технологии'), (SELECT speciality_id FROM speciality WHERE name='ИВТ')),
(2015, 'Учебный план ИТ', (SELECT department_id FROM department WHERE name='Информационные системы и технологии'), (SELECT speciality_id FROM speciality WHERE name='ИТ'));

INSERT INTO journal.group (name, plan_year_creation) VALUES
('92ПГ', 2019),
('91ПГ', 2019),
('91ИТ', 2015),
('91ИВТ', 2009);

INSERT INTO student (name, surname, patronymic, phone, address, record_book, birth_year, group_id) VALUES
('Имя1', 'Фамилия1', 'Отчество1', '+70000000001', 'q', 9987, 1999, (SELECT group_id FROM journal.group WHERE name='92ПГ')),
('Имя2', 'Фамилия2', 'Отчество2', '+70000000002', 'w', 7894, 1205, (SELECT group_id FROM journal.group WHERE name='92ПГ')),
('Имя3', 'Фамилия3', 'Отчество3', '+70000000003', 'e', 4564, 2002, (SELECT group_id FROM journal.group WHERE name='92ПГ')),
('Имя4', 'Фамилия4', 'Отчество4', '+70000000004', 'r', 1231, 2014, (SELECT group_id FROM journal.group WHERE name='92ПГ'));

INSERT INTO subject (name) VALUES
('Теория вероятностей и мат. статистика'),
('Проектная деятельность'),
('Базы данных'),
('Web-программирование');

INSERT INTO subject_type (type) VALUES
('Лекция'),
('Практика'),
('Лаба');

INSERT INTO subject_in_plan (number_of_hours, subject_type_id, subject_id, plan_year_creation) VALUES
(10, (SELECT subject_type_id FROM subject_type WHERE type='Лекция'), (SELECT subject_id FROM subject WHERE name='Базы данных'), 2019),
(8, (SELECT subject_type_id FROM subject_type WHERE type='Лекция'), (SELECT subject_id FROM subject WHERE name='Web-программирование'), 2019),
(90, (SELECT subject_type_id FROM subject_type WHERE type='Лаба'), (SELECT subject_id FROM subject WHERE name='Базы данных'), 2009),
(24, (SELECT subject_type_id FROM subject_type WHERE type='Практика'), (SELECT subject_id FROM subject WHERE name='Базы данных'), 2009);

INSERT INTO professor_subject_in_plan (professor_id, subject_in_plan_id) VALUES
((SELECT professor_id FROM professor WHERE name='Имя1' LIMIT 1), (SELECT subject_in_plan_id FROM subject_in_plan WHERE plan_year_creation='2019' LIMIT 1)),
((SELECT professor_id FROM professor WHERE name='Имя2' LIMIT 1), (SELECT subject_in_plan_id FROM subject_in_plan WHERE plan_year_creation='2019' LIMIT 1)),
((SELECT professor_id FROM professor WHERE name='Имя3' LIMIT 1), (SELECT subject_in_plan_id FROM subject_in_plan WHERE plan_year_creation='2009' LIMIT 1)),
((SELECT professor_id FROM professor WHERE name='Имя1' LIMIT 1), (SELECT subject_in_plan_id FROM subject_in_plan WHERE plan_year_creation='2009' LIMIT 1));

INSERT INTO class (date, professor_id, subject_in_plan_id, group_id) VALUES
(NOW(), (SELECT professor_id FROM professor WHERE name='Имя1' LIMIT 1), (SELECT subject_in_plan_id FROM subject_in_plan WHERE plan_year_creation='2019' LIMIT 1), (SELECT journal.group.group_id FROM journal.group WHERE name='92ПГ' LIMIT 1)),
(NOW()-1, (SELECT professor_id FROM professor WHERE name='Имя2' LIMIT 1), (SELECT subject_in_plan_id FROM subject_in_plan WHERE plan_year_creation='2019' LIMIT 1), (SELECT journal.group.group_id FROM journal.group WHERE name='91ПГ' LIMIT 1)),
(NOW()+5, (SELECT professor_id FROM professor WHERE name='Имя3' LIMIT 1), (SELECT subject_in_plan_id FROM subject_in_plan WHERE plan_year_creation='2009' LIMIT 1), (SELECT journal.group.group_id FROM journal.group WHERE name='91ИВТ' LIMIT 1)),
(NOW()+2, (SELECT professor_id FROM professor WHERE name='Имя1' LIMIT 1), (SELECT subject_in_plan_id FROM subject_in_plan WHERE plan_year_creation='2009' LIMIT 1), (SELECT journal.group.group_id FROM journal.group WHERE name='91ИТ' LIMIT 1));

INSERT INTO class_student (attendance, student_id, class_id) VALUES
(1, (SELECT student_id FROM student WHERE name='Имя1' LIMIT 1), 1),
(1, (SELECT student_id FROM student WHERE name='Имя2' LIMIT 1), 2),
(1, (SELECT student_id FROM student WHERE name='Имя3' LIMIT 1), 3),
(0, (SELECT student_id FROM student WHERE name='Имя1' LIMIT 1), 4);



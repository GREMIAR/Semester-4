-- 1) Год поступления должен быть больше года рождения на 17 лет.
-- 2) Год поступления должен быть больше или равен году создания плана.
-- 3) Год поступления должен быть меньше или равен текущему году.
DELIMITER $$ 
CREATE TRIGGER student_year
    BEFORE INSERT ON student
    FOR EACH ROW
	BEGIN
    IF((new.admission_year-new.birth_year<17) OR (new.admission_year<(SELECT plan_year_creation FROM plan JOIN journal.group USING (group_id) JOIN student USING (student_id) WHERE student.student_id = new.student_id)) OR (new.admission_year>YEAR(CURDATE()))) THEN
		signal sqlstate '45000' set message_text = 'Слишком молодой';
	END IF;
END$$

-- 4) Год создания плана должен быть меньше или равен текущему году.
DELIMITER $$
CREATE TRIGGER plan_year
	BEFORE INSERT ON plan
    FOR EACH ROW
    BEGIN
    IF(new.plan_year_creation>YEAR(CURDATE())) THEN
		signal sqlstate '45000' set message_text = 'Опередил своё время';
    END IF;
END$$

-- 5) Количество часов на каждую отведенную дисциплину кратно 2.
CREATE TRIGGER subject_in_plan_hours
BEFORE INSERT ON subject_in_plan
    FOR EACH ROW
		SET new.number_of_hours = new.number_of_hours - new.number_of_hours%2;

-- 6) Общее количество часов для лабораторных работ кратно 4.
DELIMITER $$
CREATE TRIGGER subject_in_plan_hours_lab
	BEFORE INSERT ON subject_in_plan
    FOR EACH ROW
    BEGIN
    IF (new.subject_type_id = (SELECT subject_type_id FROM subject_type WHERE type='Лаба')) THEN
		SET new.number_of_hours = new.number_of_hours - new.number_of_hours%4;
    END IF;
END$$

-- 7) Старостой должен быть студент своей группы.
DELIMITER $$
CREATE TRIGGER group_old_check_UP
	AFTER UPDATE ON journal.group
    FOR EACH ROW
    BEGIN
    IF (NOT EXISTS (SELECT * FROM student JOIN journal.group USING(group_id) WHERE student_id = new.old AND journal.group.name=new.name)) THEN
		signal sqlstate '45000' set message_text = 'В этой группе нет такого студента';
    END IF;
END$$
-- 8) Номер телефона начинается с 8 или с +7.
DELIMITER $$
CREATE TRIGGER phone_number_check
	BEFORE INSERT ON student
    FOR EACH ROW
    BEGIN
    IF (new.phone NOT LIKE '+7%' OR '8%') THEN
		signal sqlstate '45000' set message_text = 'Номер телефона должен начинаться с +7 или 8';
    END IF;
END$$
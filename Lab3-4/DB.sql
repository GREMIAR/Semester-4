CREATE SCHEMA IF NOT EXISTS `journal` DEFAULT CHARACTER SET utf8 ;
USE `journal` ;

CREATE TABLE IF NOT EXISTS `journal`.`faculty` (
  `faculty_id` TINYINT UNSIGNED NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`faculty_id`))
ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS `journal`.`department` (
  `department_id` TINYINT UNSIGNED NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(70) NOT NULL,
  `faculty_id` TINYINT UNSIGNED NOT NULL,
  PRIMARY KEY (`department_id`, `faculty_id`),
    FOREIGN KEY (`faculty_id`)
    REFERENCES `journal`.`faculty` (`faculty_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS `journal`.`speciality` (
  `speciality_id` TINYINT UNSIGNED NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(30) NOT NULL,
  PRIMARY KEY (`speciality_id`))
ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS `journal`.`subject` (
  `subject_id` SMALLINT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`subject_id`))
ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS `journal`.`department_speciality` (
  `department_id` TINYINT UNSIGNED NOT NULL,
  `speciality_id` TINYINT UNSIGNED NOT NULL,
  PRIMARY KEY (`department_id`, `speciality_id`),
    FOREIGN KEY (`department_id`)
    REFERENCES `journal`.`department` (`department_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
    FOREIGN KEY (`speciality_id`)
    REFERENCES `journal`.`speciality` (`speciality_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS `journal`.`plan` (
  `plan_year_creation` SMALLINT UNSIGNED NOT NULL,
  `name` VARCHAR(45) NOT NULL,
  `department_id` TINYINT UNSIGNED NOT NULL,
  `speciality_id` TINYINT UNSIGNED NOT NULL,
  PRIMARY KEY (`plan_year_creation`, `department_id`, `speciality_id`),
    FOREIGN KEY (`department_id` , `speciality_id`)
    REFERENCES `journal`.`department_speciality` (`department_id` , `speciality_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS `journal`.`student` (
  `student_id` MEDIUMINT UNSIGNED NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(30) NOT NULL,
  `surname` VARCHAR(45) NOT NULL,
  `patronymic` VARCHAR(45) NOT NULL,
  `phone` VARCHAR(12) NOT NULL,
  `address` VARCHAR(45) NOT NULL,
  `record_book` MEDIUMINT UNSIGNED NOT NULL,
  `birth_year` SMALLINT NOT NULL,
  `group_id` SMALLINT UNSIGNED NOT NULL,
  PRIMARY KEY (`student_id`))
ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS `journal`.`group` (
  `group_id` SMALLINT UNSIGNED NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(45) NOT NULL,
  `plan_year_creation` SMALLINT UNSIGNED NOT NULL,
  `old` MEDIUMINT UNSIGNED NULL,
  PRIMARY KEY (`group_id`, `plan_year_creation`),
    FOREIGN KEY (`plan_year_creation`)
    REFERENCES `journal`.`plan` (`plan_year_creation`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
    FOREIGN KEY (`old`)
    REFERENCES `journal`.`student` (`student_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

ALTER TABLE student ADD FOREIGN KEY (`group_id`) REFERENCES `journal`.`group` (`group_id`);

CREATE TABLE IF NOT EXISTS `journal`.`professor` (
  `professor_id` SMALLINT NOT NULL AUTO_INCREMENT,
  `surname` VARCHAR(45) NOT NULL,
  `name` VARCHAR(45) NOT NULL,
  `patronymic` VARCHAR(45) NOT NULL,
  `department_id` TINYINT UNSIGNED NOT NULL,
  PRIMARY KEY (`professor_id`, `department_id`),
    FOREIGN KEY (`department_id`)
    REFERENCES `journal`.`department` (`department_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS `journal`.`subject_type` (
  `subject_type_id` TINYINT NOT NULL AUTO_INCREMENT,
  `type` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`subject_type_id`))
ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS `journal`.`subject_in_plan` (
  `subject_in_plan_id` SMALLINT NOT NULL AUTO_INCREMENT,
  `number_of_hours` TINYINT NOT NULL,
  `subject_type_id` TINYINT NOT NULL,
  `subject_id` SMALLINT NOT NULL,
  `plan_year_creation` SMALLINT UNSIGNED NOT NULL,
  PRIMARY KEY (`subject_in_plan_id`, `subject_type_id`, `subject_id`, `plan_year_creation`),
    FOREIGN KEY (`subject_type_id`)
    REFERENCES `journal`.`subject_type` (`subject_type_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
    FOREIGN KEY (`subject_id`)
    REFERENCES `journal`.`subject` (`subject_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
    FOREIGN KEY (`plan_year_creation`)
    REFERENCES `journal`.`plan` (`plan_year_creation`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS `journal`.`professor_subject_in_plan` (
  `professor_id` SMALLINT NOT NULL,
  `subject_in_plan_id` SMALLINT NOT NULL,
  PRIMARY KEY (`professor_id`, `subject_in_plan_id`),
    FOREIGN KEY (`professor_id`)
    REFERENCES `journal`.`professor` (`professor_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
    FOREIGN KEY (`subject_in_plan_id`)
    REFERENCES `journal`.`subject_in_plan` (`subject_in_plan_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS `journal`.`class` (
  `class_id` MEDIUMINT NOT NULL AUTO_INCREMENT,
  `date` DATE NULL,
  `professor_id` SMALLINT NOT NULL,
  `subject_in_plan_id` SMALLINT NOT NULL,
  `group_id` SMALLINT UNSIGNED NOT NULL,
  PRIMARY KEY (`class_id`, `professor_id`, `subject_in_plan_id`, `group_id`),
    FOREIGN KEY (`professor_id` , `subject_in_plan_id`)
    REFERENCES `journal`.`professor_subject_in_plan` (`professor_id` , `subject_in_plan_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
    FOREIGN KEY (`group_id`)
    REFERENCES `journal`.`group` (`group_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS `journal`.`class_student` (
  `attendance` TINYINT(0) NOT NULL,
  `student_id` MEDIUMINT UNSIGNED NOT NULL,
  `class_id` MEDIUMINT NOT NULL,
  PRIMARY KEY (`student_id`, `class_id`),
    FOREIGN KEY (`student_id`)
    REFERENCES `journal`.`student` (`student_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
    FOREIGN KEY (`class_id`)
    REFERENCES `journal`.`class` (`class_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

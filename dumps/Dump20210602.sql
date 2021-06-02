-- MySQL dump 10.13  Distrib 8.0.24, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: journal
-- ------------------------------------------------------
-- Server version	8.0.24

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `class`
--

DROP TABLE IF EXISTS `class`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `class` (
  `class_id` mediumint NOT NULL AUTO_INCREMENT,
  `date` date DEFAULT NULL,
  `professor_id` smallint NOT NULL,
  `subject_in_plan_id` smallint NOT NULL,
  `group_id` smallint unsigned NOT NULL,
  PRIMARY KEY (`class_id`,`professor_id`,`subject_in_plan_id`,`group_id`),
  KEY `professor_id` (`professor_id`,`subject_in_plan_id`),
  KEY `group_id` (`group_id`),
  CONSTRAINT `class_ibfk_1` FOREIGN KEY (`professor_id`, `subject_in_plan_id`) REFERENCES `professor_subject_in_plan` (`professor_id`, `subject_in_plan_id`),
  CONSTRAINT `class_ibfk_2` FOREIGN KEY (`group_id`) REFERENCES `group` (`group_id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `class`
--

LOCK TABLES `class` WRITE;
/*!40000 ALTER TABLE `class` DISABLE KEYS */;
INSERT INTO `class` VALUES (1,'2021-05-02',1,1,1),(2,'2021-05-01',2,1,2),(3,'2021-05-03',3,3,4),(4,'2021-06-01',1,3,3);
/*!40000 ALTER TABLE `class` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `class_student`
--

DROP TABLE IF EXISTS `class_student`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `class_student` (
  `attendance` tinyint NOT NULL,
  `student_id` mediumint unsigned NOT NULL,
  `class_id` mediumint NOT NULL,
  PRIMARY KEY (`student_id`,`class_id`),
  KEY `class_id` (`class_id`),
  CONSTRAINT `class_student_ibfk_1` FOREIGN KEY (`student_id`) REFERENCES `student` (`student_id`),
  CONSTRAINT `class_student_ibfk_2` FOREIGN KEY (`class_id`) REFERENCES `class` (`class_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `class_student`
--

LOCK TABLES `class_student` WRITE;
/*!40000 ALTER TABLE `class_student` DISABLE KEYS */;
INSERT INTO `class_student` VALUES (1,1,1),(0,1,4),(0,2,1),(1,2,2),(0,2,3),(1,3,1),(1,3,3),(0,4,1);
/*!40000 ALTER TABLE `class_student` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `department`
--

DROP TABLE IF EXISTS `department`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `department` (
  `department_id` tinyint unsigned NOT NULL AUTO_INCREMENT,
  `name` varchar(70) NOT NULL,
  `faculty_id` tinyint unsigned NOT NULL,
  PRIMARY KEY (`department_id`,`faculty_id`),
  KEY `faculty_id` (`faculty_id`),
  CONSTRAINT `department_ibfk_1` FOREIGN KEY (`faculty_id`) REFERENCES `faculty` (`faculty_id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `department`
--

LOCK TABLES `department` WRITE;
/*!40000 ALTER TABLE `department` DISABLE KEYS */;
INSERT INTO `department` VALUES (1,'Техническая физика и математика',1),(2,'Информационные системы и технологии',1),(3,'Программная инженерия',1),(4,'Ин. языкий коммуникации',4);
/*!40000 ALTER TABLE `department` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `department_speciality`
--

DROP TABLE IF EXISTS `department_speciality`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `department_speciality` (
  `department_id` tinyint unsigned NOT NULL,
  `speciality_id` tinyint unsigned NOT NULL,
  PRIMARY KEY (`department_id`,`speciality_id`),
  KEY `speciality_id` (`speciality_id`),
  CONSTRAINT `department_speciality_ibfk_1` FOREIGN KEY (`department_id`) REFERENCES `department` (`department_id`),
  CONSTRAINT `department_speciality_ibfk_2` FOREIGN KEY (`speciality_id`) REFERENCES `speciality` (`speciality_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `department_speciality`
--

LOCK TABLES `department_speciality` WRITE;
/*!40000 ALTER TABLE `department_speciality` DISABLE KEYS */;
INSERT INTO `department_speciality` VALUES (3,1),(2,2),(2,3),(2,4);
/*!40000 ALTER TABLE `department_speciality` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `dolg`
--

DROP TABLE IF EXISTS `dolg`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `dolg` (
  `dolg_id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(45) NOT NULL,
  `zp` decimal(10,2) NOT NULL,
  PRIMARY KEY (`dolg_id`),
  UNIQUE KEY `name` (`name`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `dolg`
--

LOCK TABLES `dolg` WRITE;
/*!40000 ALTER TABLE `dolg` DISABLE KEYS */;
INSERT INTO `dolg` VALUES (1,'Доцент',1000000.69);
/*!40000 ALTER TABLE `dolg` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `faculty`
--

DROP TABLE IF EXISTS `faculty`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `faculty` (
  `faculty_id` tinyint unsigned NOT NULL AUTO_INCREMENT,
  `name` varchar(45) NOT NULL,
  PRIMARY KEY (`faculty_id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `faculty`
--

LOCK TABLES `faculty` WRITE;
/*!40000 ALTER TABLE `faculty` DISABLE KEYS */;
INSERT INTO `faculty` VALUES (1,'ИПАИТ'),(2,'ФИЗМАТ'),(3,'ФФКС'),(4,'Иняз');
/*!40000 ALTER TABLE `faculty` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `group`
--

DROP TABLE IF EXISTS `group`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `group` (
  `group_id` smallint unsigned NOT NULL AUTO_INCREMENT,
  `name` varchar(45) NOT NULL,
  `plan_year_creation` smallint unsigned NOT NULL,
  `old` mediumint unsigned DEFAULT NULL,
  PRIMARY KEY (`group_id`,`plan_year_creation`),
  KEY `plan_year_creation` (`plan_year_creation`),
  KEY `old` (`old`),
  CONSTRAINT `group_ibfk_1` FOREIGN KEY (`plan_year_creation`) REFERENCES `plan` (`plan_year_creation`),
  CONSTRAINT `group_ibfk_2` FOREIGN KEY (`old`) REFERENCES `student` (`student_id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `group`
--

LOCK TABLES `group` WRITE;
/*!40000 ALTER TABLE `group` DISABLE KEYS */;
INSERT INTO `group` VALUES (1,'92ПГ',2019,NULL),(2,'91ПГ',2019,NULL),(3,'91ИТ',2015,NULL),(4,'91ИВТ',2009,NULL);
/*!40000 ALTER TABLE `group` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `plan`
--

DROP TABLE IF EXISTS `plan`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `plan` (
  `plan_year_creation` smallint unsigned NOT NULL,
  `name` varchar(45) NOT NULL,
  `department_id` tinyint unsigned NOT NULL,
  `speciality_id` tinyint unsigned NOT NULL,
  PRIMARY KEY (`plan_year_creation`,`department_id`,`speciality_id`),
  KEY `department_id` (`department_id`,`speciality_id`),
  CONSTRAINT `plan_ibfk_1` FOREIGN KEY (`department_id`, `speciality_id`) REFERENCES `department_speciality` (`department_id`, `speciality_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `plan`
--

LOCK TABLES `plan` WRITE;
/*!40000 ALTER TABLE `plan` DISABLE KEYS */;
INSERT INTO `plan` VALUES (1999,'Учебный план ПИ',2,2),(2009,'Учебный план ИВТ',2,3),(2015,'Учебный план ИТ',2,4),(2019,'Учебный план ПГ',3,1);
/*!40000 ALTER TABLE `plan` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `professor`
--

DROP TABLE IF EXISTS `professor`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `professor` (
  `professor_id` smallint NOT NULL AUTO_INCREMENT,
  `surname` varchar(45) NOT NULL,
  `name` varchar(45) NOT NULL,
  `patronymic` varchar(45) NOT NULL,
  `department_id` tinyint unsigned NOT NULL,
  `dolg_id` int DEFAULT NULL,
  PRIMARY KEY (`professor_id`,`department_id`),
  KEY `department_id` (`department_id`),
  KEY `dolg_id` (`dolg_id`),
  CONSTRAINT `professor_ibfk_1` FOREIGN KEY (`department_id`) REFERENCES `department` (`department_id`),
  CONSTRAINT `professor_ibfk_2` FOREIGN KEY (`dolg_id`) REFERENCES `dolg` (`dolg_id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `professor`
--

LOCK TABLES `professor` WRITE;
/*!40000 ALTER TABLE `professor` DISABLE KEYS */;
INSERT INTO `professor` VALUES (1,'Рыженков','Денис','Викторович',3,1),(2,'Ушкоа','Илья','Отчество2',3,1),(3,'Илюшков','Сегрей','Отчество3',3,1),(4,'Фамилия4','Имя4','Отчество4',3,1);
/*!40000 ALTER TABLE `professor` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `professor_subject_in_plan`
--

DROP TABLE IF EXISTS `professor_subject_in_plan`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `professor_subject_in_plan` (
  `professor_id` smallint NOT NULL,
  `subject_in_plan_id` smallint NOT NULL,
  PRIMARY KEY (`professor_id`,`subject_in_plan_id`),
  KEY `subject_in_plan_id` (`subject_in_plan_id`),
  CONSTRAINT `professor_subject_in_plan_ibfk_1` FOREIGN KEY (`professor_id`) REFERENCES `professor` (`professor_id`),
  CONSTRAINT `professor_subject_in_plan_ibfk_2` FOREIGN KEY (`subject_in_plan_id`) REFERENCES `subject_in_plan` (`subject_in_plan_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `professor_subject_in_plan`
--

LOCK TABLES `professor_subject_in_plan` WRITE;
/*!40000 ALTER TABLE `professor_subject_in_plan` DISABLE KEYS */;
INSERT INTO `professor_subject_in_plan` VALUES (1,1),(2,1),(1,3),(3,3);
/*!40000 ALTER TABLE `professor_subject_in_plan` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `speciality`
--

DROP TABLE IF EXISTS `speciality`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `speciality` (
  `speciality_id` tinyint unsigned NOT NULL AUTO_INCREMENT,
  `name` varchar(30) NOT NULL,
  PRIMARY KEY (`speciality_id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `speciality`
--

LOCK TABLES `speciality` WRITE;
/*!40000 ALTER TABLE `speciality` DISABLE KEYS */;
INSERT INTO `speciality` VALUES (1,'ПГ'),(2,'ПИ'),(3,'ИВТ'),(4,'ИТ');
/*!40000 ALTER TABLE `speciality` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `student`
--

DROP TABLE IF EXISTS `student`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `student` (
  `student_id` mediumint unsigned NOT NULL AUTO_INCREMENT,
  `name` varchar(30) NOT NULL,
  `surname` varchar(45) NOT NULL,
  `patronymic` varchar(45) NOT NULL,
  `phone` varchar(12) NOT NULL,
  `address` varchar(45) NOT NULL,
  `record_book` mediumint unsigned NOT NULL,
  `birth_year` smallint NOT NULL,
  `group_id` smallint unsigned NOT NULL,
  `admission_year` smallint NOT NULL,
  PRIMARY KEY (`student_id`),
  KEY `group_id` (`group_id`),
  CONSTRAINT `student_ibfk_1` FOREIGN KEY (`group_id`) REFERENCES `group` (`group_id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `student`
--

LOCK TABLES `student` WRITE;
/*!40000 ALTER TABLE `student` DISABLE KEYS */;
INSERT INTO `student` VALUES (1,'Имя1','Фамилия1','Отчество1','+70000000001','q',9987,1999,1,2020),(2,'Имя2','Фамилия2','Отчество2','+70000000002','w',7894,1205,1,2019),(3,'Имя3','Фамилия3','Отчество3','+70000000003','e',4564,2001,1,2020),(4,'Имя4','Фамилия4','Отчество4','+70000000004','r',1231,2001,1,2020),(5,'Илья','Гришин','Романович','+89208281152','q',9987,1999,1,2021);
/*!40000 ALTER TABLE `student` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `subject`
--

DROP TABLE IF EXISTS `subject`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `subject` (
  `subject_id` smallint NOT NULL AUTO_INCREMENT,
  `name` varchar(45) NOT NULL,
  PRIMARY KEY (`subject_id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `subject`
--

LOCK TABLES `subject` WRITE;
/*!40000 ALTER TABLE `subject` DISABLE KEYS */;
INSERT INTO `subject` VALUES (1,'Теория вероятностей и мат. статистика'),(2,'Проектная деятельность'),(3,'Базы данных'),(4,'Web-программирование');
/*!40000 ALTER TABLE `subject` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `subject_in_plan`
--

DROP TABLE IF EXISTS `subject_in_plan`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `subject_in_plan` (
  `subject_in_plan_id` smallint NOT NULL AUTO_INCREMENT,
  `number_of_hours` tinyint NOT NULL,
  `subject_type_id` tinyint NOT NULL,
  `subject_id` smallint NOT NULL,
  `plan_year_creation` smallint unsigned NOT NULL,
  PRIMARY KEY (`subject_in_plan_id`,`subject_type_id`,`subject_id`,`plan_year_creation`),
  KEY `subject_type_id` (`subject_type_id`),
  KEY `subject_id` (`subject_id`),
  KEY `plan_year_creation` (`plan_year_creation`),
  CONSTRAINT `subject_in_plan_ibfk_1` FOREIGN KEY (`subject_type_id`) REFERENCES `subject_type` (`subject_type_id`),
  CONSTRAINT `subject_in_plan_ibfk_2` FOREIGN KEY (`subject_id`) REFERENCES `subject` (`subject_id`),
  CONSTRAINT `subject_in_plan_ibfk_3` FOREIGN KEY (`plan_year_creation`) REFERENCES `plan` (`plan_year_creation`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `subject_in_plan`
--

LOCK TABLES `subject_in_plan` WRITE;
/*!40000 ALTER TABLE `subject_in_plan` DISABLE KEYS */;
INSERT INTO `subject_in_plan` VALUES (1,10,1,3,2019),(2,8,1,4,2019),(3,90,3,3,2009),(4,24,2,3,2009);
/*!40000 ALTER TABLE `subject_in_plan` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `subject_type`
--

DROP TABLE IF EXISTS `subject_type`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `subject_type` (
  `subject_type_id` tinyint NOT NULL AUTO_INCREMENT,
  `type` varchar(45) NOT NULL,
  PRIMARY KEY (`subject_type_id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `subject_type`
--

LOCK TABLES `subject_type` WRITE;
/*!40000 ALTER TABLE `subject_type` DISABLE KEYS */;
INSERT INTO `subject_type` VALUES (1,'Лекция'),(2,'Практика'),(3,'Лаба');
/*!40000 ALTER TABLE `subject_type` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2021-06-02 17:15:14

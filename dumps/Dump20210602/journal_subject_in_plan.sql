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
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2021-06-02 17:00:32

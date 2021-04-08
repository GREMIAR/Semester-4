CREATE TABLE `Journal` (
  `id` int,
  `idGroup` int,
  `created_at` timestamp,
  `country_code` int,
  `faculty` varchar(255)
);

CREATE TABLE `Group` (
  `id` int,
  `name` varchar(255),
  `specialty` varchar(255),
  `country_code` int,
  `faculty` varchar(255)
);

CREATE TABLE `Student` (
  `idJournal` int,
  `idRB` int,
  `idGroup` int,
  `name` varchar(255),
  `faculty` varchar(255),
  `telephone` int,
  `birthdate` DATE,
  `yearAdmission` YEAR,
  `address` text
);

CREATE TABLE `Class` (
  `datetime` datetime,
  `professorAndSubject` int
);

CREATE TABLE `Subject` (
  `id` int,
  `name` varchar(255)
);

CREATE TABLE `Professor` (
  `id` int,
  `name` varchar(255)
);

CREATE TABLE `Faculty` (
  `name` varchar(255)
);

CREATE TABLE `ProfessorSubject` (
  `id` int,
  `idProfessor` int,
  `idSubject` int
);

ALTER TABLE `ProfessorSubject` ADD FOREIGN KEY (`idProfessor`) REFERENCES `Professor` (`id`);

ALTER TABLE `ProfessorSubject` ADD FOREIGN KEY (`idSubject`) REFERENCES `Subject` (`id`);

ALTER TABLE `Class` ADD FOREIGN KEY (`professorAndSubject`) REFERENCES `ProfessorSubject` (`id`);

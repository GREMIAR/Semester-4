-- DROP SCHEMA busstop;
-- use busstop;
CREATE SCHEMA IF NOT EXISTS `busstop` DEFAULT CHARACTER SET utf8 ;
USE `busstop`;


CREATE TABLE way(
	num_way SMALLINT NOT NULL PRIMARY KEY,
    title_way VARCHAR(45) NOT NULL
);

CREATE TABLE station(
	num_st SMALLINT NOT NULL PRIMARY KEY,
    title_st VARCHAR(45) NOT NULL,
    distance DECIMAL(10,2) NOT NULL
);

CREATE TABLE station_way(
	num_way SMALLINT NOT NULL,
    num_st SMALLINT NOT NULL,
     PRIMARY KEY (num_way, num_st),
    FOREIGN KEY (num_way) REFERENCES way(num_way),
    FOREIGN KEY (num_st) REFERENCES station(num_st)
);

CREATE TABLE bus(
	num_bus SMALLINT NOT NULL PRIMARY KEY,
    model VARCHAR(45) NOT NULL,
    count_places TINYINT NOT NULL
);

CREATE TABLE rays(
	num_way SMALLINT NOT NULL,
    time_hour TINYINT NOT NULL,
    time_min TINYINT NOT NULL,
    num_bus SMALLINT NULL,
	PRIMARY KEY (num_way, time_hour,time_min),
    FOREIGN KEY (num_way) REFERENCES way(num_way),
    FOREIGN KEY (num_bus) REFERENCES bus(num_bus)
);

CREATE TABLE ticket(
	place TINYINT NOT NULL,
    num_way SMALLINT NOT NULL,
    time_hour TINYINT NOT NULL,
    time_min TINYINT NOT NULL,
    num_st SMALLINT NULL,
    PRIMARY KEY (place, num_way,time_hour,time_min),
    FOREIGN KEY (num_way,time_hour,time_min) REFERENCES rays(num_way,time_hour,time_min),
    FOREIGN KEY (num_st) REFERENCES station(num_st)
);




SELECT * FROM ticket;
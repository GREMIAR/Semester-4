CREATE SCHEMA IF NOT EXISTS computer_accessories;
USE computer_accessories;

CREATE TABLE IF NOT EXISTS country (
	country_id TINYINT UNSIGNED NOT NULL AUTO_INCREMENT,
	name VARCHAR(40) NOT NULL UNIQUE,
	phone_code VARCHAR(10) NOT NULL,
	PRIMARY KEY (country_id));

CREATE TABLE IF NOT EXISTS city (
	city_id TINYINT UNSIGNED NOT NULL AUTO_INCREMENT,
	name VARCHAR(40) NOT NULL,
	country_id TINYINT UNSIGNED NOT NULL,
	PRIMARY KEY (city_id),
	FOREIGN KEY (country_id) REFERENCES country (country_id));

CREATE TABLE IF NOT EXISTS branch (
	branch_id SMALLINT UNSIGNED NOT NULL AUTO_INCREMENT,
	street VARCHAR(45) NOT NULL,
	house VARCHAR(8) NOT NULL,
	phone VARCHAR(20) NULL,
	city_id TINYINT UNSIGNED NOT NULL,
	company_id TINYINT UNSIGNED NOT NULL,
	PRIMARY KEY (branch_id),
    FOREIGN KEY (city_id) REFERENCES city (city_id));

CREATE TABLE IF NOT EXISTS manufacturer (
	manufacturer_id TINYINT UNSIGNED NOT NULL AUTO_INCREMENT,
	name VARCHAR(45) NOT NULL UNIQUE,
	phone VARCHAR(20) NULL,
	country_id TINYINT UNSIGNED NOT NULL,
	PRIMARY KEY (manufacturer_id),
    FOREIGN KEY (country_id) REFERENCES country (country_id));

CREATE TABLE IF NOT EXISTS type (
  type_id TINYINT UNSIGNED NOT NULL AUTO_INCREMENT,
  name VARCHAR(45) NOT NULL UNIQUE,
  PRIMARY KEY (type_id));

CREATE TABLE IF NOT EXISTS product (
	product_id MEDIUMINT UNSIGNED NOT NULL AUTO_INCREMENT,
	description VARCHAR(400) NULL,
	manufacturer_id TINYINT UNSIGNED NOT NULL,
	type_id TINYINT UNSIGNED NOT NULL,
	price DECIMAL(10,2) NOT NULL,
	price_purchases DECIMAL(10,2) NOT NULL,
    discount DECIMAL(2,2) NULL,
	name VARCHAR(60) NOT NULL,
	PRIMARY KEY (product_id),
	FOREIGN KEY (manufacturer_id) REFERENCES manufacturer (manufacturer_id),
    FOREIGN KEY (type_id) REFERENCES type (type_id));

CREATE TABLE IF NOT EXISTS characteristics(
  characteristics_id SMALLINT UNSIGNED NOT NULL AUTO_INCREMENT,
  name VARCHAR(70) NOT NULL UNIQUE,
  measure_units VARCHAR(70) NULL,
  PRIMARY KEY (characteristics_id));

CREATE TABLE IF NOT EXISTS type_characteristics (
	type_id TINYINT UNSIGNED NOT NULL,
	characteristics_id SMALLINT UNSIGNED NOT NULL,
	PRIMARY KEY (type_id, characteristics_id),
    FOREIGN KEY (type_id) REFERENCES type (type_id),
    FOREIGN KEY (characteristics_id) REFERENCES characteristics (characteristics_id));

CREATE TABLE IF NOT EXISTS product_characteristics (
	product_id MEDIUMINT UNSIGNED NOT NULL,
	characteristics_id SMALLINT  UNSIGNED NOT NULL,
	value VARCHAR(60) NOT NULL,
	PRIMARY KEY (product_id, characteristics_id),
    FOREIGN KEY (product_id) REFERENCES product (product_id),
    FOREIGN KEY (characteristics_id)REFERENCES characteristics (characteristics_id));

CREATE TABLE IF NOT EXISTS branch_product (
	branch_id SMALLINT UNSIGNED NOT NULL,
	product_id MEDIUMINT UNSIGNED NOT NULL,
	quantity SMALLINT NOT NULL,
	PRIMARY KEY (branch_id, product_id),
    FOREIGN KEY (branch_id) REFERENCES branch (branch_id),
    FOREIGN KEY (product_id) REFERENCES product (product_id));

CREATE TABLE IF NOT EXISTS seller (
	seller_id SMALLINT UNSIGNED NOT NULL AUTO_INCREMENT,
	lastname VARCHAR(45) NOT NULL,
	firstname VARCHAR(45) NOT NULL,
	patronymic VARCHAR(45) NOT NULL,
	phone VARCHAR(20) NULL,
	branch_id SMALLINT UNSIGNED NOT NULL,
	PRIMARY KEY (seller_id),
    FOREIGN KEY (branch_id) REFERENCES branch (branch_id));

CREATE TABLE IF NOT EXISTS sale (
	sale_id INT UNSIGNED NOT NULL AUTO_INCREMENT,
	total_cost DECIMAL(11,2) NOT NULL,
	seller_id SMALLINT UNSIGNED NOT NULL,
	date DATE NOT NULL,
	PRIMARY KEY (sale_id),
    FOREIGN KEY (seller_id) REFERENCES seller (seller_id));

CREATE TABLE IF NOT EXISTS sale_product (
	branch_id SMALLINT UNSIGNED NOT NULL,
	product_id MEDIUMINT UNSIGNED NOT NULL,
	sale_id INT UNSIGNED NOT NULL,
	quantity VARCHAR(45) NOT NULL,
	PRIMARY KEY (branch_id, product_id, sale_id),
    FOREIGN KEY (branch_id, product_id) REFERENCES branch_product (branch_id, product_id),
    FOREIGN KEY (sale_id) REFERENCES sale (sale_id));




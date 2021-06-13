DELIMITER $$

-- Товары определённого типа и производителя в неком городе
CREATE PROCEDURE search_product(city VARCHAR(45), manufacturer VARCHAR(45), type  VARCHAR(45))
BEGIN
SELECT c.name Город, CONCAT(b.street,', ', b.house) Адрес, t.name Тип, m.name Производитель, p.name Модель, bp.quantity Количество, IF(p.discount is NULL,p.price,p.price*(1-p.discount)) Цена
FROM city c
JOIN branch b USING(city_id)
JOIN branch_product bp USING(branch_id)
JOIN product p USING(product_id)
JOIN manufacturer m USING(manufacturer_id)
JOIN type t USING(type_id)
WHERE c.name = city AND m.name = manufacturer AND t.name=type;
END$$

-- Характеристики определённой модели товара
CREATE PROCEDURE search_model_characteristics(model VARCHAR(45))
BEGIN
SELECT c.name Характкристика, pc.value Значение, c.measure_units Единица_измерения
FROM product p
JOIN product_characteristics pc USING(product_id)
JOIN characteristics c USING(characteristics_id)
WHERE p.name=model;
END$$

-- все товары в неком городе
CREATE PROCEDURE search_models_in_city(city VARCHAR(45))
BEGIN
SELECT p.name Модель
FROM branch b
JOIN branch_product bp USING(branch_id)
JOIN product p USING(product_id)
JOIN city c USING(city_id)
WHERE c.name=city
GROUP BY p.name;
END$$

-- поменят скидку на товар
CREATE PROCEDURE discount_model(discounts DECIMAL(2,2),model VARCHAR(45))
BEGIN
UPDATE product
SET discount=discounts
WHERE name=model;
END$$

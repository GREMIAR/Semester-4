DELIMITER $$

-- (1)
-- Убирать из товары в филиале, купленные(-)
CREATE TRIGGER sale_fixed_capital
	AFTER INSERT ON sale_product
    FOR EACH ROW
    BEGIN
    UPDATE branch_product
    SET quantity = quantity - new.quantity
    WHERE product_id = new.product_id AND branch_id = (
    SELECT branch_id 
    FROM sale
    JOIN seller USING(seller_id)
    JOIN branch USING(branch_id)
    WHERE sale_id=new.sale_id
    );
END$$

-- (2)
-- Запрет на выстовление цены ниже закупочной INSERT
CREATE TRIGGER the_purchase_price_lower
	BEFORE INSERT ON product
    FOR EACH ROW
    BEGIN
    IF (new.price < new.price_purchases) THEN
		signal sqlstate '45000' set message_text = 'Закупочная цена выше!!';
	END IF;
END$$

-- Запрет на выстовление цены ниже закупочной UPDATE
CREATE TRIGGER the_purchase_price_lower_UP
	BEFORE UPDATE ON product
    FOR EACH ROW
    BEGIN
    IF (new.price < new.price_purchases ) THEN
		signal sqlstate '45000' set message_text = 'Закупочная цена выше!!';
	END IF;
END$$

-- (3)
-- Номер телефона производителя должен начинаться с кода страны производителя INSERT
CREATE TRIGGER phone_number_check_manufacturer
	BEFORE INSERT ON manufacturer
    FOR EACH ROW
    BEGIN
    IF (new.phone NOT LIKE (CONCAT((SELECT phone_code FROM country WHERE new.country_id = country_id),'%'))) THEN
		signal sqlstate '45000' set message_text = "Номер телефона должен начинаться с другой цифры";
    END IF;
END$$

-- Номер телефона производителя должен начинаться с кода страны производителя UPDATE
CREATE TRIGGER phone_number_check_manufacturer_UP
	BEFORE UPDATE ON manufacturer
    FOR EACH ROW
    BEGIN
    IF (new.phone NOT LIKE (CONCAT((SELECT phone_code FROM country WHERE new.country_id = country_id),'%'))) THEN
		signal sqlstate '45000' set message_text = "Номер телефона должен начинаться с другой цифры";
    END IF;
END$$

-- (4)
-- Номер телефона филиала должен начинаться с кода страны в котором находится филиал INSERT
CREATE TRIGGER phone_number_check_branch
	BEFORE INSERT ON branch
    FOR EACH ROW
    BEGIN
    IF (new.phone NOT LIKE (CONCAT((SELECT phone_code FROM city ci JOIN country co USING(country_id) WHERE new.city_id = ci.city_id),'%'))) THEN
		signal sqlstate '45000' set message_text = "Номер телефона должен начинаться с другой цифры";
    END IF;
END$$

-- Номер телефона филиала должен начинаться с кода страны в котором находится филиал UPDATE
CREATE TRIGGER phone_number_check_branch_UP
	BEFORE UPDATE ON branch
    FOR EACH ROW
    BEGIN
    IF (new.phone NOT LIKE (CONCAT((SELECT phone_code FROM city ci JOIN country co USING(country_id) WHERE new.city_id = ci.city_id),'%'))) THEN
		signal sqlstate '45000' set message_text = "Номер телефона должен начинаться с другой цифры";
    END IF;
END$$

-- (5)
-- Номер телефона продавца должен начинаться с кода страны в котором находится филиал INSERT
CREATE TRIGGER phone_number_check_seller
	BEFORE INSERT ON seller
    FOR EACH ROW
    BEGIN
    IF (new.phone NOT LIKE (CONCAT(( SELECT phone_code FROM city ci JOIN country co USING(country_id) JOIN branch b USING(city_id) WHERE new.branch_id = b.branch_id),'%'))) THEN
		signal sqlstate '45000' set message_text = "Номер телефона должен начинаться с другой цифры";
    END IF;
END$$

-- Номер телефона продавца должен начинаться с кода страны в котором находится филиал UPDATE
CREATE TRIGGER phone_number_check_seller_UP
	BEFORE UPDATE ON seller
    FOR EACH ROW
    BEGIN
    IF (new.phone NOT LIKE (CONCAT(( SELECT phone_code FROM city ci JOIN country co USING(country_id) JOIN branch b USING(city_id) WHERE new.branch_id = b.branch_id),'%'))) THEN
		signal sqlstate '45000' set message_text = "Номер телефона должен начинаться с другой цифры";
    END IF;
END$$

-- (6)
-- не может быть харатеристики у товара не того типа INSERT
CREATE TRIGGER product_type_equal_characteristics
	BEFORE INSERT ON product_characteristics
    FOR EACH ROW
    BEGIN
    IF ((SELECT type_id FROM product WHERE product_id=new.product_id) NOT IN (SELECT type_id FROM type_characteristics WHERE new.characteristics_id = characteristics_id)) THEN
		signal sqlstate '45000' set message_text = "Нет такой характеристики у такого типа товара";
    END IF;
END$$

-- не может быть харатеристики у товара не того типа UPDATE
CREATE TRIGGER product_type_equal_characteristics_UP
	BEFORE UPDATE ON product_characteristics
    FOR EACH ROW
    BEGIN
    IF ((SELECT type_id FROM product WHERE product_id=new.product_id) NOT IN (SELECT type_id FROM type_characteristics WHERE new.characteristics_id = characteristics_id)) THEN
		signal sqlstate '45000' set message_text = "Нет такой характеристики у такого типа товара";
    END IF;
END$$

-- (7)
-- нельзя купить больше товаров, чем товаров находящихся в филиале INSERT
CREATE TRIGGER quantity_limit
	BEFORE INSERT ON sale_product
    FOR EACH ROW
    BEGIN
    IF (new.quantity > (
	SELECT quantity 
    FROM sale
    JOIN seller USING(seller_id)
    JOIN branch USING(branch_id)
    JOIN branch_product USING(branch_id)
    WHERE sale_id=new.sale_id AND new.product_id=product_id)) THEN
		signal sqlstate '45000' set message_text = "Нет такого количества товара в филиале";
    END IF;
END$$

-- нельзя купить больше товаров, чем товаров находящихся в филиале UPDATE
CREATE TRIGGER quantity_limit_UP
	BEFORE UPDATE ON sale_product
    FOR EACH ROW
    BEGIN
    IF (new.quantity > (
	SELECT quantity 
    FROM sale
    JOIN seller USING(seller_id)
    JOIN branch USING(branch_id)
    JOIN branch_product USING(branch_id)
    WHERE sale_id=new.sale_id AND new.product_id=product_id)) THEN
		signal sqlstate '45000' set message_text = "Нет такого количества товара в филиале";
    END IF;
END$$

-- (8)
-- нельзя совершить покупку в будующем покупка INSERT
CREATE TRIGGER date_future
	BEFORE INSERT ON sale
    FOR EACH ROW
    BEGIN
    IF (new.date>curdate()) THEN
		signal sqlstate '45000' set message_text = "Нельзья покупать товар в будующем";
    END IF;
END$$


-- нельзя совершить покупку в будущем покупка UPDATE
CREATE TRIGGER date_future_UP
	BEFORE UPDATE ON sale
    FOR EACH ROW
    BEGIN
    IF (new.date>curdate()) THEN
		signal sqlstate '45000' set message_text = "Нельзья покупать товар в будующем";
    END IF;
END$$


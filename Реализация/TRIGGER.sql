DELIMITER $$
-- Добавляют на счёт компании цену продожи
CREATE TRIGGER sale_fixed_capital
	AFTER INSERT ON sale
    FOR EACH ROW
    BEGIN
    UPDATE company 
    SET money = money + new.total_cost
    WHERE company_id=(SELECT company_id
    FROM sale
    JOIN seller USING(seller_id)
    JOIN branch USING(branch_id)
    WHERE new.sale_id=sale_id);
END$$

-- Запрет на выстовление цены ниже закупочной INSERT
CREATE TRIGGER the_purchase_price_lower
	BEFORE INSERT ON branch_product
    FOR EACH ROW
    BEGIN
    IF (new.price < (SELECT price_purchases
    FROM product
	WHERE product_id=new.product_id) ) THEN
		signal sqlstate '45000' set message_text = 'Закупочная цена выше!!';
	END IF;
END$$

-- Запрет на выстовление цены ниже закупочной UPDATE
CREATE TRIGGER the_purchase_price_lower_UP
	BEFORE UPDATE ON branch_product
    FOR EACH ROW
    BEGIN
    IF (new.price < (SELECT price_purchases
    FROM product
	WHERE product_id=new.product_id) ) THEN
		signal sqlstate '45000' set message_text = 'Закупочная цена выше!!';
	END IF;
END$$

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

-- нельзя купить больше товаров, чем товаров находящихся в филиале INSERT
CREATE TRIGGER quantity_limit
	BEFORE INSERT ON sale_product
    FOR EACH ROW
    BEGIN
    IF (new.quantity > (SELECT quantity FROM branch_product WHERE new.branch_id= branch_id AND new.product_id=product_id )) THEN
		signal sqlstate '45000' set message_text = "Нет такого количества товара в филиале";
    END IF;
END$$

-- нельзя купить больше товаров, чем товаров находящихся в филиале UPDATE
CREATE TRIGGER quantity_limit_UP
	BEFORE UPDATE ON sale_product
    FOR EACH ROW
    BEGIN
    IF (new.quantity > (SELECT quantity FROM branch_product WHERE new.branch_id= branch_id AND new.product_id=product_id )) THEN
		signal sqlstate '45000' set message_text = "Нет такого количества товара в филиале";
    END IF;
END$$

-- после преобретения филиалом товара, со счёта компании списываются деньги
CREATE TRIGGER sale_fixed_capital_decreases
	AFTER INSERT ON branch_product
    FOR EACH ROW
    BEGIN
    UPDATE company 
    SET money = money - (SELECT price_purchases FROM product WHERE product_id=new.product_id)
    WHERE company_id=(SELECT company_id
    FROM branch
    WHERE new.branch_id=branch_id);
END$$






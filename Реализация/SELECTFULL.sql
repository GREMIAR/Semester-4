CALL search_product("Орёл","Samsung","Монитор");
CALL search_model_characteristics("Ryzen 7 3800XT");
CALL search_models_in_city("Орёл");
CALL discount_model(0.10,'S24F354FHI');

-- Количесво филиалов в городе
SELECT c.name Город, COUNT(*) Филиалов
FROM city c
JOIN branch b USING(city_id)
GROUP BY c.name;

-- Чистая прибыль от покупки
SELECT s.sale_id ID_Покупки, SUM((p.price - p.price_purchases)*sp.quantity) Прибыль
FROM sale s
JOIN sale_product sp USING(sale_id)
JOIN branch_product bp USING(branch_id,product_id)
JOIN product p USING(product_id)
GROUP BY sale_id;

-- Сотрудникои которы получат премию
SELECT CONCAT(se.lastname,' ',LEFT(se.firstname,1),'.',LEFT(se.patronymic,1),'.') Фамилия_и_Инициалы, SUM((p.price)*sp.quantity) Продал_на
FROM product p
JOIN branch_product bp USING(product_id)
JOIN sale_product sp USING(product_id,branch_id)
JOIN sale sa USING(sale_id)
JOIN seller se USING(seller_id)
WHERE MONTH(sa.date) = MONTH(curdate()) AND YEAR(sa.date) = YEAR(curdate())
GROUP BY se.seller_id
HAVING SUM((p.price)*sp.quantity)>100000
ORDER BY Фамилия_и_Инициалы;

-- Сумма прибыли с покупок за последний месяц во всей сети компьютерных магазинов
SELECT SUM((p.price)*sp.quantity) Сумма
FROM sale s
JOIN sale_product sp USING(sale_id)
JOIN branch_product bp USING(branch_id,product_id)
JOIN product p USING(product_id)
WHERE month(s.date) = month(curdate());

-- Сумма прибыли с покупок за последний год во всей сети компьютерных магазинов
SELECT SUM((p.price)*sp.quantity) Сумма
FROM sale s
JOIN sale_product sp USING(sale_id)
JOIN branch_product bp USING(branch_id,product_id)
JOIN product p USING(product_id)
WHERE (year(s.date) = year(curdate()));

-- Сумма прибыли с покупок за месяц в филиалах по отдельности
SELECT CONCAT(b.street,', ', b.house) Адрес,  SUM((p.price)*sp.quantity) Прибыль_за_месяц
FROM sale s
JOIN sale_product sp USING(sale_id)
JOIN branch_product bp USING(branch_id,product_id)
JOIN product p USING(product_id)
JOIN branch b USING (branch_id)
WHERE month(s.date) = month(curdate())
GROUP BY b.branch_id;

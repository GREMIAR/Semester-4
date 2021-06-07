CALL search_product("Орёл","Samsung","Монитор");
CALL search_model_characteristics("Ryzen 7 3800XT");
CALL search_models_in_city("Орёл");

-- Количесво филиалов в городе
SELECT c.name, COUNT(b.address) Филиалов
FROM city c
JOIN branch b USING(city_id)
GROUP BY c.name;

-- Прибыль от покупке
SELECT s.sale_id, SUM((bp.price - p.price_purchases)*sp.quantity) Прибыль
FROM sale s
JOIN sale_product sp USING(sale_id)
JOIN branch_product bp USING(branch_id,product_id)
JOIN product p USING(product_id)
GROUP BY sale_id;

-- Сотрудникои которы получат премию
SELECT se.initials Фамилия_и_Инициалы,SUM(sa.total_cost) Продал_на
FROM seller se
JOIN sale sa USING(seller_id)
WHERE MONTH(sa.date) = MONTH(curdate()) AND YEAR(sa.date) = YEAR(curdate())
GROUP BY se.seller_id
HAVING SUM(sa.total_cost)>100000
ORDER BY se.initials;

-- Сумма прибыли с покупок
SELECT SUM(sa.total_cost) Сумма
FROM seller se
JOIN sale sa USING(seller_id)
WHERE month(sa.date) = month(curdate())
GROUP BY sa.sale_id

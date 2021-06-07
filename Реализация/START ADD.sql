INSERT INTO country(name,phone_code)
VALUES ('Россия','+7'),('Франция','+33'),('Великобритания','+44'),('Китай','+86');

INSERT INTO city(name,country_id)
VALUES ('Орёл',1),('Москва',1),
		('Париж',2),('Лондон',3);
        
INSERT INTO company(name,money)
VALUES ('HoTi',1000000000);

INSERT INTO branch(address,phone,city_id,company_id)
VALUES ('Комсомольская, 81', '+79208281152',1,1),('Наугорское шоссе, 19', '+79209455432',1,1),
('50 лет Октября, 15', '+79408282354',2,1),
('Ришелье, 39', '+339704681453',3,1),
('Шафтсбери авеню, 19', '+442348424152',4,1);

INSERT INTO seller(initials,phone,branch_id)
VALUES ('Александров А.А.', '+79209872132',1),('Аринский В.С.', '+79203324562',1),
('Григорьев М.С.', '+79890539924 ',2),('Зорина Н.А.', '+79619240478',2),
('Андреев А.Д.', '+79129884445',3),('Иванов Г.А.', '+79587570037',3),
('Антуан П.А.', '+339699736518',4),('Мари И.Ф.', '+339763207731',4),
('Адамсон У.А.', '+449999992667',5),('Эдриан В.А.', '+449709461246',5);

INSERT INTO type(name)
VALUES ('Монитор'),('Процессор'),('Видеокарта'),
('Оперативная память'),('Материнская плата'),('Кулер');

INSERT INTO characteristics(name)
VALUES ('Диагональ'),('Разрешение'),('Матрица'),('Герцовка'),
('Сокет'),('Техпроцесс'),('Количество ядер'),('Количество потоков'),('Частота'),
('Разрядность шины'),('Объем'),('Тип видеопамяти'),
('Тип памяти'),
('Поддержка частот оперативной памяти'),('Максимальный объем оперативной памяти'),('Слотов памяти DDR4'),
('Ширина'),('Длинна'),('глубина');

INSERT INTO type_characteristics(type_id,characteristics_id)
VALUES (1,1),(1,2),(1,3),(1,4),
 (2,5),(2,6),(2,7),(2,8),(2,9),
 (3,10),(3,11),(3,12),(3,9),
 (4,13),(4,9),(4,11),
 (5,14),(5,15),(5,16),(5,5),
  (6,17),(6,18),(6,19);
  
INSERT INTO manufacturer(name, phone, country_id)
VALUES ('Samsung','+868005555555',4),('Aoc','+8688003508845',4),('AMD','+8688433508845',4),('NVIDIA','+868800567745',4);

INSERT INTO product(description, manufacturer_id, type_id, price_purchases,name)
VALUES ('Монитор способствует отображению детализированной и контрастной картинки, лишенной малейших искажений, поскольку в его основу положена матрица PLS.',1,1,10000,'S24F354FHI'),('Процессор представляет собой высокопроизводительное решение, которое может стать отличной основой для игровых и рабочих систем. ',2,2,30000,'Ryzen 7 3800XT'),('Отличный выбор для пользователей, которые хотят создать игровую систему начального или среднего уровня.',3,3,20000,'GeForce GTX 1050');

INSERT INTO product_characteristics(product_id, characteristics_id, value, measure_units)
VALUES (1,1,'24','Дюймов'),
(1,4,'75','Герц'),
(2,6,'7','нм'),
(2,9,'3,7','Герц'),
(3,10,'128','бит'),
(3,9,'1290','Герц');

INSERT INTO product_characteristics(product_id, characteristics_id, value)
VALUES (1,2,'1920x1080'),(1,3,'PLS'),
(2,5,'AM4'),(2,7,'8'),(2,8,'9'),(3,11,'2'),(3,12,'GDDR5');

INSERT INTO branch_product(branch_id, product_id, price, quantity)
VALUES (1,1,20000,5),(1,2,60000,5),(1,3,40000,5),
(2,1,21000,5),(2,2,61000,5),(2,3,41000,5),
(3,1,22000,5),(3,2,62000,5),(3,3,42000,5),
(4,1,23000,5),(4,2,63000,5),(4,3,43000,5),
(5,1,24000,5),(5,2,64000,5),(5,3,44000,5);

INSERT INTO sale(total_cost, seller_id, date)
VALUES (200000,1,now());

INSERT INTO sale_product(branch_id, product_id, sale_id, quantity)
VALUES (1,1,1,1),(1,2,1,3);
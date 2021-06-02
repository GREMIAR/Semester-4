SELECT COUNT(1) Количество, '92ПГ' Группа , CURDATE() Дата
FROM journal.group
	JOIN class c USING (group_id)
WHERE journal.group.name = '92ПГ' AND CURDATE() = date
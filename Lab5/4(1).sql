SELECT COUNT(*) Количество
FROM journal.group
	JOIN student USING (group_id)
WHERE group.name = '92ПГ'
SELECT p.surname Фамилия, COUNT(*) Количество
FROM professor p
 JOIN professor_subject_in_plan USING(professor_id)
 JOIN class USING(professor_id, subject_in_plan_id)
 WHERE CURDATE() = date
GROUP BY surname

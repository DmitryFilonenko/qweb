select t.id,
       t.employeer_id,
       t.item_date,
       t.result_time,
       trunc(t.result_time),
       e.name_f || ' ' || e.name_i || ' ' || e.name_o,
       to_char(substr(t.comments, 1, 3990))
  from suvd.todo_items t, suvd.employeers e
 where e.id = t.employeer_id
   and t.project_id = 2067305
   and trunc(t.result_time) between '29.08.2018' and '30.08.2018' 

select t.id,
       t.employeer_id,
       t.item_date,
       t.result_time,
       e.name_f || ' ' || e.name_i || ' ' || e.name_o,
       to_char(t.comments)
  from suvd.todo_items t, suvd.employeers e
 where e.id = t.employeer_id

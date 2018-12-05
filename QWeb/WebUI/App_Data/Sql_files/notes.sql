select t.id,
       t.employeer_id,
       p.id,
       p.business_n,
       t.item_date,
       t.result_time,
       e.name_f || ' ' || e.name_i || ' ' || e.name_o,
       to_char(t.comments)
  from suvd.todo_items t, suvd.employeers e, suvd.projects p
 where e.id = t.employeer_id
   and p.id = t.project_id

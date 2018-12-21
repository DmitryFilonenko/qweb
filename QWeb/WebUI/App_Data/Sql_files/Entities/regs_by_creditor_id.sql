SELECT cr.name, cr.id, t.d_number, t.id, t.act_date, t.start_date, t.stop_date, t.is_active
  FROM suvd.creditor_dogovors t, suvd.creditors cr
 WHERE t.creditor_id = cr.id
   AND t.creditor_id = 

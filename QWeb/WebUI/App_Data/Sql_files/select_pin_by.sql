select c.name_f,
       c.name_i,
       c.name_o,
       c.inn,
       p.id,
       p.business_n,
       p.debt_dogovor_n,
       p.debt_rest,
       p.total_zad,
       p.archive_flag,
       (select ca.zip_code || ' ' || ca.region || ' ' || ca.city || ' ' || ca.street from suvd.contact_address ca where ca.contact_id = c.id and ca.role = 1 ),
       (select ca.zip_code || ' ' || ca.region || ' ' || ca.city || ' ' || ca.street from suvd.contact_address ca where ca.contact_id = c.id and ca.role = 2 ),
       (select ca.zip_code || ' ' || ca.region || ' ' || ca.city || ' ' || ca.street from suvd.contact_address ca where ca.contact_id = c.id and ca.role = 3 ),
       (select ca.zip_code || ' ' || ca.region || ' ' || ca.city || ' ' || ca.street from suvd.contact_address ca where ca.contact_id = c.id and ca.role = 4 ),
       p.kv_prodavec,
       'c ' || p.date_create_pts || ' no ' ||  p.change_date,
       p.compensation,
       p.reason_create_pts,
       cr.name,
       cr.id,
       cd.d_number,
       cd.id,
       o.org_id,
       cd.start_date,
       cd.stop_date
  from suvd.projects p, suvd.contacts c, suvd.creditors cr, suvd.creditor_dogovors cd, eadr.organization_info o
 where p.debtor_contact_id = c.id   
   and p.creditor_id = cr.id
   and p.dogovor_id = cd.id
   and o.org_cd_id = cr.id


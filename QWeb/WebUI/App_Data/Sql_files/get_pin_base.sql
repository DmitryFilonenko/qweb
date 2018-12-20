select p.id,
       p.business_n,
       p.debt_dogovor_n,
       p.debtor_contact_id,
       c.inn,
       p.archive_flag,
       cr.name,
       cr.id,
       o.org_id,
       cd.d_number,
       cd.id,
       o.org_id
  from suvd.projects p, suvd.contacts c, suvd.creditors cr, suvd.creditor_dogovors cd, eadr.organization_info o
 where p.creditor_id = cr.id
   and p.debtor_contact_id = c.id
   and p.dogovor_id = cd.id
   and o.org_cd_id = cr.id


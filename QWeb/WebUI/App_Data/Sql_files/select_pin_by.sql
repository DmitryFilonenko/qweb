select c.name_f,
       c.name_i,
       c.name_o,
       c.inn,
       p.id,
       p.business_n,
       p.debt_dogovor_n,
       p.debt_rest,
       p.total_zad,
       p.archive_flag
  from suvd.projects p, suvd.contacts c
 where p.debtor_contact_id = c.id 


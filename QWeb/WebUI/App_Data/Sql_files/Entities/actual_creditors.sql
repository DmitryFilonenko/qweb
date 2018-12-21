select distinct o.org_name,
                o.org_cd_id,
                o.org_id
  from EADR.ORGANIZATION_INFO o, suvd.creditor_dogovors d
 where o.org_cd_id = d.creditor_id
   and d.stop_date > sysdate - 60


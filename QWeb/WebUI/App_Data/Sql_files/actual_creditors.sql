select distinct o.org_name,
                o.alias,
                o.org_cd_id,
                o.org_id,
                case
                  when d.d_number like '%ÔÃÂÔÎ%' then 'Fund'
                  when d.d_number not like '%ÔÃÂÔÎ%' then ' '
                end 
  from EADR.ORGANIZATION_INFO o, suvd.creditor_dogovors d
 where o.org_cd_id = d.creditor_id
   and d.stop_date > sysdate - 60


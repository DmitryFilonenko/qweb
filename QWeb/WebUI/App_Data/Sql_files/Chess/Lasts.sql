select sel.*
  from (select t.PLAYER, t.SIDE, t.RES_TIME, t.QUESTION, t.DT
          from Q_CHESS t
         order by t.DT desc) sel
 where ROWNUM <= 10

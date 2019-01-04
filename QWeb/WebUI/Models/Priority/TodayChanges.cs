using DbLayer.Managers;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
using System.Xml;

namespace WebUI.Models.Priority
{
    public class PriorReport
    {
        [Display(Name = "Приоритет:")]
        public string PriorValue { get; set; }
        [Display(Name = "Количество:")]
        public string PriorCount { get; set; }
    }

    public class TodayChanges
    {
        List<PriorReport> _changesList;
        public List<PriorReport> ChangesList { get { return _changesList; } }

        public TodayChanges()
        {
            _changesList = new List<PriorReport>();

            string xmlStr = ManagerPlProc.ExecFunc("q_prior_pack.get_today_count", OracleDbType.Varchar2);
            if (xmlStr.Length > 0)
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(xmlStr);
                foreach (XmlNode xmlNode in xDoc.DocumentElement.ChildNodes)
                {
                    PriorReport pr = new PriorReport();
                    foreach (XmlNode item in xmlNode)
                    {                        
                        if (item.Name == "prior")
                        {
                            pr.PriorValue = item.InnerText;
                        }
                        if(item.Name == "count")
                        {
                            pr.PriorCount = item.InnerText;
                        }                        
                    }
                    _changesList.Add(pr);
                }
            }
        }
    }
}
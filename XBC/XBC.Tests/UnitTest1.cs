using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using XBC.DataModel;

namespace XBC.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void InsertVariant()
        {
            Trace.WriteLine("--- Start Test Variant ---");
            using (var db = new XBC_Context())
            {
                t_audit_log variant = new t_audit_log();
                variant.type = "sda";
                variant.json_insert = "TSdsT";
                variant.json_before = "Tesssting";
                variant.json_after = "hhddh";
                variant.json_delete = "Deaaar";
                variant.created_by = 1;
                variant.created_on = DateTime.Now;
                db.t_audit_log.Add(variant);
                db.SaveChanges();
            }
            Trace.WriteLine("--- End Test Variant ---");
        }
    }
}

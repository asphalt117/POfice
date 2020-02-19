using Domain.Entities;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using WebUI.Infrastructure;
using System.Text;
using System.Web;

namespace WebUI.Controllers
{
    public class DocsController : BaseController
    {
        [Authorize]
        public ActionResult Index()
        {
            ViewBag.MenuItem = "trust";
            List<Trust> trusts = db.Trusts.Where(t => t.CustId == Cust.CustId).ToList();
            return View(trusts);
        }

        public ActionResult Doc()
        {
            ViewBag.MenuItem = "doc";
            Doc doc = db.Docs.Find(6);
            
            // сохраним первый файл из списка
            if (doc!=null)
            {
                HttpResponse http;
                //http.OutputStream= doc.DocBin;
                //HttpContext.Response.OutputStream= doc.DocBin;
                //HttpResponse.OutputStream = doc.DocBin;
                //using (FileStream fs = File.Create(newFile))
                //{
                //    stream.WriteTo(fs);
                //}

                //File.WriteAllBytes(doc.DocName, doc.DocBin);
                //using (System.IO.FileStream fs = new System.IO.FileStream(path1, FileMode.OpenOrCreate))
                //{
                //    fs.Write(doc.DocBin, 0, doc.DocBin.Length);
                //}

                //var html = doc.DocBin.ToString();

                //using (var stream = new MemoryStream(doc.DocBin))
                //{
                //    stream.Write(doc.DocBin, 0, doc.DocBin.Length);
                //    HttpContext.Response.Clear();
                //    HttpContext.Response.ContentType = "application/vnd.ms-excel";
                //    HttpContext.Response.BufferOutput = true;
                //    HttpContext.Response.AddHeader("content-disposition", $"attachment; filename={doc.FileName}");
                //    HttpContext.Response.ContentEncoding = Encoding.UTF8;
                //    HttpContext.Response.Charset = "utf-8";
                //    //HttpContext.Response.Write(stream);
                //    HttpContext.Response.Flush();
                //    HttpContext.Response.End();

                //    //return new ExcelResult(doc.FileName, stream);
                //}
                //using (var writer = new StreamWriter(stream))
                //{
                //    writer.Write(html);
                //    writer.Flush();
                //    stream.Position = 0;
                //}
                //return new ExcelResult(doc.FileName, "qqq");

                //string path1 = @"D:\vs2019\\test.xls";
                ////using (System.IO.FileStream fs = new System.IO.FileStream(doc.FileName, FileMode.OpenOrCreate))
                //using (System.IO.FileStream fs = new System.IO.FileStream(path1, FileMode.OpenOrCreate))
                //{
                //    fs.Write(doc.DocBin, 0, doc.DocBin.Length);
                //    //Console.WriteLine("Изображение '{0}' сохранено", images[0].Title);
                //}
            }
            return View();
        }

        public ActionResult Contract()
        {
            ViewBag.MenuItem = "contract";
            List<Contract> contracts = db.Contracts.Where(t => t.CustID == Cust.CustId).ToList();                
            return View(contracts);
        }
        public ActionResult SchFact()
        {
            ViewBag.MenuItem = "chfact";
            ViewBag.mail = User.Identity.Name;                
            return View();
        }
        [HttpPost]
        public ActionResult SchFact(string note)
        {
            OrdInvoice ord = new OrdInvoice();
            ord.CustId = Cust.CustId;
            ord.Note = note;
            db.OrdInvoices.Add(ord);
            db.SaveChanges();

            ViewBag.mail = User.Identity.Name;
            return View("SchFactZak");
        }
    }
}

using Group8AD_WebAPI.Models;
using Group8AD_WebAPI.Models.ViewModels;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Group8AD_WebAPI.BusinessLogic
{
    public class PdfBL
    {


        public static string GenerateDisbursementListbyDept(List<DisbursementDetailVM> List, string filename)
        {
            SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext();


            List<DisbursementDetailVM> disbList = entities.RequestDetails.Where(rd => rd.ReqQty > 0)
                                                   .Join(entities.Requests, rd => rd.ReqId, r => r.ReqId, (rd, r) => new { rd, r })
                                                   .Join(entities.Items, rd => rd.rd.ItemCode, i => i.ItemCode, (rd, i) => new { rd, i })
                                                   .Join(entities.Employees, r => r.rd.r.EmpId, e => e.EmpId, (r, e) => new { r, e })
                                                   .Select(result => new DisbursementDetailVM
                                                   {
                                                       DeptCode = result.e.DeptCode,
                                                       ItemCode = result.r.rd.rd.ItemCode,
                                                       Category = result.r.i.Cat,
                                                       Description = result.r.i.Desc,
                                                       ReqQty = result.r.rd.rd.ReqQty,
                                                       AwaitQty = result.r.rd.rd.AwaitQty,
                                                       FulfilledQty = result.r.rd.rd.FulfilledQty,
                                                       EmpId = result.e.EmpId,
                                                       ReqId = result.r.rd.r.ReqId
                                                   }).ToList();


            List<string> deptCodes = disbList.Select(d => d.DeptCode).Distinct().ToList();

            string filePath = HttpContext.Current.Server.MapPath("~/Report_Templates/");
            string HTML = string.Empty;
            //  HTML = File.ReadAllText(filePath + "DisbursementListByDept_Header.txt", System.Text.Encoding.UTF8);
            foreach (string dept in deptCodes)
            {
                string collectionpint = entities.CollectionPoints
                                        .Join(entities.Departments.Where(d => d.DeptCode.Equals(dept)), c => c.ColPtId, d => d.ColPtId, (c, d) => new { c, d }).Select(x => x.c.Location).First().ToString();

                string collectiontime = entities.CollectionPoints
                                        .Join(entities.Departments.Where(d => d.DeptCode.Equals(dept)), c => c.ColPtId, d => d.ColPtId, (c, d) => new { c, d }).Select(x => x.c.Time).First().ToString();

                //string colpoint = entities.Departments.Where(d => d.ColPtId == d.co

                string repname = entities.Employees
                                 .Join(entities.Departments.Where(d => d.DeptCode.Equals(dept)), e => e.DeptCode, d => d.DeptCode, (e, d) => new { e, d })
                                 .Where(r => r.e.EmpId == r.d.DeptRepId).Select(x => x.e.EmpName).First().ToString();

                string deptname = entities.Departments.Where(d => d.DeptCode.Equals(dept)).Select(d => d.DeptName).First().ToString();
                List<DisbursementDetailVM> dList = new List<DisbursementDetailVM>();



                HTML = string.Concat(HTML, File.ReadAllText(filePath + "DisbursementListByDept_Header.txt", System.Text.Encoding.UTF8));
                HTML = HTML.Replace("[disb-date]", DateTime.Now.ToString("dd MMMM yyyy"));
                HTML = HTML.Replace("[coll-point]", collectionpint);
                HTML = HTML.Replace("[coll-time]", collectiontime);
                HTML = HTML.Replace("[DeptName]", deptname);
                HTML = HTML.Replace("[rep-name]", repname);

                //foreach (DisbursementDetailVM disb in disbList.Where(d => d.DeptCode.Equals(dept)))
                //{
                //    dList.Add(disb);
                //}

                foreach (DisbursementDetailVM dis in disbList.Where(d => d.DeptCode.Equals(dept)))
                {
                    // HTML = File.ReadAllText(filePath + "DisbursementListByDept_Header.txt");


                    HTML = string.Concat(HTML, File.ReadAllText(filePath + "DisbursementListByDept_Body.txt", System.Text.Encoding.UTF8));

                    HTML = HTML.Replace("[itemcode]", dis.ItemCode);
                    HTML = HTML.Replace("[item_desc]", dis.Description);
                    HTML = HTML.Replace("[request_qty]", dis.ReqQty.ToString());
                    HTML = HTML.Replace("[await_qty]", dis.AwaitQty.ToString());
                    HTML = HTML.Replace("[fulfilled_qty]", dis.FulfilledQty.ToString());

                }
                HTML = string.Concat(HTML, File.ReadAllText(filePath + "DisbursementListByDept_Footer.txt", System.Text.Encoding.UTF8));


            }
            PDFGenerator(filename, HTML);
            return filename;
        }

        public static string GenerateDisbursementListby_Dept_Employee_OrderNo(List<DisbursementDetailVM> List, string filename)
        {
            SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext();


            List<DisbursementDetailVM> disbList = entities.RequestDetails.Where(rd => rd.ReqQty > 0)
                                                   .Join(entities.Requests, rd => rd.ReqId, r => r.ReqId, (rd, r) => new { rd, r })
                                                   .Join(entities.Items, rd => rd.rd.ItemCode, i => i.ItemCode, (rd, i) => new { rd, i })
                                                   .Join(entities.Employees, r => r.rd.r.EmpId, e => e.EmpId, (r, e) => new { r, e })
                                                   .Select(result => new DisbursementDetailVM
                                                   {
                                                       DeptCode = result.e.DeptCode,
                                                       ItemCode = result.r.rd.rd.ItemCode,
                                                       Category = result.r.i.Cat,
                                                       Description = result.r.i.Desc,
                                                       ReqQty = result.r.rd.rd.ReqQty,
                                                       AwaitQty = result.r.rd.rd.AwaitQty,
                                                       FulfilledQty = result.r.rd.rd.FulfilledQty,
                                                       EmpId = result.e.EmpId,
                                                       ReqId = result.r.rd.r.ReqId
                                                   }).ToList();


            List<string> deptCodes = disbList.Select(d => d.DeptCode).Distinct().ToList();

            string filePath = HttpContext.Current.Server.MapPath("~/Report_Templates/");
            string HTML = string.Empty;
            //  HTML = File.ReadAllText(filePath + "DisbursementListByDept_Header.txt", System.Text.Encoding.UTF8);
            foreach (string dept in deptCodes)
            {
                string collectionpint = entities.CollectionPoints
                                        .Join(entities.Departments.Where(d => d.DeptCode.Equals(dept)), c => c.ColPtId, d => d.ColPtId, (c, d) => new { c, d }).Select(x => x.c.Location).First().ToString();

                string collectiontime = entities.CollectionPoints
                                        .Join(entities.Departments.Where(d => d.DeptCode.Equals(dept)), c => c.ColPtId, d => d.ColPtId, (c, d) => new { c, d }).Select(x => x.c.Time).First().ToString();

                //string colpoint = entities.Departments.Where(d => d.ColPtId == d.co

                string repname = entities.Employees
                                 .Join(entities.Departments.Where(d => d.DeptCode.Equals(dept)), e => e.DeptCode, d => d.DeptCode, (e, d) => new { e, d })
                                 .Where(r => r.e.EmpId == r.d.DeptRepId).Select(x => x.e.EmpName).First().ToString();



                string deptname = entities.Departments.Where(d => d.DeptCode.Equals(dept)).Select(d => d.DeptName).First().ToString();
                List<DisbursementDetailVM> dList = new List<DisbursementDetailVM>();
                List<int> EmpList = new List<int>();


                HTML = string.Concat(HTML, File.ReadAllText(filePath + "DisbursementListByDept_Emp_Order_Header.txt", System.Text.Encoding.UTF8));
                HTML = HTML.Replace("[disb-date]", DateTime.Now.ToString("dd MMMM yyyy"));
                HTML = HTML.Replace("[coll-point]", collectionpint);
                HTML = HTML.Replace("[coll-time]", collectiontime);
                HTML = HTML.Replace("[DeptName]", deptname);
                HTML = HTML.Replace("[rep-name]", repname);


                List<int> empIds = disbList.Select(d => d.EmpId).Distinct().ToList();
                foreach (int emp in empIds)
                {

                    string empName = EmployeeBL.GetEmp(emp).EmpName;
                    HTML = string.Concat(HTML, File.ReadAllText(filePath + "DisbursementListByDept_Emp_Order_Sub_Header.txt", System.Text.Encoding.UTF8));
                    HTML = HTML.Replace("[emp-name]", empName);


                    foreach (DisbursementDetailVM dis in disbList.Where(d => d.EmpId == emp && d.DeptCode.Equals(dept)))
                    {
                        HTML = string.Concat(HTML, File.ReadAllText(filePath + "DisbursementListByDept_Emp_Order_Body.txt", System.Text.Encoding.UTF8));
                        HTML = HTML.Replace("[orderNo]", dis.ReqId.ToString());
                        HTML = HTML.Replace("[itemcode]", dis.ItemCode);
                        HTML = HTML.Replace("[item_desc]", dis.Description);
                        HTML = HTML.Replace("[request_qty]", dis.ReqQty.ToString());
                        HTML = HTML.Replace("[await_qty]", dis.AwaitQty.ToString());
                        HTML = HTML.Replace("[fulfilled_qty]", dis.FulfilledQty.ToString());

                    }
                    HTML = string.Concat(HTML, File.ReadAllText(filePath + "DisbursementListByDept_Emp_Order_Footer.txt", System.Text.Encoding.UTF8));

                }




            }
            PDFGenerator(filename, HTML);
            return filename;
        }

        public static void GenerateLowStockItemList(string filename)
        {
            SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext();

            List<ItemVM> LowStockItemList = ItemBL.GetLowStockItems();

            string filePath = HttpContext.Current.Server.MapPath("~/Report_Templates/");
            string HTML = string.Empty;      

            HTML = string.Concat(HTML, File.ReadAllText(filePath + "LowStockItemList_Header.txt", System.Text.Encoding.UTF8));
            HTML = HTML.Replace("[date]", DateTime.Now.ToString("dd MMMM yyyy"));

          
            foreach (ItemVM item in LowStockItemList)
            {       
                HTML = string.Concat(HTML, File.ReadAllText(filePath + "LowStockItemList_Body.txt", System.Text.Encoding.UTF8));
                HTML = HTML.Replace("[itemcode]", item.ItemCode);
                HTML = HTML.Replace("[item_cat]", item.Cat);
                HTML = HTML.Replace("[item_desc]", item.Desc);
                HTML = HTML.Replace("[item_balance]", item.Balance.ToString());
                HTML = HTML.Replace("[item_restock_lvl]", item.ReorderLevel.ToString());
                HTML = HTML.Replace("[item_restock_qty]", item.ReccReorderQty.ToString());
                HTML = HTML.Replace("[item_supp1]", item.SuppCode1);
                HTML = HTML.Replace("[item_p1]", item.Price1.ToString());
                HTML = HTML.Replace("[item_supp2]", item.SuppCode2);
                HTML = HTML.Replace("[item_p2]", item.Price2.ToString());
                HTML = HTML.Replace("[item_supp3]", item.SuppCode3);
                HTML = HTML.Replace("[item_p3]", item.Price3.ToString());
            }
            HTML = string.Concat(HTML, File.ReadAllText(filePath + "LowStockItemList_Footer.txt", System.Text.Encoding.UTF8));

            PDFGenerator_A4Landscape(filename, HTML);
        }

        public static void GeneratePurchaseOrderList(List<ItemVM> ListPO,string filename)
        {
           

            string filePath = HttpContext.Current.Server.MapPath("~/Report_Templates/");
            string HTML = string.Empty;

            HTML = string.Concat(HTML, File.ReadAllText(filePath + "POList_Header.txt", System.Text.Encoding.UTF8));
            HTML = HTML.Replace("[date]", DateTime.Now.ToString("dd MMMM yyyy"));


            foreach (ItemVM item in ListPO)
            {
                HTML = string.Concat(HTML, File.ReadAllText(filePath + "POList_Body.txt", System.Text.Encoding.UTF8));
                HTML = HTML.Replace("[itemcode]", item.ItemCode);
                HTML = HTML.Replace("[item_cat]", item.Cat);
                HTML = HTML.Replace("[item_desc]", item.Desc);
                HTML = HTML.Replace("[item_balance]", item.Balance.ToString());
                HTML = HTML.Replace("[item_restock_lvl]", item.ReorderLevel.ToString());
                HTML = HTML.Replace("[item_restock_qty]", item.ReccReorderQty.ToString());
                HTML = HTML.Replace("[item_order_qty]", item.ReorderQty.ToString());
                HTML = HTML.Replace("[item_supp1]", item.SuppCode1);
                HTML = HTML.Replace("[item_p1]", item.Price1.ToString());
                HTML = HTML.Replace("[item_supp2]", item.SuppCode2);
                HTML = HTML.Replace("[item_p2]", item.Price2.ToString());
                HTML = HTML.Replace("[item_supp3]", item.SuppCode3);
                HTML = HTML.Replace("[item_p3]", item.Price3.ToString());
            }
            HTML = string.Concat(HTML, File.ReadAllText(filePath + "POList_Footer.txt", System.Text.Encoding.UTF8));

            PDFGenerator_A4Landscape(filename, HTML);
        }
        public static void PDFGenerator(string filename, string HTML_DATA)
        {
            string filepath = HttpContext.Current.Server.MapPath("~/PDF/");

            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            Panel p = new Panel();
            p.Controls.Add(new LiteralControl(HTML_DATA));
            p.RenderControl(hw);

            StringReader sr = new StringReader(sw.ToString());
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);

            pdfDoc.SetMargins(50, 50, 50, 50);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, new FileStream(filepath + filename, FileMode.Create));

            pdfDoc.Open();
            htmlparser.Parse(sr);
            pdfDoc.Close();

            //adding page number
            byte[] bytes = File.ReadAllBytes(filepath + filename);
            Font blackFont = FontFactory.GetFont("Arial", 9, Font.NORMAL, BaseColor.BLACK);
            using (MemoryStream stream = new MemoryStream())
            {
                PdfReader reader = new PdfReader(bytes);
                using (PdfStamper stamper = new PdfStamper(reader, stream))
                {
                    int pages = reader.NumberOfPages;
                    for (int i = 1; i <= pages; i++)
                    {
                        ColumnText.ShowTextAligned(stamper.GetUnderContent(i), Element.ALIGN_RIGHT, new Phrase("Page " + i.ToString() + " of " + pages.ToString(), blackFont), 568f, 15f, 0);
                    }
                }
                bytes = stream.ToArray();
            }
            File.WriteAllBytes(filepath + filename, bytes);
        }


        public static void PDFGenerator_A4Landscape(string filename, string HTML_DATA)
        {
            string filepath = HttpContext.Current.Server.MapPath("~/PDF/");

            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            Panel p = new Panel();
            p.Controls.Add(new LiteralControl(HTML_DATA));
            p.RenderControl(hw);

            StringReader sr = new StringReader(sw.ToString());
            Document pdfDoc = new Document(PageSize.A3.Rotate(), 10f, 10f, 10f, 0f);

            pdfDoc.SetMargins(50, 50, 50, 50);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, new FileStream(filepath + filename, FileMode.Create));

            pdfDoc.Open();
            htmlparser.Parse(sr);
            pdfDoc.Close();

            //adding page number
            byte[] bytes = File.ReadAllBytes(filepath + filename);
            Font blackFont = FontFactory.GetFont("Arial", 9, Font.NORMAL, BaseColor.BLACK);
            using (MemoryStream stream = new MemoryStream())
            {
                PdfReader reader = new PdfReader(bytes);
                using (PdfStamper stamper = new PdfStamper(reader, stream))
                {
                    int pages = reader.NumberOfPages;
                    for (int i = 1; i <= pages; i++)
                    {
                        ColumnText.ShowTextAligned(stamper.GetUnderContent(i), Element.ALIGN_CENTER, new Phrase("Page " + i.ToString() + " of " + pages.ToString(), blackFont), 568f, 15f, 0);
                    }
                }
                bytes = stream.ToArray();
            }
            File.WriteAllBytes(filepath + filename, bytes);
        }
    }
}
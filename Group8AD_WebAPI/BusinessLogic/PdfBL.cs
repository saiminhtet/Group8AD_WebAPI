﻿using Group8AD_WebAPI.Models;
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


        public static string GenerateDisbursementListbyDept(List<DisbursementDetailVM> disbList, string filename)
        {
            SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext();
            


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

        public static string GenerateDisbursementListby_Dept_Employee_OrderNo(List<DisbursementDetailVM> disbList, string filename)
        {
            SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext();           


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

        public static void GenerateLowStockItemList(int empId)
        {
            string filename = "StationeryItemsWithLowStockQuantities_" + DateTime.Now.ToString("ddMMMMyyyy_HH_mm_ss") + ".pdf";
            SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext();

            List<ItemVM> LowStockItemList = ItemBL.GetLowStockItems();

            string filePath = HttpContext.Current.Server.MapPath("~/Report_Templates/");
            string HTML = string.Empty;

            HTML = string.Concat(HTML, File.ReadAllText(filePath + "LowStockItemList_Header.txt", System.Text.Encoding.UTF8));
            HTML = HTML.Replace("[date]", DateTime.Now.ToString("dd MMMM yyyy"));

            int sr_no = 1;
            foreach (ItemVM item in LowStockItemList)
            {
                HTML = string.Concat(HTML, File.ReadAllText(filePath + "LowStockItemList_Body.txt", System.Text.Encoding.UTF8));
                HTML = HTML.Replace("[#]", sr_no.ToString());
                HTML = HTML.Replace("[itemcode]", item.ItemCode);
                HTML = HTML.Replace("[item_desc]", item.Desc);
                HTML = HTML.Replace("[uom]", item.UOM);
                HTML = HTML.Replace("[item_balance]", item.Balance.ToString());
                HTML = HTML.Replace("[item_restock_lvl]", item.ReorderLevel.ToString());
                HTML = HTML.Replace("[item_restock_qty]", item.ReccReorderQty.ToString());
                HTML = HTML.Replace("[item_supp1]", item.SuppCode1);
                HTML = HTML.Replace("[item_p1]", item.Price1.ToString("C"));  //string.Format("{0:C}", Math.Round(item.Price1,3))
                HTML = HTML.Replace("[item_supp2]", item.SuppCode2);
                HTML = HTML.Replace("[item_p2]", item.Price2.ToString("C"));
                HTML = HTML.Replace("[item_supp3]", item.SuppCode3);
                HTML = HTML.Replace("[item_p3]", item.Price3.ToString("C"));

                sr_no += 1;
            }
            HTML = string.Concat(HTML, File.ReadAllText(filePath + "LowStockItemList_Footer.txt", System.Text.Encoding.UTF8));

            PDFGenerator_A4Landscape(filename, HTML);
            EmailBL.SendLowStockEmail(empId, filename);
        }

        public static void GeneratePurchaseOrderList(int empId, DateTime expected_Date, List<ItemVM> ListPO)
        {
            SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext();

            List<ItemVM> iList = ItemBL.GetAllItems().Take(10).ToList();

            List<ItemVM> items = new List<ItemVM>();

            List<Supplier> suppliers = new List<Supplier>();

            string orderby_name = EmployeeBL.GetEmp(empId).EmpName;
            string approvedby_name_rep = EmployeeBL.GetEmp(105).EmpName;
            string approvedby_name_mgr = EmployeeBL.GetEmp(104).EmpName;

            string filename = "PurchaseOrder_" + DateTime.Now.ToString("ddMMMMyyyy_HH_mm_ss") + ".pdf";
            string filePath = HttpContext.Current.Server.MapPath("~/Report_Templates/");

            string HTML = string.Empty;
            HTML = string.Concat(HTML, File.ReadAllText(filePath + "POList_Header.txt", System.Text.Encoding.UTF8));
          
            List<string> suppcodes1 = iList.Select(x => x.SuppCode1).Distinct().ToList();
            List<string> suppcodes2 = iList.Select(x => x.SuppCode2).Distinct().ToList();
            List<string> suppcodes3 = iList.Select(x => x.SuppCode3).Distinct().ToList();
            foreach (string supp in suppcodes1)
            {
                List<Supplier> suppl = entities.Suppliers.Where(s => s.SuppCode.Equals(supp)).ToList();
                suppliers.AddRange(suppl);
            }
            foreach (string supp in suppcodes2)
            {
                List<Supplier> suppl = entities.Suppliers.Where(s => s.SuppCode.Equals(supp)).ToList();

                if (suppliers.ToList().Find(x=>x.SuppCode.Equals(supp)).SuppCode.Count()==0)
                {
                    suppliers.AddRange(suppl);
                }
               
            }
            foreach (string supp in suppcodes3)
            {
                List<Supplier> suppl = entities.Suppliers.Where(s => s.SuppCode.Equals(supp)).ToList();                
                if (suppliers.ToList().Find(x => x.SuppCode.Equals(supp)).SuppCode.Count() == 0)
                {
                    suppliers.AddRange(suppl);
                }
            }

            foreach (Supplier sup in suppliers.Distinct())
            {


                HTML = string.Concat(HTML, File.ReadAllText(filePath + "POList_Sub_Header.txt", System.Text.Encoding.UTF8));
                HTML = HTML.Replace("[supp_name]", sup.SuppName);
                HTML = HTML.Replace("[supp_ctx_name]", sup.SuppCtcName);
                HTML = HTML.Replace("[supp_addr_1]", sup.SuppAddr);
                HTML = HTML.Replace("[expected_date]", expected_Date.ToString("dd MMMM yyyy"));

                int total_qyantity = 0; double total_price = 0, total_amount=0;

                foreach (ItemVM item in iList.Where(i => i.SuppCode1.Equals(sup.SuppCode) || i.SuppCode2.Equals(sup.SuppCode) || i.SuppCode3.Equals(sup.SuppCode)))
                {
                    if (item != null)
                    {
                        double price = 0;
                        double amount = 0;
                        if (sup.SuppCode.Equals(item.SuppCode1))
                        {
                            price = item.Price1;
                        }
                        if (sup.SuppCode.Equals(item.SuppCode2))
                        {
                            price = item.Price2;
                        }
                        if (sup.SuppCode.Equals(item.SuppCode3))
                        {
                            price = item.Price3;
                        }
                        HTML = string.Concat(HTML, File.ReadAllText(filePath + "POList_Body.txt", System.Text.Encoding.UTF8));
                        HTML = HTML.Replace("[itemcode]", item.ItemCode);                       
                        HTML = HTML.Replace("[item_desc]", item.Desc);                       
                        HTML = HTML.Replace("[item_order_qty]", item.ReorderQty.ToString());                      
                        HTML = HTML.Replace("[item_price]", price.ToString("C"));
                        HTML = HTML.Replace("[item_amount]", (item.ReorderQty*price).ToString("C"));

                        total_qyantity += item.ReorderQty;
                        total_price += price;
                        total_amount += item.ReorderQty * price;
                    }
                    else {break; }



                }
                HTML = string.Concat(HTML, File.ReadAllText(filePath + "POList_Footer.txt", System.Text.Encoding.UTF8));
                HTML = HTML.Replace("[total_qty]", total_qyantity.ToString());
                HTML = HTML.Replace("[total_price]", total_price.ToString("C"));   
                HTML = HTML.Replace("[total_amount]", total_amount.ToString("C"));
                HTML = HTML.Replace("[order_by]", orderby_name);
                HTML = HTML.Replace("[approved_by]", total_qyantity >= 250 ? approvedby_name_mgr : approvedby_name_rep );
                HTML = HTML.Replace("[order_by_date]", System.DateTime.Now.ToString("dd MMMM yyyy"));
                HTML = HTML.Replace("[approved_by_date]", System.DateTime.Now.ToString("dd MMMM yyyy"));
            }
            PDFGenerator(filename, HTML);
            //   EmailBL.SendPOEmail(empId,expected_Date,filename);
        }

        public static void GenerateInventoryItemList(int empId)
        {
            string filename = "InventoryStatusReport_" + DateTime.Now.ToString("ddMMMMyyyy_HH_mm_ss") + ".pdf";

            SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext();

            List<ItemVM> InventoryItemList = ItemBL.GetLowStockItems();

            string filePath = HttpContext.Current.Server.MapPath("~/Report_Templates/");



            string HTML = string.Empty;

            HTML = string.Concat(HTML, File.ReadAllText(filePath + "InventoryItem_Header.txt", System.Text.Encoding.UTF8));
            HTML = HTML.Replace("[date]", DateTime.Now.ToString("dd MMMM yyyy"));

            int sr_no = 1;
            foreach (ItemVM item in InventoryItemList)
            {
                HTML = string.Concat(HTML, File.ReadAllText(filePath + "InventoryItem_Body.txt", System.Text.Encoding.UTF8));
                HTML = HTML.Replace("[#]", sr_no.ToString());
                HTML = HTML.Replace("[itemcode]", item.ItemCode);
                HTML = HTML.Replace("[item_desc]", item.Desc);
                HTML = HTML.Replace("[location]", item.Location);
                HTML = HTML.Replace("[uom]", item.UOM);
                HTML = HTML.Replace("[item_balance]", item.Balance.ToString());
                HTML = HTML.Replace("[item_restock_lvl]", item.ReorderLevel.ToString());
                HTML = HTML.Replace("[item_restock_qty]", item.ReorderQty.ToString());
                HTML = HTML.Replace("[item_supp1]", item.SuppCode1);
                HTML = HTML.Replace("[item_supp2]", item.SuppCode2);
                HTML = HTML.Replace("[item_supp3]", item.SuppCode3);

                sr_no += 1;
            }
            HTML = string.Concat(HTML, File.ReadAllText(filePath + "InventoryItem_Footer.txt", System.Text.Encoding.UTF8));

            PDFGenerator_A4Landscape(filename, HTML);

            EmailBL.SendInvListEmail(empId, filename);
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
            Document pdfDoc = new Document(PageSize.A3, 10f, 10f, 10f, 0f);

            pdfDoc.SetMargins(50, 50, 50, 50);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            //PdfWriter.GetInstance(pdfDoc, new FileStream(filepath + filename, FileMode.Create));

            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, new FileStream(filepath + filename, FileMode.Create));
            pdfDoc.Open();
            //using header class started
            writer.PageEvent = new Header();
            Paragraph welcomeParagraph = new Paragraph();
            pdfDoc.Add(welcomeParagraph);
            //using header class ended
            htmlparser.StartDocument();
            htmlparser.Parse(sr);

            htmlparser.EndDocument();
            htmlparser.Close();
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
            //PdfWriter.GetInstance(pdfDoc, new FileStream(filepath + filename, FileMode.Create));

            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, new FileStream(filepath + filename, FileMode.Create));
            pdfDoc.Open();

            //using header class started
            writer.PageEvent = new Header_Landscape();
            Paragraph welcomeParagraph = new Paragraph();
            pdfDoc.Add(welcomeParagraph);
            //using header class ended

            htmlparser.StartDocument();
            htmlparser.Parse(sr);

            htmlparser.EndDocument();
            htmlparser.Close();
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

        //Header all pages(Portrait)
        public partial class Header : PdfPageEventHelper
        {
            public override void OnEndPage(PdfWriter writer, Document doc)
            {
                string imageurl = HttpContext.Current.Server.MapPath("~/PDF/logo.png");
                iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(imageurl);
                logo.ScaleAbsolute(130, 22);

                //Paragraph header = new Paragraph("LOGIC UNIVERSITY", FontFactory.GetFont(FontFactory.TIMES, 25, iTextSharp.text.Font.NORMAL));
                //header.Alignment = Element.ALIGN_LEFT;
                logo.Alignment = Element.ALIGN_CENTER;
                PdfPTable headerTbl = new PdfPTable(1);
                headerTbl.TotalWidth = 300;
                headerTbl.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell cell = new PdfPCell(logo);
                cell.Border = 0;
                cell.PaddingLeft = 10;

                headerTbl.AddCell(cell);
                headerTbl.WriteSelectedRows(0, -1, 230, 800, writer.DirectContent);
            }
        }

        //Header all pages(Landscape)
        public partial class Header_Landscape : PdfPageEventHelper
        {
            public override void OnEndPage(PdfWriter writer, Document doc)
            {
                string imageurl = HttpContext.Current.Server.MapPath("~/PDF/logo.png");
                iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(imageurl);
                logo.ScaleAbsolute(130, 22);

                //Paragraph header = new Paragraph("LOGIC UNIVERSITY", FontFactory.GetFont(FontFactory.TIMES, 25, iTextSharp.text.Font.NORMAL));
                //header.Alignment = Element.ALIGN_LEFT;
                //logo.Alignment = iTextSharp.text.Image.ALIGN_CENTER;
                logo.Alignment = Element.ALIGN_CENTER;
                PdfPTable headerTbl = new PdfPTable(1);
                headerTbl.TotalWidth = 300;
                headerTbl.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell cell = new PdfPCell(logo);
                cell.Border = 0;
                cell.PaddingLeft = 10;

                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;

                headerTbl.AddCell(cell);
                headerTbl.WriteSelectedRows(0, -1, 430, 800, writer.DirectContent);
            }
        }
    }
}
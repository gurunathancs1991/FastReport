using Microsoft.AspNetCore.Mvc;
using FastReport.Web;
using Newtonsoft.Json;
using FastReport.Export.PdfSimple;
using FastReport;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using FastReport.Format;
using FastReport.Table;
using FastReport.Utils;
using System.Collections.Generic;
using SPAWebReport.Models;



namespace SPAWebReport.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        
        [HttpGet("[action]")]
        public IActionResult ShowReport() {
           
            WebReport WebReport = new WebReport();            
            WebReport.Height = "1000";
            WebReport.Width = "1200";         
            WebReport.Report = GetSimpleListReport();
            WebReport.Report.Prepare();          
            ViewBag.WebReport = WebReport;
            return View();
        }

         
        [HttpGet("[action]")]
        public IActionResult Data() {           
            LoadData();
            return View();
        }
        
        
        public Report GetSimpleListReport()
        {
            Report report = new Report();

            // load nwind database
            string text = System.IO.File.ReadAllText("App_Data/dummy.json");
            System.Data.DataSet myDataSet= JsonConvert.DeserializeObject<System.Data.DataSet>(text);

            // register all data tables and relations
            report.RegisterData(myDataSet);

            // enable the "Employees" table to use it in the report
            report.GetDataSource("Employees").Enabled = true;

            // add report page
            ReportPage page = new ReportPage();
            page.Bounds = new RectangleF(0, 0, 1200, 1000);
            report.Pages.Add(page);
            // always give names to objects you create. You can use CreateUniqueName method to do this;
            // call it after the object is added to a report.
            page.CreateUniqueName();

            // create title band
            page.ReportTitle = new ReportTitleBand();
            // native FastReport unit is screen pixel, use conversion 
            page.ReportTitle.Height = Units.Centimeters * 1;
            page.ReportTitle.CreateUniqueName();

            // FIRST HEADER
            LineObject FirstHeadObj = new LineObject();
            FirstHeadObj.Parent = page.ReportTitle;
            FirstHeadObj.Bounds = new RectangleF(0, 5, 1200, 2);
            FirstHeadObj.Border.Lines = BorderLines.All;
            FirstHeadObj.Border.Width = Convert.ToSingle(1);
            FirstHeadObj.Border.Style = LineStyle.Solid;
            FirstHeadObj.Border.Color = Color.LightGray;

            TextObject titleText3 = new TextObject();
            titleText3.Parent = page.ReportTitle;
            titleText3.CreateUniqueName();
            titleText3.Bounds = new RectangleF(9, 6, 200 , 50);
            titleText3.Font = new Font("Arial", 11, FontStyle.Bold);
            titleText3.Text = "Payslip";
            titleText3.HorzAlign = HorzAlign.Left;
            titleText3.VertAlign = VertAlign.Center;
            titleText3.Style = "padding: 5px 0 0 0;";

            // Header

            LineObject HeaderObj = new LineObject();
            HeaderObj.Parent = page.ReportTitle;
            HeaderObj.Bounds = new RectangleF(0, 50, 1200, 2);
            HeaderObj.Border.Lines = BorderLines.All;
            HeaderObj.Border.Width = Convert.ToSingle(2);
            HeaderObj.Border.Style = LineStyle.Double;
            HeaderObj.Border.Color = Color.LightGray;

            //lOGO

            PictureObject pic =  new PictureObject();
            pic.Parent = page.ReportTitle;
            pic.Bounds = new RectangleF(9, 56, 150, 50); 
            pic.Image = new Bitmap("App_Data/logo.png");
            pic.Style = "margin: 5px 0 0 0";

            // create title text
            TextObject titleText = new TextObject();
            titleText.Parent = page.ReportTitle;
            titleText.CreateUniqueName();
            titleText.Bounds = new RectangleF(170, 56, 200 , 50);
            titleText.Font = new Font("Arial", 8, FontStyle.Bold);
            titleText.Text = "Allsec Technologies Limited";
            titleText.HorzAlign = HorzAlign.Left;
            titleText.VertAlign = VertAlign.Top;
            titleText.Style = "padding: 5px 0 0 0;";

            TextObject titleText1 = new TextObject();
            titleText1.Parent = page.ReportTitle;
            titleText1.CreateUniqueName();
            titleText1.Bounds = new RectangleF(170, 71, 200 , 50);
            titleText1.Font = new Font("Arial", 8);
            titleText1.Text = "48 Velachery Main Road, Velachery";
            titleText1.HorzAlign = HorzAlign.Left;
            titleText1.VertAlign = VertAlign.Top;
            titleText1.Style  =  "padding: 5px 0 0 0;";

            TextObject titleText2= new TextObject();
            titleText2.Parent = page.ReportTitle;
            titleText2.CreateUniqueName();
            titleText2.Bounds = new RectangleF(170, 86, 200 , 50);
            titleText2.Font = new Font("Arial", 8);
            titleText2.Text = "Chennai";
            titleText2.HorzAlign = HorzAlign.Left;
            titleText2.VertAlign = VertAlign.Top;
            titleText2.Style  =  "padding: 5px 0 0 0;";

            TextObject rightTitle1 = new TextObject();
            rightTitle1.Parent = page.ReportTitle;
            rightTitle1.CreateUniqueName();
            rightTitle1.Bounds = new RectangleF(590, 56, 150 , 50);
            rightTitle1.Font = new Font("Arial", 8);
            rightTitle1.Text = "Payslip For :";
            rightTitle1.HorzAlign = HorzAlign.Left;
            rightTitle1.VertAlign = VertAlign.Center;
            rightTitle1.Style  =  "padding: 5px 0 0 0;";

            TextObject rightTitle2= new TextObject();
            rightTitle2.Parent = page.ReportTitle;
            rightTitle2.CreateUniqueName();
            rightTitle2.Bounds = new RectangleF(660, 56, 150 , 50);
            rightTitle2.Font = new Font("Arial", 8, FontStyle.Bold);
            rightTitle2.Text = "March 2021";
            rightTitle2.HorzAlign = HorzAlign.Left;
            rightTitle2.VertAlign = VertAlign.Center;
            rightTitle2.Style  =  "padding: 5px 0 0 0;";

            // Logo

           
            
            LineObject HeaderBtmLine = new LineObject();
            HeaderBtmLine.Parent = page.ReportTitle;
            HeaderBtmLine.Bounds = new RectangleF(0, 100, 1200, 2);
            HeaderBtmLine.Border.Lines = BorderLines.All;
            HeaderBtmLine.Border.Width = Convert.ToSingle(1);
            HeaderBtmLine.Border.Style = LineStyle.Solid;
            HeaderBtmLine.Border.Color = Color.LightGray;

            //create data band
            DataBand dataBand = new DataBand();
            page.Bands.Add(dataBand);
            dataBand.CreateUniqueName();
            dataBand.DataSource = report.GetDataSource("Employees");
            dataBand.Height = Units.Centimeters * 0.5f;

            // Employee Table

            TableObject EmpTable = new TableObject();            
            EmpTable.Bounds =  new RectangleF(0, 80, 1200, 200);
            EmpTable.CanGrow = true;
            EmpTable.CanShrink = true;          
        
            
            EmpTable.RowCount = 8; 
            EmpTable.ColumnCount = 4;
            EmpTable.Name = "EmployeeDetails";
            EmpTable.Border.Lines = BorderLines.All;
            EmpTable.Border.Color = Color.LightGray;
            EmpTable.Border.Style = LineStyle.Solid;
            EmpTable.Columns[0].Width = Units.Centimeters * 4;
            EmpTable.Columns[1].Width = Units.Centimeters * 6;
            EmpTable.Columns[2].Width = Units.Centimeters * 4;
            EmpTable.Columns[3].Width = Units.Centimeters * 6;
            EmpTable.Rows[0].Height = Units.Centimeters * 3/4;
            EmpTable.Rows[1].Height = Units.Centimeters * 3/4;
            EmpTable.Rows[2].Height = Units.Centimeters * 3/4;
            EmpTable.Rows[3].Height = Units.Centimeters * 3/4;
            EmpTable.Rows[4].Height = Units.Centimeters * 3/4;
            EmpTable.Rows[5].Height = Units.Centimeters * 3/4;
            EmpTable.Rows[6].Height = Units.Centimeters * 3/4;
            EmpTable.Rows[07].Height = Units.Centimeters * 3/4;
            Border b = new Border();
                    b.Lines = BorderLines.All;
                    b.Color = Color.FromArgb(179, 179, 179);
                    b.Width = Units.Millimeters * 1/4;
                    b.Style = LineStyle.Solid;
                    b.BottomLine.Width = Units.Millimeters * 0;
            for(var i = 0; i < EmpTable.Columns.Count; i++){
                for(var j = 0; j < EmpTable.Rows.Count; j++){
                   
                    if(i % 2 == 0 ){
                          EmpTable[i,j].FillColor = Color.FromArgb(242, 242, 242);
                    } else if( i % 2 == 1 && j % 2 == 1){
                        EmpTable[i,j].FillColor = Color.FromArgb(246, 246, 246);
                    }
                    EmpTable[i,j].Border =  b; 
                    EmpTable[i,j].Font = new Font("Arial", 9, FontStyle.Regular);
                    EmpTable[i,j].HorzAlign = HorzAlign.Left;
                    EmpTable[i,j].VertAlign = VertAlign.Center;                  
                    
                }
            }
            PopulateEmployee(EmpTable);
            dataBand.Objects.Add(EmpTable);

            // Salary Table 

            TableObject SalTable = new TableObject();            
            SalTable.Bounds =  new RectangleF(0, 300, 1200, 200);
            SalTable.CanGrow = true;
            SalTable.CanShrink = true;          
        
            
            SalTable.RowCount = 9; 
            SalTable.ColumnCount = 5;
            SalTable.Name = "EmployeeDetails";
            SalTable.Border.Lines = BorderLines.All;
            SalTable.Border.Color = Color.LightGray;
            SalTable.Border.Style = LineStyle.Solid;
            SalTable.Columns[0].Width = Units.Centimeters * (4);
            SalTable.Columns[1].Width = Units.Centimeters * (3);
            SalTable.Columns[2].Width = Units.Centimeters * (3);
            SalTable.Columns[3].Width = Units.Centimeters * (5);
            SalTable.Columns[4].Width = Units.Centimeters * (5);
            SalTable.Rows[0].Height = Units.Centimeters * 3/4;
            SalTable.Rows[1].Height = Units.Centimeters * 3/4;
            SalTable.Rows[2].Height = Units.Centimeters * 3/4;
            SalTable.Rows[3].Height = Units.Centimeters * 3/4;
            SalTable.Rows[4].Height = Units.Centimeters * 3/4;
            SalTable.Rows[5].Height = Units.Centimeters * 3/4;
            SalTable.Rows[6].Height = Units.Centimeters * 3/4;
            SalTable.Rows[07].Height = Units.Centimeters * 3/4;
            SalTable.Rows[08].Height = Units.Centimeters * 3/4;
            for(var i = 0; i < SalTable.Columns.Count; i++){
                for(var j = 0; j < SalTable.Rows.Count; j++){
                   
                    if(j == 0 ){
                          SalTable[i,j].FillColor = Color.FromArgb(242, 242, 242);
                    } else if( j % 2 == 0 ){
                        SalTable[i,j].FillColor = Color.FromArgb(246, 246, 246);
                    } 
                    SalTable[i,j].Border =  b; 
                    SalTable[i,j].Font = new Font("Arial", 9, FontStyle.Regular);
                    SalTable[i,j].HorzAlign = HorzAlign.Left;
                    SalTable[i,j].VertAlign = VertAlign.Center;                
                    if((i == 1 || i == 2 || i ==4) && (j != 0)){
                         SalTable[i,j].HorzAlign = HorzAlign.Right;
                    }
                    if(j ==0 || j >=6 ){
                        SalTable[i,j].Font = new Font("Arial", 9, FontStyle.Bold);
                    }
                                    
                    
                }
            }
            SalTable[1,8].ColSpan = 4;
            SalTable[1,8].HorzAlign = HorzAlign.Left;
            PopulateSalary(SalTable);
            dataBand.Objects.Add(SalTable);
            

            

            return report;
        }      

        public void PopulateEmployee(TableObject tablObj){
            // First Row
            tablObj[0,0].Text = "  Employee Code";
            tablObj[1,0].Text = "  [Employees.Id]";
            tablObj[2,0].Text = "  Employee Name";
            tablObj[3,0].Text = "  [Employees.Name]";

             // Second Row
            tablObj[0,1].Text = "  Location";
            tablObj[1,1].Text = "  [Employees.Location]";
            tablObj[2,1].Text = "  Date of Joining";
            tablObj[3,1].Text = "  [Employees.Doj]";

             // Third Row
            tablObj[0,2].Text = "  PAN";
            tablObj[1,2].Text = "  [Employees.Pan]";
            tablObj[2,2].Text = "  Designation";
            tablObj[3,2].Text = "  [Employees.Designation]";

             // Fourth Row
            tablObj[0,3].Text = "  PF No.";
            tablObj[1,3].Text = "  [Employees.Pf]";
            tablObj[2,3].Text = "  ESI No.";
            tablObj[3,3].Text = "  [Employees.Esi]";

             // Fifth Row
            tablObj[0,4].Text = "  Bank Name";
            tablObj[1,4].Text = "  [Employees.BankName]";
            tablObj[2,4].Text = "  Account No.";
            tablObj[3,4].Text = "  [Employees.AccountNo]";

            // Sixth Row
            tablObj[0,5].Text = "  Work Days";
            tablObj[1,5].Text = "  [Employees.WorkDays]";
            tablObj[2,5].Text = "  Standard Days";
            tablObj[3,5].Text = "  [Employees.StandardDays]";

             // Seventh Row
            tablObj[0,6].Text = "  LOP Days";
            tablObj[1,6].Text = "  [Employees.LopDays]";
            tablObj[2,6].Text = "  PF UAN";
            tablObj[3,6].Text = "  [Employees.Uan]";
            //  // Eighth Row
            tablObj[0,7].Text = "  Previous LOP";
            tablObj[1,7].Text = "  ";
            tablObj[2,7].Text = "  Previous LOP Reversal";
            tablObj[3,7].Text = "  ";
        }

        public void PopulateSalary(TableObject salObj){
            //First Row
            salObj[0,0].Text = "  Earnings";
            salObj[1,0].Text = "  Actual Salary";
            salObj[2,0].Text = "  Earned Salary";
            salObj[3,0].Text = "  Deductions";  
            salObj[4,0].Text = "  Amount";

            //Second Row
            salObj[0,1].Text = "  Basic";
            salObj[1,1].Text = "  [Employees.BasicActual]  ";
            salObj[2,1].Text = "  [Employees.BasicEarned]  ";
            salObj[3,1].Text = "  Provident Fund";  
            salObj[4,1].Text = "  200  ";

            // Third Row          
            salObj[0,2].Text = "  HRA";
            salObj[1,2].Text = "  [Employees.HraActual]  ";
            salObj[2,2].Text = "  [Employees.HraEarned]  ";
            salObj[3,2].Text = "  Income Tax";  
            salObj[4,2].Text = "  [Employees.IncomeTax]  ";

             // Fourth Row          
            salObj[0,3].Text = "  Special Allowance";
            salObj[1,3].Text = "  [Employees.HraActual]  ";
            salObj[2,3].Text = "  [Employees.HraEarned]  ";
            salObj[3,3].Text = "  ";  
            salObj[4,3].Text = "  ";

             // Fifth Row          
            salObj[0,4].Text = "  Annual Allowance";
            salObj[1,4].Text = "  [Employees.HraActual]  ";
            salObj[2,4].Text = "  [Employees.HraEarned]  ";
            salObj[3,4].Text = "  ";  
            salObj[4,4].Text = "  ";

               // Sixth Row          
            salObj[0,5].Text = "  Transport Allowance";
            salObj[1,5].Text = "  [Employees.HraActual]  ";
            salObj[2,5].Text = "  [Employees.HraEarned]  ";
            salObj[3,5].Text = "  ";  
            salObj[4,5].Text = "  ";

             // Seventh Row          
            salObj[0,6].Text = "  Total Earnings";
            salObj[1,6].Text = "  ";
            salObj[2,6].Text = "  [Employees.TotEarned]  ";
            salObj[3,6].Text = "  Total Deductions";  
            salObj[4,6].Text = "  [Employees.TotDec]  ";

              // Eighth Row          
            salObj[0,7].Text = "  ";
            salObj[1,7].Text = "  ";
            salObj[2,7].Text = "  ";
            salObj[3,7].Text = "  Net Salary";  
            salObj[4,7].Text = "  [Employees.NetSalary]  ";

            // Nineth Row          
            salObj[0,8].Text = "  Amount in Words";
            salObj[1,8].Text = "  [Employees.Words]";          


        }


        [HttpGet("[action]")]
        public FileResult PdfExport(){

            Report data = GetSimpleListReport();
            data.Prepare();           
            
            PDFSimpleExport pdfExport = new PDFSimpleExport();            
            try{
                System.IO.File.Delete("Payslip.pdf");
            }
            catch{

            }
            pdfExport.Export(data, "Payslip.pdf");
            return Download();           
        } 

        public FileResult Download()
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes("Payslip.pdf");
            string fileName = "Payslip.pdf";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        public void LoadData(){
            DataTable ETable = new DataTable();
            ETable.Columns.Add("Key", Type.GetType("System.String"));
            ETable.Columns.Add("Value", Type.GetType("System.String"));
            string text = System.IO.File.ReadAllText("App_Data/wages.json");
            List<WageModel> DataList = JsonConvert.DeserializeObject<List<WageModel>>(text);
            if(DataList.Count > 0 ){
                ETable.Rows.Add(new object[]{ "EmployeeName", DataList[0].EmployeeName});
                ETable.Rows.Add(new object[]{ "EmployeeCode", DataList[0].EmployeeCode});
                List<Dictionary<string, string>> ListComponent = new List<Dictionary<string, string>>();
                foreach(WageModel wage in DataList){
                  Dictionary<string, string> comp = WageProcess.GetWageComponent(wage);
                   foreach(var item in comp){
                        ETable.Rows.Add(new object[]{ item.Key, item.Value });
                   }                
                }
               
            }
            
        }
    }
}

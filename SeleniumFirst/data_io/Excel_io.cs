// TO DO : 
//  -Fix Save method to allow preset file name
//

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace SeleniumFirst
{
    public class Excel_io
    {

        public void Write_Excel(Person employee, Person admin)
        {
            Excel.Application excelApp = new Excel.Application();
            if (excelApp != null)
            {
                Excel.Workbook excelWorkbook = excelApp.Workbooks.Open(Directory.GetCurrentDirectory() + @"\resources\template.xlsx");
                Excel.Worksheet excelWorksheet = excelWorkbook.Worksheets["Sheet1"];



                #region Fill Cells

                excelWorksheet.Cells[2, 1] = employee.fullname + " | " + employee.store + " | " + employee.role;

                //Email
                excelWorksheet.Cells[4, 3] = employee.email.username;
                excelWorksheet.Cells[4, 4] = employee.email.password;
                excelWorksheet.Cells[4, 6] = employee.email.addDate;
                excelWorksheet.Cells[4, 7] = employee.email.addBy;
                excelWorksheet.Cells[4, 8] = employee.email.removeDate;
                excelWorksheet.Cells[4, 9] = employee.email.removeBy;
                //DealerTrack
                excelWorksheet.Cells[5, 3] = employee.dealertrack.username;
                excelWorksheet.Cells[5, 4] = employee.dealertrack.password;
                excelWorksheet.Cells[5, 6] = employee.dealertrack.addDate;
                excelWorksheet.Cells[5, 7] = employee.dealertrack.addBy;
                excelWorksheet.Cells[5, 8] = employee.dealertrack.removeDate;
                excelWorksheet.Cells[5, 9] = employee.dealertrack.removeBy;
                //Reynolds
                excelWorksheet.Cells[7, 3] = employee.reynolds.username;
                excelWorksheet.Cells[7, 4] = employee.reynolds.password;
                excelWorksheet.Cells[7, 6] = employee.reynolds.addDate;
                excelWorksheet.Cells[7, 7] = employee.reynolds.addBy;
                excelWorksheet.Cells[7, 8] = employee.reynolds.removeDate;
                excelWorksheet.Cells[7, 9] = employee.reynolds.removeBy;
                //Talones
                excelWorksheet.Cells[8, 3] = employee.talones.username;
                excelWorksheet.Cells[8, 4] = employee.talones.password;
                excelWorksheet.Cells[8, 6] = employee.talones.addDate;
                excelWorksheet.Cells[8, 7] = employee.talones.addBy;
                excelWorksheet.Cells[8, 8] = employee.talones.removeDate;
                excelWorksheet.Cells[8, 9] = employee.talones.removeBy;
                //NNANet
                excelWorksheet.Cells[10, 4] = employee.nnanet.username;
                excelWorksheet.Cells[10, 5] = employee.nnanet.password;
                excelWorksheet.Cells[10, 6] = employee.nnanet.addDate;
                excelWorksheet.Cells[10, 7] = employee.nnanet.addBy;
                excelWorksheet.Cells[10, 8] = employee.nnanet.removeDate;
                excelWorksheet.Cells[10, 9] = employee.nnanet.removeBy;
                //Dealerconnect
                excelWorksheet.Cells[11, 4] = employee.dealerconnect.username;
                excelWorksheet.Cells[11, 5] = employee.dealerconnect.password;
                excelWorksheet.Cells[11, 6] = employee.dealerconnect.addDate;
                excelWorksheet.Cells[11, 7] = employee.dealerconnect.addBy;
                excelWorksheet.Cells[11, 8] = employee.dealerconnect.removeDate;
                excelWorksheet.Cells[11, 9] = employee.dealerconnect.removeBy;
                //GMGlobal
                excelWorksheet.Cells[12, 4] = employee.gmglobal.username;
                excelWorksheet.Cells[12, 5] = employee.gmglobal.password;
                excelWorksheet.Cells[12, 6] = employee.gmglobal.addDate;
                excelWorksheet.Cells[12, 7] = employee.gmglobal.addBy;
                excelWorksheet.Cells[12, 8] = employee.gmglobal.removeDate;
                excelWorksheet.Cells[12, 9] = employee.gmglobal.removeBy;
                //HyundaiDealer
                excelWorksheet.Cells[13, 4] = employee.hyundaidealer.username;
                excelWorksheet.Cells[13, 5] = employee.hyundaidealer.password;
                excelWorksheet.Cells[13, 6] = employee.hyundaidealer.addDate;
                excelWorksheet.Cells[13, 7] = employee.hyundaidealer.addBy;
                excelWorksheet.Cells[13, 8] = employee.hyundaidealer.removeDate;
                excelWorksheet.Cells[13, 9] = employee.hyundaidealer.removeBy;
                //KDealer
                excelWorksheet.Cells[14, 4] = employee.kdealer.username;
                excelWorksheet.Cells[14, 5] = employee.kdealer.password;
                excelWorksheet.Cells[14, 6] = employee.kdealer.addDate;
                excelWorksheet.Cells[14, 7] = employee.kdealer.addBy;
                excelWorksheet.Cells[14, 8] = employee.kdealer.removeDate;
                excelWorksheet.Cells[14, 9] = employee.kdealer.removeBy;
                //NNANet (Infiniti)
                excelWorksheet.Cells[15, 4] = employee.nnanet.username;
                excelWorksheet.Cells[15, 5] = employee.nnanet.password;
                excelWorksheet.Cells[15, 6] = employee.nnanet.addDate;
                excelWorksheet.Cells[15, 7] = employee.nnanet.addBy;
                excelWorksheet.Cells[15, 8] = employee.nnanet.removeDate;
                excelWorksheet.Cells[15, 9] = employee.nnanet.removeBy;

                //H-DNet
                //RENO
                excelWorksheet.Cells[16, 4] = employee.hdnet.username;
                excelWorksheet.Cells[16, 5] = employee.hdnet.password;
                excelWorksheet.Cells[16, 6] = employee.hdnet.addDate;
                excelWorksheet.Cells[16, 7] = employee.hdnet.addBy;
                excelWorksheet.Cells[16, 8] = employee.hdnet.removeDate;
                excelWorksheet.Cells[16, 9] = employee.hdnet.removeBy;
                //YUBA
                excelWorksheet.Cells[17, 4] = employee.hdnet1.username;
                excelWorksheet.Cells[17, 5] = employee.hdnet1.password;
                excelWorksheet.Cells[17, 6] = employee.hdnet1.addDate;
                excelWorksheet.Cells[17, 7] = employee.hdnet1.addBy;
                excelWorksheet.Cells[17, 8] = employee.hdnet1.removeDate;
                excelWorksheet.Cells[17, 9] = employee.hdnet1.removeBy;
                //REDWOOD
                excelWorksheet.Cells[18, 4] = employee.hdnet2.username;
                excelWorksheet.Cells[18, 5] = employee.hdnet2.password;
                excelWorksheet.Cells[18, 6] = employee.hdnet2.addDate;
                excelWorksheet.Cells[18, 7] = employee.hdnet2.addBy;
                excelWorksheet.Cells[18, 8] = employee.hdnet2.removeDate;
                excelWorksheet.Cells[18, 9] = employee.hdnet2.removeBy;
                excelWorksheet.Cells[18, 4] = employee.hdnet2.username;
                //DEATH VALLEY
                excelWorksheet.Cells[19, 4] = employee.hdnet3.username;
                excelWorksheet.Cells[19, 5] = employee.hdnet3.password;
                excelWorksheet.Cells[19, 6] = employee.hdnet3.addDate;
                excelWorksheet.Cells[19, 7] = employee.hdnet3.addBy;
                excelWorksheet.Cells[19, 8] = employee.hdnet3.removeDate;
                excelWorksheet.Cells[19, 9] = employee.hdnet3.removeBy;
                // CORONADO BEACH
                excelWorksheet.Cells[20, 4] = employee.hdnet4.username;
                excelWorksheet.Cells[20, 5] = employee.hdnet4.password;
                excelWorksheet.Cells[20, 6] = employee.hdnet4.addDate;
                excelWorksheet.Cells[20, 7] = employee.hdnet4.addBy;
                excelWorksheet.Cells[20, 8] = employee.hdnet4.removeDate;
                excelWorksheet.Cells[20, 9] = employee.hdnet4.removeBy;
                // ORANGE COUNTY
                excelWorksheet.Cells[21, 4] = employee.hdnet5.username;
                excelWorksheet.Cells[21, 5] = employee.hdnet5.password;
                excelWorksheet.Cells[21, 6] = employee.hdnet5.addDate;
                excelWorksheet.Cells[21, 7] = employee.hdnet5.addBy;
                excelWorksheet.Cells[21, 8] = employee.hdnet5.removeDate;
                excelWorksheet.Cells[21, 9] = employee.hdnet5.removeBy;


                //VCC
                excelWorksheet.Cells[22, 4] = employee.vcc.username;
                excelWorksheet.Cells[22, 5] = employee.vcc.password;
                excelWorksheet.Cells[22, 6] = employee.vcc.addDate;
                excelWorksheet.Cells[22, 7] = employee.vcc.addBy;
                excelWorksheet.Cells[22, 8] = employee.vcc.removeDate;
                excelWorksheet.Cells[22, 9] = employee.vcc.removeBy;
                //MxConnect
                excelWorksheet.Cells[23, 4] = employee.mxconnect.username;
                excelWorksheet.Cells[23, 5] = employee.mxconnect.password;
                excelWorksheet.Cells[23, 6] = employee.mxconnect.addDate;
                excelWorksheet.Cells[23, 7] = employee.mxconnect.addBy;
                excelWorksheet.Cells[23, 8] = employee.mxconnect.removeDate;
                excelWorksheet.Cells[23, 9] = employee.mxconnect.removeBy;
                //CUDL
                excelWorksheet.Cells[24, 4] = employee.cudl.username;
                excelWorksheet.Cells[24, 5] = employee.cudl.password;
                excelWorksheet.Cells[24, 6] = employee.cudl.addDate;
                excelWorksheet.Cells[24, 7] = employee.cudl.addBy;
                excelWorksheet.Cells[24, 8] = employee.cudl.removeDate;
                excelWorksheet.Cells[24, 9] = employee.cudl.removeBy;
                //Office365
                excelWorksheet.Cells[25, 4] = employee.office365.username;
                excelWorksheet.Cells[25, 5] = employee.office365.password;
                excelWorksheet.Cells[25, 6] = employee.office365.addDate;
                excelWorksheet.Cells[25, 7] = employee.office365.addBy;
                excelWorksheet.Cells[25, 8] = employee.office365.removeDate;
                excelWorksheet.Cells[25, 9] = employee.office365.removeBy;

                // Chris's Dept.
                // F&I Express
                excelWorksheet.Cells[29, 3] = employee.fiexpress.username;
                excelWorksheet.Cells[29, 4] = employee.fiexpress.password;
                excelWorksheet.Cells[29, 6] = employee.fiexpress.addDate;
                excelWorksheet.Cells[29, 7] = employee.fiexpress.addBy;
                excelWorksheet.Cells[29, 8] = employee.fiexpress.removeDate;
                excelWorksheet.Cells[29, 9] = employee.fiexpress.removeBy;

                //DMV Desk
                excelWorksheet.Cells[30, 3] = employee.dmvdesk.username;
                excelWorksheet.Cells[30, 4] = employee.dmvdesk.password;
                excelWorksheet.Cells[30, 6] = employee.dmvdesk.addDate;
                excelWorksheet.Cells[30, 7] = employee.dmvdesk.addBy;
                excelWorksheet.Cells[30, 8] = employee.dmvdesk.removeDate;
                excelWorksheet.Cells[30, 9] = employee.dmvdesk.removeBy;

                //Vin Solutions
                excelWorksheet.Cells[31, 3] = employee.vinsolutions.username;
                excelWorksheet.Cells[31, 4] = employee.vinsolutions.password;
                excelWorksheet.Cells[31, 6] = employee.vinsolutions.addDate;
                excelWorksheet.Cells[31, 7] = employee.vinsolutions.addBy;
                excelWorksheet.Cells[31, 8] = employee.vinsolutions.removeDate;
                excelWorksheet.Cells[31, 9] = employee.vinsolutions.removeBy;

                //Car Wars
                excelWorksheet.Cells[32, 3] = employee.carwars.username;
                excelWorksheet.Cells[32, 4] = employee.carwars.password;
                excelWorksheet.Cells[32, 6] = employee.carwars.addDate;
                excelWorksheet.Cells[32, 7] = employee.carwars.addBy;
                excelWorksheet.Cells[32, 8] = employee.carwars.removeDate;
                excelWorksheet.Cells[32, 9] = employee.carwars.removeBy;

                //Used Car Dept.
                //Rapid Recon
                excelWorksheet.Cells[34, 3] = employee.rapidrecon.username;
                excelWorksheet.Cells[34, 4] = employee.rapidrecon.password;
                excelWorksheet.Cells[34, 6] = employee.rapidrecon.addDate;
                excelWorksheet.Cells[34, 7] = employee.rapidrecon.addBy;
                excelWorksheet.Cells[34, 8] = employee.rapidrecon.removeDate;
                excelWorksheet.Cells[34, 9] = employee.rapidrecon.removeBy;

                // vAuto
                excelWorksheet.Cells[35, 3] = employee.vauto.username;
                excelWorksheet.Cells[35, 4] = employee.vauto.password;
                excelWorksheet.Cells[35, 6] = employee.vauto.addDate;
                excelWorksheet.Cells[35, 7] = employee.vauto.addBy;
                excelWorksheet.Cells[35, 8] = employee.vauto.removeDate;
                excelWorksheet.Cells[35, 9] = employee.vauto.removeBy;
#endregion

                //excelApp.ActiveWorkbook.SaveAs(@"C:\template1.xlsx");
                System.Windows.Forms.SaveFileDialog saveDialog = new System.Windows.Forms.SaveFileDialog();
                saveDialog.Filter = "xlsx files (*.xlsx)|*.xlsx";
                saveDialog.FilterIndex = 1;
                saveDialog.RestoreDirectory = true;
                saveDialog.FileName = employee.fullname + "  " + employee.store + "  " + employee.role + ".xlsx";
                if (saveDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {  
                    //System.Windows.Forms.MessageBox.Show(saveDialog.FileName);
                    excelWorkbook.SaveAs(saveDialog.FileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault);
                }
                //excelApp.ActiveWorkbook.Save();
                //excelWorkbook.Close(true);
                excelWorkbook.Close(false);
                excelApp.Quit();

                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(excelWorksheet);
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(excelWorkbook);
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(excelApp);
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

    }
}

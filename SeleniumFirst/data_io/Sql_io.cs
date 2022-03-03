using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeleniumFirst
{
    public class Sql_io
    {
        // SQL - Wise Auto Group IT Amazon RDS Database
        private string connectionString = @"***REMOVED***";
        // old db "@"***REMOVED***"";

        public bool Write_DB(Person employee, Person admin)
        {
            SqlConnection sqlconnect = new SqlConnection(connectionString);
            sqlconnect.Open();

            SqlCommand sqlcommand = new SqlCommand(
                @"INSERT INTO Employee (
                   [FullName]
                  ,[PhoneNumber]
                  ,[Store]
                  ,[Role]
                  ,[Status]
                  ,[Notes]

                  ,[Email]
                  ,[Email.password]
                  ,[Email.addDate]
                  ,[Email.removeDate]
                  ,[Email.addBy]
                  ,[Email.removeBy]

                  ,[DealerTrack]
                  ,[DealerTrack.password]
                  ,[DealerTrack.addDate]
                  ,[DealerTrack.removeDate]
                  ,[DealerTrack.addBy]
                  ,[DealerTrack.removeBy]

                  ,[Reynolds]
                  ,[Reynolds.password]
                  ,[Reynolds.addDate]
                  ,[Reynolds.removeDate]
                  ,[Reynolds.addBy]
                  ,[Reynolds.removeBy]

                  ,[TALONes]
                  ,[TALONes.password]
                  ,[TALONes.addDate]
                  ,[TALONes.removeDate]
                  ,[TALONes.addBy]
                  ,[TALONes.removeBy]

                  ,[NNANet]
                  ,[NNANet.password]
                  ,[NNANet.addDate]
                  ,[NNANet.removeDate]
                  ,[NNANet.addBy]
                  ,[NNANet.removeBy]

                  ,[DealerConnect]
                  ,[DealerConnect.password]
                  ,[DealerConnect.addDate]
                  ,[DealerConnect.removeDate]
                  ,[DealerConnect.addBy]
                  ,[DealerConnect.removeBy]

                  ,[GMGlobal]
                  ,[GMGlobal.password]
                  ,[GMGlobal.addDate]
                  ,[GMGlobal.removeDate]
                  ,[GMGlobal.addBy]
                  ,[GMGlobal.removeBy]

                  ,[HyundaiDealer]
                  ,[HyundaiDealer.password]
                  ,[HyundaiDealer.addDate]
                  ,[HyundaiDealer.removeDate]
                  ,[HyundaiDealer.addBy]
                  ,[HyundaiDealer.removeBy]

                  ,[KDealer]
                  ,[KDealer.password]
                  ,[KDealer.addDate]
                  ,[KDealer.removeDate]
                  ,[KDealer.addBy]
                  ,[KDealer.removeBy]

                      ,[HDNetReno]
                      ,[HDNetRenoPassword]
                      ,[HDNetRenoAddDate]
                      ,[HDNetRenoRemoveDate]
                      ,[HDNetRenoAddBy]
                      ,[HDNetRenoRemoveBy]

                      ,[HDNetYuba] 
                      ,[HDNetYubaPassword]
                      ,[HDNetYubaAddDate]
                      ,[HDNetYubaRemoveDate]
                      ,[HDNetYubaAddBy]
                      ,[HDNetYubaRemoveBy]

                      ,[HDNetRedwood]
                      ,[HDNetRedwoodPassword]
                      ,[HDNetRedwoodAddDate]
                      ,[HDNetRedwoodRemoveDate]
                      ,[HDNetRedwoodAddBy]
                      ,[HDNetRedwoodRemoveBy]

                      ,[HDNetDeathValley]
                      ,[HDNetDeathValleyPassword]
                      ,[HDNetDeathValleyAddDate]
                      ,[HDNetDeathValleyRemoveDate]
                      ,[HDNetDeathValleyAddBy]
                      ,[HDNetDeathValleyRemoveBy]

                      ,[HDNetCoronadoBeach]
                      ,[HDNetCoronadoBeachPassword]
                      ,[HDNetCoronadoBeachAddDate]
                      ,[HDNetCoronadoBeachRemoveDate]
                      ,[HDNetCoronadoBeachAddBy]
                      ,[HDNetCoronadoBeachRemoveBy]

                      ,[HDNetOrangeCounty]
                      ,[HDNetOrangeCountyPassword]
                      ,[HDNetOrangeCountyAddDate]
                      ,[HDNetOrangeCountyRemoveDate]
                      ,[HDNetOrangeCountyAddBy]
                      ,[HDNetOrangeCountyRemoveBy]

                  ,[VCC]
                  ,[VCC.password]
                  ,[VCC.addDate]
                  ,[VCC.removeDate]
                  ,[VCC.addBy]
                  ,[VCC.removeBy]

                  ,[MXConnect]
                  ,[MXConnect.password]
                  ,[MXConnect.addDate]
                  ,[MXConnect.removeDate]
                  ,[MXConnect.addBy]
                  ,[MXConnect.removeBy]

                  ,[CUDL]
                  ,[CUDL.password]
                  ,[CUDL.addDate]
                  ,[CUDL.removeDate]
                  ,[CUDL.addBy]
                  ,[CUDL.removeBy]

                    ,[Office365]
                    ,[Office365.password] 
                    ,[Office365.addDate]
                    ,[Office365.removeDate] 
                    ,[Office365.addBy] 
                    ,[Office365.removeBy] 

                  
                ,[FIExpress]
                ,[FIExpressPassword]
                ,[FIExpressAddDate]
                ,[FIExpressRemoveDate]
                ,[FIExpressAddBy]
                ,[FIExpressRemoveBy]

                ,[DMVDesk]
                ,[DMVDeskPassword]
                ,[DMVDeskAddDate]
                ,[DMVDeskRemoveDate]
                ,[DMVDeskAddBy]
                ,[DMVDeskRemoveBy]

                ,[VinSolutions]
                ,[VinSolutionsPassword]
                ,[VinSolutionsAddDate]
                ,[VinSolutionsRemoveDate]
                ,[VinSolutionsAddBy]
                ,[VinSolutionsRemoveBy]

                ,[CarWars]
                ,[CarWarsPassword]
                ,[CarWarsAddDate]
                ,[CarWarsRemoveDate]
                ,[CarWarsAddBy]
                ,[CarWarsRemoveBy]
                
                ,[RapidRecon]
                ,[RapidReconPassword]
                ,[RapidReconAddDate]
                ,[RapidReconRemoveDate]
                ,[RapidReconAddBy]
                ,[RapidReconRemoveBy]

                ,[vAuto]
                ,[vAutoPassword]
                ,[vAutoAddDate]
                ,[vAutoRemoveDate]
                ,[vAutoAddBy]
                ,[vAutoRemoveBy]

                  ,[DateSetup]
                  ,[DateRemove]
                  ,[DateModify]
                  ,[EmployeeNumber]
                  ,[Department])
                VALUES 
                    (@FullName, @PhoneNumber, @Store, @Role, @Status, @Notes, 
                    @Email, @Email_password, @Email_addDate, @Email_removeDate, @Email_addBy, @Email_removeBy,
                    @DealerTrack, @DealerTrack_password, @DealerTrack_addDate, @DealerTrack_removeDate, @DealerTrack_addBy, @DealerTrack_removeBy,
                    @Reynolds, @Reynolds_password, @Reynolds_addDate, @Reynolds_removeDate, @Reynolds_addBy, @Reynolds_removeBy,
                    @TALONes, @TALONes_password, @TALONes_addDate, @TALONes_removeDate, @TALONes_addBy, @TALONes_removeBy, 
                    @NNANet, @NNANet_password, @NNANet_addDate, @NNANet_removeDate, @NNANet_addBy, @NNANet_removeBy,
                    @DealerConnect, @DealerConnect_password, @DealerConnect_addDate, @DealerConnect_removeDate, @DealerConnect_addBy, @DealerConnect_removeBy,
                    @GMGlobal, @GMGlobal_password, @GMGlobal_addDate, @GMGlobal_removeDate, @GMGlobal_addBy, @GMGlobal_removeBy,
                    @HyundaiDealer, @HyundaiDealer_password, @HyundaiDealer_addDate, @HyundaiDealer_removeDate, @HyundaiDealer_addBy, @HyundaiDealer_removeBy,
                    @KDealer, @KDealer_password, @KDealer_addDate, @KDealer_removeDate, @KDealer_addBy, @KDealer_removeBy,
                        @HDNetReno, @HDNetRenoPassword, @HDNetRenoAddDate, @HDNetRenoRemoveDate, @HDNetRenoAddBy, @HDNetRenoRemoveBy,
                        @HDNetYuba, @HDNetYubaPassword, @HDNetYubaAddDate, @HDNetYubaRemoveDate, @HDNetYubaAddBy, @HDNetYubaRemoveBy,
                        @HDNetRedwood, @HDNetRedwoodPassword, @HDNetRedwoodAddDate, @HDNetRedwoodRemoveDate, @HDNetRedwoodAddBy, @HDNetRedwoodRemoveBy,
                        @HDNetDeathValley, @HDNetDeathValleyPassword, @HDNetDeathValleyAddDate, @HDNetDeathValleyRemoveDate, @HDNetDeathValleyAddBy, @HDNetDeathValleyRemoveBy,
                        @HDNetCoronadoBeach, @HDNetCoronadoBeachPassword, @HDNetCoronadoBeachAddDate, @HDNetCoronadoBeachRemoveDate, @HDNetCoronadoBeachAddBy, @HDNetCoronadoBeachRemoveBy,
                        @HDNetOrangeCounty, @HDNetOrangeCountyPassword, @HDNetOrangeCountyAddDate, @HDNetOrangeCountyRemoveDate, @HDNetOrangeCountyAddBy, @HDNetOrangeCountyRemoveBy,
                    @VCC, @VCC_password, @VCC_addDate, @VCC_removeDate, @VCC_addBy, @VCC_removeBy,
                    @MxConnect, @MxConnect_password, @MxConnect_addDate, @MxConnect_removeDate, @MxConnect_addBy, @MxConnect_removeBy,
                    @CUDL, @CUDL_password, @CUDL_addDate, @CUDL_removeDate, @CUDL_addBy, @CUDL_removeBy,
                    @Office365, @Office365_password, @Office365_addDate, @Office365_removeDate, @Office365_addBy, @Office365_removeBy,
                        @FIExpress, @FIExpressPassword, @FIExpressAddDate, @FIExpressRemoveDate, @FIExpressAddBy, @FIExpressRemoveBy,
                        @DMVDesk, @DMVDeskPassword, @DMVDeskAddDate, @DMVDeskRemoveDate, @DMVDeskAddBy, @DMVDeskRemoveBy,
                        @VinSolutions, @VinSolutionsPassword, @VinSolutionsAddDate, @VinSolutionsRemoveDate, @VinSolutionsAddBy, @VinSolutionsRemoveBy,
                        @CarWars, @CarWarsPassword, @CarWarsAddDate, @CarWarsRemoveDate, @CarWarsAddBy, @CarWarsRemoveBy,
                        @RapidRecon, @RapidReconPassword, @RapidReconAddDate, @RapidReconRemoveDate, @RapidReconAddBy, @RapidReconRemoveBy,
                        @vAuto, @vAutoPassword, @vAutoAddDate, @vAutoRemoveDate, @vAutoAddBy, @vAutoRemoveBy,
                    @DateSetup, @DateRemove, @DateModify, @EmployeeNumber, @Department)", sqlconnect);



            sqlcommand.Parameters.AddWithValue("@FullName", employee.fullname);
            sqlcommand.Parameters.AddWithValue("@PhoneNumber", "");
            sqlcommand.Parameters.AddWithValue("@Store", employee.store);
            sqlcommand.Parameters.AddWithValue("@Role", employee.role);
            sqlcommand.Parameters.AddWithValue("@Status", employee.status.ToLower());
            sqlcommand.Parameters.AddWithValue("@Notes", employee.notes);
            //Email
            sqlcommand.Parameters.AddWithValue("@Email", employee.email.username);
            sqlcommand.Parameters.AddWithValue("@Email_password", employee.email.password);
            sqlcommand.Parameters.AddWithValue("@Email_addDate", employee.email.addDate);
            sqlcommand.Parameters.AddWithValue("@Email_removeDate", DBNull.Value);
            sqlcommand.Parameters.AddWithValue("@Email_addBy", employee.email.addBy);
            sqlcommand.Parameters.AddWithValue("@Email_removeBy", "");
            //DealerTrack
            sqlcommand.Parameters.AddWithValue("@DealerTrack", employee.dealertrack.username);
            sqlcommand.Parameters.AddWithValue("@DealerTrack_Password", employee.dealertrack.password);
            sqlcommand.Parameters.AddWithValue("@DealerTrack_addDate", employee.dealertrack.addDate);
            sqlcommand.Parameters.AddWithValue("@DealerTrack_removeDate", DBNull.Value);
            sqlcommand.Parameters.AddWithValue("@DealerTrack_addBy", employee.dealertrack.addBy);
            sqlcommand.Parameters.AddWithValue("@DealerTrack_removeBy", "");
            //Reynolds
            sqlcommand.Parameters.AddWithValue("@Reynolds", employee.reynolds.username);
            sqlcommand.Parameters.AddWithValue("@Reynolds_Password", employee.reynolds.password);
            sqlcommand.Parameters.AddWithValue("@Reynolds_addDate", employee.reynolds.addDate);
            sqlcommand.Parameters.AddWithValue("@Reynolds_removeDate", DBNull.Value);
            sqlcommand.Parameters.AddWithValue("@Reynolds_addBy", employee.reynolds.addBy);
            sqlcommand.Parameters.AddWithValue("@Reynolds_removeBy", "");
            //TALONes
            sqlcommand.Parameters.AddWithValue("@TALONes", employee.talones.username);
            sqlcommand.Parameters.AddWithValue("@TALONes_Password", employee.talones.password);
            sqlcommand.Parameters.AddWithValue("@TALONes_addDate", employee.talones.addDate);
            sqlcommand.Parameters.AddWithValue("@TALONes_removeDate", DBNull.Value);
            sqlcommand.Parameters.AddWithValue("@TALONes_addBy", employee.talones.addBy);
            sqlcommand.Parameters.AddWithValue("@TALONes_removeBy", "");
            //NNANet
            sqlcommand.Parameters.AddWithValue("@NNANet", employee.nnanet.username);
            sqlcommand.Parameters.AddWithValue("@NNANet_Password", employee.nnanet.password);
            sqlcommand.Parameters.AddWithValue("@NNANet_addDate", employee.nnanet.addDate);
            sqlcommand.Parameters.AddWithValue("@NNANet_removeDate", DBNull.Value);
            sqlcommand.Parameters.AddWithValue("@NNANet_addBy", employee.nnanet.addBy);
            sqlcommand.Parameters.AddWithValue("@NNANet_removeBy", "");
            //Dealerconnect
            sqlcommand.Parameters.AddWithValue("@DealerConnect", employee.dealerconnect.username);
            sqlcommand.Parameters.AddWithValue("@DealerConnect_Password", employee.dealerconnect.password);
            sqlcommand.Parameters.AddWithValue("@DealerConnect_addDate", employee.dealerconnect.addDate);
            sqlcommand.Parameters.AddWithValue("@DealerConnect_removeDate", DBNull.Value);
            sqlcommand.Parameters.AddWithValue("@DealerConnect_addBy", employee.dealerconnect.addBy);
            sqlcommand.Parameters.AddWithValue("@DealerConnect_removeBy", "");
            //GMGlobal
            sqlcommand.Parameters.AddWithValue("@GMGlobal", employee.gmglobal.username);
            sqlcommand.Parameters.AddWithValue("@GMGlobal_Password", employee.gmglobal.password);
            sqlcommand.Parameters.AddWithValue("@GMGlobal_addDate", employee.gmglobal.addDate);
            sqlcommand.Parameters.AddWithValue("@GMGlobal_removeDate", DBNull.Value);
            sqlcommand.Parameters.AddWithValue("@GMGlobal_addBy", employee.gmglobal.addBy);
            sqlcommand.Parameters.AddWithValue("@GMGlobal_removeBy", "");
            //HyundaiDealer
            sqlcommand.Parameters.AddWithValue("@HyundaiDealer", employee.hyundaidealer.username);
            sqlcommand.Parameters.AddWithValue("@HyundaiDealer_Password", employee.hyundaidealer.password);
            sqlcommand.Parameters.AddWithValue("@HyundaiDealer_addDate", employee.hyundaidealer.addDate);
            sqlcommand.Parameters.AddWithValue("@HyundaiDealer_removeDate", DBNull.Value);
            sqlcommand.Parameters.AddWithValue("@HyundaiDealer_addBy", employee.hyundaidealer.addBy);
            sqlcommand.Parameters.AddWithValue("@HyundaiDealer_removeBy", "");
            //KDealer
            sqlcommand.Parameters.AddWithValue("@Kdealer", employee.kdealer.username);
            sqlcommand.Parameters.AddWithValue("@Kdealer_Password", employee.kdealer.password);
            sqlcommand.Parameters.AddWithValue("@Kdealer_addDate", employee.kdealer.addDate);
            sqlcommand.Parameters.AddWithValue("@Kdealer_removeDate", DBNull.Value);
            sqlcommand.Parameters.AddWithValue("@Kdealer_addBy", employee.kdealer.addBy);
            sqlcommand.Parameters.AddWithValue("@Kdealer_removeBy", "");
            //H-DNet
            sqlcommand.Parameters.AddWithValue("@HDNetReno", employee.hdnet.username);
            sqlcommand.Parameters.AddWithValue("@HDNetRenoPassword", employee.hdnet.password);
            sqlcommand.Parameters.AddWithValue("@HDNetRenoAddDate", employee.hdnet.addDate);
            sqlcommand.Parameters.AddWithValue("@HDNetRenoRemoveDate", DBNull.Value);
            sqlcommand.Parameters.AddWithValue("@HDNetRenoAddBy", employee.hdnet.addBy);
            sqlcommand.Parameters.AddWithValue("@HDNetRenoRemoveBy", "");

            sqlcommand.Parameters.AddWithValue("@HDNetYuba", employee.hdnet1.username);
            sqlcommand.Parameters.AddWithValue("@HDNetYubaPassword", employee.hdnet1.password);
            sqlcommand.Parameters.AddWithValue("@HDNetYubaAddDate", employee.hdnet1.addDate);
            sqlcommand.Parameters.AddWithValue("@HDNetYubaRemoveDate", DBNull.Value);
            sqlcommand.Parameters.AddWithValue("@HDNetYubaAddBy", employee.hdnet1.addBy);
            sqlcommand.Parameters.AddWithValue("@HDNetYubaRemoveBy", "");

            sqlcommand.Parameters.AddWithValue("@HDNetRedwood", employee.hdnet2.username);
            sqlcommand.Parameters.AddWithValue("@HDNetRedwoodPassword", employee.hdnet2.password);
            sqlcommand.Parameters.AddWithValue("@HDNetRedwoodAddDate", employee.hdnet2.addDate);
            sqlcommand.Parameters.AddWithValue("@HDNetRedwoodRemoveDate", DBNull.Value);
            sqlcommand.Parameters.AddWithValue("@HDNetRedwoodAddBy", employee.hdnet2.addBy);
            sqlcommand.Parameters.AddWithValue("@HDNetRedwoodRemoveBy", "");

            sqlcommand.Parameters.AddWithValue("@HDNetDeathValley", employee.hdnet3.username);
            sqlcommand.Parameters.AddWithValue("@HDNetDeathValleyPassword", employee.hdnet3.password);
            sqlcommand.Parameters.AddWithValue("@HDNetDeathValleyAddDate", employee.hdnet3.addDate);
            sqlcommand.Parameters.AddWithValue("@HDNetDeathValleyRemoveDate", DBNull.Value);
            sqlcommand.Parameters.AddWithValue("@HDNetDeathValleyAddBy", employee.hdnet3.addBy);
            sqlcommand.Parameters.AddWithValue("@HDNetDeathValleyRemoveBy", "");


            sqlcommand.Parameters.AddWithValue("@HDNetCoronadoBeach", employee.hdnet4.username);
            sqlcommand.Parameters.AddWithValue("@HDNetCoronadoBeachPassword", employee.hdnet4.password);
            sqlcommand.Parameters.AddWithValue("@HDNetCoronadoBeachAddDate", employee.hdnet4.addDate);
            sqlcommand.Parameters.AddWithValue("@HDNetCoronadoBeachRemoveDate", DBNull.Value);
            sqlcommand.Parameters.AddWithValue("@HDNetCoronadoBeachAddBy", employee.hdnet4.addBy);
            sqlcommand.Parameters.AddWithValue("@HDNetCoronadoBeachRemoveBy", "");

            sqlcommand.Parameters.AddWithValue("@HDNetOrangeCounty", employee.hdnet5.username);
            sqlcommand.Parameters.AddWithValue("@HDNetOrangeCountyPassword", employee.hdnet5.password);
            sqlcommand.Parameters.AddWithValue("@HDNetOrangeCountyAddDate", employee.hdnet5.addDate);
            sqlcommand.Parameters.AddWithValue("@HDNetOrangeCountyRemoveDate", DBNull.Value);
            sqlcommand.Parameters.AddWithValue("@HDNetOrangeCountyAddBy", employee.hdnet5.addBy);
            sqlcommand.Parameters.AddWithValue("@HDNetOrangeCountyRemoveBy", "");



            //VCC
            sqlcommand.Parameters.AddWithValue("@VCC", employee.vcc.username);
            sqlcommand.Parameters.AddWithValue("@VCC_Password", employee.vcc.password);
            sqlcommand.Parameters.AddWithValue("@VCC_addDate", employee.vcc.addDate);
            sqlcommand.Parameters.AddWithValue("@VCC_removeDate", DBNull.Value);
            sqlcommand.Parameters.AddWithValue("@VCC_addBy", employee.vcc.addBy);
            sqlcommand.Parameters.AddWithValue("@VCC_removeBy", "");
            //MxConnect
            sqlcommand.Parameters.AddWithValue("@MXConnect", employee.mxconnect.username);
            sqlcommand.Parameters.AddWithValue("@MXConnect_Password", employee.mxconnect.password);
            sqlcommand.Parameters.AddWithValue("@MXConnect_addDate", employee.mxconnect.addDate);
            sqlcommand.Parameters.AddWithValue("@MXConnect_removeDate", DBNull.Value);
            sqlcommand.Parameters.AddWithValue("@MXConnect_addBy", employee.mxconnect.addBy);
            sqlcommand.Parameters.AddWithValue("@MXConnect_removeBy", "");
            //CUDL
            sqlcommand.Parameters.AddWithValue("@CUDL", employee.cudl.username);
            sqlcommand.Parameters.AddWithValue("@CUDL_Password", employee.cudl.password);
            sqlcommand.Parameters.AddWithValue("@CUDL_addDate", employee.cudl.addDate);
            sqlcommand.Parameters.AddWithValue("@CUDL_removeDate", DBNull.Value);
            sqlcommand.Parameters.AddWithValue("@CUDL_addBy", employee.cudl.addBy);
            sqlcommand.Parameters.AddWithValue("@CUDL_removeBy", "");
            //Office365
            sqlcommand.Parameters.AddWithValue("@Office365", employee.office365.username);
            sqlcommand.Parameters.AddWithValue("@Office365_Password", employee.office365.password);
            sqlcommand.Parameters.AddWithValue("@Office365_addDate", employee.office365.addDate);
            sqlcommand.Parameters.AddWithValue("@Office365_removeDate", DBNull.Value);
            sqlcommand.Parameters.AddWithValue("@Office365_addBy", employee.office365.addBy);
            sqlcommand.Parameters.AddWithValue("@Office365_removeBy", "");

            //FIExpress
            sqlcommand.Parameters.AddWithValue("@FIExpress", employee.fiexpress.username);
            sqlcommand.Parameters.AddWithValue("@FIExpressPassword", employee.fiexpress.password);
            sqlcommand.Parameters.AddWithValue("@FIExpressAddDate", employee.fiexpress.addDate);
            sqlcommand.Parameters.AddWithValue("@FIExpressRemoveDate", DBNull.Value);
            sqlcommand.Parameters.AddWithValue("@FIExpressAddBy", employee.fiexpress.addBy);
            sqlcommand.Parameters.AddWithValue("@FIExpressRemoveBy", "");
            //DMVDesk
            sqlcommand.Parameters.AddWithValue("@DMVDesk", employee.dmvdesk.username);
            sqlcommand.Parameters.AddWithValue("@DMVDeskPassword", employee.dmvdesk.password);
            sqlcommand.Parameters.AddWithValue("@DMVDeskAddDate", employee.dmvdesk.addDate);
            sqlcommand.Parameters.AddWithValue("@DMVDeskRemoveDate", DBNull.Value);
            sqlcommand.Parameters.AddWithValue("@DMVDeskAddBy", employee.dmvdesk.addBy);
            sqlcommand.Parameters.AddWithValue("@DMVDeskRemoveBy", "");
            //VinSolutions
            sqlcommand.Parameters.AddWithValue("@VinSolutions", employee.vinsolutions.username);
            sqlcommand.Parameters.AddWithValue("@VinSolutionsPassword", employee.vinsolutions.password);
            sqlcommand.Parameters.AddWithValue("@VinSolutionsAddDate", employee.vinsolutions.addDate);
            sqlcommand.Parameters.AddWithValue("@VinSolutionsRemoveDate", DBNull.Value);
            sqlcommand.Parameters.AddWithValue("@VinSolutionsAddBy", employee.vinsolutions.addBy);
            sqlcommand.Parameters.AddWithValue("@VinSolutionsRemoveBy", "");
            //CarWars
            sqlcommand.Parameters.AddWithValue("@CarWars", employee.carwars.username);
            sqlcommand.Parameters.AddWithValue("@CarWarsPassword", employee.carwars.password);
            sqlcommand.Parameters.AddWithValue("@CarWarsAddDate", employee.carwars.addDate);
            sqlcommand.Parameters.AddWithValue("@CarWarsRemoveDate", DBNull.Value);
            sqlcommand.Parameters.AddWithValue("@CarWarsAddBy", employee.carwars.addBy);
            sqlcommand.Parameters.AddWithValue("@CarWarsRemoveBy", "");
            //RapidRecon
            sqlcommand.Parameters.AddWithValue("@RapidRecon", employee.rapidrecon.username);
            sqlcommand.Parameters.AddWithValue("@RapidReconPassword", employee.rapidrecon.password);
            sqlcommand.Parameters.AddWithValue("@RapidReconAddDate", employee.rapidrecon.addDate);
            sqlcommand.Parameters.AddWithValue("@RapidReconRemoveDate", DBNull.Value);
            sqlcommand.Parameters.AddWithValue("@RapidReconAddBy", employee.rapidrecon.addBy);
            sqlcommand.Parameters.AddWithValue("@RapidReconRemoveBy", "");
            //VAuto
            sqlcommand.Parameters.AddWithValue("@vAuto", employee.vauto.username);
            sqlcommand.Parameters.AddWithValue("@vAutoPassword", employee.vauto.password);
            sqlcommand.Parameters.AddWithValue("@vAutoAddDate", employee.vauto.addDate);
            sqlcommand.Parameters.AddWithValue("@vAutoRemoveDate", DBNull.Value);
            sqlcommand.Parameters.AddWithValue("@vAutoAddBy", employee.vauto.addBy);
            sqlcommand.Parameters.AddWithValue("@vAutoRemoveBy", "");

            sqlcommand.Parameters.AddWithValue("@DateSetup", DateTime.Now.ToShortDateString());
            sqlcommand.Parameters.AddWithValue("@DateRemove", "");
            sqlcommand.Parameters.AddWithValue("@DateModify", DateTime.Now.ToShortDateString());
            sqlcommand.Parameters.AddWithValue("@EmployeeNumber", employee.employeenumber);
            sqlcommand.Parameters.AddWithValue("@Department", employee.department);

            sqlcommand.ExecuteNonQuery();
            sqlconnect.Close();
            return true;
        }        
        public bool Update_DB(Person employee, Person admin)
        {
            if (employee.fullname == "")
            {
                MessageBox.Show("SQL ERROR: Employee fullname cannot be blank! [0xGecko]");
                return false;
            }

            SqlConnection sqlconnect = new SqlConnection(connectionString);
            sqlconnect.Open();
            SqlCommand sqlcommand = new SqlCommand(
                @"UPDATE Employee 
                SET 
                   [FullName] = @FullName
                  ,[PhoneNumber] = @PhoneNumber
                  ,[Store] = @Store
                  ,[Role] = @Role
                  ,[Status] = @Status
                  ,[Notes] = @Notes

                  ,[Email] = @Email
                  ,[Email.password] = @Email_password
                  ,[Email.addDate] = @Email_addDate
                  ,[Email.removeDate] = @Email_removeDate
                  ,[Email.addBy] = @Email_addBy
                  ,[Email.removeBy] = @Email_removeBy

                  ,[DealerTrack] = @DealerTrack
                  ,[DealerTrack.password] = @DealerTrack_password
                  ,[DealerTrack.addDate] = @DealerTrack_addDate
                  ,[DealerTrack.removeDate] = @DealerTrack_removeDate
                  ,[DealerTrack.addBy] = @DealerTrack_addBy
                  ,[DealerTrack.removeBy] = @DealerTrack_removeBy

                  ,[Reynolds] = @Reynolds
                  ,[Reynolds.password] = @Reynolds_password
                  ,[Reynolds.addDate] = @Reynolds_addDate
                  ,[Reynolds.removeDate] = @Reynolds_removeDate
                  ,[Reynolds.addBy] = @Reynolds_addBy
                  ,[Reynolds.removeBy] = @Reynolds_removeBy

                  ,[TALONes] = @TALONes
                  ,[TALONes.password] = @TALONes_password
                  ,[TALONes.addDate] = @TALONes_addDate
                  ,[TALONes.removeDate] = @TALONes_removeDate
                  ,[TALONes.addBy] = @TALONes_addBy
                  ,[TALONes.removeBy] = @TALONes_removeBy

                  ,[NNANet] = @NNANet
                  ,[NNANet.password] = @NNANet_password
                  ,[NNANet.addDate] = @NNANet_addDate
                  ,[NNANet.removeDate] = @NNANet_removeDate
                  ,[NNANet.addBy] = @NNANet_addBy
                  ,[NNANet.removeBy] = @NNANet_removeBy

                  ,[DealerConnect] = @DealerConnect
                  ,[DealerConnect.password] = @DealerConnect_password
                  ,[DealerConnect.addDate] = @DealerConnect_addDate
                  ,[DealerConnect.removeDate] = @DealerConnect_removeDate
                  ,[DealerConnect.addBy] = @DealerConnect_addBy
                  ,[DealerConnect.removeBy] = @DealerConnect_removeBy

                  ,[GMGlobal] = @GMGlobal
                  ,[GMGlobal.password] = @GMGlobal_password
                  ,[GMGlobal.addDate] = @GMGlobal_addDate
                  ,[GMGlobal.removeDate] = @GMGlobal_removeDate
                  ,[GMGlobal.addBy] = @GMGlobal_addBy
                  ,[GMGlobal.removeBy] = @GMGlobal_removeBy

                  ,[HyundaiDealer] = @HyundaiDealer
                  ,[HyundaiDealer.password] = @HyundaiDealer_password
                  ,[HyundaiDealer.addDate] = @HyundaiDealer_addDate
                  ,[HyundaiDealer.removeDate] = @HyundaiDealer_removeDate
                  ,[HyundaiDealer.addBy] = @HyundaiDealer_addBy
                  ,[HyundaiDealer.removeBy] = @HyundaiDealer_removeBy

                  ,[KDealer] = @KDealer
                  ,[KDealer.password] = @KDealer_password
                  ,[KDealer.addDate] = @KDealer_addDate
                  ,[KDealer.removeDate] = @KDealer_removeDate
                  ,[KDealer.addBy] = @KDealer_addBy
                  ,[KDealer.removeBy] = @KDealer_removeBy

                      ,[HDNetReno] = @HDNetReno
                      ,[HDNetRenoPassword] = @HDNetRenoPassword
                      ,[HDNetRenoAddDate] = @HDNetRenoAddDate
                      ,[HDNetRenoRemoveDate] = @HDNetRenoremoveDate
                      ,[HDNetRenoAddBy] = @HDNetRenoAddBy
                      ,[HDNetRenoRemoveBy] = @HDNetRenoRemoveBy

                      ,[HDNetYuba] = @HDNetYuba
                      ,[HDNetYubaPassword] = @HDNetYubaPassword
                      ,[HDNetYubaAddDate] = @HDNetYubaAddDate
                      ,[HDNetYubaRemoveDate] = @HDNetYubaRemoveDate
                      ,[HDNetYubaAddBy] = @HDNetYubaAddBy
                      ,[HDNetYubaRemoveBy] = @HDNetYubaRemoveBy

                      ,[HDNetRedwood] = @HDNetRedwood
                      ,[HDNetRedwoodPassword] = @HDNetRedwoodPassword
                      ,[HDNetRedwoodAddDate] = @HDNetRedwoodAddDate
                      ,[HDNetRedwoodRemoveDate] = @HDNetRedwoodRemoveDate
                      ,[HDNetRedwoodAddBy] = @HDNetRedwoodAddBy
                      ,[HDNetRedwoodRemoveBy] = @HDNetRedwoodRemoveBy

                      ,[HDNetDeathValley] = @HDNetDeathValley
                      ,[HDNetDeathValleyPassword] = @HDNetDeathValleyPassword
                      ,[HDNetDeathValleyAddDate] = @HDNetDeathValleyAddDate
                      ,[HDNetDeathValleyRemoveDate] = @HDNetDeathValleyRemoveDate
                      ,[HDNetDeathValleyAddBy] = @HDNetDeathValleyAddBy
                      ,[HDNetDeathValleyRemoveBy] = @HDNetDeathValleyRemoveBy

                      ,[HDNetCoronadoBeach] = @HDNetCoronadoBeach
                      ,[HDNetCoronadoBeachPassword] = @HDNetCoronadoBeachPassword
                      ,[HDNetCoronadoBeachAddDate] = @HDNetCoronadoBeachAddDate
                      ,[HDNetCoronadoBeachRemoveDate] = @HDNetCoronadoBeachRemoveDate
                      ,[HDNetCoronadoBeachAddBy] = @HDNetCoronadoBeachAddBy
                      ,[HDNetCoronadoBeachRemoveBy] = @HDNetCoronadoBeachRemoveBy

                      ,[HDNetOrangeCounty] = @HDNetOrangeCounty
                      ,[HDNetOrangeCountyPassword] = @HDNetOrangeCountyPassword
                      ,[HDNetOrangeCountyAddDate] = @HDNetOrangeCountyAddDate
                      ,[HDNetOrangeCountyRemoveDate] = @HDNetOrangeCountyRemoveDate
                      ,[HDNetOrangeCountyAddBy] = @HDNetOrangeCountyAddBy
                      ,[HDNetOrangeCountyRemoveBy] = @HDNetOrangeCountyRemoveBy

                ,[VCC] = @VCC
                ,[VCC.password] = @VCC_password
                ,[VCC.addDate] = @VCC_addDate
                ,[VCC.removeDate] = @VCC_removeDAte
                ,[VCC.addBy] = @VCC_addBy
                ,[VCC.removeBy] = @VCC_removeBy

                ,[MXConnect] = @MXConnect
                ,[MXConnect.password] = @MXConnect_password
                ,[MXConnect.addDate] = @MXConnect_addDate
                ,[MXConnect.removeDate] = @MXConnect_removeDate
                ,[MXConnect.addBy] = @MXConnect_addBy
                ,[MXConnect.removeBy] = @MXConnect_removeBy

                ,[CUDL] = @CUDL
                ,[CUDL.password] = @CUDL_password
                ,[CUDL.addDate] = @CUDL_addDate
                ,[CUDL.removeDate] = @CUDL_removeDate
                ,[CUDL.addBy] = @CUDL_addBy
                ,[CUDL.removeBy] = @CUDL_removeBy

                ,[Office365] = @Office365
                ,[Office365.password] = @Office365_password
                ,[Office365.addDate] = @Office365_addDate
                ,[Office365.removeDate] = @Office365_removeDate
                ,[Office365.addBy] = @Office365_addBy
                ,[Office365.removeBy] = @Office365_removeBy




                ,[FIExpress] = @FIExpress
                ,[FIExpressPassword] = @FIExpressPassword
                ,[FIExpressAddDate] = @FIExpressAddDate
                ,[FIExpressRemoveDate] = @FIExpressRemoveDate
                ,[FIExpressAddBy] = @FIExpressAddBy
                ,[FIExpressRemoveBy] = @FIExpressRemoveBy

                ,[DMVDesk] = @DMVDesk
                ,[DMVDeskPassword] = @DMVDeskPassword
                ,[DMVDeskAddDate] = @DMVDeskAddDate
                ,[DMVDeskRemoveDate] = @DMVDeskRemoveDate
                ,[DMVDeskAddBy] = @DMVDeskAddBy
                ,[DMVDeskRemoveBy] = @DMVDeskRemoveBy

                ,[VinSolutions] = @VinSolutions
                ,[VinSolutionsPassword] = @VinSolutionsPassword
                ,[VinSolutionsAddDate] = @VinSolutionsAddDate
                ,[VinSolutionsRemoveDate] = @VinSolutionsRemoveDate
                ,[VinSolutionsAddBy] = @VinSolutionsAddBy
                ,[VinSolutionsRemoveBy] = @VinSolutionsRemoveBy

                ,[CarWars] = @CarWars
                ,[CarWarsPassword] = @CarWarsPassword
                ,[CarWarsAddDate] = @CarWarsAddDate
                ,[CarWarsRemoveDate] = @CarWarsRemoveDate
                ,[CarWarsAddBy] = @CarWarsAddBy
                ,[CarWarsRemoveBy] = @CarWarsRemoveBy
                
                ,[RapidRecon] = @RapidRecon
                ,[RapidReconPassword] = @RapidReconPassword
                ,[RapidReconAddDate] = @RapidReconAddDate
                ,[RapidReconRemoveDate] = @RapidReconRemovedate
                ,[RapidReconAddBy] = @RapidReconAddBy
                ,[RapidReconRemoveBy] = @RapidReconRemoveBy

                ,[vAuto] = @vAuto
                ,[vAutoPassword] = @vAutoPassword
                ,[vAutoAddDate] = @vAutoAddDate
                ,[vAutoRemoveDate] = @vAutoRemoveDate
                ,[vAutoAddBy] = @vAutoAddBy
                ,[vAutoRemoveBy] = @vAutoRemoveBy

                ,[DateSetup] = @DateSetup
                ,[DateRemove] = @DateRemove
                ,[DateModify] = @DateModify
                ,[EmployeeNumber] = @EmployeeNumber
                ,[Department] = @Department

                WHERE Id = '" + employee.tableID + "';", sqlconnect);

            sqlcommand.Parameters.AddWithValue("@FullName", employee.fullname);
            sqlcommand.Parameters.AddWithValue("@PhoneNumber", "");
            sqlcommand.Parameters.AddWithValue("@Store", employee.store);
            sqlcommand.Parameters.AddWithValue("@Role", employee.role);


            // quick fix D: ...
            if (employee.status == null)
                employee.status = "ACTIVE";


            sqlcommand.Parameters.AddWithValue("@Status", employee.status.ToLower());   // Object reference not set to instance (when adding DT singularily....)
            sqlcommand.Parameters.AddWithValue("@Notes", employee.notes);

            #region -----------Quick Credential Signing for Edited profiles----------------

            if (employee.email.username != "" && employee.email.addDate == "") { employee.email.SignForAdd(admin);  }
            if (employee.dealertrack.username != "" && employee.dealertrack.addDate == "") { employee.dealertrack.SignForAdd(admin);  }
            if (employee.reynolds.username != "" && employee.reynolds.addDate == "") { employee.reynolds.SignForAdd(admin);  }
            if (employee.talones.username != "" && employee.talones.addDate == "") { employee.talones.SignForAdd(admin); }
            if (employee.nnanet.username != "" && employee.nnanet.addDate == "") { employee.nnanet.SignForAdd(admin); }
            if (employee.dealerconnect.username != "" && employee.dealerconnect.addDate == "") { employee.dealerconnect.SignForAdd(admin); }
            if (employee.gmglobal.username != "" && employee.gmglobal.addDate == "") { employee.gmglobal.SignForAdd(admin); }
            if (employee.hyundaidealer.username != "" && employee.hyundaidealer.addDate == "") { employee.hyundaidealer.SignForAdd(admin); }
            if (employee.kdealer.username != "" && employee.kdealer.addDate == "") { employee.kdealer.SignForAdd(admin); }
            if (employee.hdnet.username != "" && employee.hdnet.addDate == "") { employee.hdnet.SignForAdd(admin); }
            if (employee.hdnet1.username != "" && employee.hdnet1.addDate == "") { employee.hdnet1.SignForAdd(admin); }
            if (employee.hdnet2.username != "" && employee.hdnet2.addDate == "") { employee.hdnet2.SignForAdd(admin); }
            if (employee.hdnet3.username != "" && employee.hdnet3.addDate == "") { employee.hdnet3.SignForAdd(admin); }
            if (employee.hdnet4.username != "" && employee.hdnet4.addDate == "") { employee.hdnet4.SignForAdd(admin); }
            if (employee.hdnet5.username != "" && employee.hdnet5.addDate == "") { employee.hdnet5.SignForAdd(admin); }
            if (employee.vcc.username != "" && employee.vcc.addDate == "") { employee.vcc.SignForAdd(admin); }
            if (employee.office365.username != "" && employee.office365.addDate == "") { employee.office365.SignForAdd(admin); }
            if (employee.fiexpress.username != "" && employee.fiexpress.addDate == "") { employee.fiexpress.SignForAdd(admin); }
            if (employee.dmvdesk.username != "" && employee.dmvdesk.addDate == "") { employee.dmvdesk.SignForAdd(admin); }
            if (employee.vinsolutions.username != "" && employee.vinsolutions.addDate == "") { employee.vinsolutions.SignForAdd(admin); }
            if (employee.carwars.username != "" && employee.carwars.addDate == "") { employee.carwars.SignForAdd(admin); }
            if (employee.rapidrecon.username != "" && employee.rapidrecon.addDate == "") { employee.rapidrecon.SignForAdd(admin); }
            if (employee.vauto.username != "" && employee.vauto.addDate == "") { employee.vauto.SignForAdd(admin); }

            #endregion




            //Email
            sqlcommand.Parameters.AddWithValue("@Email", employee.email.username);
            sqlcommand.Parameters.AddWithValue("@Email_password", employee.email.password);
            sqlcommand.Parameters.AddWithValue("@Email_addDate", employee.email.addDate);
            sqlcommand.Parameters.AddWithValue("@Email_removeDate", employee.email.removeDate);
            sqlcommand.Parameters.AddWithValue("@Email_addBy", employee.email.addBy);
            sqlcommand.Parameters.AddWithValue("@Email_removeBy", employee.email.removeBy);
            //DealerTrack
            sqlcommand.Parameters.AddWithValue("@DealerTrack", employee.dealertrack.username);
            sqlcommand.Parameters.AddWithValue("@DealerTrack_Password", employee.dealertrack.password);
            sqlcommand.Parameters.AddWithValue("@DealerTrack_addDate", employee.dealertrack.addDate);
            sqlcommand.Parameters.AddWithValue("@DealerTrack_removeDate", employee.dealertrack.removeDate);
            sqlcommand.Parameters.AddWithValue("@DealerTrack_addBy", employee.dealertrack.addBy);
            sqlcommand.Parameters.AddWithValue("@DealerTrack_removeBy", employee.dealertrack.removeBy);
            //Reynolds
            sqlcommand.Parameters.AddWithValue("@Reynolds", employee.reynolds.username);
            sqlcommand.Parameters.AddWithValue("@Reynolds_Password", employee.reynolds.password);
            sqlcommand.Parameters.AddWithValue("@Reynolds_addDate", employee.reynolds.addDate);
            sqlcommand.Parameters.AddWithValue("@Reynolds_removeDate", employee.reynolds.removeDate);
            sqlcommand.Parameters.AddWithValue("@Reynolds_addBy", employee.reynolds.addBy);
            sqlcommand.Parameters.AddWithValue("@Reynolds_removeBy", employee.reynolds.removeBy);
            //TALONes
            sqlcommand.Parameters.AddWithValue("@TALONes", employee.talones.username);
            sqlcommand.Parameters.AddWithValue("@TALONes_Password", employee.talones.password);
            sqlcommand.Parameters.AddWithValue("@TALONes_addDate", employee.talones.addDate);
            sqlcommand.Parameters.AddWithValue("@TALONes_removeDate", employee.talones.removeDate);
            sqlcommand.Parameters.AddWithValue("@TALONes_addBy", employee.talones.addBy);
            sqlcommand.Parameters.AddWithValue("@TALONes_removeBy", employee.talones.removeBy);
            //NNANet
            sqlcommand.Parameters.AddWithValue("@NNANet", employee.nnanet.username);
            sqlcommand.Parameters.AddWithValue("@NNANet_Password", employee.nnanet.password);
            sqlcommand.Parameters.AddWithValue("@NNANet_addDate", employee.nnanet.addDate);
            sqlcommand.Parameters.AddWithValue("@NNANet_removeDate", employee.nnanet.removeDate);
            sqlcommand.Parameters.AddWithValue("@NNANet_addBy", employee.nnanet.addBy);
            sqlcommand.Parameters.AddWithValue("@NNANet_removeBy", employee.nnanet.removeBy);
            //Dealerconnect
            sqlcommand.Parameters.AddWithValue("@DealerConnect", employee.dealerconnect.username);
            sqlcommand.Parameters.AddWithValue("@DealerConnect_Password", employee.dealerconnect.password);
            sqlcommand.Parameters.AddWithValue("@DealerConnect_addDate", employee.dealerconnect.addDate);
            sqlcommand.Parameters.AddWithValue("@DealerConnect_removeDate", employee.dealerconnect.removeDate);
            sqlcommand.Parameters.AddWithValue("@DealerConnect_addBy", employee.dealerconnect.addBy);
            sqlcommand.Parameters.AddWithValue("@DealerConnect_removeBy", employee.dealerconnect.removeBy);
            //GMGlobal
            sqlcommand.Parameters.AddWithValue("@GMGlobal", employee.gmglobal.username);
            sqlcommand.Parameters.AddWithValue("@GMGlobal_Password", employee.gmglobal.password);
            sqlcommand.Parameters.AddWithValue("@GMGlobal_addDate", employee.gmglobal.addDate);
            sqlcommand.Parameters.AddWithValue("@GMGlobal_removeDate", employee.gmglobal.removeDate);
            sqlcommand.Parameters.AddWithValue("@GMGlobal_addBy", employee.gmglobal.addBy);
            sqlcommand.Parameters.AddWithValue("@GMGlobal_removeBy", employee.gmglobal.removeBy);
            //HyundaiDealer
            sqlcommand.Parameters.AddWithValue("@HyundaiDealer", employee.hyundaidealer.username);
            sqlcommand.Parameters.AddWithValue("@HyundaiDealer_Password", employee.hyundaidealer.password);
            sqlcommand.Parameters.AddWithValue("@HyundaiDealer_addDate", employee.hyundaidealer.addDate);
            sqlcommand.Parameters.AddWithValue("@HyundaiDealer_removeDate", employee.hyundaidealer.removeDate);
            sqlcommand.Parameters.AddWithValue("@HyundaiDealer_addBy", employee.hyundaidealer.addBy);
            sqlcommand.Parameters.AddWithValue("@HyundaiDealer_removeBy", employee.hyundaidealer.removeBy);
            //KDealer
            sqlcommand.Parameters.AddWithValue("@Kdealer", employee.kdealer.username);
            sqlcommand.Parameters.AddWithValue("@Kdealer_Password", employee.kdealer.password);
            sqlcommand.Parameters.AddWithValue("@Kdealer_addDate", employee.kdealer.addDate);
            sqlcommand.Parameters.AddWithValue("@Kdealer_removeDate", employee.kdealer.removeDate);
            sqlcommand.Parameters.AddWithValue("@Kdealer_addBy", employee.kdealer.addBy);
            sqlcommand.Parameters.AddWithValue("@Kdealer_removeBy", employee.kdealer.removeBy);
            //H-DNet
                sqlcommand.Parameters.AddWithValue("@HDNetReno", employee.hdnet.username);
                sqlcommand.Parameters.AddWithValue("@HDNetRenoPassword", employee.hdnet.password);
                sqlcommand.Parameters.AddWithValue("@HDNetRenoAddDate", employee.hdnet.addDate);
                sqlcommand.Parameters.AddWithValue("@HDNetRenoRemoveDate", employee.hdnet.removeDate);
                sqlcommand.Parameters.AddWithValue("@HDNetRenoAddBy", employee.hdnet.addBy);
                sqlcommand.Parameters.AddWithValue("@HDNetRenoRemoveBy", employee.hdnet.removeBy);

                sqlcommand.Parameters.AddWithValue("@HDNetYuba", employee.hdnet1.username);
                sqlcommand.Parameters.AddWithValue("@HDNetYubaPassword", employee.hdnet1.password);
                sqlcommand.Parameters.AddWithValue("@HDNetYubaAddDate", employee.hdnet1.addDate);
                sqlcommand.Parameters.AddWithValue("@HDNetYubaRemoveDate", employee.hdnet1.removeDate);
                sqlcommand.Parameters.AddWithValue("@HDNetYubaAddBy", employee.hdnet1.addBy);
                sqlcommand.Parameters.AddWithValue("@HDNetYubaRemoveBy", employee.hdnet1.removeBy);

                sqlcommand.Parameters.AddWithValue("@HDNetRedwood", employee.hdnet2.username);
                sqlcommand.Parameters.AddWithValue("@HDNetRedwoodPassword", employee.hdnet2.password);
                sqlcommand.Parameters.AddWithValue("@HDNetRedwoodAddDate", employee.hdnet2.addDate);
                sqlcommand.Parameters.AddWithValue("@HDNetRedwoodRemoveDate", employee.hdnet2.removeDate);
                sqlcommand.Parameters.AddWithValue("@HDNetRedwoodAddBy", employee.hdnet2.addBy);
                sqlcommand.Parameters.AddWithValue("@HDNetRedwoodRemoveBy", employee.hdnet2.removeBy);

                sqlcommand.Parameters.AddWithValue("@HDNetDeathValley", employee.hdnet3.username);
                sqlcommand.Parameters.AddWithValue("@HDNetDeathValleyPassword", employee.hdnet3.password);
                sqlcommand.Parameters.AddWithValue("@HDNetDeathValleyAddDate", employee.hdnet3.addDate);
                sqlcommand.Parameters.AddWithValue("@HDNetDeathValleyRemoveDate", employee.hdnet3.removeDate);
                sqlcommand.Parameters.AddWithValue("@HDNetDeathValleyAddBy", employee.hdnet3.addBy);
                sqlcommand.Parameters.AddWithValue("@HDNetDeathValleyRemoveBy", employee.hdnet3.removeBy);

                sqlcommand.Parameters.AddWithValue("@HDNetCoronadoBeach", employee.hdnet4.username);
                sqlcommand.Parameters.AddWithValue("@HDNetCoronadoBeachPassword", employee.hdnet4.password);
                sqlcommand.Parameters.AddWithValue("@HDNetCoronadoBeachAddDate", employee.hdnet4.addDate);
                sqlcommand.Parameters.AddWithValue("@HDNetCoronadoBeachRemoveDate", employee.hdnet4.removeDate);
                sqlcommand.Parameters.AddWithValue("@HDNetCoronadoBeachAddBy", employee.hdnet4.addBy);
                sqlcommand.Parameters.AddWithValue("@HDNetCoronadoBeachRemoveBy", employee.hdnet4.removeBy);

                sqlcommand.Parameters.AddWithValue("@HDNetOrangeCounty", employee.hdnet5.username);
                sqlcommand.Parameters.AddWithValue("@HDNetOrangeCountyPassword", employee.hdnet5.password);
                sqlcommand.Parameters.AddWithValue("@HDNetOrangeCountyAddDate", employee.hdnet5.addDate);
                sqlcommand.Parameters.AddWithValue("@HDNetOrangeCountyRemoveDate", employee.hdnet5.removeDate);
                sqlcommand.Parameters.AddWithValue("@HDNetOrangeCountyAddBy", employee.hdnet5.addBy);
                sqlcommand.Parameters.AddWithValue("@HDNetOrangeCountyRemoveBy", employee.hdnet5.removeBy);

            //VCC
            sqlcommand.Parameters.AddWithValue("@VCC", employee.vcc.username);
            sqlcommand.Parameters.AddWithValue("@VCC_Password", employee.vcc.password);
            sqlcommand.Parameters.AddWithValue("@VCC_addDate", employee.vcc.addDate);
            sqlcommand.Parameters.AddWithValue("@VCC_removeDate", employee.vcc.removeDate);
            sqlcommand.Parameters.AddWithValue("@VCC_addBy", employee.vcc.addBy);
            sqlcommand.Parameters.AddWithValue("@VCC_removeBy", employee.vcc.removeBy);
            //MxConnect
            sqlcommand.Parameters.AddWithValue("@MXConnect", employee.mxconnect.username);
            sqlcommand.Parameters.AddWithValue("@MXConnect_Password", employee.mxconnect.password);
            sqlcommand.Parameters.AddWithValue("@MXConnect_addDate", employee.mxconnect.addDate);
            sqlcommand.Parameters.AddWithValue("@MXConnect_removeDate", employee.mxconnect.removeDate);
            sqlcommand.Parameters.AddWithValue("@MXConnect_addBy", employee.mxconnect.addBy);
            sqlcommand.Parameters.AddWithValue("@MXConnect_removeBy", employee.mxconnect.removeBy);
            //CUDL
            sqlcommand.Parameters.AddWithValue("@CUDL", employee.cudl.username);
            sqlcommand.Parameters.AddWithValue("@CUDL_Password", employee.cudl.password);
            sqlcommand.Parameters.AddWithValue("@CUDL_addDate", employee.cudl.addDate);
            sqlcommand.Parameters.AddWithValue("@CUDL_removeDate", employee.cudl.removeDate);
            sqlcommand.Parameters.AddWithValue("@CUDL_addBy", employee.cudl.addBy);
            sqlcommand.Parameters.AddWithValue("@CUDL_removeBy", employee.cudl.removeBy);
            //Office365
            sqlcommand.Parameters.AddWithValue("@Office365", employee.office365.username);
            sqlcommand.Parameters.AddWithValue("@Office365_Password", employee.office365.password);
            sqlcommand.Parameters.AddWithValue("@Office365_addDate", employee.office365.removeDate);
            sqlcommand.Parameters.AddWithValue("@Office365_removeDate", employee.office365.removeDate);
            sqlcommand.Parameters.AddWithValue("@Office365_addBy", employee.office365.addBy);
            sqlcommand.Parameters.AddWithValue("@Office365_removeBy", employee.office365.removeBy);



            //FIExpress
            sqlcommand.Parameters.AddWithValue("@FIExpress", employee.fiexpress.username);
            sqlcommand.Parameters.AddWithValue("@FIExpressPassword", employee.fiexpress.password);
            sqlcommand.Parameters.AddWithValue("@FIExpressAddDate", employee.fiexpress.addDate);
            sqlcommand.Parameters.AddWithValue("@FIExpressRemoveDate", employee.fiexpress.removeDate);
            sqlcommand.Parameters.AddWithValue("@FIExpressAddBy", employee.fiexpress.addBy);
            sqlcommand.Parameters.AddWithValue("@FIExpressRemoveBy", employee.fiexpress.removeBy);
            //DMVDesk
            sqlcommand.Parameters.AddWithValue("@DMVDesk", employee.dmvdesk.username);
            sqlcommand.Parameters.AddWithValue("@DMVDeskPassword", employee.dmvdesk.password);
            sqlcommand.Parameters.AddWithValue("@DMVDeskAddDate", employee.dmvdesk.addDate);
            sqlcommand.Parameters.AddWithValue("@DMVDeskRemoveDate", employee.dmvdesk.RemoveDate);
            sqlcommand.Parameters.AddWithValue("@DMVDeskAddBy", employee.dmvdesk.addBy);
            sqlcommand.Parameters.AddWithValue("@DMVDeskRemoveBy", employee.dmvdesk.removeBy);
            //VinSolutions
            sqlcommand.Parameters.AddWithValue("@VinSolutions", employee.vinsolutions.username);
            sqlcommand.Parameters.AddWithValue("@VinSolutionsPassword", employee.vinsolutions.password);
            sqlcommand.Parameters.AddWithValue("@VinSolutionsAddDate", employee.vinsolutions.addDate);
            sqlcommand.Parameters.AddWithValue("@VinSolutionsRemoveDate", employee.vinsolutions.RemoveDate);
            sqlcommand.Parameters.AddWithValue("@VinSolutionsAddBy", employee.vinsolutions.addBy);
            sqlcommand.Parameters.AddWithValue("@VinSolutionsRemoveBy",employee.vinsolutions.removeBy);
            //CarWars
            sqlcommand.Parameters.AddWithValue("@CarWars", employee.carwars.username);
            sqlcommand.Parameters.AddWithValue("@CarWarsPassword", employee.carwars.password);
            sqlcommand.Parameters.AddWithValue("@CarWarsAddDate", employee.carwars.addDate);
            sqlcommand.Parameters.AddWithValue("@CarWarsRemoveDate", employee.removeDate);
            sqlcommand.Parameters.AddWithValue("@CarWarsAddBy", employee.carwars.addBy);
            sqlcommand.Parameters.AddWithValue("@CarWarsRemoveBy", employee.carwars.removeBy);
            //RapidRecon
            sqlcommand.Parameters.AddWithValue("@RapidRecon", employee.rapidrecon.username);
            sqlcommand.Parameters.AddWithValue("@RapidReconPassword", employee.rapidrecon.password);
            sqlcommand.Parameters.AddWithValue("@RapidReconAddDate", employee.rapidrecon.addDate);
            sqlcommand.Parameters.AddWithValue("@RapidReconRemoveDate", employee.rapidrecon.removeDate);
            sqlcommand.Parameters.AddWithValue("@RapidReconAddBy", employee.rapidrecon.addBy);
            sqlcommand.Parameters.AddWithValue("@RapidReconRemoveBy", employee.rapidrecon.removeBy);
            //VAuto
            sqlcommand.Parameters.AddWithValue("@vAuto", employee.vauto.username);
            sqlcommand.Parameters.AddWithValue("@vAutoPassword", employee.vauto.password);
            sqlcommand.Parameters.AddWithValue("@vAutoAddDate", employee.vauto.addDate);
            sqlcommand.Parameters.AddWithValue("@vAutoRemoveDate", employee.vauto.removeDate);
            sqlcommand.Parameters.AddWithValue("@vAutoAddBy", employee.vauto.addBy);
            sqlcommand.Parameters.AddWithValue("@vAutoRemoveBy", employee.vauto.removeBy);


            sqlcommand.Parameters.AddWithValue("@DateSetup", employee.addDate);
            sqlcommand.Parameters.AddWithValue("@DateRemove", employee.removeDate);
            sqlcommand.Parameters.AddWithValue("@DateModify", DateTime.Now.ToShortDateString());
            sqlcommand.Parameters.AddWithValue("@EmployeeNumber", employee.employeenumber);
            sqlcommand.Parameters.AddWithValue("@Department", employee.department);


            sqlcommand.ExecuteNonQuery();
            sqlconnect.Close();
            return true;
        }

        public Person Read_DB(string id)       // Reads the database and returns a Person based on Id
        {
            Person person = new Person();
            SqlConnection sqlconnect = new SqlConnection(connectionString);
            SqlCommand sqlcommand;
            sqlcommand = new SqlCommand("SELECT * FROM Employee WHERE Id = '" + id + "';", sqlconnect);
            sqlconnect.Open();
            
            using (var reader = sqlcommand.ExecuteReader())
            {
                reader.Read();
                person.tableID = reader.GetValue(reader.GetOrdinal("Id")).ToString();
                person.employeenumber = reader.GetValue(reader.GetOrdinal("EmployeeNumber")).ToString();
                person.fullname = reader.GetValue(reader.GetOrdinal("FullName")).ToString().Trim();
                person.store = reader.GetValue(reader.GetOrdinal("Store")).ToString();
                person.role = reader.GetValue(reader.GetOrdinal("Role")).ToString();
                person.department = reader.GetValue(reader.GetOrdinal("Department")).ToString();
                person.status = reader.GetValue(reader.GetOrdinal("Status")).ToString().ToLower();
                person.notes = reader.GetValue(reader.GetOrdinal("Notes")).ToString();

                person.addDate = reader.GetValue(reader.GetOrdinal("DateSetup")).ToString();
                person.removeDate = reader.GetValue(reader.GetOrdinal("DateRemove")).ToString();
                person.modifyDate = reader.GetValue(reader.GetOrdinal("DateModify")).ToString();
                /*
                person.addBy = reader.GetValue(reader.GetOrdinal("DateSetup")).ToString();
                person.removeBy = reader.GetValue(reader.GetOrdinal("DateRemove")).ToString();
                person.modifyBy = reader.GetValue(reader.GetOrdinal("DateModify")).ToString();
                */


                person.email.username = reader.GetValue(reader.GetOrdinal("Email")).ToString();
                person.email.password = reader.GetValue(reader.GetOrdinal("Email.password")).ToString();
                person.email.addDate = reader.GetValue(reader.GetOrdinal("Email.addDate")).ToString();
                person.email.removeDate = reader.GetValue(reader.GetOrdinal("Email.removeDate")).ToString();
                person.email.addBy = reader.GetValue(reader.GetOrdinal("Email.addBy")).ToString();
                person.email.removeBy = reader.GetValue(reader.GetOrdinal("Email.removeBy")).ToString();

                person.dealertrack.username = reader.GetValue(reader.GetOrdinal("DealerTrack")).ToString();
                person.dealertrack.password = reader.GetValue(reader.GetOrdinal("DealerTrack.password")).ToString();
                person.dealertrack.addDate = reader.GetValue(reader.GetOrdinal("DealerTrack.addDate")).ToString();
                person.dealertrack.removeDate = reader.GetValue(reader.GetOrdinal("DealerTrack.removeDate")).ToString();
                person.dealertrack.addBy = reader.GetValue(reader.GetOrdinal("DealerTrack.addBy")).ToString();
                person.dealertrack.removeBy = reader.GetValue(reader.GetOrdinal("DealerTrack.removeBy")).ToString();

                person.reynolds.username = reader.GetValue(reader.GetOrdinal("Reynolds")).ToString();
                person.reynolds.password = reader.GetValue(reader.GetOrdinal("Reynolds.password")).ToString();
                person.reynolds.addDate = reader.GetValue(reader.GetOrdinal("Reynolds.addDate")).ToString();
                person.reynolds.removeDate = reader.GetValue(reader.GetOrdinal("Reynolds.removeDate")).ToString();
                person.reynolds.addBy = reader.GetValue(reader.GetOrdinal("Reynolds.addBy")).ToString();
                person.reynolds.removeBy = reader.GetValue(reader.GetOrdinal("Reynolds.removeBy")).ToString();

                person.talones.username = reader.GetValue(reader.GetOrdinal("TALONes")).ToString();
                person.talones.password = reader.GetValue(reader.GetOrdinal("TALONes.password")).ToString();
                person.talones.addDate = reader.GetValue(reader.GetOrdinal("TALONes.addDate")).ToString();
                person.talones.removeDate = reader.GetValue(reader.GetOrdinal("TALONes.removeDate")).ToString();
                person.talones.addBy = reader.GetValue(reader.GetOrdinal("TALONes.addBy")).ToString();
                person.talones.removeBy = reader.GetValue(reader.GetOrdinal("TALONes.removeBy")).ToString();

                person.nnanet.username = reader.GetValue(reader.GetOrdinal("NNANet")).ToString();
                person.nnanet.password = reader.GetValue(reader.GetOrdinal("NNANet.password")).ToString();
                person.nnanet.addDate = reader.GetValue(reader.GetOrdinal("NNANet.addDate")).ToString();
                person.nnanet.removeDate = reader.GetValue(reader.GetOrdinal("NNANet.removeDate")).ToString();
                person.nnanet.addBy = reader.GetValue(reader.GetOrdinal("NNANet.addBy")).ToString();
                person.nnanet.removeBy = reader.GetValue(reader.GetOrdinal("NNANet.removeBy")).ToString();

                person.dealerconnect.username = reader.GetValue(reader.GetOrdinal("DealerConnect")).ToString();
                person.dealerconnect.password = reader.GetValue(reader.GetOrdinal("DealerConnect.password")).ToString();
                person.dealerconnect.addDate = reader.GetValue(reader.GetOrdinal("DealerConnect.addDate")).ToString();
                person.dealerconnect.removeDate = reader.GetValue(reader.GetOrdinal("DealerConnect.removeDate")).ToString();
                person.dealerconnect.addBy = reader.GetValue(reader.GetOrdinal("DealerConnect.addBy")).ToString();
                person.dealerconnect.removeBy = reader.GetValue(reader.GetOrdinal("DealerConnect.removeBy")).ToString();

                person.gmglobal.username = reader.GetValue(reader.GetOrdinal("GMGlobal")).ToString();
                person.gmglobal.password = reader.GetValue(reader.GetOrdinal("GMGlobal.password")).ToString();
                person.gmglobal.addDate = reader.GetValue(reader.GetOrdinal("GMGlobal.addDate")).ToString();
                person.gmglobal.removeDate = reader.GetValue(reader.GetOrdinal("GMGlobal.removeDate")).ToString();
                person.gmglobal.addBy = reader.GetValue(reader.GetOrdinal("GMGlobal.addBy")).ToString();
                person.gmglobal.removeBy = reader.GetValue(reader.GetOrdinal("GMGlobal.removeBy")).ToString();

                person.hyundaidealer.username = reader.GetValue(reader.GetOrdinal("HyundaiDealer")).ToString();
                person.hyundaidealer.password = reader.GetValue(reader.GetOrdinal("HyundaiDealer.password")).ToString();
                person.hyundaidealer.addDate = reader.GetValue(reader.GetOrdinal("HyundaiDealer.addDate")).ToString();
                person.hyundaidealer.removeDate = reader.GetValue(reader.GetOrdinal("HyundaiDealer.removeDate")).ToString();
                person.hyundaidealer.addBy = reader.GetValue(reader.GetOrdinal("HyundaiDealer.addBy")).ToString();
                person.hyundaidealer.removeBy = reader.GetValue(reader.GetOrdinal("HyundaiDealer.removeBy")).ToString();

                person.kdealer.username = reader.GetValue(reader.GetOrdinal("KDealer")).ToString();
                person.kdealer.password = reader.GetValue(reader.GetOrdinal("KDealer.password")).ToString();
                person.kdealer.addDate = reader.GetValue(reader.GetOrdinal("KDealer.addDate")).ToString();
                person.kdealer.removeDate = reader.GetValue(reader.GetOrdinal("KDealer.removeDate")).ToString();
                person.kdealer.addBy = reader.GetValue(reader.GetOrdinal("KDealer.addBy")).ToString();
                person.kdealer.removeBy = reader.GetValue(reader.GetOrdinal("KDealer.removeBy")).ToString();


                //H-D NET
                person.hdnet.username = reader.GetValue(reader.GetOrdinal("HDNetReno")).ToString();
                person.hdnet.password = reader.GetValue(reader.GetOrdinal("HDNetRenoPassword")).ToString();
                person.hdnet.addDate = reader.GetValue(reader.GetOrdinal("HDNetRenoAddDate")).ToString();
                person.hdnet.removeDate = reader.GetValue(reader.GetOrdinal("HDNetRenoRemoveDate")).ToString();
                person.hdnet.addBy = reader.GetValue(reader.GetOrdinal("HDNetRenoAddBy")).ToString();
                person.hdnet.removeBy = reader.GetValue(reader.GetOrdinal("HDNetRenoRemoveBy")).ToString();

                person.hdnet1.username = reader.GetValue(reader.GetOrdinal("HDNetYuba")).ToString();
                person.hdnet1.password = reader.GetValue(reader.GetOrdinal("HDNetYubaPassword")).ToString();
                person.hdnet1.addDate = reader.GetValue(reader.GetOrdinal("HDNetYubaAddDate")).ToString();
                person.hdnet1.removeDate = reader.GetValue(reader.GetOrdinal("HDNetYubaRemoveDate")).ToString();
                person.hdnet1.addBy = reader.GetValue(reader.GetOrdinal("HDNetYubaAddBy")).ToString();
                person.hdnet1.removeBy = reader.GetValue(reader.GetOrdinal("HDNetYubaRemoveBy")).ToString();

                person.hdnet2.username = reader.GetValue(reader.GetOrdinal("HDNetRedwood")).ToString();
                person.hdnet2.password = reader.GetValue(reader.GetOrdinal("HDNetRedwoodPassword")).ToString();
                person.hdnet2.addDate = reader.GetValue(reader.GetOrdinal("HDNetRedwoodAddDate")).ToString();
                person.hdnet2.removeDate = reader.GetValue(reader.GetOrdinal("HDNetRedwoodRemoveDate")).ToString();
                person.hdnet2.addBy = reader.GetValue(reader.GetOrdinal("HDNetRedwoodAddBy")).ToString();
                person.hdnet2.removeBy = reader.GetValue(reader.GetOrdinal("HDNetRedwoodRemoveBy")).ToString();

                person.hdnet3.username = reader.GetValue(reader.GetOrdinal("HDNetDeathValley")).ToString();
                person.hdnet3.password = reader.GetValue(reader.GetOrdinal("HDNetDeathValleyPassword")).ToString();
                person.hdnet3.addDate = reader.GetValue(reader.GetOrdinal("HDNetDeathValleyAddDate")).ToString();
                person.hdnet3.removeDate = reader.GetValue(reader.GetOrdinal("HDNetDeathValleyRemoveDate")).ToString();
                person.hdnet3.addBy = reader.GetValue(reader.GetOrdinal("HDNetDeathValleyAddBy")).ToString();
                person.hdnet3.removeBy = reader.GetValue(reader.GetOrdinal("HDNetDeathValleyRemoveBy")).ToString();

                person.hdnet4.username = reader.GetValue(reader.GetOrdinal("HDNetCoronadoBeach")).ToString();
                person.hdnet4.password = reader.GetValue(reader.GetOrdinal("HDNetCoronadoBeachPassword")).ToString();
                person.hdnet4.addDate = reader.GetValue(reader.GetOrdinal("HDNetCoronadoBeachAddDate")).ToString();
                person.hdnet4.removeDate = reader.GetValue(reader.GetOrdinal("HDNetCoronadoBeachRemoveDate")).ToString();
                person.hdnet4.addBy = reader.GetValue(reader.GetOrdinal("HDNetCoronadoBeachAddBy")).ToString();
                person.hdnet4.removeBy = reader.GetValue(reader.GetOrdinal("HDNetCoronadoBeachRemoveBy")).ToString();

                person.hdnet5.username = reader.GetValue(reader.GetOrdinal("HDNetOrangeCounty")).ToString();
                person.hdnet5.password = reader.GetValue(reader.GetOrdinal("HDNetOrangeCountyPassword")).ToString();
                person.hdnet5.addDate = reader.GetValue(reader.GetOrdinal("HDNetOrangeCountyAddDate")).ToString();
                person.hdnet5.removeDate = reader.GetValue(reader.GetOrdinal("HDNetOrangeCountyRemoveDate")).ToString();
                person.hdnet5.addBy = reader.GetValue(reader.GetOrdinal("HDNetOrangeCountyAddBy")).ToString();
                person.hdnet5.removeBy = reader.GetValue(reader.GetOrdinal("HDNetOrangeCountyRemoveBy")).ToString();





                person.vcc.username = reader.GetValue(reader.GetOrdinal("VCC")).ToString();
                person.vcc.password = reader.GetValue(reader.GetOrdinal("VCC.password")).ToString();
                person.vcc.addDate = reader.GetValue(reader.GetOrdinal("VCC.addDate")).ToString();
                person.vcc.removeDate = reader.GetValue(reader.GetOrdinal("VCC.removeDate")).ToString();
                person.vcc.addBy = reader.GetValue(reader.GetOrdinal("VCC.addBy")).ToString();
                person.vcc.removeBy = reader.GetValue(reader.GetOrdinal("VCC.removeBy")).ToString();

                person.mxconnect.username = reader.GetValue(reader.GetOrdinal("MXConnect")).ToString();
                person.mxconnect.password = reader.GetValue(reader.GetOrdinal("MXConnect.password")).ToString();
                person.mxconnect.addDate = reader.GetValue(reader.GetOrdinal("MXConnect.addDate")).ToString();
                person.mxconnect.removeDate = reader.GetValue(reader.GetOrdinal("MXConnect.removeDate")).ToString();
                person.mxconnect.addBy = reader.GetValue(reader.GetOrdinal("MXConnect.addBy")).ToString();
                person.mxconnect.removeBy = reader.GetValue(reader.GetOrdinal("MXConnect.removeBy")).ToString();

                person.cudl.username = reader.GetValue(reader.GetOrdinal("CUDL")).ToString();
                person.cudl.password = reader.GetValue(reader.GetOrdinal("CUDL.password")).ToString();
                person.cudl.addDate = reader.GetValue(reader.GetOrdinal("CUDL.addDate")).ToString();
                person.cudl.removeDate = reader.GetValue(reader.GetOrdinal("CUDL.removeDate")).ToString();
                person.cudl.addBy = reader.GetValue(reader.GetOrdinal("CUDL.addBy")).ToString();
                person.cudl.removeBy = reader.GetValue(reader.GetOrdinal("CUDL.removeBy")).ToString();

                person.office365.username = reader.GetValue(reader.GetOrdinal("Office365")).ToString();
                person.office365.password = reader.GetValue(reader.GetOrdinal("Office365.password")).ToString();
                person.office365.addDate = reader.GetValue(reader.GetOrdinal("Office365.addDate")).ToString();
                person.office365.removeDate = reader.GetValue(reader.GetOrdinal("Office365.removeDate")).ToString();
                person.office365.addBy = reader.GetValue(reader.GetOrdinal("Office365.addBy")).ToString();
                person.office365.removeBy = reader.GetValue(reader.GetOrdinal("Office365.removeBy")).ToString();

                // Chris Dept.
                person.fiexpress.username = reader.GetValue(reader.GetOrdinal("FIExpress")).ToString();
                person.fiexpress.password = reader.GetValue(reader.GetOrdinal("FIExpressPassword")).ToString();
                person.fiexpress.addDate = reader.GetValue(reader.GetOrdinal("FIExpressAddDate")).ToString();
                person.fiexpress.removeDate = reader.GetValue(reader.GetOrdinal("FIExpressRemoveDate")).ToString();
                person.fiexpress.addBy = reader.GetValue(reader.GetOrdinal("FIExpressAddBy")).ToString();
                person.fiexpress.removeBy = reader.GetValue(reader.GetOrdinal("FIExpressRemoveBy")).ToString();

                person.dmvdesk.username = reader.GetValue(reader.GetOrdinal("DMVDesk")).ToString();
                person.dmvdesk.password = reader.GetValue(reader.GetOrdinal("DMVDeskPassword")).ToString();
                person.dmvdesk.addDate = reader.GetValue(reader.GetOrdinal("DMVDeskAddDate")).ToString();
                person.dmvdesk.removeDate = reader.GetValue(reader.GetOrdinal("DMVDeskRemoveDate")).ToString();
                person.dmvdesk.addBy = reader.GetValue(reader.GetOrdinal("DMVDeskAddBy")).ToString();
                person.dmvdesk.removeBy = reader.GetValue(reader.GetOrdinal("DMVDeskRemoveBy")).ToString();

                person.vinsolutions.username = reader.GetValue(reader.GetOrdinal("VinSolutions")).ToString();
                person.vinsolutions.password = reader.GetValue(reader.GetOrdinal("VinSolutionsPassword")).ToString();
                person.vinsolutions.addDate = reader.GetValue(reader.GetOrdinal("VinSolutionsAddDate")).ToString();
                person.vinsolutions.removeDate = reader.GetValue(reader.GetOrdinal("VinSolutionsRemoveDate")).ToString();
                person.vinsolutions.addBy = reader.GetValue(reader.GetOrdinal("VinSolutionsAddBy")).ToString();
                person.vinsolutions.removeBy = reader.GetValue(reader.GetOrdinal("VinSolutionsRemoveBy")).ToString();

                person.carwars.username = reader.GetValue(reader.GetOrdinal("CarWars")).ToString();
                person.carwars.password = reader.GetValue(reader.GetOrdinal("CarWarsPassword")).ToString();
                person.carwars.addDate = reader.GetValue(reader.GetOrdinal("CarWarsAddDate")).ToString();
                person.carwars.removeDate = reader.GetValue(reader.GetOrdinal("CarWarsRemoveDate")).ToString();
                person.carwars.addBy = reader.GetValue(reader.GetOrdinal("CarWarsAddBy")).ToString();
                person.carwars.removeBy = reader.GetValue(reader.GetOrdinal("CarWarsRemoveBy")).ToString();

                person.rapidrecon.username = reader.GetValue(reader.GetOrdinal("RapidRecon")).ToString();
                person.rapidrecon.password = reader.GetValue(reader.GetOrdinal("RapidReconPassword")).ToString();
                person.rapidrecon.addDate = reader.GetValue(reader.GetOrdinal("RapidReconAddDate")).ToString();
                person.rapidrecon.removeDate = reader.GetValue(reader.GetOrdinal("RapidReconRemoveDate")).ToString();
                person.rapidrecon.addBy = reader.GetValue(reader.GetOrdinal("RapidReconAddBy")).ToString();
                person.rapidrecon.removeBy = reader.GetValue(reader.GetOrdinal("RapidReconRemoveBy")).ToString();

                person.vauto.username = reader.GetValue(reader.GetOrdinal("vAuto")).ToString();
                person.vauto.password = reader.GetValue(reader.GetOrdinal("vAutoPassword")).ToString();
                person.vauto.addDate = reader.GetValue(reader.GetOrdinal("vAutoAddDate")).ToString();
                person.vauto.removeDate = reader.GetValue(reader.GetOrdinal("vAutoRemoveDate")).ToString();
                person.vauto.addBy = reader.GetValue(reader.GetOrdinal("vAutoAddBy")).ToString();
                person.vauto.removeBy = reader.GetValue(reader.GetOrdinal("vAutoRemoveBy")).ToString();

            }

            return person;
        }

        public void Update_DB_Status(Person person, Person admin)   //Update the status of an employee
        {
            SqlConnection sqlconnect = new SqlConnection(connectionString);
            sqlconnect.Open();
            SqlCommand sqlcommand = new SqlCommand(@"UPDATE Employee
                                                    Set Status = '" + person.status + "'Where Id = '" + person.tableID + "';", sqlconnect);

            sqlcommand.ExecuteNonQuery();
            sqlconnect.Close();

            return;
        }

        public bool UserExist_DB(string name, bool strict = true)
        {
            SqlConnection sqlconnect = new SqlConnection(connectionString);
            SqlCommand sqlcommand;
            try
            {
                sqlconnect.Open();
            }
            catch
            {
                string ipaddress = new WebClient().DownloadString("http://icanhazip.com").Replace("\\r\\n", "").Replace("\\n", "").Trim();
                //var externalIp = IPAddress.Parse(externalIpString);
                MessageBox.Show("Could not connect to the SQL Database. You are likely outside of the secure network! [" + ipaddress + "]");
                return false;
            }

            if (strict)
            {
                sqlcommand = new SqlCommand(@"SELECT COUNT(*) FROM Employee WHERE FullName='" + name + @"';", sqlconnect);
            }
            else
            {
                sqlcommand = new SqlCommand(@"SELECT COUNT(*) FROM Employee WHERE FullName LIKE '%" + name + @"%';", sqlconnect);
            }
            int count = (int)sqlcommand.ExecuteScalar();

            //sqlconnect.Close();
            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool EmailExist_DB(string email, bool strict = true)
        {
            SqlConnection sqlconnect = new SqlConnection(connectionString);
            SqlCommand sqlcommand;
            sqlconnect.Open();



            if (strict)
            {   // default...
                sqlcommand = new SqlCommand(@"SELECT COUNT(*) FROM Employee WHERE Email='" + email + @"' AND Status='active';", sqlconnect);
            }
            else
            {   //idk if this is used...
                sqlcommand = new SqlCommand(@"SELECT COUNT(*) FROM Employee WHERE Email LIKE '%" + email + @"%';", sqlconnect);
            }



            int count = (int)sqlcommand.ExecuteScalar();
            //sqlconnect.Close();



            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public Person[] QueryUserByName_DB(string name, bool strict = false)
        {
            string[] splitName = new string[2];
            splitName = name.Split(" ".ToCharArray(), 2, StringSplitOptions.None);

            //lv_search.Items.Clear();
            Person[] people = new Person[500];
            SqlConnection sqlconnect = new SqlConnection(connectionString);
            SqlCommand sqlcommand;

            if (strict)
            {
                sqlcommand = new SqlCommand(@"SELECT Id, FullName, EmployeeNumber, Status FROM Employee WHERE FullName='" + name + @"';", sqlconnect);
            }
            else //MESSY
            {
                if (splitName.Length > 1)
                {
                    sqlcommand = new SqlCommand(@"SELECT * FROM Employee 
                                                WHERE FullName LIKE '%" + splitName[0] + "% %" + splitName[1] + "%';", sqlconnect);
                }
                else
                {
                    {
                        sqlcommand = new SqlCommand(@"SELECT *
                                            FROM Employee WHERE FullName LIKE '%" + name + "%';", sqlconnect);
                    }
                }

            }
            
            sqlconnect.Open();

            SqlDataReader reader = sqlcommand.ExecuteReader();

            int i = 0;
            while (reader.HasRows)
            {
                while (reader.Read())
                {
                    Person person = new Person();
                    person.tableID = reader.GetInt32(reader.GetOrdinal("Id")).ToString();
                    if (!reader.IsDBNull(reader.GetOrdinal("FullName"))) { person.fullname = reader.GetString(reader.GetOrdinal("FullName")); }
                    if (!reader.IsDBNull(reader.GetOrdinal("EmployeeNumber"))) { person.employeenumber = reader.GetString(reader.GetOrdinal("EmployeeNumber")); }
                    if (!reader.IsDBNull(reader.GetOrdinal("Store"))) { person.store = reader.GetString(reader.GetOrdinal("Store")); }
                    if (!reader.IsDBNull(reader.GetOrdinal("Role"))) { person.role = reader.GetString(reader.GetOrdinal("Role")); }
                    if (!reader.IsDBNull(reader.GetOrdinal("Department"))) { person.department = reader.GetString(reader.GetOrdinal("Department")); }
                    if (!reader.IsDBNull(reader.GetOrdinal("Status"))) { person.status = reader.GetString(reader.GetOrdinal("Status")); }

                    if (!reader.IsDBNull(reader.GetOrdinal("DateModify"))) { person.modifyDate = reader.GetValue(reader.GetOrdinal("DateModify")).ToString(); }
                    if (!reader.IsDBNull(reader.GetOrdinal("DateRemove"))) { person.removeDate = reader.GetValue(reader.GetOrdinal("DateRemove")).ToString(); }
                    if (!reader.IsDBNull(reader.GetOrdinal("DateSetup"))) { person.addDate = reader.GetValue(reader.GetOrdinal("DateSetup")).ToString(); }



                    if (!reader.IsDBNull(reader.GetOrdinal("Email"))) { person.email.username = reader.GetString(reader.GetOrdinal("Email")); }
                    try { person.email.password = reader.GetString(reader.GetOrdinal("Email.password")); } catch { }
                    if (!reader.IsDBNull(reader.GetOrdinal("DealerTrack"))) { person.dealertrack.username = reader.GetString(reader.GetOrdinal("DealerTrack")); }
                    try { person.dealertrack.password = reader.GetString(reader.GetOrdinal("DealerTrack.password")); } catch { }
                    if (!reader.IsDBNull(reader.GetOrdinal("Reynolds"))) { person.reynolds.username = reader.GetString(reader.GetOrdinal("Reynolds")); }
                    try { person.reynolds.password = reader.GetString(reader.GetOrdinal("Reynolds.password")); } catch { }
                    if (!reader.IsDBNull(reader.GetOrdinal("NNANet"))) { person.nnanet.username = reader.GetString(reader.GetOrdinal("NNANet")); }
                    try { person.nnanet.password = reader.GetString(reader.GetOrdinal("NNANet.password")); } catch { }
                    if (!reader.IsDBNull(reader.GetOrdinal("DealerConnect"))) { person.dealerconnect.username = reader.GetString(reader.GetOrdinal("DealerConnect")); }
                    try { person.dealerconnect.password = reader.GetString(reader.GetOrdinal("DealerConnect.password")); } catch { }
                    if (!reader.IsDBNull(reader.GetOrdinal("GMGlobal"))) { person.gmglobal.username = reader.GetString(reader.GetOrdinal("GMGlobal")); }
                    try { person.gmglobal.password = reader.GetString(reader.GetOrdinal("GMGlobal.password")); } catch { }
                    if (!reader.IsDBNull(reader.GetOrdinal("HyundaiDealer"))) { person.hyundaidealer.username = reader.GetString(reader.GetOrdinal("HyundaiDealer")); }
                    try { person.hyundaidealer.password = reader.GetString(reader.GetOrdinal("HyundaiDealer.password")); } catch { }
                    if (!reader.IsDBNull(reader.GetOrdinal("KDealer"))) { person.kdealer.username = reader.GetString(reader.GetOrdinal("KDealer")); }
                    try { person.kdealer.password = reader.GetString(reader.GetOrdinal("KDealer.password")); } catch { }
                    if (!reader.IsDBNull(reader.GetOrdinal("HDNetReno"))) { person.hdnet.username = reader.GetString(reader.GetOrdinal("HDNetReno")); }
                    try { person.hdnet.password = reader.GetString(reader.GetOrdinal("HDNetRenoPassword")); } catch { }
                    if (!reader.IsDBNull(reader.GetOrdinal("HDNetYuba"))) { person.hdnet1.username = reader.GetString(reader.GetOrdinal("HDNetYuba")); }
                    try { person.hdnet1.password = reader.GetString(reader.GetOrdinal("HDNetYubaPassword")); } catch { }
                    if (!reader.IsDBNull(reader.GetOrdinal("HDNetRedwood"))) { person.hdnet2.username = reader.GetString(reader.GetOrdinal("HDNetRedwood")); }
                    try { person.hdnet2.password = reader.GetString(reader.GetOrdinal("HDNetRedwoodPassword")); } catch { }
                    /*
                    if (!reader.IsDBNull(reader.GetOrdinal("HDNet3"))) { person.hdnet2.username = reader.GetString(reader.GetOrdinal("HDNet3")); }
                    try { person.hdnet3.password = reader.GetString(reader.GetOrdinal("HDNet4.password")); } catch { }
                    */
                    if (!reader.IsDBNull(reader.GetOrdinal("VCC"))) { person.vcc.username = reader.GetString(reader.GetOrdinal("VCC")); }
                    try { person.vcc.password = reader.GetString(reader.GetOrdinal("VCC.password")); } catch { }
                    if (!reader.IsDBNull(reader.GetOrdinal("MXConnect"))) { person.mxconnect.username = reader.GetString(reader.GetOrdinal("MXConnect")); }
                    try { person.mxconnect.password = reader.GetString(reader.GetOrdinal("MXConnect.password")); } catch { }
                    if (!reader.IsDBNull(reader.GetOrdinal("CUDL"))) { person.cudl.username = reader.GetString(reader.GetOrdinal("CUDL")); }
                    try { person.cudl.password = reader.GetString(reader.GetOrdinal("CUDL.password")); } catch { }
                    if (!reader.IsDBNull(reader.GetOrdinal("Office365"))) { person.office365.username = reader.GetString(reader.GetOrdinal("Office365")); }
                    try { person.office365.password = reader.GetString(reader.GetOrdinal("Office365.password")); } catch { }
                    try
                    {
                        people[i] = person;
                    }
                    catch { }
                    //MessageBox.Show(person.fullname + " INSTANCE: " + i.ToString() + "  " + people[i].fullname.ToString());
                    i++;
                }
                reader.NextResult();
            }
            //sqlconnect.Close();
            return people;
        }
        public Person[] QueryUserListByName_DB(string name)
        {
            string[] splitName = new string[2];
            splitName = name.Split(" ".ToCharArray(), 2, StringSplitOptions.None);

            //lv_search.Items.Clear();
            Person[] people = new Person[500];
            SqlConnection sqlconnect = new SqlConnection(connectionString);
            SqlCommand sqlcommand;

            // Handles first, middle, last name, etc...
            if (splitName.Length > 1)
            {
                sqlcommand = new SqlCommand(@"SELECT Id, FullName, EmployeeNumber, Status FROM Employee 
                                            WHERE FullName LIKE '%" + splitName[0] + "% %" + splitName[1] + "%';", sqlconnect);
            }
            else
            {
                {
                    sqlcommand = new SqlCommand(@"SELECT Id, FullName, EmployeeNumber, Status FROM Employee
                                                WHERE FullName LIKE '%" + name + "%';", sqlconnect);
                }
            }

            sqlconnect.Open();
            SqlDataReader reader = sqlcommand.ExecuteReader();
            int i = 0;
            while (reader.HasRows)
            {
                while (reader.Read())
                {
                    Person person = new Person();
                    person.tableID = reader.GetInt32(reader.GetOrdinal("Id")).ToString();
                    if (!reader.IsDBNull(reader.GetOrdinal("FullName"))) { person.fullname = reader.GetString(reader.GetOrdinal("FullName")); }
                    if (!reader.IsDBNull(reader.GetOrdinal("EmployeeNumber"))) { person.employeenumber = reader.GetString(reader.GetOrdinal("EmployeeNumber")); }
                    try { person.status = reader.GetString(reader.GetOrdinal("Status")); } catch { }
                    try
                    {
                        people[i] = person;
                    }
                    catch { }
                    //MessageBox.Show(person.fullname + " INSTANCE: " + i.ToString() + "  " + people[i].fullname.ToString());
                    i++;
                }
                reader.NextResult();
            }
            //sqlconnect.Close();
            return people;
        }

        public void QueryUserByEmployeeNumber_DB(string employeeNumber, ListView lv_search)
        {
            SqlConnection sqlconnect = new SqlConnection(connectionString);
            SqlCommand sqlcommand;
            sqlcommand = new SqlCommand(@"SELECT FullName FROM Employee WHERE EmployeeNumber='" + employeeNumber + @"';", sqlconnect);
            sqlconnect.Open();
            SqlDataReader reader = sqlcommand.ExecuteReader();
            while (reader.HasRows)
            {
                while (reader.Read())
                {

                    string results = reader.GetString(0);
                    lv_search.Items.Add(results + " #" + employeeNumber).ImageIndex = 0;
                }
                reader.NextResult();
            }
            //sqlconnect.Close();
        }
    }
}

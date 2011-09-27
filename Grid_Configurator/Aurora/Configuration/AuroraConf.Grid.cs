    
    using System;
    using System.IO;
    using System.Net;
    using System.Text;
    using OpenMetaverse;

    namespace Aurora.Configuration
{
    public class Configure
    {
        private static string dbPasswd = "aurora";
        private static string dbSchema = "aurora";
        private static string dbUser = "aurora";
        private static string ipAddress = "127.0.0.1";
        private static bool auroraReconfig = false;
        private static string platform = "1";
        private static string worldName = "Aurora-Sim";
        private static string regionFlag = "Aurora";

        private static void CheckAuroraConfigHostName()
        {
            if (System.IO.File.Exists("Aurora.ini"))
            {
                try
                {
                    System.IO.File.Move("Aurora.ini", "Aurora.ini.old");
                }
                catch
                {
                }
                auroraReconfig = true;
            }
        }
        
        private static void CheckAuroraConfigData()
        {
            if (System.IO.File.Exists("Configuration/Data/Data.ini"))
            {
                try
                {
                    System.IO.File.Move("Configuration/Data/Data.ini", "Configuration/Data/Data.ini.old");
                }
                catch
                {
                }
                auroraReconfig = true;
            }
        }

        private static void CheckAuroraServerConfigData()
        {
            if (System.IO.File.Exists("AuroraServerConfiguration/Data/Data.ini"))
            {
                try
                {
                    System.IO.File.Move("AuroraServerConfiguration/Data/Data.ini", "AuroraServerConfiguration/Data/Data.ini.old");
                }
                catch
                {
                }
                auroraReconfig = true;
            }
        }

        private static void CheckAuroraConfigMySql()
        {
            if (System.IO.File.Exists("Configuration/Data/MySql.ini"))
            {
                try
                {
                    System.IO.File.Move("Configuration/Data/MySql.ini", "Configuration/Data/MySql.ini.old");
                }
                catch
                {
                }
                auroraReconfig = true;
            }
        }

        private static void CheckAuroraServerMySQL()
        {
            if (System.IO.File.Exists("AuroraServerConfiguration/Data/MySQL.ini"))
            {
                try
                {
                    System.IO.File.Move("AuroraServerConfiguration/Data/MySQL.ini", "AuroraServerConfiguration/Data/MySQL.ini.old");
                }
                catch
                {
                }
                auroraReconfig = true;
            }
        }

        private static void CheckAuroraConfigMain()
        {
            if (System.IO.File.Exists("Configuration/Main.ini"))
            {
                try
                {
                    System.IO.File.Move("Configuration/Main.ini", "Configuration/Main.ini.old");
                }
                catch
                {
                }
                auroraReconfig = true;
            }
        }

        private static void CheckAuroraGridCommon()
        {
            if (System.IO.File.Exists("Configuration/Grid/AuroraGridCommon.ini"))
            {
                try
                {
                    System.IO.File.Move("Configuration/Grid/AuroraGridCommon.ini", "Configuration/Grid/AuroraGridCommon.ini.old");
                }
                catch
                {
                }
                auroraReconfig = true;
            }
        }

        private static void CheckAuroraLogin()
        {
            if (System.IO.File.Exists("AuroraServerConfiguration/Login.ini"))
            {
                try
                {
                    System.IO.File.Move("AuroraServerConfiguration/Login.ini", "AuroraServerConfiguration/Login.ini.old");
                }
                catch
                {
                }
                auroraReconfig = true;
            }
        }

        private static void CheckAuroraGridInfoService()
        {
            if (System.IO.File.Exists("AuroraServerConfiguration/GridInfoService.ini"))
            {
                try
                {
                    System.IO.File.Move("AuroraServerConfiguration/GridInfoService.ini", "AuroraServerConfiguration/GridInfoService.ini.old");
                }
                catch
                {
                }
                auroraReconfig = true;
            }
        }

        private static void CheckAuroraAutoConfiguration()
        {
            if (System.IO.File.Exists("AuroraServerConfiguration/AutoConfiguration.ini"))
            {
                try
                {
                    System.IO.File.Move("AuroraServerConfiguration/AutoConfiguration.ini", "AuroraServerConfiguration/AutoConfiguration.ini.old");
                }
                catch
                {
                }
                auroraReconfig = true;
            }
        }

        private static void CheckAuroraServer()
        {
            if (System.IO.File.Exists("AuroraServer.ini"))
            {
                try
                {
                    System.IO.File.Move("AuroraServer.ini", "AuroraServer.ini.old");
                }
                catch
                {
                }
                auroraReconfig = true;
            }
        }

        private static void ConfigureAuroraini()
        {
            CheckAuroraConfigHostName();
            string str = string.Format("Define-<HostName> = \"{0}\"", ipAddress);
            try
            {
                using (TextReader reader = new StreamReader("Aurora.ini.example"))
                {
                    using (TextWriter writer = new StreamWriter("Aurora.ini"))
                    {
                        string str2;
                        while ((str2 = reader.ReadLine()) != null)
                        {
                            if (str2.Contains("Define-<HostName>"))
                            {
                                str2 = str;
                            }
                            if (str2.Contains("127.0.0.1"))
                            {
                                str2 = str2.Replace("127.0.0.1", ipAddress);
                            }
                            if (str2.Contains("NoGUI = false") && platform.Equals("2"))
                            {
                                str2 = str2.Replace("NoGUI = false", "NoGUI = true");
                            }
                            if (str2.Contains("Default = RegionLoaderDataBaseSystem") && platform.Equals("2"))
                            {
                                str2 = str2.Replace("Default = RegionLoaderDataBaseSystem", "Default = RegionLoaderFileSystem");
                            }
                            if (str2.Contains("RegionLoaderDataBaseSystem_Enabled = true") && platform.Equals("2"))
                            {
                                str2 = str2.Replace("RegionLoaderDataBaseSystem_Enabled = true", "RegionLoaderDataBaseSystem_Enabled = false");
                            }
                            if (str2.Contains("RegionLoaderFileSystem_Enabled = false") && platform.Equals("2"))
                            {
                                str2 = str2.Replace("RegionLoaderFileSystem_Enabled = false", "RegionLoaderFileSystem_Enabled = true");
                            }
                            if (str2.Contains("RegionLoaderWebServer_Enabled = true") && platform.Equals("2"))
                            {
                                str2 = str2.Replace("RegionLoaderWebServer_Enabled = true", "RegionLoaderWebServer_Enabled = false");
                            }
                            writer.WriteLine(str2);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error configuring Aurora.ini " + exception.Message);
                return;
            }
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Your Aurora.ini has been successfully configured");
        }

        private static void ConfigureAuroraServer()
        {
            CheckAuroraServer();
            string str = string.Format("Define-<HostName> = \"{0}\"", ipAddress);
            try
            {
                using (TextReader reader = new StreamReader("AuroraServer.ini.example"))
                {
                    using (TextWriter writer = new StreamWriter("AuroraServer.ini"))
                    {
                        string str2;
                        while ((str2 = reader.ReadLine()) != null)
                        {
                            if (str2.Contains("Define-<HostName>"))
                            {
                                str2 = str;
                            }
                            if (str2.Contains("127.0.0.1"))
                            {
                                str2 = str2.Replace("127.0.0.1", ipAddress);
                            }
                            if (str2.Contains("Region_RegionName ="))
                            {
                                str2 = str2.Replace("Region_RegionName =", "Region_" + regionFlag.Replace(' ', '_') + " =");
                            }
                            writer.WriteLine(str2);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error configuring AuroraServer.ini " + exception.Message);
                return;
            }
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Your AuroraServer.ini has been successfully configured");
        }

        private static void ConfigureAuroraData()
        {
            CheckAuroraConfigData();
            string str = string.Format("Include-SQLite =", ipAddress);
            try
            {
                using (TextReader reader = new StreamReader("Configuration/Data/Data.ini.example"))
                {
                    using (TextWriter writer = new StreamWriter("Configuration/Data/Data.ini"))
                    {
                        string str2;
                        while ((str2 = reader.ReadLine()) != null)
                        {
                            if (str2.Contains("Include-SQLite = Configuration/Data/SQLite.ini"))
                            {
                                str2 = str2.Replace("Include-SQLite = Configuration/Data/SQLite.ini", ";Include-SQLite = Configuration/Data/SQLite.ini");
                            }
                            if (str2.Contains(";Include-MySQL = Configuration/Data/MySQL.ini"))
                            {
                                str2 = str2.Replace(";Include-MySQL = Configuration/Data/MySQL.ini", "Include-MySQL = Configuration/Data/MySQL.ini");
                            }
                            
                            writer.WriteLine(str2);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error configuring Data.ini " + exception.Message);
                return;
            }
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Your Data.ini has been successfully configured");
        }

        private static void ConfigureAuroraServerData()
        {
            CheckAuroraServerConfigData();
            string str = string.Format("Include-SQLite =", ipAddress);
            try
            {
                using (TextReader reader = new StreamReader("AuroraServerConfiguration/Data/Data.ini.example"))
                {
                    using (TextWriter writer = new StreamWriter("AuroraServerConfiguration/Data/Data.ini"))
                    {
                        string str2;
                        while ((str2 = reader.ReadLine()) != null)
                        {
                            if (str2.Contains("Include-SQLite = AuroraServerConfiguration/Data/SQLite.ini"))
                            {
                                str2 = str2.Replace("Include-SQLite = AuroraServerConfiguration/Data/SQLite.ini", ";Include-SQLite = AuroraServerConfiguration/Data/SQLite.ini");
                            }
                            if (str2.Contains(";Include-MySQL = AuroraServerConfiguration/Data/MySQL.ini"))
                            {
                                str2 = str2.Replace(";Include-MySQL = AuroraServerConfiguration/Data/MySQL.ini", "Include-MySQL = AuroraServerConfiguration/Data/MySQL.ini");
                            }

                            writer.WriteLine(str2);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error configuring AuroraServer Data.ini " + exception.Message);
                return;
            }
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Your AuroraServer Data.ini has been successfully configured");
        }

        private static void ConfigureAuroraMain()
        {
            CheckAuroraConfigMain();
            string str = string.Format("Include-Standalone = ", ipAddress);
            try
            {
                using (TextReader reader = new StreamReader("Configuration/Main.ini.example"))
                {
                    using (TextWriter writer = new StreamWriter("Configuration/Main.ini"))
                    {
                        string str2;
                        while ((str2 = reader.ReadLine()) != null)
                        {
                            if (str2.Contains("Include-Standalone = Configuration/Standalone/StandaloneCommon.ini"))
                            {
                                str2 = str2.Replace("Include-Standalone = Configuration/Standalone/StandaloneCommon.ini", ";Include-Standalone = Configuration/Standalone/StandaloneCommon.ini");
                            }
                            if (str2.Contains(";Include-Grid = Configuration/Grid/AuroraGridCommon.ini"))
                            {
                                str2 = str2.Replace(";Include-Grid = Configuration/Grid/AuroraGridCommon.ini", "Include-Grid = Configuration/Grid/AuroraGridCommon.ini");
                            }

                            writer.WriteLine(str2);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error configuring Main.ini " + exception.Message);
                return;
            }
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Your Main.ini has been successfully configured");
        }

        private static void ConfigureAuroraGridCommon()
        {
            CheckAuroraGridCommon();
            string str = string.Format("RegistrationURI = ", ipAddress);
            try
            {
                using (TextReader reader = new StreamReader("Configuration/Grid/AuroraGridCommon.ini.example"))
                {
                    using (TextWriter writer = new StreamWriter("Configuration/Grid/AuroraGridCommon.ini"))
                    {
                        string str2;
                        while ((str2 = reader.ReadLine()) != null)
                        {
                            if (str2.Contains("127.0.0.1"))
                            {
                                str2 = str2.Replace("127.0.0.1", ipAddress);
                            }
                            
                            writer.WriteLine(str2);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error configuring AuroraGridCommon.ini " + exception.Message);
                return;
            }
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Your AuroraGridCommon.ini has been successfully configured");
        }

        private static void ConfigureAuroraMySql()
        {
            CheckAuroraConfigMySql();
            string str = string.Format("ConnectionString = \"Data Source=localhost;Database={0};User ID={1};Password={2};\"", dbSchema, dbUser, dbPasswd);
            try
            {
                using (TextReader reader = new StreamReader("Configuration/Data/MySql.ini.example"))
                {
                    using (TextWriter writer = new StreamWriter("Configuration/Data/MySql.ini"))
                    {
                        string str2;
                        while ((str2 = reader.ReadLine()) != null)
                        {
                            if (str2.Contains("Database=opensim;User ID=opensim;Password=***;"))
                            {
                                str2 = str2.Replace("Database=opensim;User ID=opensim;Password=***;", "Database="+dbSchema+";User ID="+dbUser+";Password="+dbPasswd+";");
                            }
                            
                            writer.WriteLine(str2);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error configuring MySql.ini " + exception.Message);
                return;
            }
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Your MySql.ini has been successfully configured");
        }

        private static void ConfigureAuroraServerMySQL()
        {
            CheckAuroraServerMySQL();
            string str = string.Format("ConnectionString = \"Data Source=localhost;Database={0};User ID={1};Password={2};\"", dbSchema, dbUser, dbPasswd);
            try
            {
                using (TextReader reader = new StreamReader("AuroraServerConfiguration/Data/MySQL.ini.example"))
                {
                    using (TextWriter writer = new StreamWriter("AuroraServerConfiguration/Data/MySQL.ini"))
                    {
                        string str2;
                        while ((str2 = reader.ReadLine()) != null)
                        {
                            if (str2.Contains("Database=opensim;User ID=opensim;Password=***;"))
                            {
                                str2 = str2.Replace("Database=opensim;User ID=opensim;Password=***;", "Database=" + dbSchema + ";User ID=" + dbUser + ";Password=" + dbPasswd + ";");
                            }

                            writer.WriteLine(str2);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error configuring AuroraServer MySQL.ini " + exception.Message);
                return;
            }
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Your AuroraServer MySQL.ini has been successfully configured");
        }

        private static void ConfigureAuroraLogin()
        {
            CheckAuroraLogin();
            string str = string.Format("DefaultHomeRegion =", regionFlag);
            try
            {
                using (TextReader reader = new StreamReader("AuroraServerConfiguration/Login.ini.example"))
                {
                    using (TextWriter writer = new StreamWriter("AuroraServerConfiguration/Login.ini.ini"))
                    {
                        string str2;
                        while ((str2 = reader.ReadLine()) != null)
                        {
                            if (str2.Contains("Welcome to Aurora Simulator"))
                            {
                                str2 = str2.Replace("Welcome to Aurora Simulator", "Welcome to " + worldName);
                            }
                            if (str2.Contains("AllowAnonymousLogin = false"))
                            {
                                str2 = str2.Replace("AllowAnonymousLogin = false", "AllowAnonymousLogin = true");
                            }
                            if (str2.Contains("DefaultHomeRegion = "))
                            {
                                str2 = str2.Replace("DefaultHomeRegion = \"\"", "DefaultHomeRegion = \"" + regionFlag + "\"");
                            }
                            writer.WriteLine(str2);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error configuring Login.ini " + exception.Message);
                Console.ResetColor();
                return;
            }
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Your Login.ini has been successfully configured");
        }

        private static void ConfigureAuroraGridInfoService()
        {
            CheckAuroraGridInfoService();
            string str = string.Format("DefaultHomeRegion =", regionFlag);
            try
            {
                using (TextReader reader = new StreamReader("AuroraServerConfiguration/GridInfoService.ini.example"))
                {
                    using (TextWriter writer = new StreamWriter("AuroraServerConfiguration/GridInfoService.ini.ini"))
                    {
                        string str2;
                        while ((str2 = reader.ReadLine()) != null)
                        {
                            if (str2.Contains("127.0.0.1"))
                            {
                                str2 = str2.Replace("127.0.0.1", ipAddress);
                            }
                            if (str2.Contains("the lost continent of hippo"))
                            {
                                str2 = str2.Replace("the lost continent of hippo", worldName);
                            }
                            if (str2.Contains("hippogrid"))
                            {
                                str2 = str2.Replace("hippogrid", worldName);
                            }
                            writer.WriteLine(str2);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error configuring GridInfoService.ini " + exception.Message);
                Console.ResetColor();
                return;
            }
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Your GridInfoService.ini has been successfully configured");
        }

        private static void ConfigureAuroraAutoConfiguration()
        {
            CheckAuroraServer();
            string str = string.Format("Define-<HostName> = \"{0}\"", ipAddress);
            try
            {
                using (TextReader reader = new StreamReader("AuroraServerConfiguration/AutoConfiguration.ini.example"))
                {
                    using (TextWriter writer = new StreamWriter("AuroraServerConfiguration/AutoConfiguration.ini"))
                    {
                        string str2;
                        while ((str2 = reader.ReadLine()) != null)
                        {
                            if (str2.Contains("127.0.0.1"))
                            {
                                str2 = str2.Replace("127.0.0.1", ipAddress);
                            }
                            writer.WriteLine(str2);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error configuring AutoConfiguration.ini " + exception.Message);
                return;
            }
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Your AutoConfiguration.ini has been successfully configured");
        }

        private static void DisplayInfo()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n***************************************************");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Your world is " + worldName);
            Console.WriteLine("Your loginuri is http://" + ipAddress + ":8002/");
            Console.WriteLine("If you want other people connecting their regions to your grid, this is the Registration URL: http://" + ipAddress + ":8003/");
            Console.WriteLine("Once your Aurora.exe is started, you can create your avatar using the Firstname/Lastname/Password you wish directly in the viewer. Yes : this will create your account !");
            Console.WriteLine("Now start AuroraServer.exe then Aurora.exe and use this name for your Welcome Land:" + regionFlag);
            
            if (auroraReconfig)
            {
                Console.WriteLine("\nNOTE: Aurora-Sim has been reconfigured as Grid Mode.\nPrevious configurations are marked *.old.\nPlease revise the new configurations.\n");
            }
            else
            {
                Console.WriteLine("Your Aurora-Sim's configuration is complete.\nPlease revise it.");
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("***************************************************\n");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("<Aurora-Sim Configurator v.0.1 by Rico - Press enter to exit>");
            Console.ReadLine();
        }

        private static void GetUserInput()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("***************************************************\n");
            Console.WriteLine("**            Aurora-Sim Configurator            **\n");
            Console.WriteLine("**                  Grid mode                    **\n");
            Console.WriteLine("***************************************************\n");
            Console.ResetColor();
            Console.Write("Name of your Aurora-Sim: ");
            worldName = Console.ReadLine();
            if (worldName == string.Empty)
            {
                worldName = "My Aurora";
            }
            else
            {
                worldName = worldName.Trim();
            }
            Console.Write("MySql database name: [aurora]");
            string str = Console.ReadLine();
            if (str != string.Empty)
            {
                dbSchema = str;
            }
            Console.Write("MySql database user account: [aurora]");
            str = Console.ReadLine();
            if (str != string.Empty)
            {
                dbUser = str;
            }
            Console.Write("MySql database password for that account: ");
            dbPasswd = Console.ReadLine();
            Console.Write("Your external domain name (preferred) or IP address: ");
            ipAddress = Console.ReadLine();
            if (ipAddress == string.Empty)
            {
                ipAddress = "127.0.0.1";
            }
            Console.Write("The name you will use for your Welcome Land: ");
            regionFlag = Console.ReadLine();
            if (regionFlag == string.Empty)
            {
                regionFlag = "Aurora";
            }
            Console.Write("This installation is going to run on \n [1] .NET/Windows \n [2] *ix/Mono \nChoose 1 or 2 [1]: ");
            platform = Console.ReadLine();
            if (platform == string.Empty)
            {
                platform = "1";
            }
            platform = platform.Trim();
           
        }

        public static void Main(string[] args)
        {
            GetUserInput();
            ConfigureAuroraini();
            ConfigureAuroraData();
            ConfigureAuroraMySql();
            ConfigureAuroraMain();
            ConfigureAuroraGridCommon();
            ConfigureAuroraServer();
            ConfigureAuroraServerData();
            ConfigureAuroraServerMySQL();
            ConfigureAuroraLogin();
            ConfigureAuroraGridInfoService();
            ConfigureAuroraAutoConfiguration();
            DisplayInfo();
        }

        
    }
}
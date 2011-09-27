/*
 * Copyright (c) Contributors, http://world.4d-web.eu , http://aurora-sim.org/
 * See CONTRIBUTORS.TXT for a full list of copyright holders.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *     * Redistributions of source code must retain the above copyright
 *       notice, this list of conditions and the following disclaimer.
 *     * Redistributions in binary form must reproduce the above copyright
 *       notice, this list of conditions and the following disclaimer in the
 *       documentation and/or other materials provided with the distribution.
 *     * Neither the name of the Aurora-Sim Project nor the name of Nova Project nor the
 *       names of its contributors may be used to endorse or promote products
 *       derived from this software without specific prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE DEVELOPERS ``AS IS'' AND ANY
 * EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED. IN NO EVENT SHALL THE CONTRIBUTORS BE LIABLE FOR ANY
 * DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
 * (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
 * LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
 * ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
 * SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */
﻿    
    using System;
    using System.IO;
    using System.Net;
    using System.Text;
    

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

        private static void CheckAuroraConfigCommon()
        {
            if (System.IO.File.Exists("Configuration/Standalone/StandaloneCommon.ini"))
            {
                try
                {
                    System.IO.File.Move("Configuration/Standalone/StandaloneCommon.ini", "Configuration/Standalone/StandaloneCommon.ini.old");
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
            Console.WriteLine("Your Aurora.ini has been successfully configured");
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
            Console.WriteLine("Your Data.ini has been successfully configured");
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
            Console.WriteLine("Your MySql.ini has been successfully configured");
        }
        
        private static void ConfigureAuroraCommon()
        {
            CheckAuroraConfigCommon();
            string str = string.Format("Region_Aurora =", regionFlag);
            try
            {
                using (TextReader reader = new StreamReader("Configuration/Standalone/StandaloneCommon.ini.example"))
                {
                    using (TextWriter writer = new StreamWriter("Configuration/Standalone/StandaloneCommon.ini"))
                    {
                        string str2;
                        while ((str2 = reader.ReadLine()) != null)
                        {
                            if (str2.Contains("Region_Aurora ="))
                            {
                                str2 = str2.Replace("Region_Aurora =", "Region_" + regionFlag.Replace(' ', '_') + " =");
                            }
                            if (str2.Contains("127.0.0.1"))
                            {
                                str2 = str2.Replace("127.0.0.1", ipAddress);
                            }
                            if (str2.Contains("My Aurora Simulator"))
                            {
                                str2 = str2.Replace("My Aurora Simulator", worldName);
                            }
                            if (str2.Contains("AuroraSim"))
                            {
                                str2 = str2.Replace("AuroraSim", worldName);
                            }
                            if (str2.Contains("Welcome to Aurora Simulator"))
                            {
                                str2 = str2.Replace("Welcome to Aurora Simulator", "Welcome to "+worldName);
                            }
                            if (str2.Contains("AllowAnonymousLogin = false"))
                            {
                                str2 = str2.Replace("AllowAnonymousLogin = false", "AllowAnonymousLogin = true");
                            }
                            if (str2.Contains("DefaultHomeRegion = "))
                            {
                                str2 = str2.Replace("DefaultHomeRegion = \"\"", "DefaultHomeRegion = \""+regionFlag+"\"");
                            }
                            writer.WriteLine(str2);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error configuring StandaloneCommon.ini " + exception.Message);
                Console.ResetColor();
                return;
            }
            Console.WriteLine("Your StandaloneCommon.ini has been successfully configured");
        }
        
        private static void DisplayInfo()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n***************************************************");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Your world is " + worldName);
            Console.WriteLine("Your loginuri is http://" + ipAddress + ":9000/");
            Console.WriteLine("Once your Aurora.exe is started, you can create your avatar using the Firstname/Lastname/Password you wish directly in the viewer. Yes : this will create your account !");
            Console.WriteLine("Now start Aurora.exe and use this name for your Welcome Land:" + regionFlag);
            
            if (auroraReconfig)
            {
                Console.WriteLine("\nNOTE: Aurora-Sim has been reconfigured as Standalone.\nPrevious configurations are marked *.old.\nPlease revise the new configurations.\n");
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
            Console.WriteLine("**                Standalone mode                **\n");
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
            ConfigureAuroraCommon();
            DisplayInfo();
        }

        
    }
}
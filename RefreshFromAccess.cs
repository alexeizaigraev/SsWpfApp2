using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SsWpfApp2
{
    internal class RefreshFromAccess : Papa
    {
        internal static void MainRefreshFromAccess()
        {
            ClearInfo();
            Process proc = new Process();
            try
            {
                //SayBlue("\nSharpForPy start...\n");
                proc.StartInfo.FileName = @"C:\SharpForPy\SharpForPy.exe";
                proc.Start();
                proc.WaitForExit();
                SayBlue("\nSharpForPy finish\n");
            }
            catch (Exception ex) { SayRed("\nno start SgsarpForPy\n"); }
            finally
            {
                try { proc.Kill(); }
                catch { }
            }

            DbOtborMet.RefreshOtborFromFile();
            //Say("otbor refreshed");
            DbDepMeth.RefreshDepartment();
            //Say("dep refreshed");
            DbTermMeth.RefreshTerminal();
            //Say("term refreshed");
            Say("\n\tall refreshed");
        }
    }
}

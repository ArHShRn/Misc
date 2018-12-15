//=============================================================================
// Custom Maid 3D 2 x64 Launcher
// Introduction :
//  CM3D2x64_Launcher.exe will auto-detect environment 
//   and launch CM3D2 conveniently and silently.
// 
// Code And Concept By ArHShRn
// https://github.com/ArHShRn
//
// Release Log :
//  Add comments.
//
// Last Update :
//  Dec.15th 2018
//=============================================================================
using Microsoft.Win32;
using System;
using System.IO;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace CM3D2x64_Laucher
{
    static class Launcher
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Registry Hive
            RegistryKey hkcu = Registry.CurrentUser;
            RegistryKey hkcr = Registry.ClassesRoot;
            RegistryKey kiss, ntlea;
            //Executable Path
            string cm3d_dir = "";
            string ntlea_dir = "";
            //Names
            string cm3dx64_exec = "CM3D2x64.exe";
            // current_dir.FullName = X:\CM3D2
            // Registry Key's InstallPath = X:\CM3D2\
            DirectoryInfo current_dir = new DirectoryInfo(Environment.CurrentDirectory);

            //Check launching directory
            var dExec = current_dir.GetFiles().Count(tar => tar.Name == cm3dx64_exec);
            if (dExec <= 0)
            {
                MessageBox.Show(
                    "Please launch me in CM3D2 directory =w=" +
                    Environment.NewLine + Environment.NewLine + "[Current Directory]" + Environment.NewLine + current_dir.FullName,
                    "[CM3D2 Launcher] Invalid Directory");
                return;
            }

            //Check cm3d2 registry keys
            try
            {
                kiss = hkcu.OpenSubKey(@"Software\KISS\カスタムメイド3D2", true);
                cm3d_dir = kiss.GetValue("InstallPath").ToString();

                //If the app is launched at different CM3D2 path instead of current CM3D2 path
                if (current_dir.FullName + @"\" != cm3d_dir)
                {
                    if (
                            MessageBox.Show
                            (
                                "[Attention]" + Environment.NewLine +
                                "If you continue to launch CM3D2 in this directory," + Environment.NewLine +
                                "the launcher will change CM3D2 installation path." + Environment.NewLine +
                                "Would you like to launch anyway?" + Environment.NewLine +
                                Environment.NewLine +
                                "[Launching Directory]" + Environment.NewLine +
                                current_dir.FullName + @"\" + Environment.NewLine +
                                "[Valid CM3D2 Directory]" + Environment.NewLine +
                                cm3d_dir,
                                "[CM3D2 Launcher] Select Directory"
                                , MessageBoxButtons.OKCancel
                            )
                            == DialogResult.OK
                        )
                    {
                        //Clicked OK
                        cm3d_dir = current_dir + @"\";
                        kiss.SetValue("InstallPath", cm3d_dir);
                    }
                    else
                    {
                        //Clicked Cancel
                        kiss.Close();
                        hkcu.Close();
                        return;
                    }
                }
                //If the app is launched at current CM3D2 path
                //  then close all opened registry keys and continue
                else
                {
                    kiss.Close();
                    hkcu.Close();
                }
            }
            catch (Exception ex)
            {
                //If it does not exist
                if (ex is NullReferenceException || cm3d_dir == null)
                {
                    kiss = hkcu.CreateSubKey(@"Software\KISS\カスタムメイド3D2");
                    kiss.SetValue("InstallPath", current_dir.FullName + @"\");
                    kiss.Close();
                    hkcu.Close();
                }
                else
                {
                    MessageBox.Show(
                        "A Strange Error Occured QAQ" + Environment.NewLine + Environment.NewLine +
                        "[Exception Message @ REGCHK]" + Environment.NewLine + ex.Message,
                        "[CM3D2 Launcher] Error");
                    return;
                }
            }

            //Check NTLEA availability
            try
            {
                //Try open
                ntlea = hkcr.OpenSubKey(@"CLSID\{9C31DD66-412C-4B28-BD17-1F0BEBE29E8B}\InprocServer32", false);
                ntlea_dir = ntlea.GetValue(null).ToString();
                var lastidx = ntlea_dir.LastIndexOf(@"\");
                ntlea_dir = ntlea_dir.Substring(0, lastidx + 1);
                ntlea.Close();
                hkcr.Close();
            }
            catch (Exception ex)
            {
                if (ex is NullReferenceException)
                {
                    MessageBox.Show(
                        "No NTLEA installation available !" + Environment.NewLine +
                        "Please re-check NTLEA registry status :" + Environment.NewLine + Environment.NewLine +
                        "1.Enter NTLEA's directory，launch [ntleasWin.exe]." + Environment.NewLine +
                        "2.Set parameters and click [-->] button at right bottom.",
                        "[CM3D2 Launcher] No NTLEA Detected"
                        );
                    hkcr.Close();
                    return;
                }
                else if (ex is System.Security.SecurityException || ex is UnauthorizedAccessException)
                {
                    MessageBox.Show(
                        "The launcher would check Registry Keys," + Environment.NewLine +
                        "please launch as Administrator." + Environment.NewLine + Environment.NewLine,
                        "[CM3D2 Launcher] Insufficient Authorization");
                    hkcr.Close();
                    return;
                }
                else
                {
                    MessageBox.Show(
                        "A Strange Error Occured QAQ" + Environment.NewLine + Environment.NewLine +
                        "[Exception Message @ NTLEA CHK]" + Environment.NewLine + ex.Message,
                        "[CM3D2 Launcher] Error");
                    hkcr.Close();
                    return;
                }
            }

            //Run Game
            cm3d_dir = cm3d_dir + @"CM3D2x64.exe";
            ntlea_dir = ntlea_dir + @"x64\ntleas.exe";
            try
            {
                Process proc = Process.Start(ntlea_dir, cm3d_dir + " \"C932\" \"L0411\"");
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "A Strange Error Occured QAQ" + Environment.NewLine + Environment.NewLine +
                    "[Exception Message @ LAUNCH]" + Environment.NewLine + ex.Message,
                    "[CM3D2 Launcher] Error");
                return;
            }
        }
    }
}

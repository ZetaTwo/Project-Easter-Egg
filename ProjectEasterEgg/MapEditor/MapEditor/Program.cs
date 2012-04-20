#region File Description
//-----------------------------------------------------------------------------
// Program.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Windows.Forms;
#endregion

namespace Mindstep.EasterEgg.MapEditor
{
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainForm mainForm;
            if (args.Length > 1)
            {
                mainForm = new MainForm(args[1]);
            }
            else
            {
                mainForm = new MainForm();
            }
            Application.Run(mainForm);
        }
    }
}

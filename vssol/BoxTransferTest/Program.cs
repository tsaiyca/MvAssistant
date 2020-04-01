﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoxTransferTest
{
    static class Program
    {

        public static ProgramMgr ProgMgr;
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            ProgMgr = new ProgramMgr();

            Application.Run(new FmMain());
        }
    }
}

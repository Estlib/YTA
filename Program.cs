//Copyright(C) 2026 Estlib

//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.

//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.

//You should have received a copy of the GNU General Public License
//along with this program.  If not, see <https://gnu.org>.
using YTA.DBDomain;

namespace YTA
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            try
            {
                DBInitializer.InitDB();
            }
            catch (Exception ex)
            {
                string message = $"Cannot create DB, see exact error below:\n\n{ex.Message}";
                string title = "Error";
                var result = MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Application.Run(new Form1());
        }
    }
}
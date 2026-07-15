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
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YTA.DBDomain.Models;

namespace YTA.DBDomain
{
    public class DBConfiguration : DbContext
    {
        private const string ConnectionString =
    @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\YTA-app.mdf;Initial Catalog=YTA-AppDb;Integrated Security=True;";
        public DbSet<YTContent> YTContents => Set<YTContent>();
        public DbSet<Prefab> Prefabs => Set<Prefab>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // TODO: optimise this shit later
            string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            string databaseFolder = Path.Combine(appDataFolder, "YTA");

            Directory.CreateDirectory(databaseFolder);

            string databasePath = Path.Combine(databaseFolder, "YTA-app.mdf");

            string connectionString =
                $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={databasePath};Initial Catalog=YTAAppDb;Integrated Security=True;";

            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}

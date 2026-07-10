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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YTA.DBDomain;
using YTA.DBDomain.Models;

namespace YTA.Controllers
{
    public class ContentController
    {
        /// <summary>
        /// Reads all entries, without filtering
        /// 
        /// </summary>
        /// <returns>a list of content entries as List<YTContent></returns>
        public List<YTContent> GetAll()
        {
            using DBConfiguration database = new DBConfiguration();
            List<YTContent> unfiltered = new List<YTContent>();
            try
            {
                unfiltered = database.YTContents.OrderByDescending(c => c.YTAHandleTime_CreatedAt).ToList();

            }
            catch (Exception ex)
            {
                string message = $"Cannot Connect to DB, see exact error below:\n\n{ex.Message}";
                string title = "Error";
                var result = MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (unfiltered.Count < 1)
            {
                return null;
            }
            return unfiltered;
        }
        /// <summary>
        /// Adds new entry into db
        /// </summary>
        /// <param name="content">content to adfd</param>
        public void Create(YTContent content)
        {
            //content.YTAHandleTime_CreatedAt = DateTime.Now;
            //content.YTAHandleTime_ModifiedAt = DateTime.Now;
            //content.YTAHandleTime_PassedToYTAt = DateTime.Now;
            if (content == null)
            {
                string message = $"Cannot save entry to DB, content is null";
                string title = "Error";
                var result = MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using DBConfiguration database = new DBConfiguration();
            try
            {
                database.YTContents.Add(content);
                database.SaveChanges();
            }
            catch (Exception ex)
            {
                string message = $"Cannot save entry to DB, see exact error below:\n\n{ex.Message}";
                string title = "Error";
                var result = MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Update(YTContent updatedEntry)
        {
            using DBConfiguration database = new DBConfiguration();
            database.YTContents.Update(updatedEntry);
            database.SaveChanges();
        }

        public void Delete(YTContent removeThisEntry)
        {
            using DBConfiguration database = new DBConfiguration();
            database.YTContents.Remove(removeThisEntry);
            database.SaveChanges();
        }

        public List<YTContent> GetRange(DateTime from, DateTime until)
        {
            using DBConfiguration database = new DBConfiguration();
            List<YTContent> result = database.YTContents
                .Where(c => 
                c.YTAHandleTime_PassedToYTAt >= from &&
                c.YTAHandleTime_PassedToYTAt < until)
                .OrderBy(x => x.YTAHandleTime_PassedToYTAt)
                .ToList();
            if (result == null || result.Count < 1)
            {
                string message = $"No entries found for selected range";
                string title = "Infos";
                var resultBox = MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return new List<YTContent>();
            }
            else
            {
                return result;
            }
        }

        public int CountUploaded()
        {
            using DBConfiguration database = new DBConfiguration();
            DateTime today = DateTime.Today;
            DateTime tomorrow = today.AddDays(1);
            return database.YTContents.Count
                (
                x => x.VideoID != null && x.VideoID != "" 
                && x.YTAHandleTime_PassedToYTAt >= today 
                && x.YTAHandleTime_PassedToYTAt < tomorrow
                );

        }

        public List<YTContent> GetUploadQueue(DateTime from, DateTime until)
        {
            using DBConfiguration database = new DBConfiguration();
            return database.YTContents.Where
                (
                x => string.IsNullOrEmpty(x.VideoID) 
                && x.YTAHandleTime_PassedToYTAt >= from 
                && x.YTAHandleTime_PassedToYTAt < until
                && (x.ThisMediaIs == MediaType.Video || x.ThisMediaIs == MediaType.Short))
                .OrderBy(x => x.YTAHandleTime_PassedToYTAt)
                .ToList();

        }
    }
}

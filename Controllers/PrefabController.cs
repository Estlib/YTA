using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YTA.DBDomain;
using YTA.DBDomain.Models;

namespace YTA.Controllers
{
    public class PrefabController
    {
        public void Create(Prefab newPrefab)
        {
            using DBConfiguration database = new DBConfiguration();
            if (newPrefab == null)
            {
                string message = $"Prefab is null";
                string title = "Error";
                var mbox = MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            newPrefab.ID = Guid.NewGuid();
            newPrefab.Prefab_CreatedAt = DateTime.Now;
            newPrefab.Prefab_ModifiedAt = DateTime.Now;
            try
            {
                database.Prefabs.Add(newPrefab);
                database.SaveChanges();
                string message = $"Prefab saved";
                string title = "Infos";
                var mbox = MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                string message = $"Save to db has failed. See details:\n\n{ex.Message}\n\nInner: {ex.InnerException.Message}";
                string title = "Error";
                var result = MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
        }

        public List<Prefab> GetAllPrefabs() 
        {
            using DBConfiguration database = new DBConfiguration();
            var result = database.Prefabs.OrderBy(x => x.PrefabName).ToList();
            return result;
        }

        public Prefab GetOneByID(Guid id)
        {
            using DBConfiguration database = new DBConfiguration();
            Prefab result = (Prefab)database.Prefabs.FirstOrDefault(x => x.ID == id);
            return result;
        }

        public void Update(Prefab updatePrefab, Guid id)
        {

            using DBConfiguration database = new DBConfiguration();
            if (updatePrefab == null)
            {
                string message = $"Prefab is null";
                string title = "Error";
                var mbox = MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            updatePrefab.ID = id;
            updatePrefab.Prefab_ModifiedAt = DateTime.Now;
            try
            {
                database.Prefabs.Update(updatePrefab);
                database.SaveChanges();
                string message = $"Prefab saved";
                string title = "Infos";
                var mbox = MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                string message = $"Save to db has failed. See details:\n\n{ex.Message}\n\nInner: {ex.InnerException.Message}";
                string title = "Error";
                var result = MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        public void Delete(Guid id)
        {
            if (id == null || id == Guid.Empty)
            {
                return;
            }
            else
            {
                using DBConfiguration database = new DBConfiguration();
                Prefab result = (Prefab)database.Prefabs.FirstOrDefault(x => x.ID == id);
                database.Prefabs.Remove(result);
                database.SaveChanges();
            }
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using work_tracker.Data;
using work_tracker.Data.Entities;

namespace work_tracker.Services
{
    public class WorkItemRelationService
    {
        private readonly WorkTrackerDbContext _context;

        public WorkItemRelationService(WorkTrackerDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Bir iş silinmeden önce ilişkileri yönetir
        /// </summary>
        /// <param name="workItemId">Silinecek iş ID'si</param>
        /// <param name="currentUser">İşlemi yapan kullanıcı</param>
        /// <returns>İşlem sonucu ve mesaj</returns>
        public (bool Success, string Message) HandleWorkItemDeletion(int workItemId, string currentUser)
        {
            try
            {
                var relations = _context.WorkItemRelations
                    .Where(r => r.WorkItemId1 == workItemId || r.WorkItemId2 == workItemId)
                    .Include(r => r.SourceWorkItem)
                    .Include(r => r.TargetWorkItem)
                    .ToList();

                if (!relations.Any())
                {
                    return (true, "İlişkili iş bulunamadı.");
                }

                var childRelations = relations.Where(r => 
                    r.WorkItemId1 == workItemId && r.RelationType == WorkItemRelationTypes.Parent).ToList();

                var siblingRelations = relations.Where(r => 
                    r.RelationType == WorkItemRelationTypes.Sibling).ToList();

                var parentRelation = relations.FirstOrDefault(r => 
                    r.WorkItemId2 == workItemId && r.RelationType == WorkItemRelationTypes.Parent);

                // Child işleri varsa, onların parent ilişkisini temizle
                if (childRelations.Any())
                {
                    foreach (var childRelation in childRelations)
                    {
                        // Child işin parent ilişkisini sil
                        _context.WorkItemRelations.Remove(childRelation);
                        
                        // Aktivite kaydı oluştur
                        CreateActivityForChildWorkItem(childRelation.WorkItemId2, 
                            $"Üst iş ({workItemId}) silindiği için parent ilişkisi temizlendi.", currentUser);
                    }
                }

                // Sibling ilişkileri varsa, çift yönlü olduğu için hepsini sil
                if (siblingRelations.Any())
                {
                    foreach (var siblingRelation in siblingRelations)
                    {
                        _context.WorkItemRelations.Remove(siblingRelation);
                        
                        // Kardeş iş için aktivite kaydı
                        var siblingWorkItemId = siblingRelation.WorkItemId1 == workItemId ? 
                            siblingRelation.WorkItemId2 : siblingRelation.WorkItemId1;
                        
                        CreateActivityForChildWorkItem(siblingWorkItemId, 
                            $"Kardeş iş ({workItemId}) silindiği için ilişki temizlendi.", currentUser);
                    }
                }

                // Parent ilişkisi varsa, sadece bu ilişkiyi sil
                if (parentRelation != null)
                {
                    _context.WorkItemRelations.Remove(parentRelation);
                }

                _context.SaveChanges();
                return (true, $"İlişkiler başarıyla yönetildi. {childRelations.Count} alt iş ve {siblingRelations.Count/2} kardeş iş ilişkisi temizlendi.");
            }
            catch (Exception ex)
            {
                return (false, $"İlişkiler yönetilirken hata oluştu: {ex.Message}");
            }
        }

        /// <summary>
        /// Bir işin tüm ilişkilerini güvenli bir şekilde siler
        /// </summary>
        /// <param name="workItemId">İş ID'si</param>
        /// <param name="currentUser">İşlemi yapan kullanıcı</param>
        /// <returns>İşlem sonucu</returns>
        public (bool Success, string Message) DeleteAllRelations(int workItemId, string currentUser)
        {
            try
            {
                var relations = _context.WorkItemRelations
                    .Where(r => r.WorkItemId1 == workItemId || r.WorkItemId2 == workItemId)
                    .ToList();

                if (!relations.Any())
                {
                    return (true, "Silinecek ilişki bulunamadı.");
                }

                _context.WorkItemRelations.RemoveRange(relations);
                _context.SaveChanges();

                return (true, $"{relations.Count} ilişki başarıyla silindi.");
            }
            catch (Exception ex)
            {
                return (false, $"İlişkiler silinirken hata oluştu: {ex.Message}");
            }
        }

        /// <summary>
        /// Bir işin hiyerarşik yolunu bulur (kökten bu işe kadar)
        /// </summary>
        /// <param name="workItemId">İş ID'si</param>
        /// <returns>Hiyerarşi zinciri</returns>
        public List<int> GetHierarchyPath(int workItemId)
        {
            var path = new List<int>();
            var visited = new HashSet<int>();
            GetHierarchyPathRecursive(workItemId, path, visited);
            return path;
        }

        private void GetHierarchyPathRecursive(int workItemId, List<int> path, HashSet<int> visited)
        {
            if (visited.Contains(workItemId))
                return; // Döngü tespiti

            visited.Add(workItemId);
            path.Add(workItemId);

            var parentRelation = _context.WorkItemRelations
                .FirstOrDefault(r => r.WorkItemId2 == workItemId && r.RelationType == WorkItemRelationTypes.Parent);

            if (parentRelation != null)
            {
                GetHierarchyPathRecursive(parentRelation.WorkItemId1, path, visited);
            }
        }

        /// <summary>
        /// Bir işin tüm alt işlerini recursive olarak bulur
        /// </summary>
        /// <param name="workItemId">İş ID'si</param>
        /// <returns>Alt işlerin listesi</returns>
        public List<int> GetAllChildWorkItems(int workItemId)
        {
            var childWorkItems = new List<int>();
            GetAllChildWorkItemsRecursive(workItemId, childWorkItems, new HashSet<int>());
            return childWorkItems;
        }

        private void GetAllChildWorkItemsRecursive(int workItemId, List<int> childWorkItems, HashSet<int> visited)
        {
            if (visited.Contains(workItemId))
                return; // Döngü tespiti

            visited.Add(workItemId);

            var childRelations = _context.WorkItemRelations
                .Where(r => r.WorkItemId1 == workItemId && r.RelationType == WorkItemRelationTypes.Parent)
                .ToList();

            foreach (var childRelation in childRelations)
            {
                childWorkItems.Add(childRelation.WorkItemId2);
                GetAllChildWorkItemsRecursive(childRelation.WorkItemId2, childWorkItems, visited);
            }
        }

        private void CreateActivityForChildWorkItem(int workItemId, string description, string currentUser)
        {
            var activity = new WorkItemActivity
            {
                WorkItemId = workItemId,
                ActivityType = "RelationChange",
                Description = description,
                CreatedBy = currentUser,
                CreatedAt = DateTime.Now
            };

            _context.WorkItemActivities.Add(activity);
        }
    }
}
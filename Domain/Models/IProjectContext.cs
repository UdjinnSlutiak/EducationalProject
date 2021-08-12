using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Domain.Models
{
    public interface IProjectContext
    {

        IQueryable<Equipment> Equipments { get; }
        IQueryable<User> Users { get; }
        IQueryable<Record> Records { get; }

        //public IQueryable<Equipment> QEquipments => Equipments;
        //public IQueryable<User> QUsers => Users;
        //public IQueryable<Record> QRecords => Records;

        void Add<T>(T entity);
        void SaveChangesAsync();
        void Update<T>(T entity);
        void Remove<T>(T entity);
    }
}

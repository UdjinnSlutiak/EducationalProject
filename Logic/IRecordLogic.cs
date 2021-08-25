// <copyright file="IRecordLogic.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace EquipmentControll.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using EquipmentControll.Domain.Models;

    /// <summary>
    /// Describes all necessary Record logic CRUD methods.
    /// </summary>
    public interface IRecordLogic
    {
        /// <summary>
        /// Method to get Records from Context.
        /// </summary>
        /// <param name="offset">Count of Records to skip.</param>
        /// <param name="count">Count of Records to take.</param>
        /// <returns>IEnumerable collection of Record ToString strings.</returns>
        public Task<IEnumerable<Record>> GetRecordsAsync(int offset, int count);

        /// <summary>
        /// Overrided Get method to get Record by Id from Context.
        /// </summary>
        /// <param name="id">Record to find Id value.</param>
        /// <returns>String of found Record instance ToString method.</returns>
        public Task<Record> GetRecordByIdAsync(int id);

        /// <summary>
        /// Method to create Record.
        /// </summary>
        /// <param name="record">Record instance to add to database.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task CreateRecordAsync(Record record);

        /// <summary>
        /// Method to update Record.
        /// </summary>
        /// <param name="record">Record instance that contains information to update.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task UpdateRecordAsync(Record record);

        /// <summary>
        /// Method to delete Record.
        /// </summary>
        /// <param name="id">Record to delete Id value.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task DeleteRecordAsync(int id);

        /// <summary>
        /// Method to filter getting Records.
        /// </summary>
        /// <param name="predicate">Predicate to filter Records.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public Task<IEnumerable<Record>> FilterRecordsAsync(Expression<Func<Record, bool>> predicate);

        /// <summary>
        /// Method to get notifications by receiver id.
        /// </summary>
        /// <param name="receiverId">User receiver id.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public Task<IEnumerable<Record>> GetNotificationsByReceiverId(int receiverId);

        /// <summary>
        /// Method to get notifications by sender id.
        /// </summary>
        /// <param name="senderId">User sender id.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public Task<IEnumerable<Record>> GetNotificationsBySenderId(int senderId);
    }
}

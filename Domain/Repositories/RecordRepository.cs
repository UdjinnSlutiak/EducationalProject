﻿using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Models;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repositories
{
    public class RecordRepository : IRecordRepository
    {
        private readonly IProjectContext context;

        public RecordRepository(IProjectContext context)
        {
            this.context = context;
        }

        public IEnumerable<string> Get()
        {
            List<Record> records = context.Records.Include(r => r.Sender).Include(r => r.Receiver).Include(r => r.Equipment).ToList();

            List<string> strings = new();

            foreach (var item in records)
                strings.Add(item.ToString());

            return strings;
        }

        public string Get(int id)
        {
            Record record = GetRecord(id);
            if (record != null)
                return record.ToString();
            else
                return "Not Found";
        }

        public void Create(Record partialRecord)
        {
            Record record = CreateRecord(partialRecord.senderId, partialRecord.receiverId, partialRecord.equipmentId);

            if (record.isValid())
            {
                context.Add(record);
                context.SaveChangesAsync();
            }
        }

        public void Update(int id, Record partialrecord)
        {
            Record record = CreateRecord(partialrecord.senderId, partialrecord.receiverId, partialrecord.equipmentId);
            if (record.isValid(id))
            {
                record.Id = id;
                context.Update(record);
                context.SaveChangesAsync();
            }
        }

        public void Delete(int id)
        {
            if (id > 0)
            {
                context.Remove(context.Records.FirstOrDefault(r => r.Id == id));
                context.SaveChangesAsync();
            }
        }

        private Record CreateRecord(int senderId, int receiverId, int equipmentId)
        {
            User sender = context.Users.FirstOrDefault(r => r.Id == senderId);
            User receiver = context.Users.FirstOrDefault(r => r.Id == receiverId);
            Equipment equipment = context.Equipments.FirstOrDefault(r => r.Id == equipmentId);

            return new()
            {
                Sender = sender,
                Receiver = receiver,
                Equipment = equipment
            };
        }

        private Record GetRecord(int id)
        {
            return context.Records.Include(r => r.Sender).Include(r => r.Receiver).Include(r => r.Equipment).FirstOrDefault(r => r.Id == id);
        }
    }
}

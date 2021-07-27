using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EducationalProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EducationalProject.Controllers
{
    [Route("record")]
    public class RecordController : Controller
    {
        private readonly ProjectContext context;

        public RecordController(ProjectContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            List<Record> records = context.Records.Include(r => r.Sender).Include(r => r.Receiver).Include(r => r.Equipment).ToList();

            List<string> strings = new();

            foreach (var item in records)
                strings.Add(item.ToString());

            return strings;
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            Record record = GetRecord(id);
            if (record != null)
                return record.ToString();
            else
                return "Not Found";
        }

        [HttpPost]
        public void Post([FromBody] Record partialRecord)
        {
            Record record = CreateRecord(partialRecord.senderId, partialRecord.receiverId, partialRecord.equipmentId);

            if (record.isValid())
            {
                context.Add(record);
                context.SaveChangesAsync();
            }
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Record partialrecord)
        {
            Record record = CreateRecord(partialrecord.senderId, partialrecord.receiverId, partialrecord.equipmentId);
            if (record.isValid(id))
            {
                record.Id = id;
                context.Update(record);
                context.SaveChangesAsync();
            }
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            if (id > 0)
            {
                context.Remove(context.Records.Find(id));
                context.SaveChangesAsync();
            }
        }

        private Record CreateRecord(int senderId, int receiverId, int equipmentId)
        {
            User sender = context.Users.Find(senderId);
            User receiver = context.Users.Find(receiverId);
            Equipment equipment = context.Equipments.Find(equipmentId);

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

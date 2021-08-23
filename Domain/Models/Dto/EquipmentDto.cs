using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentControll.Domain.Models.Dto
{
    public class EquipmentDto
    {
        public EquipmentDto() { }

        public EquipmentDto(Equipment equipment)
        {
            this.Id = equipment.Id;
            this.Name = equipment.Name;
            this.Price = equipment.Price;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}

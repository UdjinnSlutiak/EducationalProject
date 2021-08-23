namespace EquipmentControll.Domain.Models.Dto
{
    public class RecordDto
    {
        public RecordDto() { }

        public RecordDto(Record record)
        {
            this.Id = record.Id;
            this.SenderId = record.SenderId;
            this.ReceiverId = record.ReceiverId;
            this.EquipmentId = record.EquipmentId;
        }

        public int Id { get; set; }
        public int? SenderId { get; set; }
        public int? ReceiverId { get; set; }
        public int EquipmentId { get; set; }
    }
}

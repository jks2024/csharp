using System.Text.Json.Serialization;

namespace MesMachineSim.Models
{
    public class ProductionReportDto
    {
        public long OrderId { get; set; }
        public string MachineId { get; set; }
        public string SerialNo { get; set; } // 추가
        public string Result { get; set; }
        public string DefectCode { get; set; }
    }
}
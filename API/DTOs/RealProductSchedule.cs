using System.Collections;
using System.Collections.Generic;

namespace API.DTOs
{
    public class RealProductSchedule
    {
        public int RealProductId { get; set; }
        public ICollection<ProductScheduleSegmentsDto> Segments { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCourse.SharedCore7.Messages
{
    public class CourseNameChangedEvent
    {
        public string CourseId { get; set; }

        public string UpdatedName { get; set; }
    }
}

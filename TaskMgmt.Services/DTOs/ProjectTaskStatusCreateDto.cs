using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMgmt.Services.DTOs
{
    internal class ProjectTaskStatusCreateDto
    {
        
        public int ProjectId { get; set; }
        public string StatusText { get; set; } = null!;
        public string StatusColor { get; set; } = null!;
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMgmt.DataAccess.Models;

namespace TaskMgmt.Services.DTOs
{
    public class ProjectTaskStatusCreate
    {

        public int ProjectId { get; set; }
        public string StatusText { get; set; } = null!;
        public string StatusColor { get; set; } = null!;

    }
}

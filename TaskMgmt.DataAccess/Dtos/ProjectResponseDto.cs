using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMgmt.DataAccess.Dtos
{
    public class ProjectResponseDto
    {
        public int ProjectId { get; set; }
        public int GroupId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public int OwnerId { get; set; }
    }
}
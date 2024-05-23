﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHDataManager.Library.Models
{
    public class TaskModel
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? InitiatorId { get; set; }
        public string? ExecutorId { get; set; }
        public int? ProjectId { get; set; }
        public int? StateId { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}

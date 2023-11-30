﻿using System;
using ToDo.DomainModel.Legal.Enums;

namespace ToDo
{
    public class Task
    {
        public int Id { get; set; }

        public TaskType TaskType { get; set; }

        public bool Active { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}

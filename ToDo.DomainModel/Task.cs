using System;

namespace ToDo
{
    public class Task
    {
        public int Id { get; set; }

        public Web.Legal.Enums.Type Type { get; set; }

        public bool Active { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}

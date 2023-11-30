using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Collections.Generic;
using System.Linq;
using ToDo.DomainModel.Enums;

namespace ToDo.DataAccess.ValueConverters
{
    public class TaskTypeConverter : ValueConverter<TaskType, string>
    {
        public static IReadOnlyDictionary<string, TaskType> TaskTypeCodeToTaskType { get; } =
            new Dictionary<string, TaskType>
            {
                { "TTTODO    ",  TaskType.ToDo },
                { "TTINDEVELO",  TaskType.InDevelopment },
                { "TTDONE    ",  TaskType.Done },
            };

        public static IReadOnlyDictionary<TaskType, string> TaskTypeToTaskTypeCode { get; } =
            TaskTypeCodeToTaskType.ToDictionary(keyValuePair => keyValuePair.Value, keyValuePair => keyValuePair.Key);

        public TaskTypeConverter()
            : base(taskType => TaskTypeToTaskTypeCode[taskType],
                   taskTypeCode => TaskTypeCodeToTaskType[taskTypeCode])
        {
        }
    }
}

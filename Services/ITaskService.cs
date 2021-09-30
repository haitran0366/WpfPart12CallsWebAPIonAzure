using System.Collections.Generic;
using WpfPart12CallsWebAPIonAzure.Models;

namespace WpfPart12CallsWebAPIonAzure.Services
{
    public interface ITaskService
    {
        bool DeleteTask(TaskModel taskModel);
        List<TaskModel> GetTask();
        bool SaveTask(TaskModel taskModel);
        bool UpdateTask(TaskModel taskModel);
    }
}
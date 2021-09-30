using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfPart12CallsWebAPIonAzure.Models;
using WpfPart12CallsWebAPIonAzure.Services;

namespace WpfPart12CallsWebAPIonAzure.ViewModel
{
    public class TaskViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        private bool isUpdate = false;


        private int tabs = 2;
        public int Tabs
        {
            get { return tabs; }
            set
            {
                tabs = value;
                NotifyPropertyChanged("Tabs");
            }
        }
        private string isAddTask = "Collapsed";
        public string IsAddTask
        {
            get { return isAddTask; }
            set
            {
                isAddTask = value;
                NotifyPropertyChanged("IsAddTask");
            }
        }


        private string taskName;
        public string TaskName
        {
            get { return taskName; }
            set
            {
                taskName = value;
                NotifyPropertyChanged("TaskName");
            }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                NotifyPropertyChanged("Description");
            }
        }
        private DateTime dueDate = DateTime.Now;
        public DateTime DueDate
        {
            get { return dueDate; }
            set
            {
                dueDate = value;
                NotifyPropertyChanged("DueDate");
            }
        }

        private List<TaskModel> taskList;
        public List<TaskModel> TaskList
        {
            get { return taskList; }
            set
            {
                taskList = value;
                NotifyPropertyChanged("TaskList");

            }
        }

        private TaskModel selectedTask;
        public TaskModel SelectedTask
        {
            get { return selectedTask; }
            set
            {
                selectedTask = value;
                NotifyPropertyChanged("SelectedTask");
            }
        }

        public ICommand cmdUpdateItem { get; private set; }
        public ICommand cmdCancelTask { get; private set; }
        public ICommand cmdAddaTask { get; private set; }
        public ICommand cmdAddTask { get; private set; }
        public ICommand cmdDeleteItem { get; private set; }
        public bool CanExectute
        {
            get { return !string.IsNullOrEmpty(TaskName); }
        }
        public bool CanExectute_Update
        {
            get { return IsAddTask == "Collapsed"; }
        }
        public bool CanDelete
        {
            get { return SelectedTask != null; }
        }
        public bool CanAddaTask
        {
            get { return IsAddTask == "Collapsed"; }
        }

        public TaskViewModel()
        {
            cmdCancelTask = new RelayCommand(CancelAddingTask, () => true);
            cmdAddaTask = new RelayCommand(AddaTask, () => CanAddaTask);
            cmdAddTask = new RelayCommand(AddTask, () => CanExectute);
            cmdDeleteItem = new RelayCommand(Delete, () => CanDelete);
            cmdUpdateItem = new RelayCommand(Update, () => CanExectute_Update);
            getTask();
        }
        ITaskService task = new TaskService();
        private void Update()
        {
            IsAddTask = "Visible";
            isUpdate = true;

            TaskName = selectedTask.TaskName;
            Description = selectedTask.Description;
            DueDate = (DateTime)selectedTask.DueDate;

        }

        private void CancelAddingTask()
        {
            IsAddTask = "Collapsed";
            isUpdate = false;
        }

        private void AddaTask()
        {
            IsAddTask = "Visible";
            isUpdate = false;
            TaskName = string.Empty;
            Description = string.Empty;
        }

        private void Delete()
        {

            task.DeleteTask(SelectedTask);
            getTask();
        }

        private void AddTask()
        {
            if (!isUpdate)
            {
                var r = task.SaveTask(new TaskModel
                {
                    TaskName = TaskName,
                    Description = Description,
                    DueDate = DueDate
                });
            }
            else
            {

                SelectedTask.TaskName = TaskName;
                SelectedTask.Description = Description;
                SelectedTask.DueDate = DueDate;
                var r = task.UpdateTask(SelectedTask);
                isUpdate = false;
            }
            IsAddTask = "Collapsed";

            getTask();

        }
        public void getTask()
        {
            TaskList = task.GetTask();
        }
    }
}

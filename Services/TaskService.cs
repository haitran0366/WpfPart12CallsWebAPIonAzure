using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WpfPart12CallsWebAPIonAzure.Models;

namespace WpfPart12CallsWebAPIonAzure.Services
{
    public class TaskService : ITaskService
    {
        public bool SaveTask(TaskModel taskModel)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("https://taskschedule20210928092215.azurewebsites.net/");
                //client.DefaultRequestHeaders.Add("appkey", "myapp_key");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.PostAsJsonAsync("task", taskModel).Result;
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool DeleteTask(TaskModel taskModel)
        {
            try
            {

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("https://taskschedule20210928092215.azurewebsites.net/");
                //client.DefaultRequestHeaders.Add("appkey", "myapp_key");
                client.DefaultRequestHeaders.Accept.Add(
                   new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.DeleteAsync($"task/{taskModel.Id}").Result;
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateTask(TaskModel taskModel)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("https://taskschedule20210928092215.azurewebsites.net/");
                //client.DefaultRequestHeaders.Add("appkey", "myapp_key");
                client.DefaultRequestHeaders.Accept.Add(
                   new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.PutAsJsonAsync($"task/{taskModel.Id}", taskModel).Result;
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<TaskModel> GetTask()
        {
            try
            {

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("https://taskschedule20210928092215.azurewebsites.net/");
                //client.DefaultRequestHeaders.Add("appkey", "myapp_key");
                client.DefaultRequestHeaders.Accept.Add(
                   new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync("task").Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<List<TaskModel>>().Result;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}

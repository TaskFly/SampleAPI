using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TaskFly.Entities;

namespace TaskFly.Integra
{
    public class TaskFlyHelper
    {
        private string apiURL = "https://integra.gotaskfly.com/api/v1";
        private string apiToken;

        public TaskFlyHelper(string token)
        {
            apiToken = token;
        }

        private HttpClient GetHttpClientToken()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("api_key", apiToken);
            return client;
        }

        private T CallService<T>(string method)
        {
            using (var client = GetHttpClientToken())
            {
                var url = apiURL + method;
                using (var response = client.GetAsync(url).Result)
                {
                    response.EnsureSuccessStatusCode();
                    var responsebody = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<T>(responsebody);
                }
            }
        }

        private void ServicePut(string method, object data)
        {
            using (var client = GetHttpClientToken())
            {
                HttpResponseMessage responsebody;
                var url = apiURL + method;
                if (data != null)
                {
                    var content = JsonConvert.SerializeObject(data);
                    var buffer = Encoding.UTF8.GetBytes(content);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var response = client.PutAsync(url, byteContent);
                    responsebody = response.Result;
                }
                else
                {
                    var content = new StringContent("");
                    content.Headers.ContentType = null;
                    var response = client.PutAsync(url, content);
                    responsebody = response.Result;
                }
            }
        }

        public List<Customers> GetCustomers() => CallService<List<Customers>>("/customers");
        public Customers GetCustomerByID(int id) => CallService<Customers>("/customers/" + id);
        public List<Projects> GetProjects() => CallService<List<Projects>>("/projects");
        public List<Sectors> GetSectors() => CallService<List<Sectors>>("/sectors");
        public List<TaskPhases> GetTaskPhases() => CallService<List<TaskPhases>>("/taskphases");
        public List<TaskPriority> GetTaskPriority() => CallService<List<TaskPriority>>("/taskpriorities");
        public List<TaskType> GetTaskType() => CallService<List<TaskType>>("/tasktypes");
        public List<Users> GetUsers() => CallService<List<Users>>("/users");
        public List<UsersToTransferTask> GetUsersToTransferTask() => CallService<List<UsersToTransferTask>>("/tasks/transfer/users");
        public List<Tasks> GetTasks(Dictionary<string, object> filter)
        {
            var strFilter = "?";
            foreach(var f in filter.ToList())
            {
                strFilter += f.Key + "=" + f.Value.ToString() + "&";
            }
            strFilter = strFilter.Substring(0, strFilter.Length - 1);
            return CallService<List<Tasks>>($"/tasks" + strFilter);
        }

        public void TransferTask(int taskId, int newUserId)
        {
            ServicePut($"/tasks/{taskId}/transfer/{newUserId}",null);
        }


    }
}

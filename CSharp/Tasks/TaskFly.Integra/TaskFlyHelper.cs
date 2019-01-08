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
        public string Message = "";

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
            Message = "";
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

        private int SendToService(string httpVerb, string urlMethod, object data)
        {
            int ID = 0;
            Message = "";
            using (var client = GetHttpClientToken())
            {
                HttpResponseMessage responsebody;
                var url = apiURL + urlMethod;
                HttpContent httpContent;
                if (data != null)
                {
                    var content = JsonConvert.SerializeObject(data);
                    var buffer = Encoding.UTF8.GetBytes(content);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    httpContent = byteContent;
                }
                else
                {
                    var content = new StringContent("");
                    content.Headers.ContentType = null;
                    httpContent = content;
                }
                Task<HttpResponseMessage> response = null;
                switch (httpVerb)
                {
                    case "POST":
                        {
                            response = client.PostAsync(url, httpContent);
                            break;
                        }
                    case "PUT":
                        {
                            response = client.PutAsync(url, httpContent);
                            break;
                        }
                    case "DELETE":
                        {
                            response = client.DeleteAsync(url);
                            break;
                        }
                }
                responsebody = response.Result;
                string result = (responsebody.Content.ReadAsStringAsync().Result);
                if (result == "")
                {
                    Message = responsebody.StatusCode.ToString();
                }
                if(result.Contains("Message"))
                {
                    var newType = new { Message = "" };
                    var obj = JsonConvert.DeserializeAnonymousType(result, newType);
                    Message = obj.Message;
                }
                if (httpVerb == "POST")
                {
                    var newType = new { ID = 0 };
                    var obj = JsonConvert.DeserializeAnonymousType(result, newType);
                    ID = obj.ID;
                    Message = "OK";
                }
            }
            return ID;
        }

        public List<Customers> GetCustomers() => CallService<List<Customers>>("/customers");
        public Customers GetCustomerByID(int id) => CallService<Customers>("/customers/" + id);
        public int AddCustomer(Customers customer) => SendToService("POST","/customers",customer);
        public void ChangeCustomer(Customers customer) => SendToService("PUT", $"/customers/{customer.Id}", customer);
        public void DeleteCustomer(int ID) => SendToService("DELETE", $"/customers/{ID}", null);
        public List<Projects> GetProjects() => CallService<List<Projects>>("/projects");
        public int AddProject(Projects project) => SendToService("POST", "/projects", project);
        public void ChangeProject(Projects project) => SendToService("POST", $"/projects/{project.Id}", project);
        public void DeleteProject(int ID) => SendToService("DELETE", $"/projects/{ID}", null);
        public List<Sectors> GetSectors() => CallService<List<Sectors>>("/sectors");
        public List<TaskPhases> GetTaskPhases() => CallService<List<TaskPhases>>("/taskphases");
        public List<TaskPriority> GetTaskPriority() => CallService<List<TaskPriority>>("/taskpriorities");
        public List<TaskType> GetTaskType() => CallService<List<TaskType>>("/tasktypes");
        public List<Users> GetUsers() => CallService<List<Users>>("/users");
        public List<UsersToTransferTask> GetUsersToTransferTask() => CallService<List<UsersToTransferTask>>("/tasks/transfer/users");
        public List<Tasks> GetTasks(Dictionary<string, object> filter)
        {
            var sb = new StringBuilder("?");
            foreach(var f in filter.ToList())
            {
                sb.Append($"{f.Key}={f.Value}&");
            }
            var strFilter = sb.ToString();
            strFilter = strFilter.Substring(0, strFilter.Length - 1);
            return CallService<List<Tasks>>($"/tasks" + strFilter);
        }

        public void TransferTask(int taskId, int newUserId)
        {
            SendToService("PUT",$"/tasks/{taskId}/transfer/{newUserId}",null);
        }


    }
}

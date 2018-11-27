using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TaskFly.Integra;

namespace TaskFlySampleAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            // API V1 url: https://integra.gotaskfly.com/api/v1
            // Token is generated on TaskFly/User Profile/Integration
            // Each Team has an integration token
            var APIToken = "<your_token>";
            var taskfly = new TaskFlyHelper(APIToken);

            //var customers = taskfly.GetCustomers();
            //var customer1 = taskfly.GetCustomerByID(1);
            //var users = taskfly.GetUsers();
            //var projects = taskfly.GetProjects();
            //var sectors = taskfly.GetSectors();
            //var phases = taskfly.GetTaskPhases();
            //var proprity = taskfly.GetTaskPriority();
            //var type = taskfly.GetTaskType();
            var filter = new Dictionary<string, object>
            {
                {"Id", 203 }
            };
            var task = taskfly.GetTasks(filter);

            var userTransfer = taskfly.GetUsersToTransferTask();

            taskfly.TransferTask(task.First().Id, 38);


        }
    }
}

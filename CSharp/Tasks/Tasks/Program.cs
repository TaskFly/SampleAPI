using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TaskFly.Entities;
using TaskFly.Integra;

namespace TaskFlySampleAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            // API V1 url: https://integra.gotaskfly.com/api/v1
            // API Documentation: https://integra.gotaskfly.com/docs/index
            // Token is generated on TaskFly/User Profile/Integration
            // Each Team has an integration token
            var APIToken = "";
            var taskfly = new TaskFlyHelper(APIToken);

            var customers = taskfly.GetCustomers();

            var newCustomer = new Customers()
            {
                CompanyName = "Customers Sample",
                Name = "Customers Sample Name",
                Address = "Av. Alberto Carazzai, 762",
                City = "Cornelio Procopio",
                District = "Vila Ipiranga",
                UF = "PR",
                Contact = "Carlos dos Santos",
                Email = "cds@cds-software.com.br",
                Active = true
            };
            int newCustomerId = taskfly.AddCustomer(newCustomer);
            newCustomer.Id = newCustomerId;
            newCustomer.Active = true;
            newCustomer.Name = "Customer Sample Changed";
            taskfly.ChangeCustomer(newCustomer);

            //taskfly.DeleteCustomer(727);

            var customer1 = taskfly.GetCustomerByID(1);
            //var users = taskfly.GetUsers();
            //var projects = taskfly.GetProjects();
            //var sectors = taskfly.GetSectors();
            //var phases = taskfly.GetTaskPhases();
            //var proprity = taskfly.GetTaskPriority();
            //var type = taskfly.GetTaskType();
            //var filter = new Dictionary<string, object>
            //{
            //    {"Id", 203 }
            //};
            //var task = taskfly.GetTasks(filter);

            //var userTransfer = taskfly.GetUsersToTransferTask();

            //taskfly.TransferTask(206, 38);



        }
    }
}

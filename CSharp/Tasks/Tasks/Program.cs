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
        private static TaskFlyHelper taskfly;

        static void Main(string[] args)
        {
            // API V1 url: https://integra.gotaskfly.com/api/v1
            // API Documentation: https://integra.gotaskfly.com/docs/index
            // Token is generated on TaskFly/User Profile/Integration
            // Each Team has an integration token
            var APIToken = "";
            taskfly = new TaskFlyHelper(APIToken);

            UsersSample();
            CustomersSample();
            ProjectsSample();
            SectorsSample();
            PhasesSample();
            PrioritySample();
            TaskTypeSample();
            TaskSample();
        }

        private static void CustomersSample()
        {
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
            taskfly.DeleteCustomer(727);
            var customer1 = taskfly.GetCustomerByID(1);

        }

        private static void UsersSample()
        {
            var users = taskfly.GetUsers();
        }

        private static void ProjectsSample()
        {
            var projects = taskfly.GetProjects();
            var newPrj = new Projects()
            {
                Description = "Project Inserted"
            };
            int newId = taskfly.AddProject(newPrj);
            newPrj.Id = newId;
            newPrj.Description = "Project Changed";
            taskfly.ChangeProject(newPrj);
            taskfly.DeleteProject(newId);
        }

        private static void SectorsSample()
        {
            var sectors = taskfly.GetSectors();
            var newSector = new Sectors()
            {
                Description = "Sector Inserted"
            };
            int newId = taskfly.AddSector(newSector);
            newSector.Id = newId;
            newSector.Description = "Sector Changed";
            taskfly.ChangeSector(newSector);
            taskfly.DeleteSector(newId);
        }

        private static void PhasesSample()
        {
            var phases = taskfly.GetTaskPhases();
            var newPhase = new TaskPhases()
            {
                Description = "Phase Inserted"
            };
            int newId = taskfly.AddPhase(newPhase);
            newPhase.Id = newId;
            newPhase.Description = "Phase Changed";
            taskfly.ChangePhase(newPhase);
            taskfly.DeletePhase(newId);
        }

        private static void PrioritySample()
        {
            var priority = taskfly.GetTaskPriority();
            var newPriority = new TaskPriority()
            {
                Description = "Priority Inserted"
            };
            int newId = taskfly.AddPriority(newPriority);
            newPriority.Id = newId;
            newPriority.Description = "Priority Changed";
            taskfly.ChangePriority(newPriority);
            taskfly.DeletePriority(newId);
        }

        private static void TaskTypeSample()
        {
            var type = taskfly.GetTaskType();
            var newType = new TaskType()
            {
                Description = "Type Inserted"
            };
            int newId = taskfly.AddTaskType(newType);
            newType.Id = newId;
            newType.Description = "Type Changed";
            taskfly.ChangeTaskType(newType);
            taskfly.DeleteTaskType(newId);
        }

        private static void TaskSample()
        {
            var filter = new Dictionary<string, object>
            {
                {"Id", 203 }
            };
            var task = taskfly.GetTasks(filter);

            var phase = taskfly.GetTaskPhases().First();
            var priority = taskfly.GetTaskPriority().First();
            var taskType = taskfly.GetTaskType().First();
            var customer = taskfly.GetCustomers().First();
            var project = taskfly.GetProjects().First();

            var newTask = new Tasks
            {
                Description = "Task Created",
                EstimatedDate = DateTime.Now.AddDays(10),
                PhaseId = phase.Id,
                PriorityId = priority.Id,
                TypeId = taskType.Id,
                CustomerId = customer.Id,
                ProjectId = project.Id
            };
            int newID = taskfly.AddTask(newTask);

            taskfly.TaskStartTimer(newID);
            taskfly.TaskStopTimer(newID);

            var userTransfer = taskfly.GetUsersToTransferTask();

            taskfly.TransferTask(newID, userTransfer.First().UserId);

        }
    }
}

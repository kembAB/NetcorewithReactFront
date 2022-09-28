using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebApp.Models.Home
{
    class MockProjectRepository : IProjectRepository
    {
        public IEnumerable<Project> GetAllProjects =>
            new List<Project>
            {
                new Project {},
                new Project {},

            };
    }
}

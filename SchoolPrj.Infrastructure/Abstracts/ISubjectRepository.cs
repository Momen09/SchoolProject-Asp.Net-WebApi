using SchoolPrj.Infrastructure.InfrastructureBases;
using SchoolProject.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPrj.Infrastructure.Abstracts
{
    public interface ISubjectRepository : IGenericRepositoryAsync<Subjects>
    {
    }
}

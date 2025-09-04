using SchoolPrj.Data.Commons;
using SchoolProject.Data.Entites;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPrj.Data.Entites
{
    public class InsSubject : GeneralLocalizableEnitity
    {
        public int InsId { get; set; }
        public int SubId { get; set; }
        [ForeignKey(nameof(InsId))]
        [InverseProperty("InsSubjects")]
        public Instructor? instructor { get; set; }
        [ForeignKey(nameof(SubId))]
        [InverseProperty("InsSubjects")]
        public Subjects? Subject { get; set; }
    }
}

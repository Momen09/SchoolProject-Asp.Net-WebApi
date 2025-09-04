using SchoolPrj.Data.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Data.Entites
{
    public class DepartmentSubject : GeneralLocalizableEnitity
    {
        [Key]
        public int DID { get; set; }
        [Key] 
        public int SubID { get; set; }
        [ForeignKey("DID")]
        [InverseProperty("DepartmentSubjects")]
        public virtual Department? Department { get; set; }
        [ForeignKey("SubID")]
        [InverseProperty("DepartmentSubjects")]
        public virtual Subjects? Subject { get; set; }
    }
}

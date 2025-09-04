using SchoolPrj.Data.Commons;
using SchoolPrj.Data.Entites;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Data.Entites
{
    public class Subjects : GeneralLocalizableEnitity
    {
        public Subjects() { 
        StudentSubjects = new HashSet<StudentSubject>();
        DepartmentSubjects = new HashSet<DepartmentSubject>();
        InsSubjects = new HashSet<InsSubject>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubId { get; set; }
        [StringLength(500)]
        public string? SubjectNameAr { get; set; }
        [StringLength(500)]
        public string? SubjectNameEn { get; set; }
        public int? Period { get; set; }
        [InverseProperty("Subject")]
        public virtual ICollection<StudentSubject> StudentSubjects { get; set; }
        [InverseProperty("Subject")]
        public virtual ICollection<DepartmentSubject> DepartmentSubjects { get; set; }
        [InverseProperty("Subject")]
        public virtual ICollection<InsSubject> InsSubjects{ get; set; }
    }
}

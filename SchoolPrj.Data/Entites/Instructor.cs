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
    public class Instructor : GeneralLocalizableEnitity
    {
        public Instructor()
        {
            Instructors = new HashSet<Instructor>();
            InsSubjects = new HashSet<InsSubject>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InsId { get; set; }
        public string? ENameAr { get; set; }
        public string? ENameEn { get; set; }
        public string? Address { get; set; }
        public string? Position { get; set; }
        public int? SupervisorId { get; set; }
        public decimal? Salary { get; set; }
        public int DID { get; set; }
        [ForeignKey(nameof(DID))]
        [InverseProperty("Instructors")]
        public Department? Department { get; set; }
        [InverseProperty("Instructor")]
        public Department? DepartmentManager { get; set; }
        [ForeignKey(nameof(SupervisorId))]
        [InverseProperty("Instructors")]
        public Instructor? Supervisor { get; set; }
        [InverseProperty("Supervisor")]
        public virtual ICollection<Instructor> Instructors { get; set; }
        [InverseProperty("instructor")]
        public virtual ICollection<InsSubject> InsSubjects{ get; set; }

    }
}

using SchoolPrj.Data.Commons;
using SchoolPrj.Data.Entites;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Data.Entites
{
    public partial class Department : GeneralLocalizableEnitity
    {
        public Department()
        {
            Students = new HashSet<Student>();
            DepartmentSubjects = new HashSet<DepartmentSubject>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DID { get; set; }
        [StringLength(200)]
        public string? DNameAr { get; set; }
        [StringLength(200)]
        public string? DNameEn { get; set; }
        public int? InsManager { get; set; }
        [InverseProperty("Department")]
        public virtual ICollection<Student> Students { get; set; }
        [InverseProperty("Department")]
        public virtual ICollection<DepartmentSubject> DepartmentSubjects { get; set; }
        [InverseProperty("Department")]
        public virtual ICollection<Instructor> Instructors { get; set; }
        [ForeignKey("InsManager")]
        [InverseProperty("DepartmentManager")]
        public virtual Instructor? Instructor { get; set; }
    }
}

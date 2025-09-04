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
    public class StudentSubject : GeneralLocalizableEnitity
    {
        [Key]
        public int StudID { get; set; }
        [Key]
        public int SubID { get; set; }
        public decimal? Grade { get; set; }
        [ForeignKey("StudID")]
        [InverseProperty("StudentSubject")]
        public virtual Student? Student { get; set; }
        [ForeignKey("SubID")]
        [InverseProperty("StudentSubjects")]
        public virtual Subjects? Subject { get; set; }
    }
}

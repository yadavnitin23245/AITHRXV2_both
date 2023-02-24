using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airthwholesale.Data.Models
{
    public  class JDPExceptionLoggingToDataBase
    {

        [Key]
        public long Logid { get; set; }

        [MaxLength(1000)]
        public string ExceptionMsg { get; set; }

        [MaxLength(1000)]
        public string ExceptionType { get; set; }


        [MaxLength(1000)]
        public string ExceptionSource { get; set; }

        [MaxLength(1000)]
        public string ExceptionURL { get; set; }



        [MaxLength(1000)]
        public DateTime? Logdate { get; set; }
    }
}

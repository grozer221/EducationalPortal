using EducationalPortal.Server.Database.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPortal.Server.Database.Models
{
    public class SettingModel : BaseModel
    {
        public string Name { get; set; }
        public string? Value { get; set; }
    }
}

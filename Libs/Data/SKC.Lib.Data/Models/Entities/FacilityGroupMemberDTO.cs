using SKC.Lib.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKC.Lib.Data.Models.Entities
{
    /// <summary>
    /// DTO for Facility Group Member
    /// </summary>
    public sealed class FacilityGroupMemberDTO : FacilityGroupMember
    {
        [NotMapped]
        public int GroupId { get; set; }

        [NotMapped]
        public string Value { get; set; }

        [NotMapped]
        public int ValueId { get; set; }

        [NotMapped]
        public string ValueDataType { get; set; }
    }
}

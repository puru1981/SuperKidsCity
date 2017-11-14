using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKC.Lib.Core.Models
{
    /// <summary>
    /// Enum for Facility Group Memebr Types
    /// </summary>
    public enum FacilityGroupMemberType
    {
        /// <summary>
        /// Checkbox Type : Checkbox control will be created on UI
        /// </summary>
        Checkbox = 0,

        /// <summary>
        /// Textbox Type : Textbox control will be created on UI
        /// </summary>
        Textbox = 1,

        /// <summary>
        /// Dropdown Type : Dropdown control will be created on UI
        /// </summary>
        Dropdown = 2,

        /// <summary>
        /// Radio Type : Radio control will be created on UI
        /// </summary>
        Radio = 3,

        /// <summary>
        /// TextArea Type : TextArea control will be created on UI
        /// </summary>
        TextArea = 4
    }
};

using SKC.Lib.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKC.Lib.Data.Models.Mappings
{
    /// <summary>
    /// Mapping Class for Base Logs
    /// </summary>
    public sealed class BaseLogMap : EntityTypeConfiguration<AuditBase>
    {
        #region Ctor

        public BaseLogMap()
        {
            this.HasKey(k => k.Id);
            this.ToTable("AuditLogs");

        }

        #endregion
    }
}

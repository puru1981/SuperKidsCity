using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperKidCity.Models
{
    public class SettingViewModel : BaseViewModel
    {
        public SettingViewModel()
        {
            this.CamSetting = new CamSettingViewModel();
            this.RFIDSetting = new RFIDSettingsViewModel();
        }
        public int SettingTypeId { get; set; }

        public CamSettingViewModel CamSetting { get; set; }

        public RFIDSettingsViewModel RFIDSetting { get; set; }
    }
}
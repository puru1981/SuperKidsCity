using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKC.Lib.Service.Helpers
{
    public static class QueryHelper
    {
        public static string GETLocalityByState(int stateId)
        {
            return string.Format("select L.* from Localities L, Cities C, States S where C.StateId= S.Id and C.Id= L.CityId and L.Deleted = S.Deleted and L.Active=S.Active and S.Id={0}", stateId);
        }

        public static string GETSchoolByAddressId(int addressId)
        {
            return string.Format("select school_Id from addresses where Id={0} and Deleted='False 'and Active='True'", addressId);
        }

        public static string GETFacilityGroupMembersByFacilityGroupId(int facilityGroupId)
        {
            return string.Format("select Id,Name,ValueType,GUID, Required,Type,CreatedAt,UpdatedAt, Deleted,Active,UserId,SessionId, Group_Id as GroupId from FacilityGroupMembers where Group_Id={0} and Deleted='False 'and Active='True'", facilityGroupId);
        }
    }
}

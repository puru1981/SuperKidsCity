using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKC.Lib.Service
{
    public interface IRepository<T> where T : class
    {
        IList<T> Get();
        T GetById(int Id);
        T Insert(T obj);
        T Update(T Obj);
        bool Delete(T Obj);
        bool Delete(int Id);

        T GetSchoolByUserId(string userId);
        T GetSchoolAddressBySchoolId(int schoolId);
        List<T> GetSchoolMembersBySchoolId(int schoolId);
        List<T> GetSchoolMembersContactBySchoolId(int schoolId);
        T GetSchoolBySchoolId(int schoolId);
        List<T> GetSchoolsByLocationId(int localityId);
        List<T> GetLocalityByStateId(int stateId);
        T GetSchoolByAddressId(int addressId);
        List<T> GetFacilityGroupMemberByFacilityGroupId(int facilityGroupId);
        List<T> GetConfiguredFacilitiesBySchoolId(int schoolId);

        //Async Methods
        Task<IList<T>> GetAsync();
        Task<T> GetByIdAsync(int Id);
        Task<T> InsertAsync(T Obj);
        Task<T> UpdateAsync(T Obj);
        Task<bool> DeleteAsync(T Obj);
        Task<bool> DeleteAsync(int Id);

    }
}

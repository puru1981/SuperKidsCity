using SKC.Lib.Data.Models.Entities;
using SKC.Lib.Service.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKC.Lib.Service.Repos
{
    public sealed class RepositoryContext<T> : IRepository<T> where T : class
    {
        public RepositoryContext(string connStr)
        {
            this.ConnectionString = connStr;
        }

        private string ConnectionString;

        public IList<T> Get()
        {
            using (var context = new RespositoryContext(ConnectionString))
            {
                return context.Set<T>().ToList();
            }
        }

        public T GetById(int Id)
        {
            using (var context = new RespositoryContext(ConnectionString))
            {
                return context.Set<T>().Find(Id);
            }
        }

        public T Insert(T Obj)
        {
            using (var context = new RespositoryContext(ConnectionString))
            {
                var entity = context.Entry<T>(Obj);
                entity.State = System.Data.Entity.EntityState.Added;
                entity.Property<DateTime>("CreatedAt").CurrentValue = DateTime.UtcNow;
                entity.Property<DateTime>("UpdatedAt").CurrentValue = DateTime.UtcNow;
                context.SaveChanges();

                return Obj;
            }
        }

        public T Update(T Obj)
        {
            using (var context = new RespositoryContext(ConnectionString))
            {
                var entity = context.Entry<T>(Obj);
                entity.State = System.Data.Entity.EntityState.Modified;
                entity.Property<DateTime>("UpdatedAt").CurrentValue = DateTime.UtcNow;
                context.SaveChanges();
                return Obj;
            }
        }

        public bool Delete(T Obj)
        {
            using (var context = new RespositoryContext(ConnectionString))
            {
                var entity = context.Entry<T>(Obj);
                entity.State = System.Data.Entity.EntityState.Deleted;
                entity.Property<DateTime>("UpdatedAt").CurrentValue = DateTime.UtcNow;
                context.SaveChanges();
                return true;
            }
        }

        public bool Delete(int Id)
        {
            using (var context = new RespositoryContext(ConnectionString))
            {
                var entry = this.GetById(Id);
                var entity = context.Entry(entry);
                entity.State = System.Data.Entity.EntityState.Deleted;
                entity.Property<DateTime>("UpdatedAt").CurrentValue = DateTime.UtcNow;
                entity.Property<bool>("Deleted").CurrentValue = true;
                entity.Property<bool>("Active").CurrentValue = true;
                context.SaveChanges();
                return true;
            }
        }

        public T GetSchoolByUserId(string userId)
        {
            if (typeof(T) == typeof(SchoolDTO))
            {
                using (var context = new RespositoryContext(ConnectionString))
                {
                    return context.Set<SchoolDTO>().Where(s => !s.Deleted && s.Active == true && s.UserId == userId).FirstOrDefault() as T;
                }
            }
            else
                return null;
        }

        public T GetSchoolBySchoolId(int schoolId)
        {
            if (typeof(T) == typeof(SchoolDTO))
            {
                using (var context = new RespositoryContext(ConnectionString))
                {
                    var school = context.Set<SchoolDTO>().Find(schoolId) as T;
                    return school;
                }
            }
            else
                return null;
        }

        public List<T> GetSchoolsByLocationId(int localityId)
        {
            if (typeof(T) == typeof(AddressDTO))
            {
                List<AddressDTO> _addressDTO;
                using (var context = new RespositoryContext(ConnectionString))
                {
                    _addressDTO = context.Set<AddressDTO>().Where(s => !s.Deleted && s.Active && s.Locality_Id == localityId).ToList();
                }

                foreach (var addr in _addressDTO)
                {
                    addr.School = this.GetSchoolByAddress(addr.Id);
                }
                return _addressDTO as List<T>;
            }
            else
                return null;
        }

        public T GetSchoolAddressBySchoolId(int schoolId)
        {
            if (typeof(T) == typeof(AddressDTO))
            {
                using (var context = new RespositoryContext(ConnectionString))
                {
                    return context.Set<AddressDTO>().Where(s => !s.Deleted && s.Active && s.School.Id == schoolId).FirstOrDefault() as T;
                }
            }
            else
                return null;
        }

        public List<T> GetSchoolMembersBySchoolId(int schoolId)
        {
            if (typeof(T) == typeof(MemberDTO))
            {
                using (var context = new RespositoryContext(ConnectionString))
                {
                    return context.Set<MemberDTO>().Where(s => !s.Deleted && s.Active && s.School.Id == schoolId).ToList() as List<T>;
                }
            }
            else
                return null;
        }

        public List<T> GetSchoolMembersContactBySchoolId(int schoolId)
        {
            if (typeof(T) == typeof(ContactDTO))
            {
                using (var context = new RespositoryContext(ConnectionString))
                {
                    return context.Set<ContactDTO>().Where(s => !s.Deleted && s.Active && s.School.Id == schoolId).ToList() as List<T>;
                }
            }
            else
                return null;
        }

        public async Task<IList<T>> GetAsync()
        {
            using (var context = new RespositoryContext(ConnectionString))
            {
                //var entity = context.States.ToList();
                return await Task.Run<IList<T>>(() => context.Set<T>().ToList());
            }
        }

        public async Task<T> GetByIdAsync(int Id)
        {
            using (var context = new RespositoryContext(ConnectionString))
            {
                return await Task.Run<T>(() => context.Set<T>().FindAsync(Id));
            }
        }

        public async Task<T> InsertAsync(T Obj)
        {
            using (var context = new RespositoryContext(ConnectionString))
            {
                var entity = context.Entry<T>(Obj);
                entity.Property<DateTime>("CreatedAt").CurrentValue = DateTime.UtcNow;
                entity.Property<DateTime>("UpdatedAt").CurrentValue = DateTime.UtcNow;
                await Task.Run<int>(() => context.SaveChangesAsync());
                return Obj;
            }
        }

        public async Task<T> UpdateAsync(T Obj)
        {
            using (var context = new RespositoryContext(ConnectionString))
            {
                var entity = context.Entry<T>(Obj);
                entity.Property<DateTime>("UpdatedAt").CurrentValue = DateTime.UtcNow;
                await Task.Run<int>(() => context.SaveChangesAsync());
                return Obj;
            }
        }

        public async Task<bool> DeleteAsync(T Obj)
        {
            using (var context = new RespositoryContext(ConnectionString))
            {
                var entity = context.Entry<T>(Obj);
                entity.Property<bool>("Deleted").CurrentValue = true;
                entity.Property<bool>("Active").CurrentValue = false;
                entity.Property<DateTime>("UpdatedAt").CurrentValue = DateTime.UtcNow;
                entity.State = System.Data.Entity.EntityState.Modified;
                await Task.Run<int>(() => context.SaveChangesAsync());
                return (bool)entity.Property<bool>("Deleted").CurrentValue;
            }
        }

        public async Task<bool> DeleteAsync(int Id)
        {
            using (var context = new RespositoryContext(ConnectionString))
            {
                var entry = this.GetByIdAsync(Id).Result;
                var entity = context.Entry<T>(entry);
                entity.Property<bool>("Deleted").CurrentValue = true;
                entity.Property<bool>("Active").CurrentValue = false;
                entity.Property<DateTime>("UpdatedAt").CurrentValue = DateTime.UtcNow;
                entity.State = System.Data.Entity.EntityState.Modified;
                await Task.Run<int>(() => context.SaveChangesAsync());
                return (bool)entity.Property<bool>("Deleted").CurrentValue;
            }
        }




        public List<T> GetLocalityByStateId(int stateId)
        {
            if (typeof(T) == typeof(LocalityDTO))
            {
                using (var context = new RespositoryContext(ConnectionString))
                {
                    var result = context.Database.SqlQuery<LocalityDTO>(QueryHelper.GETLocalityByState(stateId), stateId).OrderBy(l => l.Name).ToList();
                    return result as List<T>;
                }
            }
            else
                return null;
        }


        public T GetSchoolByAddressId(int addressId)
        {
            if (typeof(T) == typeof(SchoolDTO))
            {
                return this.GetSchoolByAddressId(addressId) as T;
            }
            else
                return null;
        }

        private SchoolDTO GetSchoolByAddress(int addressId)
        {
            int schoolId = 0;
            using (var context = new RespositoryContext(ConnectionString))
            {
                schoolId = context.Database.SqlQuery<int>(QueryHelper.GETSchoolByAddressId(addressId)).FirstOrDefault();
                if (schoolId > 0)
                {
                    return context.Set<SchoolDTO>().Where(s => !s.Deleted && s.Active == true && s.Id == schoolId).FirstOrDefault();
                }
                else
                    return null;
            }
        }


        public List<T> GetFacilityGroupMemberByFacilityGroupId(int facilityGroupId)
        {
            if (typeof(T) == typeof(FacilityGroupMemberDTO))
            {
                try
                {
                    using (var context = new RespositoryContext(ConnectionString))
                    {
                        var members = context.Database.SqlQuery<FacilityGroupMemberDTO>(QueryHelper.GETFacilityGroupMembersByFacilityGroupId(facilityGroupId)).ToList();
                        return members as List<T>;
                    }
                }
                catch
                {
                    return new List<T>();
                }
            }
            else
                return null;
        }

        public List<T> GetConfiguredFacilitiesBySchoolId(int schoolId)
        {
            throw new NotImplementedException();
        }
    }
}

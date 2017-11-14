using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKC.API.Models
{
    /// <summary>Class GenericAutoMapperConfiguration.</summary>
    public sealed class GenericAutoMapperConfiguration
    {
        /// <summary>Gets the instance.</summary>
        /// <value>The instance.</value>
        private static volatile GenericAutoMapperConfiguration instance;

        /// <summary>The synchronize root.</summary>
        private static object syncRoot = new object();

        /// <summary>
        /// Prevents a default instance of the <see cref="GenericAutoMapperConfiguration"/> class from being created.
        /// </summary>
        private GenericAutoMapperConfiguration()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<GenericDtoToEntityMapper>();

                // x.AddProfile<GenericEntityToDtoMapper>();
            });
        }

        /// <summary>Gets the instance.</summary>
        /// <value>The instance.</value>
        public static GenericAutoMapperConfiguration Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new GenericAutoMapperConfiguration();
                        }
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// Initialize class level variables here.
        /// </summary>
        public void Initialize()
        {
            // Initialize class level variables here
        }
    }
}

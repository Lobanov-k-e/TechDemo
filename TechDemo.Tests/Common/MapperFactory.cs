using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TechDemo.Core.Infrastructure.Mappings;

namespace TechDemo.Tests.Common
{
    static class MapperFactory
    {
        public static IMapper Create()
        {

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            return configurationProvider.CreateMapper();
        }
    }
}

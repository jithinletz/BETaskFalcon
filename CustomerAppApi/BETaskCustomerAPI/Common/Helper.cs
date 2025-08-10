using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using AutoMapper;

namespace BETaskAPI.Common
{
    public static class Helper
    {
        /// <summary>
        /// Mapping of simple class
        /// </summary>
        /// <typeparam name="Type1"></typeparam>
        /// <typeparam name="Type2"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Type2 ConvertClass<Type1, Type2>(Type1 source)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Type1, Type2>();
            });
            IMapper mapper = config.CreateMapper();
            return mapper.Map<Type1, Type2>(source);
        }
        /// <summary>
        /// Mapping of list of class
        /// </summary>
        /// <typeparam name="Type1"></typeparam>
        /// <typeparam name="Type2"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static List<Type2> ConvertList<Type1, Type2>(List<Type1> source)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Type1, Type2>();
            });
            IMapper mapper = config.CreateMapper();
            return mapper.Map<List<Type1>, List<Type2>>(source);
        }



    }
}
using BLL.Interface.Services;
using DependencyResolver;
using Ninject;

using BLL.Interface.Entities;
using System;
using System.Linq;
using System.Collections.Generic;

namespace ConsolePL
{
    static class Program
    {
        static void Main(string[] args)
        {

            IKernel resolver = new StandardKernel();
            resolver.ConfigurateResolverConsole();
            var service = resolver.Get<IUserService>();
            var contentService = resolver.Get<IContentService>();
            var searchService = resolver.Get<IContentSearchService>();
            try
            {
                var arr = searchService.Search("amazing");
                foreach(var item in arr)
                {
                    Console.WriteLine(item);
                }
                
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                Console.WriteLine(e.EntityValidationErrors);
            }

        }
    }
}

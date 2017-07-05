using Excelsior.Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;
using System.IO;
using System;
using System.Collections.Generic;

namespace Excelsior.Infrastructure.Utilities
{
    public class StringResources : IStringResources
    {
        public List<string> GetSecurityQuestions()
        {
            return GetValues("SecurityQuestions");
        }

        private List<string> GetValues( string file)
        {
            string pathRoot = Path.Combine(Directory.GetCurrentDirectory(), "StringResources");

            var builder = new ConfigurationBuilder()
                              .SetBasePath(pathRoot)
                             .AddJsonFile($"{file}.json");
            var configuration = builder.Build();
            var values = configuration.AsEnumerable();
            var result = new List<string>();
            foreach (var item in values)
            {
                result.Add(item.Value);
            }
            return result;
        }

       
    }
}

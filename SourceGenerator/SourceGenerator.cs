using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.CodeAnalysis;

namespace SourceGenerator
{
    [Generator]
    public class SourceGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            //var classNames = new List<string>() { "Class1", "Class2", "Class3" };
            //System.Diagnostics.Debugger.Launch();
            var classNames = new PostgreDataProvider().GetData();

            foreach (var name in classNames)
            {
                var code =  @"
                     using FluentNHibernate.Mapping;

                    namespace TestTaskSolution.Models.Mappings
                    {
                        public class " + name + @"
                        {
                            public virtual long Id { get; set; }
                        }

                        public class " + name + @"Map: ClassMap<" + name + @">
                        {
                            public " + name + @"Map()
                            {
                            this.Id(x => x.Id);            
                            }
                        }
                    }";

                context.AddSource($"{name}Map.cs", code);
            }
        }
        public void Initialize(GeneratorInitializationContext context)
        {

        }
    }
}

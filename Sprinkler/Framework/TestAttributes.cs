﻿/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.github.com/furore-fhir/sprinkler/master/LICENSE
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sprinkler.Framework
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class SprinklerTestModuleAttribute : Attribute
    {
        // This is a positional argument
        public SprinklerTestModuleAttribute(string name)
        {
            this.Name = name;
        }

        public string Name
        {
            get;
            private set;
        }
    }

    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class SprinklerTestAttribute : Attribute
    {
        public string Title { get; private set; }
        public string Code { get; private set; }

        // This is a positional argument
        public SprinklerTestAttribute(string code, string title)
        {
            this.Code = code;
            this.Title = title;
        }

        
    }


}

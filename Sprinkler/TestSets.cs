﻿using System;
using System.Collections.Generic;
using System.Linq;
using Hl7.Fhir.Rest;
using Sprinkler.Framework;

namespace Sprinkler
{
    public class TestSets
    {
        private static Action<TestResult> LogTest(TestResults results)
        {
            return results.AddTestResult;
        }

        private static FhirClient GetClient(string url)
        {
            if (!CheckUrlHasValidPrefix(url))
                url = "http://" + url;

            var endpoint = new Uri(url, UriKind.Absolute);
            return new FhirClient(endpoint);
        }

        private static bool CheckUrlHasValidPrefix(string url)
        {
            string[] validPrefixes = { "http:", "https:" };
            return validPrefixes.Any(url.StartsWith);
        }

        public static bool IsValidUrl(string text)
        {
            Uri uri;
            
            return (Uri.TryCreate(text, UriKind.Absolute, out uri) && null != uri && CheckUrlHasValidPrefix(text));
        }

        // overloaded Run method
        public static TestResults Run(string url)
        {
            return Run(url, null as string[]);
        }

        // overloaded Run method
        public static TestResults Run(string url, params string[] tests)
        {
            return Run(url, new TestResults(), tests);
        }

        // Run method
        public static TestResults Run(string url, TestResults results, params string[] tests)
        {
            var client = GetClient(url);
            var tester = new TestRunner(client, LogTest(results));

            tester.Run(tests);
            return results;
        }

        public static IDictionary<string, List<Tuple<string, string>>> GetTestModules()
        {
            IDictionary<string, List<Tuple<string, string>>> dictModulesCodes =
                new Dictionary<string, List<Tuple<string, string>>>();
            var testclasses = TestRunner.GetTestClasses();

            foreach (var testclass in testclasses)
            {
                var testmethods =
                    TestRunner.GetTestMethods(testclass)
                        .Select(SprinklerTestAttribute.AttributeOf)
                        .Select(methodAttr => Tuple.Create(methodAttr.Code, methodAttr.Title))
                        .ToList();
                var moduleAttr = SprinklerTestModuleAttribute.AttributeOf(testclass);
                dictModulesCodes.Add(moduleAttr.Name, testmethods);
            }
            return dictModulesCodes;
        }
    }
}
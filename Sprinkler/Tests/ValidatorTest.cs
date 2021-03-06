﻿/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.github.com/furore-fhir/sprinkler/master/LICENSE
 */
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Validation;
using Sprinkler.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Sprinkler.Tests
{
  
    [SprinklerTestModule("Validation")]
    public class ValidatorTest : SprinklerTestClass
    {
        [SprinklerTest("V001", "Validate a valid resource")]
        public void ValidateCreateResource()
        {
            Patient patient = DemoData.GetDemoPatient();

            OperationOutcome oo;
            if(!client.TryValidateCreate(patient, out oo, null))
                TestResult.Fail("Validation incorrectly reported failure.");
        }

        [SprinklerTest("V002", "Validate an invalid resource")]
        public void ValidateInvalidCreateResource()
        {
            Patient patient = DemoData.GetDemoPatient();
            patient.Identifier = new List<Identifier> { new Identifier { System = new Uri("urn:oid:hallo") } };

            OperationOutcome oo;
            if (!client.TryValidateCreate(patient, out oo, null))
                TestResult.Fail("Validation incorrectly reported failure.");
        }

        [SprinklerTest("V003", "Validate a valid resource update")]
        public void ValidateUpdateResource()
        {
            Patient patient = DemoData.GetDemoPatient();
            ResourceEntry<Patient> result = client.Create<Patient>(patient);

            OperationOutcome oo;
            if(!client.TryValidateUpdate(result, out oo))
                TestResult.Fail("Validation incorrectly reported failure.");
            
        }

        [SprinklerTest("V004", "Validate an invalid resource update")]
        public void ValidateInvalidUpdateResource()
        {
            Patient patient = DemoData.GetDemoPatient();
            ResourceEntry<Patient> result = client.Create<Patient>(patient);
            patient.Identifier = new List<Identifier> { new Identifier { System = new Uri("urn:oid:hallo") } };

            OperationOutcome oo;
            if (!client.TryValidateUpdate(result, out oo)) 
                TestResult.Fail("Validation incorrectly reported failure.");
        }
       
    }
}

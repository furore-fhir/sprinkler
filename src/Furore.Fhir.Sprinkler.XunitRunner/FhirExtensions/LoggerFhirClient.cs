﻿using System;
using System.IO;
using Hl7.Fhir.Rest;

namespace Furore.Fhir.Sprinkler.XunitRunner.FhirExtensions
{
    public class LoggerFhirClient : FhirClient
    {
        public LoggerFhirClient(Uri endpoint, bool verifyFhirVersion = false) : base(endpoint, verifyFhirVersion)
        {
            this.OnBeforeRequest += LoggerFhirClient_OnBeforeRequest;
            this.OnAfterResponse += LoggerFhirClient_OnAfterResponse;
        }

        private void LoggerFhirClient_OnAfterResponse(object sender, AfterResponseEventArgs e)
        {
            if (e.Body!= null && e.Body.Length > 0)
            {
                string body = new StreamReader(new MemoryStream(e.Body)).ReadToEnd();
            }
        }

        private void LoggerFhirClient_OnBeforeRequest(object sender, BeforeRequestEventArgs e)
        {
            string url = e.RawRequest.RequestUri.ToString();
            string method = e.RawRequest.Method;
            if (e.Body != null && e.Body.Length > 0)
            {
                string body = new StreamReader(new MemoryStream(e.Body)).ReadToEnd();
            }
        }

        public LoggerFhirClient(string endpoint, bool verifyFhirVersion = false) : base(endpoint, verifyFhirVersion)
        {
            this.OnBeforeRequest += LoggerFhirClient_OnBeforeRequest;
            this.OnAfterResponse += LoggerFhirClient_OnAfterResponse;
        }
    }
}
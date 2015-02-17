﻿using System;
using System.Net;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Rebilly.v2_1
{
    public class Contact : RebillyRequest
    {
        const string CONTACT_END_POINT = "contacts/";

        /// <summary>
        /// firstName
        /// </summary>
        public string firstName = null;
        /// <summary>
        /// lastName
        /// </summary>
        public string lastName = null;
        /// <summary>
        /// organization
        /// </summary>
        public string organization = null;
        /// <summary>
        /// address
        /// </summary>
        public string address = null;
        /// <summary>
        /// address2
        /// </summary>
        public string address2 = null;
        /// <summary>
        /// city
        /// </summary>
        public string city = null;
        /// <summary>
        /// region
        /// </summary>
        public string region = null;
        /// <summary>
        /// country
        /// </summary>
        public string country = null;
        /// <summary>
        /// phoneNumber
        /// </summary>
        public string phoneNumber = null;
        /// <summary>
        /// postalCode
        /// </summary>
        public string postalCode = null;
        /// <summary>
        /// Contact's ID
        /// </summary>
        private string id = null;
        
        /// <summary>
        /// Set api version and endpoint
        /// </summary>
        /// <param name="id"></param>
        public Contact(string id = null)
        {
            if (!String.IsNullOrEmpty(id))
            {
                this.id = id;
            }
            this.setApiVersion("v2.1");
            this.setApiController(CONTACT_END_POINT);
        }

        /// <summary>
        /// Create new contact
        /// </summary>
        /// <returns>RebillyResponse</returns>
        /// <example>
        /// <code>
        ///     Rebilly.v2_1.Contact contact = new Rebilly.v2_1.Contact();
        ///     contact.setApiKey("your api key");
        ///     contact.setEnvironment(RebillyRequest.ENV_SANDBOX);
        ///     
        ///     contact.firstName = "John";
        ///     contact.lastName = "Doe";
        ///     contact.organization = "Test Org";
        ///     contact.address = "2020 Rue test";
        ///     contact.city = "City";
        ///     
        ///     RebillyResponse response = contact.create();
        ///     if (response.statusCode == HttpStatusCode.Created)
        ///     {
        ///         // Successfully created
        ///     }
        /// </code>
        /// </example>
        public RebillyResponse create()
        {
            string data = this.buildRequest(this);

            return this.sendPostRequest(data);
        }

        /// <summary>
        /// Create a new contact with ID
        /// </summary>
        /// <returns>RebillyResponse</returns>
        /// <example>
        /// <code>
        ///     Rebilly.v2_1.Contact contact = new Rebilly.v2_1.Contact("contactId");
        ///     contact.setApiKey("your api key");
        ///     contact.setEnvironment(RebillyRequest.ENV_SANDBOX);
        ///     
        ///     RebillyResponse response = contact.update();
        ///     if (response.statusCode == HttpStatusCode.Created)
        ///     {
        ///         // Successfully created
        ///     }
        /// </code>
        /// </example>
        public RebillyResponse update()
        {
            if (String.IsNullOrEmpty(this.id))
            {
                throw new Exception("contact id cannot be empty.");
            }
            this.setApiController(CONTACT_END_POINT + this.id);
            string data = this.buildRequest(this);

            return this.sendPutRequest(data);
        }

        /// <summary>
        /// Get a contact
        /// </summary>
        /// <returns>RebillyResponse</returns>
        /// <example>
        /// <code>
        ///     Rebilly.v2_1.Contact contact = new Rebilly.v2_1.Contact("contactId");
        ///     contact.setApiKey("your api key");
        ///     contact.setEnvironment(RebillyRequest.ENV_SANDBOX);
        ///     
        ///     RebillyResponse response = contact.retrieve();
        ///     if (response.statusCode == HttpStatusCode.OK)
        ///     {
        ///         // Success
        ///     }
        /// </code>
        /// </example>
        public RebillyResponse retrieve()
        {
            if (String.IsNullOrEmpty(this.id))
            {
                throw new Exception("contact id cannot be empty.");
            }
            this.setApiController(CONTACT_END_POINT + this.id);

            return this.sendGetRequest();
        }

        /// <summary>
        /// Get a contact
        /// </summary>
        /// <returns>RebillyResponse</returns>
        /// <example>
        /// <code>
        ///     Rebilly.v2_1.Contact contact = new Rebilly.v2_1.Contact();
        ///     contact.setApiKey("your api key");
        ///     contact.setEnvironment(RebillyRequest.ENV_SANDBOX);
        ///     
        ///     RebillyResponse response = contact.listAll();
        ///     if (response.statusCode == HttpStatusCode.OK)
        ///     {
        ///         // Success
        ///     }
        /// </code>
        /// </example>
        public RebillyResponse listAll()
        {
            return this.sendGetRequest();
        }

        /// <summary>
        /// Helper function to convert from object to JSON ready to send to Rebilly
        /// </summary>
        /// <param name="blacklist">Blacklist object</param>
        /// <returns>data in JSON format</returns>
        private string buildRequest(Contact contact)
        {
            string data = JsonConvert.SerializeObject(contact, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            return data;
        }
    }
}

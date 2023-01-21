using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xrm.Tooling.Connector;
using Microsoft.Xrm.Sdk;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;

namespace CRUD_operation
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var connectionString = @"AuthType = Office365; Url = https://org8b2a8c21.api.crm8.dynamics.com/XRMServices/2011/Organization.svc;Username=aliahmed@1107admin.onmicrosoft.com;Password=Roman@1107$";
                CrmServiceClient conn = new CrmServiceClient(connectionString);

                IOrganizationService service;
                service = (IOrganizationService)conn.OrganizationWebProxyClient != null ? (IOrganizationService)conn.OrganizationWebProxyClient : (IOrganizationService)conn.OrganizationServiceProxy;

                // Create a new record
                Entity contact = new Entity("contact");
                contact["firstname"] = "New";
                contact["lastname"] = "Contact";
                contact["mobilephone"] = "987565321";
                contact["emailaddress1"] = "slef@newcontact.co";
                contact["birthdate"] = new DateTime(2000, 01, 01);
                contact.Attributes.Add("preferredcontactmethodcode", new OptionSetValue(1));
               // contact["parentcustomerid"] = new EntityReference("contact", new Guid("8894c5c4-bc98-ed11-aad0-000d3af2358c"));
                contact["parentcustomerid"] = new EntityReference("contact", new Guid("CEB4873D-BC98-ED11-AAD0-000D3AF2358C"));
                Guid contactId = service.Create(contact);
                Console.WriteLine("New contact id: {0}.", contactId.ToString());

                // Retrieve a record using Id
                Entity retrievedContact = service.Retrieve(contact.LogicalName, contactId, new ColumnSet(true));
                Console.WriteLine("Record retrieved {0}", retrievedContact.Id.ToString());


                // Update record using Id, retrieve all attributes
                Entity updatedContact = new Entity("contact");
                updatedContact = service.Retrieve(contact.LogicalName, contactId, new ColumnSet(true));
                //string string.empty to make empty a string value
                updatedContact["firstname"] = "vishal";
                updatedContact["lastname"] = "Kumar";
                updatedContact["jobtitle"] = "senior DEVELOPER";
                updatedContact["emailaddress1"] = "vishal@test.com";
                updatedContact["mobilephone"] = "000000";
                updatedContact["preferredcontactmethodcode"] = new OptionSetValue(3);
                service.Update(updatedContact);
                Console.WriteLine("Updated contact");
                Console.ReadLine();



                // Retrieve specific fields using ColumnSet
                /* ColumnSet attributes = new ColumnSet(new string[] { "firstname", "lastname", "jobtitle", "emailaddress1", "mobilephone", "birthdate" });
                 retrievedContact = service.Retrieve(contact.LogicalName, contactId, attributes);
                 foreach (var a in retrievedContact.Attributes)
                 {
                     // Console.WriteLine("Retrieved contact first name field {0} - {0}", a.Key, a.Value);
                     Console.WriteLine("Retrieved contact last name  field {0} - {1}", a.Key, a.Value);
                     Console.WriteLine("Retrieved contact jobtitle   field {0} - {2}", a.Key, a.Value);
                     Console.WriteLine("Retrieved contact email address field {0} - {3}", a.Key, a.Value);
                     Console.WriteLine("Retrieved contact mobile number field {0} - {4}", a.Key, a.Value);

                     Console.ReadLine();
                 }*/

                // Delete a record using Id
                /* service.Delete(contact.LogicalName, contactId);
                 Console.WriteLine("Deleted");
                 Console.ReadLine();*/

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
    }
}

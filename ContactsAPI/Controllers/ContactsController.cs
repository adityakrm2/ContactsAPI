using ContactsAPI.Config;
using ContactsAPI.Data;
using ContactsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.Net;
using System.Numerics;
using System.Text.Json;

namespace ContactsAPI.Controllers
{
    public class EmployeeJSON
    {
        public string EmployeeName { get; set; }
        public string EmployeeEmail { get; set; }
        public long EmployeePhone { get; set; }
        public string? EmployeeAddress { get; set; }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : Controller
    {
        private readonly ContactsAPIDbContext dbContext;
        public ContactsController(ContactsAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            return Ok(await dbContext.Contacts.ToListAsync());
        }
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetContact([FromRoute] Guid id)
        {
            var contact = await dbContext.Contacts.FindAsync(id);
            if (contact != null) return Ok(contact);
            return NotFound();
        }
        //[HttpPost]
        //public async Task<IActionResult> AddContact(AddContactRequest addContactRequest)
        //{
        //    var contact = new Contact()
        //    {
        //        Id = Guid.NewGuid(),
        //        Address = addContactRequest.Address,
        //        Email = addContactRequest.Email,
        //        FullName = addContactRequest.FullName,
        //        Phone = addContactRequest.Phone,

        //    };
        //    await dbContext.Contacts.AddAsync(contact);
        //    await dbContext.SaveChangesAsync();
        //    return Ok(contact);
        //}
        [HttpPost]
        public async Task<IActionResult> AddContact()
        {
            string jsonString =
        @"{""EmployeeName"":""adityakumar199"",
            ""EmployeeEmail"": ""adityakumar188@gmail.com"",
            ""EmployeeAddress"":""phase 1"",
            ""EmployeePhone"":99345,
            }";
            var contact = new Contact()
            {
                Id = Guid.NewGuid(),
            };
            Dictionary<string, dynamic> mappedDict = FieldConfig.UsingDynamic(jsonString);
            contact.FullName = mappedDict["EmployeeName"];
            contact.Email = mappedDict["EmployeeEmail"];
            contact.Address = mappedDict["EmployeeAddress"];
            contact.Phone = mappedDict["EmployeePhone"];

            await dbContext.Contacts.AddAsync(contact);
            await dbContext.SaveChangesAsync();
            return Ok(contact);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateContact([FromRoute] Guid id,UpdateContactRequest updateContactRequest)
        {
            var contact = await dbContext.Contacts.FindAsync(id);
            if (contact != null)
            {
                contact.Address = updateContactRequest.Address;
                contact.Email = updateContactRequest.Email;
                contact.FullName = updateContactRequest.FullName;
                contact.Phone = updateContactRequest.Phone;

                
                await dbContext.SaveChangesAsync();
                return Ok(contact);
            }
            return NotFound();
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteContact([FromRoute] Guid id)
        {
            var contact = await dbContext.Contacts.FindAsync(id);
            if (contact != null)
            {
                dbContext.Remove(contact);
                await dbContext.SaveChangesAsync();
                return Ok(contact);
            }
            return NotFound();
        }
    }
}

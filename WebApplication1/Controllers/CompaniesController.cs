using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CompaniesController : ApiController
    {
        // GET api/<controller>
        public List<Company> Get()
        {
            Company c = new Company();
            return c.getAllCompanies();
        }


        [HttpGet]
        // GET api/<controller>/5
        [Route("api/Companies/{username}/{password}")]
        public int Get(string username, string password)
        {
            Company c = new Company(username, password);
            return c.getId();
        }

        [HttpGet]
        // GET api/<controller>/5
        [Route("api/Companies/users")]
        public List<string> GetUsers()
        {
            Company c = new Company();
            return c.getUsers();
        }

        // POST api/<controller>
        public string Post([FromBody]Company c)
        {
            if (c.insertCompany() == -1)
                return "Company Already Exist";
            return "Company successfully added";
        }


        // PUT api/<controller>/5
        public List<Company> Put([FromBody]Company c)
        {
            return c.edit();
        }

        // DELETE api/<controller>/5
        public List<Company> Delete(int id)
        {
            Company c = new Company();
            c.Id = id;
            return c.deleteByID();
        }
    }
}
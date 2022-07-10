using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models.DAL;

namespace WebApplication1.Models
{
    public class Company
    {
        private string userName;
        private string password;
        private string compName;
        private string logoSrc;
        private string oprCountry;
        private int numCinemaOwns;
        private string established;
        private int id;

        public Company(string userName, string password, string compName, string logoSrc, string oprCountry, int numCinemaOwns, string established)
        {
            this.userName = userName;
            this.password = password;
            this.compName = compName;
            this.logoSrc = logoSrc;
            this.oprCountry = oprCountry;
            this.numCinemaOwns = numCinemaOwns;
            this.established = established;
            this.Id = -1;
        }

        public Company(int id,string userName, string password, string compName, string logoSrc, string oprCountry, int numCinemaOwns, string established)
        {
            this.Id = id;
            this.userName = userName;
            this.password = password;
            this.compName = compName;
            this.logoSrc = logoSrc;
            this.oprCountry = oprCountry;
            this.numCinemaOwns = numCinemaOwns;
            this.established = established;
        }


        public Company(string userName, string password)
        {
            this.userName = userName;
            this.password = password;
            this.compName = "";
            this.logoSrc = "";
            this.oprCountry = "";
            this.numCinemaOwns = -1;
            this.established = "";
            this.Id = -1;
        }

        public Company()
        {
        }

        public string UserName { get => userName; set => userName = value; }
        public string Password { get => password; set => password = value; }
        public string CompName { get => compName; set => compName = value; }
        public string LogoSrc { get => logoSrc; set => logoSrc = value; }
        public string OprCountry { get => oprCountry; set => oprCountry = value; }
        public int NumCinemaOwns { get => numCinemaOwns; set => numCinemaOwns = value; }
        public string Established { get => established; set => established = value; }
        public int Id { get => id; set => id = value; }

        public override bool Equals(object obj)
        {
            var company = obj as Company;
            return company != null &&
                   compName == company.compName;
        }

        public int insertCompany()
        {
            DataServices ds = new DataServices();
            if (ds.insert(this) == 1)
                return 1;

            return -1;
        }

        public int getId()
        {
            DataServices ds = new DataServices();
            return ds.getId(this);
        }

        public List<Company> getAllCompanies()
        {
            DataServices ds = new DataServices();
            return ds.getAllCompaniesData();
        }

        public List<Company> deleteByID()
        {
            DataServices ds = new DataServices();
            return ds.deleteByID(this.Id);
        }

        public List<Company> edit()
        {
            DataServices ds = new DataServices();
            return ds.edit(this);
        }

        public List<string> getUsers()
        {
            DataServices ds = new DataServices();
            return ds.getAllUsers();
        }

    }
}
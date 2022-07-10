using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models.DAL;

namespace WebApplication1.Models
{
    public class C_M
    {
        private int idC;
        private int idM;

        public C_M(int idC, int idM)
        {
            this.IdC = idC;
            this.IdM = idM;
        }

        public int IdC { get => idC; set => idC = value; }
        public int IdM { get => idM; set => idM = value; }

        public int insert()
        {
            DataServices ds = new DataServices();
            return ds.insert(this);
        }
    }
}
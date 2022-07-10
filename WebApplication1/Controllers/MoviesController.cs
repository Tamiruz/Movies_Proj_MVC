using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class MoviesController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        //GET api/<controller>/5
        public Movie Get(int id)
        {
            Movie m = new Movie();
            return m.getMovieById(id);
        }

        //POST api/<controller>
        public int Post([FromBody]Movie m)
        {
            return m.insertMovie();
        }

        [HttpGet]
        [Route("api/Movies/list/{id}")]
        public List<Movie> GetMovies(int id)
        {
            Movie m1 = new Movie();
            return m1.getMoviesList(id);
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}
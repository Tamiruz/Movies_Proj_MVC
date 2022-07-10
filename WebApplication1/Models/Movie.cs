using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models.DAL;
using System.Collections;

namespace WebApplication1.Models
{
    public class Movie
    {
        int id;
        string name;
        string genre;
        string publish;
        double avg_sc;
        string src;
        string descr;

        public Movie(int id, string name, string genre,string publish, double avg_sc, string src, string descr)
        {
            this.id = id;
            this.name = name;
            this.genre = genre;
            this.publish = publish;
            this.avg_sc = avg_sc;
            this.src = src;
            this.descr = descr;
        }

        public Movie()
        {
            this.id = 0;
            this.name = "";
            this.Genre = null;
            this.publish = "";
            this.avg_sc = 0.0;
            this.src = "";
            this.descr = "";
        }

        public string Name { get => name; set => name = value; }
        public string Publish { get => publish; set => publish = value; }
        public string Genre { get => genre; set => genre = value; }
        public double Avg_sc { get => avg_sc; set => avg_sc = value; }
        public string Src { get => src; set => src = value; }
        public string Descr { get => descr; set => descr = value; }
        public int Id { get => id; set => id = value; }

        public override bool Equals(object obj)
        {
            var movie = obj as Movie;
            return movie != null &&
                   id == movie.id;
        }

        public int insertMovie()
        {
            DataServices ds = new DataServices();
            return ds.insert(this);             
        }

        public List<Movie> getMoviesList(int id)
        {
            DataServices ds = new DataServices();
            return ds.getMovies(id);
        }

        public Movie getMovieById(int id)
        {
            DataServices ds = new DataServices();
            return ds.getMovieById(id);
        }


    }
}
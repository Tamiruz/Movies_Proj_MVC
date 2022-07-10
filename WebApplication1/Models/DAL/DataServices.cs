using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Helpers;
using Newtonsoft.Json.Linq;



namespace WebApplication1.Models.DAL
{
    public class DataServices
    {

        //Insert company to the database.
        public int insert(Company c)
        {
            // Connect
            SqlConnection con = Connect();

            // Create Command
            SqlCommand command = CreateInsertCommand(con, c);

            // Execute
            int numAffected = command.ExecuteNonQuery();

            // Close Connection

            con.Close();

            return numAffected;
        }

        //Creating insert command for insert a new company.
        private SqlCommand CreateInsertCommand(SqlConnection con, Company c)
        {
            SqlCommand command = new SqlCommand();

            command.Parameters.AddWithValue("@username", c.UserName);
            command.Parameters.AddWithValue("@password", c.Password);
            command.Parameters.AddWithValue("@logoSrc", c.LogoSrc);
            command.Parameters.AddWithValue("@compName", c.CompName);
            command.Parameters.AddWithValue("@oprCountry", c.OprCountry);
            command.Parameters.AddWithValue("@numCinemaOwns", c.NumCinemaOwns);
            command.Parameters.AddWithValue("@established", c.Established);

            command.CommandText = "SPinsertCompany";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds

            return command;
        }

        //Get all movies by company id.
        public List<Movie> getMovies(int id)
        {
            // Connect
            SqlConnection con = Connect();

            // Create Command
            SqlCommand command = CreateSelectCommand(con, id);

            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);

            List<Movie> movies = new List<Movie>();

            string name = "";
            string genre = "";
            string publish = "";
            double avg_sc = 0.0;
            string src = "";
            string desc = "";
            int idM = -1;
            Movie m = null;
            while (dr.Read())
            {
                idM = Convert.ToInt32(dr["id"]);
                name = dr["name"].ToString();
                genre = dr["genre"].ToString();
                publish = dr["publish"].ToString();
                avg_sc = Convert.ToDouble(dr["avg_sc"]);
                src = dr["src"].ToString();
                desc = dr["descr"].ToString();
                m = new Movie(idM, name, genre, publish, avg_sc, src, desc);
                movies.Add(m);

            }

            con.Close();

            return movies;
        }

        //Get all movies select command by company ID.
        private SqlCommand CreateSelectCommand(SqlConnection con, int id)
        {

            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@id", id);
            command.CommandText = "SPgetAllMovies";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds
            return command;
        }
        
        //Insert movie to the database.
        public int insert(Movie m)
        {
            // Connect
            SqlConnection con = Connect();

            // Create Command
            SqlCommand command = CreateInsertCommand(con, m);

            // Check if movie already exists
            if (search(m) == m.Id)
                return m.Id;

            // Execute
            int numAffected = command.ExecuteNonQuery();

            // Close Connection

            con.Close();

            return m.Id;
        }

        //Creating insert command for insert a new movie to DB.
        private SqlCommand CreateInsertCommand(SqlConnection con, Movie m)
        {
            SqlCommand command = new SqlCommand();
             //Getting all parameters from the movie.
            command.Parameters.AddWithValue("@id",m.Id);
            command.Parameters.AddWithValue("@name", m.Name);
            command.Parameters.AddWithValue("@genre", m.Genre);
            command.Parameters.AddWithValue("@publish", m.Publish);
            command.Parameters.AddWithValue("@avg_sc", m.Avg_sc);
            command.Parameters.AddWithValue("@src", m.Src);
            command.Parameters.AddWithValue("@desc", m.Descr);


            command.CommandText = "SPinsertMovie";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds

            return command;
        }

        //Get a specific movie by movie id.
        public Movie getMovieById(int id)
        {
            SqlConnection con = Connect();

            // Create Command
            SqlCommand command = CreateSelectMovieCommand(con, id);

            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);

            string name = "";
            string genre = "";
            string publish = "";
            double avg_sc = 0.0;
            string src = "";
            string desc = "";
            Movie m = null;
            while (dr.Read())
            {
                name = dr["name"].ToString();
                genre = dr["genre"].ToString();
                publish = dr["publish"].ToString();
                avg_sc = Convert.ToDouble(dr["avg_sc"]);
                src = dr["src"].ToString();
                desc = dr["descr"].ToString();
            }
            m = new Movie(id, name, genre, publish, avg_sc, src, desc);
            con.Close();
            return m;
        }

        //Creating a select command to get a specific movie by its ID.
        private SqlCommand CreateSelectMovieCommand(SqlConnection con, int id)
        {

            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@id", id);
            command.CommandText = "SPgetMovieByID";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds
            return command;
        }

        //Creating an SQL connection to the DB.
        private SqlConnection Connect()
        {
            // read the connection string from the web.config file
            string connectionString = WebConfigurationManager.ConnectionStrings["DBmovies"].ConnectionString;

            // create the connection to the db
            SqlConnection con = new SqlConnection(connectionString);

            // open the database connection
            con.Open();

            return con;

        }

        //Get the company ID, by username and password.
        public int getId(Company c)
        {
            SqlConnection con = Connect();

            SqlCommand command = CreateSelectCommand(con, c);

            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);

            int id = -1;

            while (dr.Read())  
                id = Convert.ToInt32(dr["id"]);

            con.Close();

            return id;
        }

        //Create select command and get the company's ID.
        private SqlCommand CreateSelectCommand(SqlConnection con, Company c)
        {

            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@username", c.UserName);
            command.Parameters.AddWithValue("@password", c.Password);
            command.CommandText = "SPreturnCId";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds

            return command;
        }

        //Inserting a Movie,Company relation.
        public int insert(C_M cm)
        {
            // Connect
            SqlConnection con = Connect();

            // Create Command
            SqlCommand command = CreateInsertCommand(con, cm);

            // Check if movie already exists in the company
            List<int> id = search(cm);
            if (id.Count() > 0)
            {
                if (id[0] == cm.IdM && id[1] == cm.IdC)
                    return 0;
            }

            int numAffected = command.ExecuteNonQuery();

            // Close Connection

            con.Close();

            return numAffected;
        }

        //Create a insert command for a Movie,Company relation, by SPinsertCM.
        private SqlCommand CreateInsertCommand(SqlConnection con, C_M cm)
        {

            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@idC", cm.IdC); 
            command.Parameters.AddWithValue("@idM", cm.IdM);
            command.CommandText = "SPinsertCM";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds

            return command;
        }

        //Searching for a specific movie, and return his ID.
        public int search(Movie m)
        {
            SqlConnection con = Connect();

            SqlCommand command = CreateSelectCommand(con, m);

            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);

            int id = -1;

            while (dr.Read())
                id = Convert.ToInt32(dr["id"]);

            con.Close();

            return id;

        }

        //Create a select command by SPsearchMovie, and finds the movie id.
        private SqlCommand CreateSelectCommand(SqlConnection con, Movie m)
        {

            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@id", m.Id);
            command.CommandText = "SPsearchMovie";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds

            return command;
        }

        //Searching for a specific Movie, Company relation, and return the movie and company ID in array.
        public List<int> search(C_M cm)
        {
            SqlConnection con = Connect();

            SqlCommand command = CreateSelectCommand(con, cm);

            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);

            List<int> id = new List<int>();

            while (dr.Read())
            {
                id.Add(Convert.ToInt32(dr["idM"]));
                id.Add(Convert.ToInt32(dr["idC"]));
            }

            con.Close();

            return id;

        }

        //Create select command with a Movie, Company relation, get from DB by SPsearchC_M
        private SqlCommand CreateSelectCommand(SqlConnection con, C_M cm)
        {

            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@idC", cm.IdC);
            command.Parameters.AddWithValue("@idM", cm.IdM);
            command.CommandText = "SPsearchC_M";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds

            return command;
        }

        //Get all Companies that exist in our DB.
        public List<Company> getAllCompaniesData()
        {
            SqlConnection con = Connect();

            SqlCommand command = CreateSelectCommand(con);

            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);

            List<Company> companies = new List<Company>();


            while (dr.Read())
            {
                string username = dr["username"].ToString();
                string password = dr["password"].ToString();
                string compName = dr["compName"].ToString();
                string logoSrc = dr["logoSrc"].ToString();
                string oprCountry = dr["oprCountry"].ToString();
                int numCinemaOwns = Convert.ToInt32(dr["numCinemaOwns"]);
                string established = dr["established"].ToString();
                int id = Convert.ToInt32(dr["id"]);
                Company c = new Company(username, password, compName, logoSrc, oprCountry, numCinemaOwns, established);
                c.Id = id;
                companies.Add(c);
            }

            con.Close();

            return companies;
        }

        //Create select command with SPgetAllCompanies.
        private SqlCommand CreateSelectCommand(SqlConnection con)
        {

            SqlCommand command = new SqlCommand();
            command.CommandText = "SPgetAllCompanies";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds

            return command;
        }

        //Delete Company from the DB by the company's ID.
        public List<Company> deleteByID(int id)
        {
            SqlConnection con = Connect();     

            SqlCommand command = CreateDeleteCommand(con,id);

            int numAffected = command.ExecuteNonQuery();

            con.Close();

            return getAllCompaniesData();
        
        }

        // Create a select command by selecting a specific company by its ID and delete it.
        private SqlCommand CreateDeleteCommand(SqlConnection con,int id)
        {

            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@id",id);
            command.CommandText = "SPdeleteByID";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds

            return command;
        }

        // Edit exist company details.
        public List<Company> edit(Company c)
        {
            SqlConnection con = Connect();

            SqlCommand command = CreateEditCommand(con, c);

            int numAffected = command.ExecuteNonQuery();

            con.Close();

            return getAllCompaniesData();

        }

        // Creating command for editing company.
        private SqlCommand CreateEditCommand(SqlConnection con, Company c)
        {

            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@id", c.Id);
            command.Parameters.AddWithValue("@username", c.UserName);
            command.Parameters.AddWithValue("@password", c.Password);
            command.Parameters.AddWithValue("@logoSrc", c.LogoSrc);
            command.Parameters.AddWithValue("@compName", c.CompName);
            command.Parameters.AddWithValue("@oprCountry", c.OprCountry);
            command.Parameters.AddWithValue("@numCinemaOwns", c.NumCinemaOwns);
            command.Parameters.AddWithValue("@established", c.Established);

            command.CommandText = "SPupdateCompany";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds

            return command;
        }

        // Returns all users and their password.
        public List<string> getAllUsers()
        {
            // Connect
            SqlConnection con = Connect();

            // Create Command
            SqlCommand command = CreateSelectUsersCommand(con);

            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);

            List<string> users = new List<string>();

            while (dr.Read())
            {
                users.Add(dr["username"].ToString());
                users.Add(dr["password"].ToString());
                users.Add(dr["id"].ToString());
            }

            con.Close();

            return users;
        }

        //Create select command to get all users and their password from our DB.
        private SqlCommand CreateSelectUsersCommand(SqlConnection con)
        {

            SqlCommand command = new SqlCommand();
            command.CommandText = "SPgetAllUsers";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds

            return command;
        }

    }
}
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tour_Planner.Models;

namespace Tour_Planner.DAL
{
    public class TourDAO : ITourDAO
    {
        private readonly NpgsqlConnection conn = new(DBConfigAccess.Instance().GetConnectionString());

        ~TourDAO()
        {
            if (conn.State.Equals(System.Data.ConnectionState.Open))
                conn.Close();
        }
        /*private void DB_Connect()
        {
            conn.Open();
        }

        private void DB_Close()
        {
            conn.Close();
        }*/

        public void DB_AddLog(Tour tour)
        {
            throw new NotImplementedException();
        }

        public void DB_AddTour(Tour tour)
        {
            conn.Open();

            string query = $"INSERT INTO public.\"Tours\" (title) values ('Gustav');";
            NpgsqlCommand command = new NpgsqlCommand(query, conn);
            NpgsqlDataReader read = command.ExecuteReader(); 
            
            conn.Close();
        }

        public void DB_DeleteLog(Tour tour)
        {
            throw new NotImplementedException();
        }

        public void DB_DeleteTour(Tour tour)
        {
            throw new NotImplementedException();
        }

        public void DB_GetAllTours()
        {
            throw new NotImplementedException();
        }

        public void DB_SearchTour(string search)
        {
            throw new NotImplementedException();
        }

        public void DB_UpdateLog(Tour tour)
        {
            throw new NotImplementedException();
        }

        public void DB_UpdateTour(Tour tour)
        {
            throw new NotImplementedException();
        }
    }
}

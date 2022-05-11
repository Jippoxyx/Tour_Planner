using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tour_Planner.Models;

namespace Tour_Planner.DAL
{
    class TourManager : ITourManager
    {
        private readonly NpgsqlConnection conn = new(DBConfigAccess.Instance().GetConnectionString());

        public void CreateLog(Tour tour)
        {
                conn.Open();
            foreach (var log in tour.Logs)
            {

                string query = $"INSERT INTO public.\"Logs\" (id, date, time, comment, difficulty, total_time, rating, Id_tour)" +
                    $" values (@id, @date, @time, @comment, @difficulty, @total_time, @rating, @id_tour);";
                NpgsqlCommand command = new NpgsqlCommand(query, conn);
                command.Parameters.AddWithValue("id", log.Id);
                command.Parameters.AddWithValue("date", log.Date);
                command.Parameters.AddWithValue("time", log.Time);
                command.Parameters.AddWithValue("comment", log.Comment);
                command.Parameters.AddWithValue("difficulty", log.Difficulty);
                command.Parameters.AddWithValue("total_time", log.TotalTime);
                command.Parameters.AddWithValue("rating", log.Rating);
                command.Parameters.AddWithValue("id_tour", tour.Id);
                command.Prepare();
                NpgsqlDataReader reader = command.ExecuteReader();
                
            }
                conn.Close();
        }

        public void CreateTour(Tour tour)
        {
            conn.Open();
            string query = $"INSERT INTO public.\"Tours\" (id, title, description, location_from, location_to, type. distance, estimated_time, RouteImagePath)" +
                $" values (@id, @title, @description, @location_from, @location_to, @type, @distance, @estimated_time, @RouteImagePath);";
            NpgsqlCommand command = new NpgsqlCommand(query, conn);
            command.Parameters.AddWithValue("id", tour.Id);
            command.Parameters.AddWithValue("title", tour.Title);
            command.Parameters.AddWithValue("description", tour.Desciption);
            command.Parameters.AddWithValue("location_from", tour.From);
            command.Parameters.AddWithValue("location_to", tour.To);
            command.Parameters.AddWithValue("type", tour.TransportType);
            command.Parameters.AddWithValue("distance", tour.TourDistance);
            command.Parameters.AddWithValue("estimated_time", tour.EstimatedTime);
            command.Parameters.AddWithValue("RouteImagePath", tour.RouteImagePath);
            command.Prepare();
            command.ExecuteReader();
            conn.Close();
        }

        public Tour CreateTour()
        {
            throw new NotImplementedException();
        }

        public void DeleteAllTours()
        {
            conn.Open();
            string query = $"DELETE FROM public.\"Tours\";";
            NpgsqlCommand command = new NpgsqlCommand(query, conn);
            command.ExecuteReader();
            conn.Close();
        }

        public void DeleteTour(Tour tour)
        {
            conn.Open();
            string query = $"DELETE FROM public.\"Tours\" where id=@id;";
            NpgsqlCommand command = new NpgsqlCommand(query, conn);
            command.Parameters.AddWithValue("id", tour.Id);
            command.ExecuteReader();
            conn.Close();
        }

        public void DeleteTourLog(Tour tour, TourLog log)
        {
            conn.Open();
            string query = $"DELETE FROM public.\"Logs\" where (id=@logId AND Id_tour=@tourId";
            NpgsqlCommand command = new NpgsqlCommand(query, conn);
            command.Parameters.AddWithValue("logId", log.Id);
            command.Parameters.AddWithValue("tourId", tour.Id);
            command.ExecuteReader();
            conn.Close();
        }

        public List<Tour> GetTourData()
        {
            List<Tour> tours = new List<Tour>();
            List<TourLog> logs = new List<TourLog>();

            conn.Open();
            string query = $"SELECT * FROM public.\"Tours\";";
            NpgsqlCommand command = new NpgsqlCommand(query, conn);
            NpgsqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                tours.Add(new Tour()
                {
                    Id = (Guid)reader[0],
                    Title = reader[1].ToString(),
                    Desciption = reader[2].ToString(),
                    From = reader[3].ToString(),
                    To = reader[4].ToString(),
                    TransportType = reader[5].ToString(),
                    TourDistance = reader[6].ToString(),
                    EstimatedTime = reader[7].ToString(),
                    RouteImagePath = reader[8].ToString(),
                    //Logs = GetTourLogData(tours[i])       Logs = GetTourLogData(tour.id) ???
                });
            }

            conn.Close();
            return tours;
        }

        public List<TourLog> GetTourLogData(Tour tour)
        {
            List<TourLog> tourlogs = new List<TourLog>();
            conn.Open();

            string query = $"SELECT * FROM public.\"Logs\" where Id_tour=@id_tour;";
            NpgsqlCommand command = new NpgsqlCommand(query, conn);
            command.Parameters.AddWithValue("id_tour", tour.Id);
            NpgsqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                tourlogs.Add(new TourLog()
                {
                    
                    Id = (Guid)reader[7],
                    Date = (DateTime)reader[1],
                    Time = (TimeSpan)reader[2],
                    Comment = reader[3].ToString(),
                    Difficulty = (int)reader[4],
                    TotalTime = (int)reader[5],
                    Rating = (int)reader[6]
                });
            }

            conn.Close();
            return tourlogs;
        }

        TourLog ITourManager.CreateLog(Tour tour)
        {
            throw new NotImplementedException();
        }
    }
}

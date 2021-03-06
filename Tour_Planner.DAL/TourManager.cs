using log4net.Core;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tour_Planner.Logging;
using Tour_Planner.Models;

namespace Tour_Planner.DAL
{
    public class TourManager : ITourManager
    {
        private readonly NpgsqlConnection conn = new(ConfigClass.Instance.GetConnectionString());

        public void CreateLog(Tour tour, TourLog log)
        {
            conn.Open();
            string query = $"INSERT INTO tour_log (id, date, time, rating, difficulty, total_time, comment, tour_id)" +
                $" values (@id, @date, @time, @rating, @difficulty, @total_time, @comment, @tour_id);";
            NpgsqlCommand command = new NpgsqlCommand(query, conn);
            command.Parameters.AddWithValue("id", log.Id);
            command.Parameters.AddWithValue("date", log.Date ?? string.Empty);
            command.Parameters.AddWithValue("time", log.Time );
            command.Parameters.AddWithValue("comment", log.Comment ?? string.Empty);
            command.Parameters.AddWithValue("difficulty", log.Difficulty );
            command.Parameters.AddWithValue("total_time", log.TotalTime );
            command.Parameters.AddWithValue("rating", log.Rating);
            command.Parameters.AddWithValue("tour_id", tour.Id);
            command.Prepare();
            command.ExecuteReader();


            conn.Close();
        }

        public bool CreateTour(Tour tour)
        {
            bool tourDoesntExist = true;
            try
            {
                conn.Open();
                string query = $"INSERT INTO tour (id, title, description, _from, _to, transport_type, distance, estimated_time, route_image_path)" +
                    $" values (@id, @title, @description, @_from, @_to, @transport_type, @distance, @estimated_time, @route_image_path);";
                NpgsqlCommand command = new NpgsqlCommand(query, conn);
                command.Parameters.AddWithValue("id", tour.Id);
                command.Parameters.AddWithValue("title", tour.Title ?? string.Empty);
                command.Parameters.AddWithValue("description", tour.Desciption ?? string.Empty);
                command.Parameters.AddWithValue("_from", tour.From ?? string.Empty);
                command.Parameters.AddWithValue("_to", tour.To ?? string.Empty);
                command.Parameters.AddWithValue("transport_type", tour.TransportType ?? string.Empty);
                command.Parameters.AddWithValue("distance", tour.TourDistance ?? string.Empty);
                command.Parameters.AddWithValue("estimated_time", tour.EstimatedTime ?? string.Empty);
                command.Parameters.AddWithValue("route_image_path", tour.RouteImagePath ?? string.Empty);
                command.Prepare();
                command.ExecuteReader();               
            }
            catch (PostgresException)
            {
                tourDoesntExist = false;
            }
            conn.Close();
            return tourDoesntExist;
        }

        public void DeleteAllTours()
        {
            conn.Open();
            string query = $"DELETE FROM Tour";
            NpgsqlCommand command = new NpgsqlCommand(query, conn);
            command.ExecuteReader();
            conn.Close();
        }

        public void DeleteTour(Tour tour)
        {
            conn.Open();
            string query = $"DELETE FROM Tour where id=@id;";
            NpgsqlCommand command = new NpgsqlCommand(query, conn);
            command.Parameters.AddWithValue("id", tour.Id.ToString());
            command.ExecuteReader();
            conn.Close();
        }

        public void DeleteTourLog(Tour tour, TourLog log)
        {
            conn.Open();
            string query = $"DELETE FROM tour_log where id=@logId";
            NpgsqlCommand command = new NpgsqlCommand(query, conn);
            command.Parameters.AddWithValue("logId", log.Id.ToString());
            command.ExecuteReader();
            conn.Close(); 
        }

        public List<Tour> GetTourData()
        {

            List<Tour> tours = new List<Tour>();
            List<TourLog> logs = new List<TourLog>();

            conn.Open();
            string query = $"SELECT * FROM Tour;";
            NpgsqlCommand command = new NpgsqlCommand(query, conn);
            NpgsqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                tours.Add(new Tour()
                {
                    Id = new Guid(reader[0].ToString()),
                    Title = reader[1].ToString(),
                    Desciption = reader[2].ToString(),
                    From = reader[3].ToString(),
                    To = reader[4].ToString(),
                    TransportType = reader[5].ToString(),
                    TourDistance = reader[6].ToString(),
                    EstimatedTime = reader[7].ToString(),
                    RouteImagePath = reader[8].ToString(),
                    
                });               
            }
            conn.Close();
            foreach (Tour tour in tours)
            {
               tour.Logs = GetTourLogData(tour);
                Console.WriteLine(tour.Logs.Count());
            }
            return tours;
        }

        public List<TourLog> GetTourLogData(Tour tour)
        {
            List<TourLog> tourlogs = new List<TourLog>();          
            conn.Open();
                          
            string query = $"SELECT * FROM tour_log where tour_id=@id_tour;";
            NpgsqlCommand command = new NpgsqlCommand(query, conn);
            command.Parameters.AddWithValue("id_tour", tour.Id.ToString());
            NpgsqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                tourlogs.Add(new TourLog()
                {                  
                    Id = new Guid(reader[0].ToString()),
                    Date = (string)reader[1],
                    Time = (int)reader[2],
                    Rating = (int)reader[3],
                    Difficulty = (int)reader[4],
                    TotalTime = (int)reader[5],
                    Comment = reader[6].ToString(),                  
                });
            }
             conn.Close();       
            return tourlogs;
        }

        public void UpdateTourData(Tour tour)
        {
            conn.Open();
            string query = $"UPDATE tour SET title=@title, description=@description, " +
                $"_from=@_from, _to=@_to, transport_type=@transport_type," +
                $" distance=@distance, estimated_time=@estimated_time, " +
                $"route_image_path=@route_image_path WHERE id=@id";
            NpgsqlCommand command = new NpgsqlCommand(query, conn);
            command.Parameters.AddWithValue("id", tour.Id.ToString());
            command.Parameters.AddWithValue("title", tour.Title ?? string.Empty);
            command.Parameters.AddWithValue("description", tour.Desciption ?? string.Empty);
            command.Parameters.AddWithValue("_from", tour.From ?? string.Empty);
            command.Parameters.AddWithValue("_to", tour.To ?? string.Empty);
            command.Parameters.AddWithValue("transport_type", tour.TransportType ?? string.Empty);
            command.Parameters.AddWithValue("distance", tour.TourDistance ?? string.Empty);
            command.Parameters.AddWithValue("estimated_time", tour.EstimatedTime ?? string.Empty);
            command.Parameters.AddWithValue("route_image_path", tour.RouteImagePath ?? string.Empty);
            command.Prepare();
            command.ExecuteReader();
            conn.Close();
            foreach (TourLog l in tour.Logs)
            {
                UpdateTourLogData(tour, l);
                Console.WriteLine(tour.Logs.Count());
            }
        }

        public void UpdateTourLogData(Tour tour, TourLog log)
        {
            conn.Open();
            string query = $"UPDATE tour_log SET  date=@date, time=@time, " +
                $"rating=@rating, difficulty=@difficulty, total_time=@total_time," +
                $" comment=@comment WHERE id=@id";
            Console.WriteLine(log.Id);
            Console.WriteLine(log.Comment);
            NpgsqlCommand command = new NpgsqlCommand(query, conn);
            command.Parameters.AddWithValue("id", log.Id.ToString());
            command.Parameters.AddWithValue("date", log.Date ?? string.Empty);
            command.Parameters.AddWithValue("time", log.Time);
            command.Parameters.AddWithValue("rating", log.Rating);
            command.Parameters.AddWithValue("difficulty", log.Difficulty);
            command.Parameters.AddWithValue("total_time", log.TotalTime);
            command.Parameters.AddWithValue("comment", log.Comment ?? string.Empty);
            //command.Parameters.AddWithValue("tour_id", tour.Id.ToString());
            command.Prepare();
            command.ExecuteReader();
            conn.Close();
        }
    }
}

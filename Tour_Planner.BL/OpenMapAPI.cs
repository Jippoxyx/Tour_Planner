using System;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Tour_Planner.Models;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Drawing.Imaging;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;
using Tour_Planner.BL.Service;
using Tour_Planner.Logging;
using Tour_Planner.BL.Exceptions;

namespace Tour_Planner.BL
{
    public class OpenMapAPI
    {
        ParseResponse _parseResponse = new ParseResponse();
        ConfigService _configService = new ConfigService();
        ILoggerWrapper _loggerWrapper = LoggerFactory.GetLogger();

        public async Task<Tour> GetTour(string title, string from, string to, string transportType)
        {
            try
            {
                var tour = new Tour() { Id = Guid.NewGuid() };

                var url = $"http://open.mapquestapi.com/directions/v2/route?key={_configService.GetSingletonInstance().GetKeyFromConfig()}&from={from}&to={to}";
                using var client = new HttpClient();

                var response = await client.GetStringAsync(url);

                if (response == null)
                {
                    _loggerWrapper.Error("Server returned nothing");
                }
                else
                {
                    tour = _parseResponse.ParseTourFromServer(response);
                    tour.RouteImagePath = await GetTourImage(tour.Session, tour.BoundingBox);
                    if (String.IsNullOrEmpty(title))
                        title = "New_S.Tour";

                    tour.Title = title;
                    tour.TransportType = transportType;
                    tour.From = from;
                    tour.To = to;

                    _loggerWrapper.Debug("Server returned a Tour & Image");
                }
                return tour;
            }
            catch (Exception)
            {
                throw new OpenMapAPI_Exception("Could not find a Tour with the requested Data");
            }
        }

        public async Task<string> GetTourImage(string session, string boundingBox)
        {
            var url = $"http://mapquestapi.com/staticmap/v5/map?key={_configService.GetSingletonInstance().GetKeyFromConfig()}&size=800,300&session={session}&boundingBox={boundingBox}";
            using var client = new HttpClient();

            var response = await client.GetByteArrayAsync(url);

            byte[] bitmap = response;

            string path = _configService.GetSingletonInstance().GetImageSourceFromConfig();
            path += $"{Guid.NewGuid()}.jpeg";
            using (System.Drawing.Image image = System.Drawing.Image.FromStream(new MemoryStream(bitmap)))
            {
                image.Save(path, ImageFormat.Jpeg);  // Or Png
            }
            return path;
        }
    }
}

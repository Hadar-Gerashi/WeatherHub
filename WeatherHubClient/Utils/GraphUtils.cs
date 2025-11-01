using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherHubClient.Utils
{
    public static class GraphUtils
    {



        public static string CalculatePolyline(IEnumerable<double> values, double canvasWidth, double canvasHeight)
        {
            if (values == null || !values.Any()) return "";

            double max = values.Max();
            double min = values.Min();
            int count = values.Count();
            double stepX = canvasWidth / (count - 1);

            var points = values.Select((v, i) =>
            {
                double x = i * stepX;
                double y = canvasHeight - ((v - min) / (max - min) * canvasHeight);
                return $"{x},{y}";
            });

            return string.Join(" ", points);
        }

        public static string CalculatePolygon(IEnumerable<double> values, double canvasWidth, double canvasHeight)
        {
            var polyline = CalculatePolyline(values, canvasWidth, canvasHeight);
            if (string.IsNullOrEmpty(polyline)) return "";

            var linePoints = polyline.Split(' ');
            var polygonPoints = new List<string>(linePoints)
            {
                $"{linePoints.Last().Split(',')[0]},{canvasHeight}", 
                $"0,{canvasHeight}"
            };

            return string.Join(" ", polygonPoints);
        }




    }





}

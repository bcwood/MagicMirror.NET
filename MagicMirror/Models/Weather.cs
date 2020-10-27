using System.Collections.Generic;
using Newtonsoft.Json;

namespace MagicMirror.Models
{
    public class Weather
    {
        [JsonProperty("current")]
        public CurrentWeather Current { get; set; }

        [JsonProperty("daily")]
        public IList<ForecastWeather> Forecast { get; set; }

        [JsonProperty("alerts")]
        public IList<WeatherAlert> Alerts { get; set; }
    }

    public class WeatherBase
    {
        [JsonProperty("dt")]
        public int Timestamp { get; set; }

        [JsonProperty("sunrise")]
        public int Sunrise { get; set; }

        [JsonProperty("sunset")]
        public int Sunset { get; set; }

        [JsonProperty("pressure")]
        public int Pressure { get; set; }

        [JsonProperty("humidity")]
        public int Humidity { get; set; }

        [JsonProperty("dew_point")]
        public double DewPoint { get; set; }

        [JsonProperty("uvi")]
        public double Uvi { get; set; }

        [JsonProperty("clouds")]
        public int Clouds { get; set; }

        [JsonProperty("visibility")]
        public int Visibility { get; set; }

        [JsonProperty("wind_speed")]
        public double WindSpeed { get; set; }

        [JsonProperty("wind_degrees")]
        public int WindDegrees { get; set; }

        [JsonProperty("weather")]
        public IList<WeatherCondition> WeatherConditions { get; set; }
    }

    public class CurrentWeather : WeatherBase
    {
        [JsonProperty("temp")]
        public double Temp { get; set; }

        [JsonProperty("feels_like")]
        public double FeelsLike { get; set; }        
    }

    public class ForecastWeather : WeatherBase
    {
        [JsonProperty("temp")]
        public TempRange Temp { get; set; }

        [JsonProperty("feels_like")]
        public TempRange FeelsLike { get; set; }
    }

    //More info: https://openweathermap.org/weather-conditions
    public class WeatherCondition
    {
        [JsonProperty("main")]
        public string Label { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        public string IconUrl => $"/img/weather/{Icon}.png";
        public string IconLargeUrl => $"/img/weather/{Icon}.png";
    }

    public class TempRange
    {
        [JsonProperty("min")]
        public double? Min { get; set; }

        [JsonProperty("max")]
        public double? Max { get; set; }

        [JsonProperty("morn")]
        public double Morning { get; set; }

        [JsonProperty("day")]
        public double Day { get; set; }

        [JsonProperty("eve")]
        public double Evening { get; set; }

        [JsonProperty("night")]
        public double Night { get; set; }        
    }

    public class WeatherAlert
    {
        [JsonProperty("sender_name")]
        public string Sender { get; set; }

        [JsonProperty("event")]
        public string Event { get; set; }

        [JsonProperty("start")]
        public int Start { get; set; }

        [JsonProperty("end")]
        public int End { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
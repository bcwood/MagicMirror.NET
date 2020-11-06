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

    // More info: 
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

        // We need to map the weather condition codes used by OpenWeatherMap, to 
        // the CSS class names used by our weather icons.
        // More info:
        //   https://openweathermap.org/weather-conditions
        //   https://fontawesome.com/icons?d=gallery&c=weather&m=free
        public string IconCssClass => ConditionIcons[Icon];

        private Dictionary<string, string> ConditionIcons = new Dictionary<string, string>
            {
                { "01d", "fas fa-sun" },
                { "01n", "far fa-moon" },
                { "02d", "fas fa-cloud-sun" },
                { "02n", "fas fa-cloud-moon" },
                { "03d", "fas fa-cloud" },
                { "03n", "fas fa-cloud" },
                { "04d", "fas fa-cloud-sun" },
                { "04n", "fas fa-cloud-moon" },
                { "09d", "fas fa-cloud-showers-heavy" },
                { "09n", "fas fa-cloud-showers-heavy" },
                { "10d", "fas fa-cloud-rain" },
                { "10n", "fas fa-cloud-rain" },
                { "11d", "fas fa-bolt" },
                { "11n", "fas fa-bolt" },
                { "13d", "far fa-snowflake" },
                { "13n", "far fa-snowflake" },
                { "50d", "fas fa-smog" },
                { "50n", "fas fa-smog" },
            };

        //   https://erikflowers.github.io/weather-icons/
        //private Dictionary<string, string> ConditionIcons = new Dictionary<string, string>
        //    {
        //        { "01d", "wi wi-day-sunny" },
        //        { "01n", "wi wi-night-clear" },
        //        { "02d", "wi wi-day-sunny-overcast" },
        //        { "02n", "wi wi-night-alt-partly-cloudy" },
        //        { "03d", "wi wi-cloud" },
        //        { "03n", "wi wi-cloud" },
        //        { "04d", "wi wi-day-cloudy" },
        //        { "04n", "wi wi-night-alt-cloudy" },
        //        { "09d", "wi wi-raindrops" },
        //        { "09n", "wi wi-raindrops" },
        //        { "10d", "wi wi-raindrops" },
        //        { "10n", "wi wi-raindrops" },
        //        { "11d", "wi wi-thunderstorm" },
        //        { "11n", "wi wi-thunderstorm" },
        //        { "13d", "wi wi-snowflake-cold" },
        //        { "13n", "wi wi-snowflake-cold" },
        //        { "50d", "wi wi-fog" },
        //        { "50n", "wi wi-fog" },
        //    };
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
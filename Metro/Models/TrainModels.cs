using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Metro.Models
{
    public enum TrainStationColor { None = 0, Red = 1, Green = 2 };
    public enum StationType { None = 0, Initial = 1, Final = 2 };
    public class Train
    {
        public TrainStationColor Color { get; set; }
    }
    public class Station
    {
        private StationType _type;
        public StationType Type { get { return _type; } }
        public Station(StationType type)
        {
            this._type = type;
        }
        public string Name { get; set; }
        public TrainStationColor Color { get; set; }
        public List<string> NextStations { get; set; }
    }

    public class NetWork
    {
        private Station _initialStation;
        private Station _finalStation;
        public Train Train { get; set; }

        public Station InitialStation
        {
            get
            {
                if (this._initialStation == null)
                {
                    this._initialStation = this.Stations.FirstOrDefault(s => s.Type == StationType.Initial);
                }
                return this._initialStation;
            }
        }
        public Station FinalStation
        {
            get
            {
                if (this._finalStation == null)
                {
                    this._finalStation = this.Stations.FirstOrDefault(s => s.Type == StationType.Final);
                }
                return this._finalStation;
            }
        }
        public List<Station> Stations { get; set; }
    }

    public class MetroManager:IMetroManager
    {
        private NetWork _network;

        public MetroManager(string jsonDescription)
        {
            this._network = Newtonsoft.Json.JsonConvert.DeserializeObject<NetWork>(jsonDescription);
        }
        public bool IsValidNetwork()
        {
            var result = false;
            var initialStations = this._network.Stations.Where(s => s.Type == StationType.Initial);
            var finalStations = this._network.Stations.Where(s => s.Type == StationType.Final);
            result = initialStations.Count() == 1;
            result = result && finalStations.Count() == 1;
            result = result && this._network.Stations.Where(s => s.NextStations.Any())
                .Count() == this._network.Stations.Count() - 1;
            return result;
        }

        public IEnumerable<IEnumerable<Station>> GetShortestRoutes()
        {
            var allRoutes = GetShortestRoutes(this._network.InitialStation);
            var minRouteSize = allRoutes.Min(r => r.Count());
            var shortestRoutes = allRoutes.Where(r=>r.Count() == minRouteSize);
            return shortestRoutes;
        }
        private List<List<Station>> GetShortestRoutes(Station station)
        {
            var result = new List<List<Station>>();
            var partial = new List<Station>();

            if (station.Color == TrainStationColor.None
            || this._network.Train.Color == TrainStationColor.None
            || this._network.Train.Color == station.Color)
            {
                partial.Add(station);
            }

            if (station.Type != StationType.Final)
            {
                foreach (string stationName in station.NextStations)
                {
                    var nextStation = this._network.Stations.FirstOrDefault(s => s.Name == stationName);
                    var routes = GetShortestRoutes(nextStation);
                    foreach (List<Station> route in routes)
                    {
                        route.InsertRange(0, partial);
                        result.Add(route);
                    }
                }

            }
            else
            {
                result.Add(partial);
            }
            return result;
        }

    }

    public interface IMetroManager
    {
        IEnumerable<IEnumerable<Station>> GetShortestRoutes();
    }
}

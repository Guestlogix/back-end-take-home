using GuestLogixRouteAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace GuestLogixRouteAPI.BusinessLayer
{
    public class RouteDecision : IRouteDecision
    {
        public Dictionary<int,List<String>> GetAirportRoutes(string originCity, string destinationCity, DbSet<AirportRoute> listOfAirportRoutes)
        {
            try
            {
                List<string> route = new List<string>();
                List<String> tempOriginCities = new List<string>();
                List<String> tempDestCities = new List<string>();
                Dictionary<int, List<String>> allRoutes = new Dictionary<int, List<string>>();
                route.Add(originCity);

                var listOfRoutes = listOfAirportRoutes.Where(c => c.originAirport == originCity).ToList<AirportRoute>();

                for (int originCityIndex = 0; originCityIndex < listOfRoutes.Count; originCityIndex++)
                {
                    route = DecideRoutes(listOfRoutes[originCityIndex].destAirport, destinationCity, listOfAirportRoutes, route);

                    if (route != null)
                    {
                        List<string> preservedRoute = new List<string>();
                        preservedRoute.AddRange(route);
                        route.RemoveRange(1, route.Count - 1);
                        allRoutes.Add(originCityIndex, preservedRoute);
                    }
                }
                return allRoutes;
            }
            catch(Exception ex)
            {
                var responseMessage = new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("Error occured while finding routes: " + ex.Message),
                    ReasonPhrase = ex.Message
                };
                throw new HttpResponseException(responseMessage);
            }
        }
        public List<String> DecideRoutes(string originCity, string destCity, DbSet<AirportRoute> airportRoutes, List<String> route)
        {
            try
            {
                List<String> tempOriginCities = new List<string>();
                List<String> tempDestCities = new List<string>();

                List<AirportRoute> listOfRoutes = airportRoutes.Where(c => c.originAirport == originCity).ToList<AirportRoute>();
                List<AirportRoute> modifiedListOfRoutes = new List<AirportRoute>();
                foreach (var airportObj in listOfRoutes)
                {
                    if (!route.Exists(x => x == airportObj.destAirport))
                        modifiedListOfRoutes.Add(airportObj);
                }

                if (modifiedListOfRoutes.Exists(c => c.originAirport == originCity))
                    route.Add(originCity);

                for (int i = 0; i < modifiedListOfRoutes.Count; i++)
                {
                    if (modifiedListOfRoutes[i].destAirport == destCity)
                    {
                        route.Add(destCity);
                    }
                    else
                    {
                        DecideRoutes(modifiedListOfRoutes[i].destAirport, destCity, airportRoutes, route);
                    }
                }
                if (!route.Exists(c => c == destCity))
                    route.RemoveRange(1, route.Count - 1);
                if (originCity == destCity)
                    route.Add(destCity);
                return route;
            }
            catch (Exception ex)
            {
                var responseMessage = new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("Error occured while deciding routes: " + ex.Message),
                    ReasonPhrase = ex.Message
                };
                throw new HttpResponseException(responseMessage);
            }
        }
    }
}
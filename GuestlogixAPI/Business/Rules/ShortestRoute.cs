using System;
using System.Collections.Generic;
using System.Linq;
using Business.DTO;
using Business.Exceptions;

namespace Business.Rules
{
    public static class ShortestRoute
    {
        private static Dictionary<String, List<String>> dictRoute;

        public static string GetRoute(string origin, string destin, List<RouteDTO> allRoutes)
        {
            if (allRoutes.All(a => a.Origin != origin) && allRoutes.All(a => a.Destin != destin))
            {
                throw new ValidationException("No Route");
            }

            if (dictRoute == null)
            {
                dictRoute = new Dictionary<string, List<string>>();
                foreach (var route in allRoutes)
                {
                    if (dictRoute.ContainsKey(route.Origin))
                    {
                        dictRoute[route.Origin].Add(route.Destin);
                    }
                    else
                    {
                        dictRoute[route.Origin] = new List<string> {route.Destin};
                    }
                }
            }

            var resultRoute = BFSRoute(origin, destin);
            if(String.IsNullOrEmpty(resultRoute))
                throw new ValidationException("No Route");

            return resultRoute;
        }

        private static string BFSRoute(string origin, string destin)
        {
            var listNodesToVisit = new List<NodeRoute>();

            var listNodesVisited = new List<NodeRoute>();
            listNodesToVisit.Add(new NodeRoute{Code = origin});
            var result = string.Empty;
            while (listNodesToVisit.Any())
            {
                var currentNode = listNodesToVisit[0];
                listNodesToVisit.RemoveAt(0);
                listNodesVisited.Add(currentNode);
                if (currentNode.Code == destin)
                {
                    while (currentNode.Code != origin) 
                    {
                        result = string.IsNullOrEmpty(result) ? currentNode.Code: currentNode.Code+ " -> " + result;
                        currentNode = listNodesVisited.First(a => a.Code == currentNode.ParentCode);
                    }

                    return origin + " -> " + result;
                }

                var childs = dictRoute[currentNode.Code];
                foreach (var child in childs)
                {
                    if (listNodesVisited.All(a => a.Code != child))
                    {
                        var childNode = new NodeRoute { Code = child, ParentCode = currentNode.Code };
                        listNodesToVisit.Add(childNode);
                    }
                }
                
            }

            return string.Empty;
        }
    }

    public class NodeRoute
    {
        public string Code { get; set; }    
        public string ParentCode { get; set; }
    }
}

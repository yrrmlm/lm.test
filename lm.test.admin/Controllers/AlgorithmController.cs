using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lm.algorithm.Dijkstra;
using lm.test.admin.Models.Algorithm;
using Microsoft.AspNetCore.Mvc;

namespace lm.test.admin.Controllers
{
    public class AlgorithmController : Controller
    {
        #region Dijkstra
        public const int Max = 9999;

        public static int[,] Graph = new int[,]
                {
                    { Max,  Max,  10,   Max,  30,   100,Max},
                    { Max,  Max,  5,    Max,  Max,  Max,Max},
                    { 30,  Max,  Max,  50,   Max,  Max,Max},
                    { Max,  80,  Max,  Max,  Max,   10,Max},
                    { Max,  Max,  Max,  20,   Max,  60,50},
                    { Max,  30,  Max,  Max,  Max,  Max,80},
                    { 10,  Max,  40,  Max,  Max,  Max,Max}
                };

        public PartialViewResult Dijkstra()
        {
            var data = new List<G_Series_Data>();
            var links = new List<G_Series_Link>();
            for (var i = 0; i < Graph.GetLength(0); i++)
            {
                data.Add(GetSeriesData(new G_Series_Data
                {
                    name = $"v{i}",
                    x = 300,
                    y = 200
                }, (360 / Graph.GetLength(0)) * i, 100));
                for (var j = 0; j < Graph.GetLength(1); j++)
                {
                    if (Max != Graph[i, j])
                    {
                        links.Add(new G_Series_Link
                        {
                            source = $"v{i}",
                            target = $"v{j}",
                            value = Graph[i, j].ToString(),
                            label = new G_Series_Link_Label
                            {
                                formatter = "{c}",
                                color = "#800"
                            }
                        });
                    }
                }
            }
            var graphData = new GraphData
            {
                series = new List<G_Serie>
                {
                    new G_Serie
                    {
                        data =data,
                        links = links
                    }
                }
            };
            return PartialView(graphData);
        }

        /// <summary>
        /// 获取初始矩阵
        /// </summary>
        /// <returns></returns>
        [ActionName("getInitMatrix")]
        public PartialViewResult GetInitMatrix()
        {
            return PartialView(Graph);
        }

        /// <summary>
        /// 获取路径规划
        /// </summary>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        [ActionName("getRoute")]
        public PartialViewResult GetRoute(int startIndex)
        {
            var routes = new List<Route>();
            try
            {
                var path = new int[Graph.GetLength(1)];
                var cost = new int[Graph.GetLength(1)];
                DijkstraSolution.FindShortestPath(Graph, startIndex, path, cost, Max);

                for (var i = 0; i < path.Length; i++)
                {
                    routes.Add(new Route
                    {
                        Index = i,
                        PointName = "v" + i,
                        Cost = cost[i],
                        Path = GetPath(path, i, startIndex)
                    });
                }
            }
            catch (Exception ex)
            {

            }
            return PartialView(routes);
        }

        private string GetPath(int[] path, int i, int startIndex)
        {
            if (path[i] == -1)
            {
                return "不可达";
            }
            else if (path[i] == startIndex)
            {
                return $"v{startIndex}-->v{i}";
            }
            else
            {
                return path[path[i]] == -1 ? $"v{startIndex}-->v{i}" : GetPath(path, path[i], startIndex) + $"-->v{i}";
            }
        }

        private G_Series_Data GetSeriesData(G_Series_Data center, double angle, double radius)
        {
            G_Series_Data p = new G_Series_Data();
            double angleHude = angle * Math.PI / 180;/*角度变成弧度*/
            p.x = (int)(radius * Math.Cos(angleHude)) + center.x;
            p.y = (int)(radius * Math.Sin(angleHude)) + center.y;
            p.name = center.name;
            return p;
        }

        #endregion
    }
}
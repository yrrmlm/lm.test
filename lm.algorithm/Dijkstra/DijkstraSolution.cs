using System;
using System.Collections.Generic;
using System.Text;

namespace lm.algorithm.Dijkstra
{
    public class DijkstraSolution
    {
        /*
            * 求解各节点最短路径，获取path，和cost数组，
            * path[i]表示vi节点的前继节点索引，一直追溯到起点。
            * cost[i]表示vi节点的花费
            */
        public static void FindShortestPath(int[,] graph, int startIndex, int[] path, int[] cost, int max)
        {
            int nodeCount = graph.GetLength(0);
            bool[] v = new bool[nodeCount];
            //初始化 path，cost，V
            for (int i = 0; i < nodeCount; i++)
            {
                if (i == startIndex)//如果是出发点
                {
                    v[i] = true;//
                }
                else
                {
                    cost[i] = graph[startIndex, i];
                    if (cost[i] < max) path[i] = startIndex;
                    else path[i] = -1;
                    v[i] = false;
                }
            }
            //
            for (int i = 1; i < nodeCount; i++)//求解nodeCount-1个
            {
                int minCost = max;
                int curNode = -1;
                for (int w = 0; w < nodeCount; w++)
                {
                    if (!v[w])//未在V集合中
                    {
                        if (cost[w] < minCost)
                        {
                            minCost = cost[w];
                            curNode = w;
                        }
                    }
                }//for  获取最小权值的节点
                if (curNode == -1) break;//剩下都是不可通行的节点，跳出循环
                v[curNode] = true;
                for (int w = 0; w < nodeCount; w++)
                {
                    if (!v[w] && (graph[curNode, w] + cost[curNode] < cost[w]))
                    {
                        cost[w] = graph[curNode, w] + cost[curNode];//更新权值
                        path[w] = curNode;//更新路径
                    }
                }//for 更新其他节点的权值（距离）和路径
            }//
        }
    }
}

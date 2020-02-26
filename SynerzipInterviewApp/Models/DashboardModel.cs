using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SynerzipInterviewApp.Models
{
    public class DashboardModel
    {
        public ContentBlock ContentBlock { get; set; }
        public PieChartData PieChartData { get; set; }
        public BarChartData BarChartData { get; set; }
        public GridData GridData { get;set; }
    }
    public class GridData
    {
        public object data { get; set; }
    }
    public class BarChartData
    {
        public List<string> ChartLabels { get; set; }
        public List<ChartData> ChartData { get; set; }
    }

    public class ChartData
    {
        public string label { get; set; }
        public List<string> data { get; set; }
    }

    public class PieChartData
    {
        public List<string> label { get; set; }
        public List<string> data { get; set; }
    }

}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SynerzipInterviewApp.Models;

namespace SynerzipInterviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly AppConfig config;

        public DashboardController(IOptions<AppConfig> config)
        {
            this.config = config.Value;
        }
        [HttpPost("GetPreview")]
        public IActionResult GetPreview([FromBody] ContentBlock contentBlock)
        {
            string connectionString = config.SynerzipInterviewDB;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
               
                string sql =  contentBlock.Query;
                sql = sql.Replace("Select", "Select Top 20",StringComparison.CurrentCultureIgnoreCase);
                List<string> Labels = new List<string>();
                List<string> Datas = new List<string>();
                List<string> Datas1 = new List<string>();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var dr = command.ExecuteReader();
                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();
                    ds.Tables.Add(dt);
                    ds.Load(dr, LoadOption.Upsert, dt);
                    connection.Close();
                    DashboardModel dashboardModel = new DashboardModel();
                    dashboardModel.ContentBlock = contentBlock;
                    switch (contentBlock.VisualizationType)
                    {
                        case "BarChart":
                           foreach(DataRow row in dt.Rows)
                            {
                                var label = row[0];
                                var data = row[1];
                                var data1 = row[2];
                                Datas.Add(data.ToString());
                                Datas1.Add(data1.ToString());
                                Labels.Add(label.ToString());
                                
                            }
                            dashboardModel.BarChartData = new BarChartData { ChartLabels = Labels, ChartData = new List<ChartData> { new ChartData { data = Datas, label = dt.Columns[1].ColumnName }, new ChartData { data=Datas1,label=dt.Columns[2].ColumnName} } };
                            break;
                        case "PieChart":
                            foreach (DataRow row in dt.Rows)
                            {
                                var label = row[0];
                                var data = row[1];
                                Datas.Add(data.ToString());
                                Labels.Add(label.ToString());
                            }
                            dashboardModel.PieChartData = new PieChartData { label = Labels, data =  Datas};
                            break;
                        case "SingleGrid":
                        case "MultipleGrid":
                            dashboardModel.GridData = new GridData { data=dt};
                            break;
                    }
                  

                    return Ok(dashboardModel);
                }
                
            }
        }
    }
}
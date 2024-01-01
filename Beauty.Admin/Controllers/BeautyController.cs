﻿using Beauty.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace Beauty.Admin.Controllers
{
    public class BeautyController : Controller
    {
        #region------Variable-------

        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adt;
        DataSet ds;
        DataTable dt;

        #endregion

        private readonly IConfiguration _configuration;

        public BeautyController(IConfiguration configuration)
        {
            _configuration = configuration;
            con = new SqlConnection();
            cmd = new SqlCommand();
            adt = new SqlDataAdapter();
            ds = new DataSet();
            dt = new DataTable();
        }
        public async Task<IActionResult> Aboutus(int id = 0)
        {
            List<Aboutus> list = new List<Aboutus>();
            con = new SqlConnection(_configuration.GetConnectionString("ConnectionString"));
            cmd = new SqlCommand("Beauty_SP", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@QueryType", id == 0 ? "GET_ALL_ABOUTUS" : "GET_ABOUTUS");
            adt = new SqlDataAdapter(cmd);
            adt.Fill(ds);

            dt = ds.Tables[0];
            if (id == 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Aboutus about = new Aboutus();
                    about.Title = dt.Rows[i]["Title"].ToString();
                    about.SubTitle = dt.Rows[i]["SubTitle"].ToString();
                    about.Description = dt.Rows[i]["Description"].ToString();
                    about.Image = dt.Rows[i]["Image"].ToString();
                    list.Add(about);
                }
            }
           
            await con.OpenAsync();
            return View(list);
        }

    }
}

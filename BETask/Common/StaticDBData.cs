using BETask.DAL.DAL;
using BETask.Model;
using EDMX = BETask.DAL.EDMX;
using System;
using System.Collections.Generic;
using System.Data;
using RPT = BETask.Report.ReportForm;
using BETask.Common;
using System.Diagnostics;
using System.Linq;
using AutoMapper;
using AutoMapper;

namespace BETask.Common
{
    public static class StaticDBData
    {
        public static List<EDMX.building> listBuilding=null;
        public static List<EDMX.employee> listEmployee=null;

        public static void LoadBuildings()
        {
            BuildingDAL building = new BuildingDAL();
            listBuilding = building.GetAllBuildings();
        }

        public static void LoadEmployee()
        {
            EmployeeDAL employeeDAL = new EmployeeDAL();
            listEmployee = employeeDAL.GetAllEmployee();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleApp.Models.StoredProcedureModels;
using SampleApp.Models.ResultsModels;
using SampleApp.Utilities;
using System.Reflection;

namespace SampleApp.Controllers
{
    public class EmployeeController : Controller
    {
        private SpRepository _spRepository;
        public EmployeeController(AppDbContext context)
        {
            _spRepository = new SpRepository(new StoredProcedureExecutor(context));
        }

        [HttpGet]
        public void GetData(GetEmployees inputObj)
        {
            inputObj.Name = "din11";
            inputObj.Designation = "SSE1";
            var res = _spRepository.CreateStoreProcedureModel<List<Employee>>(inputObj);
        }

        [HttpPost]
        public void PostData(InsertEmployee inputObj)
        {
            inputObj.Name = "din11";
            inputObj.Designation = "SSE1";
            inputObj.Mobile = 5643;
            inputObj.Address = "nzb";
            var res = _spRepository.CreateStoreProcedureModel<int>(inputObj);
        }

        [HttpPut]
        public void UpdateData(Employee inputObj)
        {
            inputObj.Id = 1;
            inputObj.Name = "din11";
            inputObj.Designation = "SSE1";
            inputObj.Mobile = 5643;
            inputObj.Address = "nzb";
            var res = _spRepository.CreateStoreProcedureModel<int>(inputObj);
        }

        [HttpDelete]
        public void DeleteData(Employee inputObj)
        {
            inputObj.Name = "dinesh";
            var res = _spRepository.CreateStoreProcedureModel<int>(inputObj);
        }
    }
}
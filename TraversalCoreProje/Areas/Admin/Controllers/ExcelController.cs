using BusinessLayer.Abstract;
using ClosedXML.Excel;
using DataAccessLayer.Concrete;
using DTOLayer.DTOs.DestinationDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using TraversalCoreProje.Areas.Admin.Models;

namespace TraversalCoreProje.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AllowAnonymous]
    public class ExcelController : Controller
    {
        private readonly IExcelService _excelservice;
        public ExcelController(IExcelService excelService)
        {
            _excelservice = excelService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public List<DestinationDTO> DestinationList()
        {
            List<DestinationDTO> destinationModels = new List<DestinationDTO>();
            using(var c=new Context())
            {
                destinationModels = c.Destinations.Select(x => new DestinationDTO
                {
                    DestinationCity=x.DestinationCity,
                    DestinationDayNight=x.DestinationDayNight,
                    DestinationCapacity=x.DestinationCapacity,
                    DestinationPrice=x.DestinationPrice,
                }).ToList();
            }
            return destinationModels;
        }
        public IActionResult StaticExcelReport()
        {
            return File(_excelservice.ExcelList(DestinationList()), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "YeniExcel.xlsx");
        }
        public IActionResult DestinationExcelReport()
        {
            using(var workbook =new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Tur Listesi");
                worksheet.Cell(1, 1).Value = "Şehir";
                worksheet.Cell(1, 2).Value = "Süre";
                worksheet.Cell(1, 3).Value = "Kapasite";
                worksheet.Cell(1, 4).Value = "Fiyat";
                int rowcount = 2;
                foreach(var item in DestinationList())
                {
                    worksheet.Cell(rowcount, 1).Value = item.DestinationCity;
                    worksheet.Cell(rowcount, 2).Value=item.DestinationDayNight;
                    worksheet.Cell(rowcount, 3).Value=item.DestinationCapacity;
                    worksheet.Cell(rowcount, 4).Value=item.DestinationPrice;
                    rowcount++;
                }
                using(var stream=new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content=stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "YeniListe.xlsx");
                }
            }
        }
    }
}

using AutoMapper;
using AngularCRUDvs.Entidades;
using AngularCRUDvs.Models;
using AngularCRUDvs.Services.Contratos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OfficeOpenXml;
using System.Data;
using System.Globalization;
using Amazon.S3;
using Amazon.S3.Model;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Layout;
using Microsoft.IdentityModel.Tokens;
using AngularCRUDvs;

namespace AngularCRUDvs.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReciboController : ControllerBase
    {

        private readonly ILogger<ReciboController> _logger;
        private readonly IClaimsServices _claimsServices;
        private readonly IConceptoServices _conceptoServices;
        private readonly IReciboServices _reciboServices;
        private readonly IReciboPagoServices _reciboPagoServices;
        private readonly IReciboConceptoServices _reciboConceptoServices;
        private readonly IUnidadServices _unidadServices;
        private readonly IMapper _mapper;

        private static IAmazonS3? _s3Client;

        string bucketName = "Habitat";
        string accessKey = "33932DE2D75DF0DCDA83";
        string secretKey = "x4I48Rxz02XimdL3zEIPKd94EXh0hjoeFecEPTRT";
        string hostUrlFileBase = "https://ipfs.filebase.io/ipfs";
        string serviceUrlFileBase = "https://s3.filebase.com";

        //private const string OBJECT_NAME1 = "object-name";
        //private static string LOCAL_PATH = "/path/to/object";

        public ReciboController(ILogger<ReciboController> logger,
                                IClaimsServices claimsServices,
                                IConceptoServices conceptoServices,
                                IReciboServices reciboServices,
                                IReciboPagoServices reciboPagoServices,
                                IReciboConceptoServices reciboConceptoServices,
                                IUnidadServices unidadServices,
                                IMapper mapper)
        {
            _logger = logger;
            _claimsServices = claimsServices;
            _conceptoServices = conceptoServices;
            _reciboServices = reciboServices;
            _reciboPagoServices = reciboPagoServices;
            _reciboConceptoServices = reciboConceptoServices;
            _unidadServices = unidadServices;
            _mapper = mapper;

        }

        [HttpGet]
        public async Task<IEnumerable<DataTableListModel>> GetAsync()
        {
            var modeloList = new List<DataTableListModel>();
            var modelo = new DataTableListModel();

            UsersModel userLogin = new UsersModel();
            userLogin.UserName = "webmaster";
            string? userName = HttpContext.User.Identities.FirstOrDefault()?.Name;
            if (userName != null)
            {
                //var claims = HttpContext.User.Identities.FirstOrDefault()?.Claims.ToList();
                userLogin = _claimsServices.getUserClaimsHttpContext(HttpContext);

                modelo.recibo = new Recibo();
                modelo.recibo.Mes = DateTime.Today.Month;
                modelo.recibo.Anio = DateTime.Today.Year;
            }

            string year = "0";
            string month = "0";
            if (string.IsNullOrEmpty(year) && string.IsNullOrEmpty(month))
            {
                year = "0"; // DateTime.Today.Year.ToString();
                month = "0"; // (DateTime.Today.Month).ToString();
            }

            var recibosFilterAnio = new Recibo() { Anio = Convert.ToInt32(year) };
            var recibosAniosList = await _reciboServices.GetAll(recibosFilterAnio);

            Recibo recibo = new Recibo();
            if (month != "0")
                recibo = recibosAniosList.Where(x => x.Mes == Convert.ToInt32(month)).SingleOrDefault();
            else
                recibo = recibosAniosList.OrderByDescending(x => x.Mes).FirstOrDefault();


            //var filter = _mapper.Map<List<DataTableModelModel>>(entidad);
            var dt = new DataTable();
            if (recibo != null)
            {
                dt = await PivotAsync(recibo);

                
                Dictionary<string, string> totales = CalcularTotales(dt);

                List<int> mesesList = recibosAniosList.Select(c => Convert.ToInt32(c.Mes)).Distinct().Order().ToList();
                List<string> cabecerasOriginal = dt.Columns.Cast<DataColumn>().Select(column => column.ColumnName).ToList();

                modelo.recibo = recibo;
                modelo.recibos = recibosAniosList.ToList();
                modelo.Totales = totales;
                modelo.mesesList = mesesList;
                modelo.cabeceras = cabecerasOriginal;
                modelo.user = userLogin;
                //modelo.DataTable = dt;

                string[,] matriz = CrearMatrizDesdeDataTable(dt);
                string jsonData = JsonConvert.SerializeObject(matriz, Formatting.Indented);
                modelo.MatrizString = jsonData;

            }
            

            modeloList.Add(modelo);
            return modeloList.ToArray<DataTableListModel>();
        }



        static string[,] CrearMatrizDesdeDataTable(DataTable dataTable)
        {
            int filas = dataTable.Rows.Count;
            int columnas = dataTable.Columns.Count;

            // Crear la matriz con el tamaño adecuado
            string[,] matriz = new string[filas, columnas];

            // Recorrer filas y columnas del DataTable para llenar la matriz
            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    // Obtener el valor de la celda en la fila 'i' y columna 'j'
                    matriz[i, j] = dataTable.Rows[i][j].ToString();
                }
            }

            return matriz;
        }


        public async Task<IActionResult> BlockAsync(string year, string month)
        {
            var modelo = new DataTableModel();

            if (string.IsNullOrEmpty(year) && string.IsNullOrEmpty(month))
            {
                year = "0"; // DateTime.Today.Year.ToString();
                month = "0"; // (DateTime.Today.Month).ToString();
            }

            string? userName = HttpContext.User.Identities.FirstOrDefault()?.Name;
            if (userName != null)
            {
                //var claims = HttpContext.User.Identities.FirstOrDefault()?.Claims.ToList();
                UsersModel userLogin = _claimsServices.getUserClaimsHttpContext(HttpContext);

                var recibosFilterAnio = new Recibo() { Anio = Convert.ToInt32(year) };
                var recibosAniosList = await _reciboServices.GetAll(recibosFilterAnio);

                Recibo recibo = new Recibo();
                if (month != "0")
                    recibo = recibosAniosList.Where(x => x.Mes == Convert.ToInt32(month)).SingleOrDefault();
                else
                    recibo = recibosAniosList.OrderByDescending(x => x.Mes).FirstOrDefault();


                //var filter = _mapper.Map<List<DataTableModelModel>>(entidad);
                if (recibo != null)
                {
                    var dt = await PivotAsync(recibo);
                    Dictionary<string, string> totales = CalcularTotales(dt);

                    List<int> mesesList = recibosAniosList.Select(c => Convert.ToInt32(c.Mes)).Distinct().Order().ToList();
                    List<string> cabecerasOriginal = dt.Columns.Cast<DataColumn>().Select(column => column.ColumnName).ToList();

                    modelo.DataTable = dt;
                    modelo.recibo = recibo;
                    modelo.Totales = totales;
                    modelo.mesesList = mesesList;
                    modelo.cabeceras = cabecerasOriginal;
                    modelo.user = userLogin;

                }

            }
            else
            {

                _ = HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToAction("Login", "Account");
            }

            return (IActionResult)modelo;
        }

        public async Task<DataTable> PivotAsync(Recibo recibo)
        {

            ReciboConceptoReporte reciboConceptoFilter = new ReciboConceptoReporte() { ReciboId = recibo.ReciboId };
            var reciboConceptos = await _reciboConceptoServices.GetAll(reciboConceptoFilter);

            DataTable pivotedTable = new DataTable();
            pivotedTable = new DataTable();
            pivotedTable.Columns.Add("UnidadId", typeof(string));
            pivotedTable.Columns.Add("ReciboId", typeof(string));
            pivotedTable.Columns.Add("DPTO", typeof(string));
            pivotedTable.Columns.Add("FechaPago", typeof(string));
            pivotedTable.Columns.Add("MontoPago", typeof(string));
            pivotedTable.Columns.Add("urlVoucher", typeof(string));
            pivotedTable.Columns.Add("nombreVoucher", typeof(string));

            var conceptos = reciboConceptos.Select(c => c.DescripcionConcepto).Distinct();
            foreach (var conceptoDescripcion in conceptos)
            {
                pivotedTable.Columns.Add(conceptoDescripcion, typeof(string));
            }

            pivotedTable.Columns.Add("MULTA PAGO FUERA DE FECHA", typeof(string));
            pivotedTable.Columns.Add("CARGO SIGUIENTE MES", typeof(string));

            var unidades = reciboConceptos.Where(x => x.ReciboId == recibo.ReciboId).Select(c => c.UnidadId).Distinct();
            var reciboId = reciboConceptos.Where(x => x.ReciboId == recibo.ReciboId).Select(c => c.ReciboId).Distinct().First();
            var blockName = reciboConceptos.Where(x => x.ReciboId == recibo.ReciboId).Select(c => c.Block).Distinct().First();
            var departamentos = reciboConceptos.Where(x => x.ReciboId == recibo.ReciboId).Select(c => c.Dpto).Distinct();


            int i = 0;
            foreach (var unidadId in unidades)
            {
                var reciboPagoFilter = new ReciboPago() { UnidadId = unidadId, ReciboId = recibo.ReciboId };
                ReciboPago pagosReciboUnidad = await _reciboPagoServices.Get(reciboPagoFilter);

                DataRow newRow = pivotedTable.NewRow();
                newRow["UnidadId"] = unidadId;
                newRow["ReciboId"] = reciboId.ToString();
                newRow["DPTO"] = blockName?.ToString() + " " + departamentos.ToList()[i]?.ToString();

                if (pagosReciboUnidad.FechaPago != null)
                {
                    newRow["FechaPago"] = pagosReciboUnidad.FechaPago;
                }
                else
                {
                    newRow["FechaPago"] = System.String.Empty;
                }

                if (pagosReciboUnidad.MontoPago != null)
                {
                    newRow["MontoPago"] = pagosReciboUnidad.MontoPago?.ToString().Replace(",", ".");
                }
                else
                {
                    newRow["MontoPago"] = System.String.Empty;
                }

                if (!string.IsNullOrEmpty(pagosReciboUnidad.urlVoucher))
                {
                    newRow["urlVoucher"] = pagosReciboUnidad.urlVoucher;
                    newRow["nombreVoucher"] = pagosReciboUnidad.nombreVoucher;
                }
                else
                {
                    newRow["urlVoucher"] = System.String.Empty;
                    newRow["nombreVoucher"] = System.String.Empty;
                }


                //multa 
                string _fechaVencimiento = recibo.FechaVencimiento.ToString("yyyyMMdd");
                int multa = 20;

                foreach (var conceptoDescripcion in conceptos)
                {
                    var valor = reciboConceptos
                        .Where(c => c.UnidadId == unidadId && c.DescripcionConcepto == conceptoDescripcion)
                        .Select(c => c.Total)
                        .FirstOrDefault();

                    newRow[conceptoDescripcion] = valor.ToString().Replace(",", ".");

                    ////
                    if (recibo.FechaVencimiento < DateTime.Today)
                    {
                        if (pagosReciboUnidad.MontoPago == null)
                        {
                            newRow["MULTA PAGO FUERA DE FECHA"] = multa;
                            newRow["CARGO SIGUIENTE MES"] = valor.ToString().Replace(",", ".");
                        }
                        else
                        {
                            string _fechaPago = Convert.ToDateTime(pagosReciboUnidad.FechaPago).ToString("yyyyMMdd");
                            if (Convert.ToInt32(_fechaPago) > Convert.ToInt32(_fechaVencimiento))
                            {
                                newRow["MULTA PAGO FUERA DE FECHA"] = multa;
                                newRow["CARGO SIGUIENTE MES"] = System.String.Empty;

                            }
                            else
                            {
                                newRow["MULTA PAGO FUERA DE FECHA"] = System.String.Empty; ;
                                newRow["CARGO SIGUIENTE MES"] = System.String.Empty;
                            }
                        }

                    }
                    else
                    {
                        newRow["MULTA PAGO FUERA DE FECHA"] = System.String.Empty;
                        newRow["CARGO SIGUIENTE MES"] = System.String.Empty;
                    }


                }

                pivotedTable.Rows.Add(newRow);
                i++;
            }


            return pivotedTable;
        }

        public Dictionary<string, string> CalcularTotales(DataTable DataTableModel)
        {
            var totales = new Dictionary<string, string>();

            foreach (DataColumn column in DataTableModel.Columns)
            {
                decimal total = 0;

                foreach (DataRow row in DataTableModel.Rows)
                {
                    var valor = row[column].ToString();
                    if (!string.IsNullOrEmpty(valor))
                    {
                        var newvalor = StringNumberToDecimal(valor);
                        total += newvalor;
                    }
                }

                totales[column.ColumnName] = total.ToString().Replace(",", ".");

            }

            return totales;
        }

        [Route("api/UploadReciboMantenimiento")]
        public async Task<IActionResult> UploadReciboMantenimiento()
        {
            try
            {
                var file = Request.Form.Files.FirstOrDefault();
                var year = Convert.ToInt32(Request.Form["year"].FirstOrDefault());
                var month = Convert.ToInt32(Request.Form["month"].FirstOrDefault());


                if (file == null || file.Length == 0)
                {
                    return BadRequest("No se ha enviado ningún Recibo.");
                }

                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                    // Creamos una instancia de ExcelPackage utilizando el MemoryStream
                    using (var package = new ExcelPackage(memoryStream))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                        if (worksheet == null)
                        {
                            return BadRequest("El Archivo excel no contiene hojas de trabajo.");
                        }

                        int rows = worksheet.Dimension.Rows;
                        int columns = worksheet.Dimension.Columns;


                        var conceptos = new List<Concepto>();
                        int i = 1;
                        for (int col = 3; col <= columns; col++)
                        {
                            Concepto rowDataConcepto = new Concepto();
                            rowDataConcepto.Descripcion = worksheet.Cells[1, col].Text.Trim();
                            rowDataConcepto.Estado = "Activo";

                            conceptos.Add(rowDataConcepto);
                            i++;

                        };


                        //addConceptosList
                        var conceptosList = await _conceptoServices.AddList(conceptos);

                        //addRecibo
                        string nameRecibo = $"Recibo {CultureInfo.CurrentCulture.TextInfo.ToTitleCase(DateTimeFormatInfo.CurrentInfo.GetMonthName(month))} {year}";
                        Recibo recibo = new Recibo();
                        recibo.Descripcion = nameRecibo;
                        recibo.FechaEmision = DateTime.Now;
                        recibo.FechaVencimiento = CalculoFechaVencimiento(year, month);
                        recibo.Mes = month;
                        recibo.Anio = year;
                        recibo.Estado = "Activo";

                        var idRecibo = await _reciboServices.Add(recibo);

                        //addReciboConcepto
                        var reciboConceptoList = new List<ReciboConcepto>();
                        for (int row = 2; row <= rows; row++)
                        {
                            //add dptoList
                            string block = worksheet.Cells[row, 1].Text.Trim().Replace(".", string.Empty).Replace(" ", string.Empty);
                            string dpto = worksheet.Cells[row, 2].Text.Trim();

                            //obtener ReciboConcepto.UnidadId
                            var unidadFilter = new Unidad() { Block = block, Dpto = dpto };
                            var unidadList = await _unidadServices.Get(unidadFilter);
                            int unidadId = unidadList.UnidadId;

                            //addReciboPago
                            ReciboPago reciboPago = new ReciboPago();
                            reciboPago.UnidadId = unidadId;
                            reciboPago.ReciboId = idRecibo;
                            await _reciboPagoServices.Add(reciboPago);

                            //add conceptosList
                            var col = 3;
                            foreach (var item in conceptosList)
                            {

                                ReciboConcepto rowData = new ReciboConcepto();
                                rowData.UnidadId = unidadId;
                                rowData.ReciboId = idRecibo;
                                rowData.ConceptoId = item.ConceptoId;
                                rowData.Total = TryParseDecimal(worksheet.Cells[row, col].Text.Trim());
                                reciboConceptoList.Add(rowData);
                                col++;
                            }

                        };

                        await _reciboConceptoServices.AddList(reciboConceptoList);

                        //S3
                        await UploadFileS3(file, nameRecibo);

                        var modelo = new DataTableModel();
                        modelo.recibo = recibo;
                        modelo.conceptos = conceptos;

                        //// Convierte la lista de objetos a formato JSON
                        string jsonData = JsonConvert.SerializeObject(modelo, Formatting.Indented);

                        return Ok(jsonData);

                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al procesar el Archivo Excel.");
                return StatusCode(500, "Error interno del servidor.");
            }
        }


        [HttpPost]
        public async Task<ActionResult> ProcesarPagoAsync(IFormCollection form)
        {
            // Obtener los valores del formulario
            string idUnidad = form["idUnidad"].ToString();
            string idRecibo = form["idRecibo"].ToString();

            string fechaPago = form["fechaPago"].ToString();
            var monto = form["monto"].ToString();
            string monthForm = form["month"].ToString();
            string yearForm = form["year"].ToString();

            try
            {
                var idUnidadInt = Convert.ToInt32(idUnidad);
                var idReciboInt = Convert.ToInt32(idRecibo);
                var reciboPagoFilter = new ReciboPago() { ReciboId = idReciboInt, UnidadId = idUnidadInt };
                var obj = await _reciboPagoServices.Get(reciboPagoFilter);

                var fileName = await UploadReciboPagoAsync(form);
                var myUrlVoucher = await GetUrlPathObjectDataAsync(fileName);

                obj.nombreVoucher = fileName;
                obj.urlVoucher = myUrlVoucher;
                obj.FechaPago = Convert.ToDateTime(fechaPago);
                obj.MontoPago = StringNumberToDecimal(monto);

                var rptaUpdate = await _reciboPagoServices.Update(obj);


                ////whasap
                //string accountSid = "AC3d80371220736ef94b9924900ce475e6";
                //string authToken = "210fead4833f9a200d2966ec855413e5";

                //TwilioClient.Init(accountSid, authToken);
                //string strBody = "Habitat: Hola se adjunto el Recibo: " + fileName;

                //var from = new Twilio.Types.PhoneNumber("whatsapp:+14155238886");
                //var toList = new List<Twilio.Types.PhoneNumber>
                //        {
                //            new Twilio.Types.PhoneNumber("whatsapp:+51966918363"),
                //            new Twilio.Types.PhoneNumber("whatsapp:+51962916062"), // Agrega más números de destinatario aquí
                //        };

                //foreach (var to in toList)
                //{
                //    var message = MessageResource.Create(
                //        from: from,
                //        body: strBody,
                //        to: to
                //    );

                //    Console.WriteLine($"Mensaje enviado a {to.ToString()}. SID del mensaje: {message.Sid}");
                //}
                ////fin wasap


                //return RedirectToAction("Recibos", "Recibo");
                return RedirectToAction("Block", "Recibo", new { year = yearForm, month = monthForm, unidad = idUnidad });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                // Manejar cualquier excepción que pueda ocurrir durante el proceso.
                return RedirectToAction("Error");
            }

        }

        public async Task<string> UploadReciboPagoAsync(IFormCollection form)
        {
            var newFileName = string.Empty;
            var file = Request.Form.Files.FirstOrDefault(); // Obtiene el Recibo desde la solicitud HTTP

            if (file != null)
            {
                // Obtener los valores del formulario
                var dpto = form["dpto"].ToString().Replace(".", "").Replace(" ", "");
                string monthForm = form["month"].ToString();
                string yearForm = form["year"].ToString();
                var anio = yearForm; // DateTime.Today.Year.ToString();
                var mes = new DateTime(Convert.ToInt32(yearForm), Convert.ToInt32(monthForm), 1).ToString("MMMM", new System.Globalization.CultureInfo("es-MX"));
                var name = $"{anio}_{mes}_{dpto}";

                newFileName = await UploadFileS3(file, name);
            }


            return newFileName;

        }


        private async Task<string> UploadFileS3(IFormFile file, string nameFile)
        {
            var newFileName = string.Empty;

            //IWebHostEnvironment _webHostEnvironment
            //var webHostEnvironmentWebRootPath = _webHostEnvironment.WebRootPath;
            var environmentCurrentDirectory = Environment.CurrentDirectory;
            var appContextBaseDirectory = AppContext.BaseDirectory;
            var appDomainDirectory = AppDomain.CurrentDomain.BaseDirectory;


            var uploadsFolder = Path.Combine(environmentCurrentDirectory, "uploads");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder); // Crea la carpeta si no existe
            }

            //el nombre deberia ser anio_mes_nombrebloque_dpto
            newFileName = $"{nameFile}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(uploadsFolder, newFileName);

            if (System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);


            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream); // Copia el Recibo al servidor
            }


            //S3
            var config = new AmazonS3Config()
            {
                ServiceURL = string.Format(serviceUrlFileBase),
                ForcePathStyle = true,
            };
            _s3Client = new AmazonS3Client(accessKey, secretKey, config);

            await UploadObjectFromFileAsync(_s3Client, bucketName, newFileName, filePath);

            if (System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);

            return newFileName;
        }

        private async Task UploadObjectFromFileAsync(IAmazonS3 client, string bucketName, string objectName, string filePath)
        {
            try
            {

                var extension = Path.GetExtension(filePath);
                var contentType = GetContentType(extension);

                var putRequest = new PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = objectName,
                    FilePath = filePath,
                    ContentType = contentType,
                };

                putRequest.Metadata.Add("x-amz-meta-title", "FileName");

                PutObjectResponse response = await client.PutObjectAsync(putRequest);


            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }

        private async Task<string> GetUrlPathObjectDataAsync(string keyName)
        {

            string pathHref = string.Empty;

            if (!String.IsNullOrEmpty(keyName))
            {
                var config = new AmazonS3Config()
                {
                    ServiceURL = string.Format(serviceUrlFileBase),
                    ForcePathStyle = true,
                };
                var client = new AmazonS3Client(accessKey, secretKey, config);

                try
                {
                    GetObjectRequest request = new GetObjectRequest
                    {
                        BucketName = bucketName,
                        Key = keyName,
                    };

                    using (GetObjectResponse response = await client.GetObjectAsync(request))
                    {

                        string title = response.Metadata["x-amz-meta-title"];
                        string cid = response.Metadata["x-amz-meta-cid"];


                        pathHref = Path.Combine(hostUrlFileBase, cid);

                    }

                }
                catch (AmazonS3Exception e)
                {

                    Console.WriteLine($"Error: '{e.Message}'");
                }
            }


            return pathHref;

        }

        private string GetContentType(string extension)
        {
            var contentTypes = new Dictionary<string, string>
            {
                { ".jpg", "image/jpeg" },
                { ".jpeg", "image/jpeg" },
                { ".png", "image/png" },
                { ".gif", "image/gif" },
                { ".pdf", "application/pdf" },
                { ".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document" },
                { ".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" },
                { ".pptx", "application/vnd.openxmlformats-officedocument.presentationml.presentation" },
            };

            // If the content type is not found in the list, return the default content type.
            return contentTypes.GetValueOrDefault(extension, "application/octet-stream");
        }

        public async Task getBucketFilesTestAsync()
        {
            var config = new AmazonS3Config
            {
                ServiceURL = string.Format(serviceUrlFileBase),
                ForcePathStyle = true,
                SignatureVersion = "2", // Usar SignatureVersion 2 para compatibilidad con s3.filebase.com
                                        // RegionEndpoint = RegionEndpoint.USEast1
            };

            _s3Client = new AmazonS3Client(accessKey, secretKey, config);


            var request = new ListObjectsV2Request
            {
                BucketName = bucketName,
                MaxKeys = 20
            };

            try
            {
                ListObjectsV2Response response = await _s3Client.ListObjectsV2Async(request);
                Console.WriteLine("Success when listing objects");
                foreach (S3Object obj in response.S3Objects)
                {
                    Console.WriteLine($"Object Key: {obj.Key}");
                }
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine("Error when listing objects: " + e.Message);
            }
        }

        public DateTime CalculoFechaVencimiento(int year, int month)
        {
            int diaVencimiento = 2;
            // Obtén el mes seleccionado (asegúrate de que sea un número)
            int selectedMonth = month;

            // Obtén el año actual
            int currentYear = year;

            // Calcula el mes y año del próximo vencimiento
            int nextMonth;
            int nextYear;

            if (selectedMonth == 12)
            {
                // Si se selecciona diciembre, el próximo mes es enero del próximo año
                nextMonth = 1; // 1 representa enero
                nextYear = currentYear + 1;
            }
            else
            {
                // Para otros meses, el próximo mes es el siguiente
                nextMonth = selectedMonth + 1;
                nextYear = currentYear;
            }

            // Obtiene el nombre del próximo mes
            string nextMonthName = new DateTime(nextYear, nextMonth, 1)
                .ToString("MMMM", new System.Globalization.CultureInfo("es-MX"));

            // Formatea la fecha de vencimiento
            string fechaVencimientoStr = $"{diaVencimiento} de {nextMonthName} de {nextYear}";

            // Asigna la fecha de vencimiento al campo deseado
            DateTime fechaVencimiento = new DateTime(nextYear, nextMonth, diaVencimiento);

            return fechaVencimiento;

        }

        public decimal StringNumberToDecimal(string monto)
        {
            // Especifica una cultura con punto decimal
            CultureInfo culture = new CultureInfo("es-MX");
            decimal ret = 0;
            if (decimal.TryParse(monto, NumberStyles.Float, culture, out decimal result))
                ret = result;

            return ret;
        }

        public decimal TryParseDecimal(string input)
        {
            decimal resultado = 0;
            if (!string.IsNullOrEmpty(input))
            {
                input.Replace(",", ".");
                decimal.TryParse(input, out resultado);
            }


            return resultado;
        }


        #region "Departamento"
        public async Task<IActionResult> DepartamentoAsync(string year, int idUnidad)
        {
            var modelo = new DataTableModel();

            if (string.IsNullOrEmpty(year))
                year = DateTime.Today.Year.ToString();

            string? userName = HttpContext.User.Identities.FirstOrDefault()?.Name;
            if (userName != null)
            {
                //var claims = HttpContext.User.Identities.FirstOrDefault()?.Claims.ToList();
                UsersModel userLogin = _claimsServices.getUserClaimsHttpContext(HttpContext);


                var unidades = await _unidadServices.GetAll();
                var unidadesList = unidades.ToList();

                Unidad unidad = new Unidad();
                if (userLogin.Role == "PROPIETARIO")
                {
                    idUnidad = userLogin.UnidadId;
                    Unidad unidadFilter = new Unidad() { UnidadId = idUnidad };
                    unidad = await _unidadServices.Get(unidadFilter);
                }
                else
                {
                    if (idUnidad == 0)
                    {
                        var unidadList = await _unidadServices.GetAll();
                        unidad = unidadList.First();
                        idUnidad = unidad.UnidadId;
                    }

                    Unidad unidadFilter = new Unidad() { UnidadId = idUnidad };
                    unidad = await _unidadServices.Get(unidadFilter);

                }


                var reciboFilter = new Recibo() { Anio = Convert.ToInt32(year) };
                var recibos = await _reciboServices.GetAll(reciboFilter);
                var recibosList = recibos.ToList();

                var mesesList = recibosList.Select(x => Convert.ToInt32(x.Mes)).Distinct().ToList();
                var aniosList = recibosList.Select(x => Convert.ToInt32(x.Anio)).Distinct().ToList();


                if (recibos.Count() > 0)
                {
                    modelo.DataTable = await PivotDepartamentoAsync(recibosList, idUnidad);
                    Dictionary<string, string> totales = CalcularTotalesDepartamento(modelo.DataTable);
                    List<string> cabecerasOriginal = modelo.DataTable.Columns.Cast<DataColumn>().Select(column => column.ColumnName).ToList();

                    modelo.cabeceras = cabecerasOriginal;
                    modelo.unidades = unidadesList;
                    modelo.Totales = totales;
                    modelo.mesesList = mesesList;
                    modelo.aniosList = aniosList;
                    modelo.user = userLogin;
                    modelo.unidad = unidad;
                }

            }
            else
            {

                _ = HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                //return RedirectToAction("Login", "Account");
            }

            return (IActionResult)modelo;

            //return View(modelo);
        }

        public async Task<DataTable> PivotDepartamentoAsync(List<Recibo> recibos, int idUnidad)
        {
            //if (idUnidad == 0) idUnidad = 1;

            List<ReciboConceptoReporte> reciboConceptos = new List<ReciboConceptoReporte>();
            foreach (Recibo recibo in recibos)
            {
                ReciboConceptoReporte reciboConceptoFilter = new ReciboConceptoReporte() { UnidadId = idUnidad, Anio = Convert.ToInt32(recibo.Anio) };
                var rpta = await _reciboConceptoServices.GetAll(reciboConceptoFilter); //debe traer mas campos;
                reciboConceptos = rpta.ToList();
            }

            DataTable pivotedTable = new DataTable();
            pivotedTable = new DataTable();
            pivotedTable.Columns.Add("UnidadId", typeof(string));
            pivotedTable.Columns.Add("ReciboId", typeof(string));
            pivotedTable.Columns.Add("MES", typeof(string));
            pivotedTable.Columns.Add("FechaPago", typeof(string));
            pivotedTable.Columns.Add("MontoPago", typeof(string));
            pivotedTable.Columns.Add("urlVoucher", typeof(string));
            pivotedTable.Columns.Add("nombreVoucher", typeof(string));
            pivotedTable.Columns.Add("DPTO", typeof(string));
            pivotedTable.Columns.Add("FechaVencimiento", typeof(string));

            var conceptos = reciboConceptos.Select(c => c.DescripcionConcepto).Distinct();
            foreach (var conceptoDescripcion in conceptos)
            {
                pivotedTable.Columns.Add(conceptoDescripcion, typeof(string));
            }

            pivotedTable.Columns.Add("MULTA PAGO FUERA DE FECHA", typeof(string));
            pivotedTable.Columns.Add("CARGO SIGUIENTE MES", typeof(string));

            //var unidades = reciboConceptos.Where(x => x.UnidadId == idUnidad).Select(c => c.UnidadId).Distinct();


            var blockName = reciboConceptos.Where(x => x.UnidadId == idUnidad).Select(c => c.Block).Distinct().First();
            var departamentos = reciboConceptos.Where(x => x.UnidadId == idUnidad).Select(c => c.Dpto).Distinct().First();
            var mesesList = reciboConceptos.Where(x => x.UnidadId == idUnidad).Select(c => Convert.ToInt32(c.Mes)).Distinct().OrderBy(mes => mes).ToList();


            int i = 0;
            foreach (var mes in mesesList)
            {
                int idRecibo = reciboConceptos.Where(x => x.UnidadId == idUnidad && x.Mes == mes).Select(c => c.ReciboId).First();


                var reciboPagoFilter = new ReciboPago() { UnidadId = idUnidad, ReciboId = idRecibo };
                ReciboPago pagosReciboUnidad = await _reciboPagoServices.Get(reciboPagoFilter);

                DataRow newRow = pivotedTable.NewRow();
                newRow["UnidadId"] = idUnidad;
                newRow["ReciboId"] = idRecibo;
                newRow["DPTO"] = blockName?.ToString() + " " + departamentos?.ToString();
                newRow["Mes"] = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(DateTimeFormatInfo.CurrentInfo.GetMonthName(Convert.ToInt32(mes)));

                if (pagosReciboUnidad.FechaPago != null)
                {
                    newRow["FechaPago"] = pagosReciboUnidad.FechaPago;
                }
                else
                {
                    newRow["FechaPago"] = System.String.Empty;
                }

                if (pagosReciboUnidad.MontoPago != null)
                {
                    newRow["MontoPago"] = pagosReciboUnidad.MontoPago?.ToString().Replace(",", ".");
                }
                else
                {
                    newRow["MontoPago"] = System.String.Empty;
                }

                if (!string.IsNullOrEmpty(pagosReciboUnidad.urlVoucher))
                {
                    newRow["urlVoucher"] = pagosReciboUnidad.urlVoucher;
                    newRow["nombreVoucher"] = pagosReciboUnidad.nombreVoucher;
                }
                else
                {
                    newRow["urlVoucher"] = System.String.Empty;
                    newRow["nombreVoucher"] = System.String.Empty;
                }

                newRow["FechaVencimiento"] = recibos.Where(x => x.ReciboId == idRecibo).First().FechaVencimientoStr;

                int multa = 20;
                foreach (var conceptoDescripcion in conceptos)
                {

                    var valor = reciboConceptos
                        .Where(c => c.UnidadId == idUnidad && c.DescripcionConcepto == conceptoDescripcion && c.Mes == mes)
                        .Select(c => c.Total)
                        .FirstOrDefault();

                    newRow[conceptoDescripcion] = valor.ToString().Replace(",", ".");


                    ////multa
                    string fechaVencimientoCompare = recibos.Where(x => x.ReciboId == idRecibo).First().FechaVencimiento.ToString("yyyyMMdd");
                    DateTime fechaVencimiento = recibos.Where(x => x.ReciboId == idRecibo).First().FechaVencimiento;
                    if (fechaVencimiento < DateTime.Today)
                    {
                        if (pagosReciboUnidad.MontoPago == null)
                        {
                            newRow["MULTA PAGO FUERA DE FECHA"] = multa;
                            newRow["CARGO SIGUIENTE MES"] = valor.ToString().Replace(",", ".");
                        }
                        else
                        {
                            string _fechaPago = Convert.ToDateTime(pagosReciboUnidad.FechaPago).ToString("yyyyMMdd");
                            if (Convert.ToInt32(_fechaPago) > Convert.ToInt32(fechaVencimientoCompare))
                            {
                                newRow["MULTA PAGO FUERA DE FECHA"] = multa;
                                newRow["CARGO SIGUIENTE MES"] = System.String.Empty;

                            }
                            else
                            {
                                newRow["MULTA PAGO FUERA DE FECHA"] = System.String.Empty; ;
                                newRow["CARGO SIGUIENTE MES"] = System.String.Empty;
                            }
                        }

                    }
                    else
                    {
                        newRow["MULTA PAGO FUERA DE FECHA"] = System.String.Empty;
                        newRow["CARGO SIGUIENTE MES"] = System.String.Empty;
                    }


                }

                pivotedTable.Rows.Add(newRow);
                i++;
            }

            return pivotedTable;
        }

        public Dictionary<string, string> CalcularTotalesDepartamento(DataTable DataTableModel)
        {
            var totales = new Dictionary<string, string>();

            foreach (DataColumn column in DataTableModel.Columns)
            {
                decimal total = 0;

                foreach (DataRow row in DataTableModel.Rows)
                {
                    var valor = row[column].ToString();
                    if (!string.IsNullOrEmpty(valor))
                    {
                        var newvalor = StringNumberToDecimal(valor);
                        total += newvalor;
                    }
                }

                totales[column.ColumnName] = total.ToString().Replace(",", ".");

            }

            return totales;
        }

        [HttpPost]
        public async Task<ActionResult> ProcesarPagoDepartamentoAsync(IFormCollection form)
        {
            // Obtener los valores del formulario
            string idUnidad = form["idUnidad"].ToString();
            string idRecibo = form["idRecibo"].ToString();

            string fechaPago = form["fechaPago"].ToString();
            var monto = form["monto"].ToString();
            string yearForm = form["year"].ToString();

            try
            {
                var idUnidadInt = Convert.ToInt32(idUnidad);
                var idReciboInt = Convert.ToInt32(idRecibo);
                var reciboPagoFilter = new ReciboPago() { ReciboId = idReciboInt, UnidadId = idUnidadInt };
                var obj = await _reciboPagoServices.Get(reciboPagoFilter);

                var fileName = await UploadReciboPagoAsync(form);
                var myUrlVoucher = await GetUrlPathObjectDataAsync(fileName);

                obj.nombreVoucher = fileName;
                obj.urlVoucher = myUrlVoucher;
                obj.FechaPago = Convert.ToDateTime(fechaPago);
                obj.MontoPago = StringNumberToDecimal(monto);

                var rptaUpdate = await _reciboPagoServices.Update(obj);

                return RedirectToAction("Departamento", "Recibo", new { year = yearForm, idUnidad = idUnidadInt, recibo = idReciboInt });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                // Manejar cualquier excepción que pueda ocurrir durante el proceso.
                return RedirectToAction("Error");
            }

        }
        #endregion


    }
}

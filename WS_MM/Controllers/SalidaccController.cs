using Shared.Controllers;
using Shared.Helpers;
using Shared.Util;
using System;
using static WS_MM.Models.SalidaccModel;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Globalization;
using System.Web;
using System.IO;
using System.Threading.Tasks;

namespace WS_MM.Controllers
{
    [CorsOrigin]
    [Authorize]
    public class SalidaccController : BaseController
    {
        [HttpGet]
        [ActionName("PMMM_GetTipoMovimientoCC")]
        public IHttpActionResult PMMM_GetTipoMovimientoCC(int prmintEmpresa, Boolean? prmbitActivo)
        {
            try
            {
                DataTable dt = Instancia.GetTipoMovimientoCC(prmintEmpresa, prmbitActivo);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_GetTipoDocumentoCC")]
        public IHttpActionResult PMMM_GetTipoDocumentoCC(int prmintEmpresa, Boolean? prmbitActivo)
        {
            try
            {
                DataTable dt = Instancia.GetTipoDocumentoCC(prmintEmpresa, prmbitActivo);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_GetReservaDetail")]
        public IHttpActionResult PMMM_GetReservaDetail(int prmintReserva)
        {
            try
            {
                DataTable dt = Instancia.GetReservaDetail(prmintReserva);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpPost]
        [ActionName("SaveUpdate_ReservasMigo")]
        public IHttpActionResult SaveUpdate_ReservasMigo(dynamic obj)
        {
            try
            {
                int prmintMigo = obj.Migo;
                int prmintTipoMovimiento = obj.TipoMovimiento;
                int prmintTipoDocumento = obj.TipoDocumento;
                string prmstrValeConsumo = obj.ValeConsumo;
                string prmstrSolicitante = obj.Solicitante;
                int? prmintReserva = obj.Reserva;
                DateTime prmdateFechaContable = obj.FechaContable;
                DateTime prmdateFechaDocumento = obj.FechaDocumento;
                int idSociedad = JWT.IdSociedad;
                int idEmpresa = JWT.IdEmpresa;
                bool prmintActivo = obj.Activo;
                string xml = obj.strXML;
                string ussername = JWT.Login;

                int dt = Instancia.SaveUpdate_ReservaMaterial(prmintMigo, prmintTipoMovimiento, prmintTipoDocumento, prmstrValeConsumo, prmintReserva, prmdateFechaContable, prmdateFechaDocumento, idSociedad, idEmpresa, prmintActivo,  xml, ussername, prmstrSolicitante);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }
        [HttpGet]
        [ActionName("PMMM_GetTipoMovimiento")]
        public IHttpActionResult PMMM_GetTipoMovimiento()
        {
            try
            {
                DataTable dt = Instancia.GetTipoMovimiento();
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }
        [HttpPost]
        [ActionName("EC_GetDetalleConsumos")]
        public IHttpActionResult EC_GetDetalleConsumos(dynamic obj)
        {
            try
            {
                DateTime? prmdatefInicio = obj.prmFinicio;
                DateTime? prmdatefFin = obj.prmfFin;
                int? prmintCentro = obj.prmintCentro;
                int? prmintTipoMovimiento = obj.prmMovimiento;
                int? prmintTipoMaterial = obj.prmintTMaterial;
                int? prmintMaterial = obj.prmMaterial;
                int? prmintGrupoarticulo = obj.prmGarticulo;
                int? prmintAlmacen = obj.prmAlmacen;
                string prmstrLote= obj.prmLote;
                int? prmintCCosto = obj.prmCecos;
                int idEmpresa = JWT.IdEmpresa;
                int pkUser = JWT.IdUsuario;
                DataTable dt = Instancia.getReporteConsumo(prmdatefInicio, prmdatefFin, prmintCentro, prmintTipoMovimiento, 
                                                            prmintTipoMaterial, prmintMaterial, prmintGrupoarticulo, prmintAlmacen, 
                                                            prmstrLote, prmintCCosto, idEmpresa, pkUser);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [ActionName("EC_GetSalidaCC")]
        public IHttpActionResult EC_GetSalidaCC(dynamic obj)
        {
            try
            {
                DateTime? prmdatefInicio = obj.prmFinicio;
                DateTime? prmdatefFin = obj.prmfFin;
                int? prmintCentro = obj.prmintCentro;
                DataTable dt = Instancia.getSalidasCC(prmdatefInicio, prmdatefFin, prmintCentro);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [ActionName("EC_AnulacionCC")]
        public IHttpActionResult EC_AnulacionCC(dynamic obj)
        {
            try
            {
                DateTime prmdatefContable = obj.prmFcontable;
                int prmintMigo = obj.migo;
                int Empresa = JWT.IdEmpresa;
                int Sociedad = JWT.IdSociedad;
                string Usser = JWT.Login;
                int dt = Instancia.AnulacionSalidasCC(prmdatefContable, prmintMigo, Empresa, Sociedad, Usser);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [ActionName("PMMM_GetAlmacenDestino")]
        public IHttpActionResult PMMM_GetAlmacenDestino(int prmintAlmacen, int prmintCentro)
        {
            try
            {
                int Empresa = JWT.IdEmpresa;
                DataTable dt = Instancia.GetAlmacenDestino(prmintAlmacen, prmintCentro, Empresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }
        [HttpPost]
        [ActionName("SaveUpdate_TraladoSimple")]
        public IHttpActionResult SaveUpdate_TraladoSimple(dynamic obj)
        {
            try
            {
                int prmintMigo = obj.Migo;
                int prmintAlmacenOrigen = obj.prmintAlmacenOrigen;
                int prmintAlmacenDestino = obj.prmintAlmacenDestino;
                DateTime prmdateFechaContable = obj.FechaContable;
                DateTime prmdateFechaDocumento = obj.FechaDocumento;
                string prmstrSerie = obj.Serie;
                string prmstrNumero = obj.Numero;
                int idSociedad = JWT.IdSociedad;
                int idEmpresa = JWT.IdEmpresa;
                bool prmintActivo = obj.Activo;
                string xml = obj.strXML;
                string ussername = JWT.Login;

                int dt = Instancia.SaveUpdate_TrasladoSimple(prmintMigo, prmintAlmacenOrigen, prmintAlmacenDestino, prmdateFechaContable, prmdateFechaDocumento, idSociedad, idEmpresa, prmintActivo, xml, ussername, prmstrSerie, prmstrNumero);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }
        [HttpPost]
        [ActionName("EC_GetTrasladoSimple")]
        public IHttpActionResult EC_GetTrasladoSimple(dynamic obj)
        {
            try
            {
                DateTime? prmdatefInicio = obj.prmFinicio;
                DateTime? prmdatefFin = obj.prmfFin;
                int? prmintCentro = obj.prmintCentro;
                DataTable dt = Instancia.getTrasladosSimples(prmdatefInicio, prmdatefFin, prmintCentro);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [ActionName("IT_GetWsPerUser")]
        public IHttpActionResult IT_GetWsPerUser()
        {
            try
            {
                int Usuario = JWT.IdUsuario;
                DataTable dt = Instancia.GetWsPerUser(Usuario);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_GetMateriales")]
        public IHttpActionResult PMMM_GetMateriales()
        {
            try
            {
                DataTable dt = Instancia.GetMateriales();
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_GetProveedoresList")]
        public IHttpActionResult PMMM_GetProveedoresList()
        {
            try
            {
                DataTable dt = Instancia.GetProveedores();
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpPost]
        [ActionName("Save_MaterialPrecio")]
        public IHttpActionResult Save_MaterialPrecio(dynamic obj)
        {
            try
            {
                string xml = obj.strXML;
                string ussername = JWT.Login;

                int dt = Instancia.Save_MaterialPrecio( xml, ussername);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_GetPrecioList")]
        public IHttpActionResult PMMM_GetPrecioList()
        {
            try
            {
                DataTable dt = Instancia.GetPreciosList();
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_GetSolicitanteList")]
        public IHttpActionResult PMMM_GetSolicitanteList()
        {
            try
            {
                DataTable dt = Instancia.GetSolicitanteList();
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PMM_GetFomatoSalida")]
        public async Task<IHttpActionResult> PMM_GetFomatoSalida(int prmintMigo)
        {
            try
            {
                int prmintEmpresa = JWT.IdEmpresa;
                DataTable dt = Instancia.GetDataMigoToPdf(prmintMigo, prmintEmpresa);
                int Repuesta = Convert.ToInt32(dt.Rows[0]["Respuesta"]);

                if (Repuesta == -1)
                {
                    return Ok(new
                    {
                        ok = false,
                        message = -1
                    });

                }
                else if (Repuesta == -2)
                {
                    return Ok(new
                    {
                        ok = false,
                        message = -2
                    });
                }
                else

                    // GENERA PDF 95%
                    GenerarPDF95Porc(dt);


                if (dt.Rows.Count > 0 & Repuesta != -1)
                {
                    string filePath_95 = HttpContext.Current.Server.MapPath("~/Salidas/" + dt.Rows[0]["Correlativo"].ToString() + ".pdf");
                    string strBlob_95 = null;

                    if (File.Exists(filePath_95))
                    {

                        using (FileStream stream = File.Open(filePath_95, FileMode.Open))
                        {
                            var result = new byte[stream.Length];
                            await stream.ReadAsync(result, 0, (int)stream.Length);

                            strBlob_95 = Convert.ToBase64String(result);
                        }


                        return Ok(new
                        {
                            ok = true,
                            message = 1,
                            name_95 = dt.Rows[0]["Correlativo"].ToString(),
                            base64_95 = strBlob_95,
                        });
                    }
                    else
                    if (Repuesta == -1)

                    {

                        return Ok(new
                        {
                            ok = false,
                            message = -1
                        });
                    }
                    else
                    {

                        return Ok(new
                        {
                            ok = false,
                            message = string.Format("File Id not found: {0} .", prmintMigo)
                        });
                    }

                }
                else
                if (Repuesta == -1)

                {

                    return Ok(new
                    {
                        ok = false,
                        message = -1
                    });
                }
                else

                {
                    return Ok(new
                    {
                        ok = false,
                        message = string.Format("File Id not found: {0} .", prmintMigo)
                    });
                }
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    ok = false,
                    message = string.Format("File Id not found: {0} .", prmintMigo)
                });
            }
        }

        public void GenerarPDF95Porc(DataTable dt)
        {
            var filePathPrimero = HttpContext.Current.Server.MapPath("~/Salidas");

            List<string> strFiles = Directory.GetFiles(filePathPrimero, "*", SearchOption.AllDirectories).ToList();

            foreach (string fichero in strFiles)
            {
                File.Delete(fichero);
            }


            string filePath = HttpContext.Current.Server.MapPath("~/Salidas/" + dt.Rows[0]["Correlativo"].ToString() + ".pdf");

            Document doc = new Document(PageSize.LETTER.Rotate(), 30f, 20f, 150f, 40f);
            // Indicamos donde vamos a guardar el documento
            PdfWriter writer = PdfWriter.GetInstance(doc,
                                        new FileStream(filePath, FileMode.Create));

            string pathImage = HttpContext.Current.Server.MapPath("~/Content/images/logo.jpg");

            writer.PageEvent = new HeaderFooter(pathImage, dt, "95");

            // Le colocamos el título y el autor
            // **Nota: Esto no será visible en el documento
            doc.AddTitle(dt.Rows[0]["NroGuia"].ToString());
            doc.AddCreator(dt.Rows[0]["NroGuia"].ToString());

            // Abrimos el archivo
            doc.Open();

            var FontColour = new BaseColor(0, 0, 0);
            var FontColourCell = new BaseColor(173, 216, 230);


            iTextSharp.text.Font _standardFont_negrita = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.BOLD, FontColour);
            iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 11, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font _standardFont_detalle_negrita = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 11, iTextSharp.text.Font.BOLD, FontColour);
            iTextSharp.text.Font _standardFont_detalle = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9, iTextSharp.text.Font.NORMAL, FontColour);
            iTextSharp.text.Font _standardFontHeader_det = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font _standardFontTable = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font _standardFontMotivo = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font _standardFontHeader = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

            // Datos acá

            PdfPTable tblDestino = new PdfPTable(9);
            tblDestino.WidthPercentage = 100;
            float[] medidaCeldasTable = { 0.25f,0.01f,0.5f, 0.01f, 0.5f,0.01f,0.3f,0.7f,1f };

            tblDestino.SetWidths(medidaCeldasTable);


            PdfPCell _cell_destino = new PdfPCell(new Paragraph("Número de Orden", _standardFontHeader));
            _cell_destino.HorizontalAlignment = Element.ALIGN_LEFT;
            _cell_destino.Colspan = 9;
            _cell_destino.BorderWidth = 0;
            //_cell_destino.BackgroundColor = new BaseColor(173, 216, 230);
            tblDestino.AddCell(_cell_destino);

            _cell_destino = new PdfPCell(new Paragraph(dt.Rows[0]["Correlativo"].ToString(), _standardFontTable));
            _cell_destino.HorizontalAlignment = Element.ALIGN_LEFT;
            _cell_destino.Colspan = 3;
            _cell_destino.BorderWidth = 1;
            tblDestino.AddCell(_cell_destino);

            _cell_destino = new PdfPCell(new Paragraph(" ", _standardFontTable));
            _cell_destino.HorizontalAlignment = Element.ALIGN_CENTER;
            _cell_destino.BorderWidth = 0;
            //_cell_destino.BackgroundColor = new BaseColor(173, 216, 230);
            tblDestino.AddCell(_cell_destino);

            _cell_destino = new PdfPCell(new Paragraph(dt.Rows[0]["RazonSocial"].ToString(), _standardFontTable));
            _cell_destino.HorizontalAlignment = Element.ALIGN_CENTER;
            _cell_destino.BorderWidth = 1;
            //_cell_destino.BackgroundColor = new BaseColor(173, 216, 230);
            tblDestino.AddCell(_cell_destino);

            _cell_destino = new PdfPCell(new Paragraph("", _standardFontTable));
            _cell_destino.HorizontalAlignment = Element.ALIGN_LEFT;
            _cell_destino.Colspan = 4;
            _cell_destino.Border = 0;
            tblDestino.AddCell(_cell_destino);

            _cell_destino = new PdfPCell(new Paragraph("Punto de Emisión", _standardFontHeader));
            _cell_destino.HorizontalAlignment = Element.ALIGN_LEFT;
            _cell_destino.Colspan = 3;
            _cell_destino.BorderWidth = 1;
            //_cell_destino.BackgroundColor = new BaseColor(173, 216, 230);
            tblDestino.AddCell(_cell_destino);

            _cell_destino = new PdfPCell(new Paragraph(" ", _standardFontTable));
            _cell_destino.HorizontalAlignment = Element.ALIGN_CENTER;
            _cell_destino.BorderWidth = 0;
            _cell_destino.BackgroundColor = new BaseColor(173, 216, 230);
            tblDestino.AddCell(_cell_destino);

            _cell_destino = new PdfPCell(new Paragraph("Periodo", _standardFontHeader));
            _cell_destino.HorizontalAlignment = Element.ALIGN_CENTER;
            _cell_destino.BorderWidth = 1;
            //_cell_destino.BackgroundColor = new BaseColor(173, 216, 230);
            tblDestino.AddCell(_cell_destino);

            _cell_destino = new PdfPCell(new Paragraph("", _standardFontTable));
            _cell_destino.HorizontalAlignment = Element.ALIGN_LEFT;
            _cell_destino.Colspan = 4;
            _cell_destino.Border = 0;
            tblDestino.AddCell(_cell_destino);

            _cell_destino = new PdfPCell(new Paragraph(dt.Rows[0]["CodigoCentro"].ToString(), _standardFontTable));
            _cell_destino.HorizontalAlignment = Element.ALIGN_CENTER;
            _cell_destino.BorderWidth = 1;
            //_cell_destino.BackgroundColor = new BaseColor(173, 216, 230);
            tblDestino.AddCell(_cell_destino);

            _cell_destino = new PdfPCell(new Paragraph("", _standardFontTable));
            _cell_destino.HorizontalAlignment = Element.ALIGN_LEFT;
            _cell_destino.Border = 0;
            tblDestino.AddCell(_cell_destino);

            _cell_destino = new PdfPCell(new Paragraph(dt.Rows[0]["NombreCentro"].ToString(), _standardFontTable));
            _cell_destino.HorizontalAlignment = Element.ALIGN_CENTER;
            _cell_destino.BorderWidth = 1;
            /*cell_destino.BackgroundColor = new BaseColor(173, 216, 230);*/
            tblDestino.AddCell(_cell_destino);

            _cell_destino = new PdfPCell(new Paragraph("", _standardFontTable));
            _cell_destino.HorizontalAlignment = Element.ALIGN_LEFT;
            _cell_destino.Border = 0;
            tblDestino.AddCell(_cell_destino);

            _cell_destino = new PdfPCell(new Paragraph(dt.Rows[0]["DesMes"].ToString(), _standardFontTable));
            _cell_destino.HorizontalAlignment = Element.ALIGN_CENTER;
            _cell_destino.BorderWidth = 1;
            //_cell_destino.BackgroundColor = new BaseColor(173, 216, 230);
            tblDestino.AddCell(_cell_destino);

            _cell_destino = new PdfPCell(new Paragraph("", _standardFontTable));
            _cell_destino.HorizontalAlignment = Element.ALIGN_LEFT;
            _cell_destino.Border = 0;
            tblDestino.AddCell(_cell_destino);

            _cell_destino = new PdfPCell(new Paragraph(dt.Rows[0]["Anio"].ToString(), _standardFontTable));
            _cell_destino.HorizontalAlignment = Element.ALIGN_CENTER;
            _cell_destino.BorderWidth = 1;
            //_cell_destino.BackgroundColor = new BaseColor(173, 216, 230);
            tblDestino.AddCell(_cell_destino);

            _cell_destino = new PdfPCell(new Paragraph("", _standardFontTable));
            _cell_destino.HorizontalAlignment = Element.ALIGN_LEFT;
            _cell_destino.Border = 0;
            _cell_destino.Colspan = 2;
            tblDestino.AddCell(_cell_destino);

            _cell_destino = new PdfPCell(new Paragraph("", _standardFontTable));
            _cell_destino.HorizontalAlignment = Element.ALIGN_LEFT;
            _cell_destino.Border = 0;
            _cell_destino.Colspan = 2;
            tblDestino.AddCell(_cell_destino);

            _cell_destino = new PdfPCell(new Paragraph("Fecha", _standardFontHeader));
            _cell_destino.HorizontalAlignment = Element.ALIGN_CENTER;
            _cell_destino.BorderWidth = 1;
            //_cell_destino.BackgroundColor = new BaseColor(173, 216, 230);
            tblDestino.AddCell(_cell_destino);

            _cell_destino = new PdfPCell(new Paragraph("", _standardFontTable));
            _cell_destino.HorizontalAlignment = Element.ALIGN_LEFT;
            _cell_destino.Border = 0;
            tblDestino.AddCell(_cell_destino);

            _cell_destino = new PdfPCell(new Paragraph(dt.Rows[0]["Fecha"].ToString(), _standardFontTable));
            _cell_destino.HorizontalAlignment = Element.ALIGN_CENTER;
            _cell_destino.BorderWidth = 1;
            //_cell_destino.BackgroundColor = new BaseColor(173, 216, 230);
            tblDestino.AddCell(_cell_destino);

            _cell_destino = new PdfPCell(new Paragraph("", _standardFontTable));
            _cell_destino.HorizontalAlignment = Element.ALIGN_LEFT;
            _cell_destino.Border = 0;
            _cell_destino.Colspan = 4;
            tblDestino.AddCell(_cell_destino);


            _cell_destino = new PdfPCell(new Paragraph("", _standardFontTable));
            _cell_destino.HorizontalAlignment = Element.ALIGN_LEFT;
            _cell_destino.Border = 0;
            _cell_destino.Colspan = 2;
            tblDestino.AddCell(_cell_destino);

            _cell_destino = new PdfPCell(new Paragraph("Área", _standardFontHeader));
            _cell_destino.HorizontalAlignment = Element.ALIGN_CENTER;
            _cell_destino.BorderWidth = 1;
            //_cell_destino.BackgroundColor = new BaseColor(173, 216, 230);
            tblDestino.AddCell(_cell_destino);

            _cell_destino = new PdfPCell(new Paragraph("", _standardFontTable));
            _cell_destino.HorizontalAlignment = Element.ALIGN_LEFT;
            _cell_destino.Border = 0;
            tblDestino.AddCell(_cell_destino);

            _cell_destino = new PdfPCell(new Paragraph(dt.Rows[0]["AreaSolicitante"].ToString(), _standardFontTable));
            _cell_destino.HorizontalAlignment = Element.ALIGN_CENTER;
            _cell_destino.BorderWidth = 1;
            //_cell_destino.BackgroundColor = new BaseColor(173, 216, 230);
            tblDestino.AddCell(_cell_destino);

            _cell_destino = new PdfPCell(new Paragraph("", _standardFontTable));
            _cell_destino.HorizontalAlignment = Element.ALIGN_LEFT;
            _cell_destino.Border = 0;
            _cell_destino.Colspan = 4;
            tblDestino.AddCell(_cell_destino);


            _cell_destino = new PdfPCell(new Paragraph("", _standardFontTable));
            _cell_destino.HorizontalAlignment = Element.ALIGN_LEFT;
            _cell_destino.Border = 0;
            _cell_destino.Colspan = 2;
            tblDestino.AddCell(_cell_destino);

            _cell_destino = new PdfPCell(new Paragraph("Solicitante", _standardFontHeader));
            _cell_destino.HorizontalAlignment = Element.ALIGN_CENTER;
            _cell_destino.BorderWidth = 1;
            //_cell_destino.BackgroundColor = new BaseColor(173, 216, 230);
            tblDestino.AddCell(_cell_destino);

            _cell_destino = new PdfPCell(new Paragraph("", _standardFontTable));
            _cell_destino.HorizontalAlignment = Element.ALIGN_LEFT;
            _cell_destino.Border = 0;
            tblDestino.AddCell(_cell_destino);

            _cell_destino = new PdfPCell(new Paragraph(dt.Rows[0]["DniSolicitante"].ToString(), _standardFontTable));
            _cell_destino.HorizontalAlignment = Element.ALIGN_CENTER;
            _cell_destino.BorderWidth = 1;
            //_cell_destino.BackgroundColor = new BaseColor(173, 216, 230);
            tblDestino.AddCell(_cell_destino);

            _cell_destino = new PdfPCell(new Paragraph("", _standardFontTable));
            _cell_destino.HorizontalAlignment = Element.ALIGN_LEFT;
            _cell_destino.Border = 0;
            tblDestino.AddCell(_cell_destino);

            _cell_destino = new PdfPCell(new Paragraph(dt.Rows[0]["NombreSolicitante"].ToString(), _standardFontTable));
            _cell_destino.HorizontalAlignment = Element.ALIGN_CENTER;
            _cell_destino.BorderWidth = 1;
            _cell_destino.Colspan = 2;
            //_cell_destino.BackgroundColor = new BaseColor(173, 216, 230);
            tblDestino.AddCell(_cell_destino);

            _cell_destino = new PdfPCell(new Paragraph("", _standardFontTable));
            _cell_destino.HorizontalAlignment = Element.ALIGN_LEFT;
            _cell_destino.Border = 0;
            tblDestino.AddCell(_cell_destino);


            _cell_destino = new PdfPCell(new Paragraph("", _standardFontTable));
            _cell_destino.HorizontalAlignment = Element.ALIGN_LEFT;
            _cell_destino.Border = 0;
            _cell_destino.Colspan = 2;
            tblDestino.AddCell(_cell_destino);

            _cell_destino = new PdfPCell(new Paragraph("Responsable", _standardFontHeader));
            _cell_destino.HorizontalAlignment = Element.ALIGN_CENTER;
            _cell_destino.BorderWidth = 1;
            //_cell_destino.BackgroundColor = new BaseColor(173, 216, 230);
            tblDestino.AddCell(_cell_destino);

            _cell_destino = new PdfPCell(new Paragraph("", _standardFontTable));
            _cell_destino.HorizontalAlignment = Element.ALIGN_LEFT;
            _cell_destino.Border = 0;
            tblDestino.AddCell(_cell_destino);

            _cell_destino = new PdfPCell(new Paragraph(dt.Rows[0]["DocIdentidad"].ToString(), _standardFontTable));
            _cell_destino.HorizontalAlignment = Element.ALIGN_CENTER;
            _cell_destino.BorderWidth = 1;
            //_cell_destino.BackgroundColor = new BaseColor(173, 216, 230);
            tblDestino.AddCell(_cell_destino);

            _cell_destino = new PdfPCell(new Paragraph("", _standardFontTable));
            _cell_destino.HorizontalAlignment = Element.ALIGN_LEFT;
            _cell_destino.Border = 0;
            tblDestino.AddCell(_cell_destino);

            _cell_destino = new PdfPCell(new Paragraph(dt.Rows[0]["Nombre"].ToString(), _standardFontTable));
            _cell_destino.HorizontalAlignment = Element.ALIGN_CENTER;
            _cell_destino.BorderWidth = 1;
            _cell_destino.Colspan = 2;
            //_cell_destino.BackgroundColor = new BaseColor(173, 216, 230);
            tblDestino.AddCell(_cell_destino);

            _cell_destino = new PdfPCell(new Paragraph("", _standardFontTable));
            _cell_destino.HorizontalAlignment = Element.ALIGN_LEFT;
            _cell_destino.Border = 0;
            tblDestino.AddCell(_cell_destino);

            _cell_destino = new PdfPCell(new Paragraph(" ", _standardFontTable));
            _cell_destino.HorizontalAlignment = Element.ALIGN_LEFT;
            _cell_destino.Border = 0;
            _cell_destino.Colspan = 9;
            tblDestino.AddCell(_cell_destino);

            doc.Add(tblDestino);

            doc.Add(Chunk.NEWLINE);
            doc.Add(Chunk.NEWLINE);

            // Creamos una tabla que contendrá el Refencia, Descripción, Cantidad, Precio, Importe.

            PdfPTable tblDetalle = new PdfPTable(12);
            tblDetalle.WidthPercentage = 100;

            float[] medidaCeldas = { 0.10f, 0.40f, 0.80f, 0.20f, 0.20f, 0.30f, 0.40f, 0.40f, 0.40f, 0.40f, 0.40f, 0.80f };

            // ASIGNAS LAS MEDIDAS A LA TABLA (ANCHO)
            tblDetalle.SetWidths(medidaCeldas);


            // Configuramos el título de las columnas de la tabla
            PdfPCell clNro = new PdfPCell(new Phrase("Nro", _standardFont));
            clNro.BorderWidth = 1f;
            clNro.BorderColor = FontColour;
            clNro.BackgroundColor = FontColourCell;
            clNro.HorizontalAlignment = Element.ALIGN_LEFT;
            clNro.VerticalAlignment = Element.ALIGN_CENTER;
            clNro.FixedHeight = 18f;


            PdfPCell clCodigo = new PdfPCell(new Phrase("Codigo", _standardFont));
            clCodigo.BorderWidth = 1f;
            clCodigo.BorderColor = FontColour;
            clCodigo.BackgroundColor = FontColourCell;
            clCodigo.HorizontalAlignment = Element.ALIGN_LEFT;
            clCodigo.VerticalAlignment = Element.ALIGN_CENTER;
            clCodigo.FixedHeight = 18f;

            PdfPCell clDescripcion = new PdfPCell(new Phrase("Descripcion", _standardFont));
            clDescripcion.BorderWidth = 1f;
            clDescripcion.BorderColor = FontColour;
            clDescripcion.BackgroundColor = FontColourCell;
            //clPrecio.BorderWidthBottom = 0.75f;

            //clReferencia.BorderWidthBottom = 1f;

            PdfPCell clCantidad = new PdfPCell(new Phrase("Cantidad", _standardFont));
            clCantidad.BorderWidth = 1f;
            clCantidad.BorderColor = FontColour;
            clCantidad.BackgroundColor = FontColourCell;
            clCantidad.FixedHeight = 18f;
            //clDescripcion.BorderWidthBottom = 1f;

            PdfPCell clMedida = new PdfPCell(new Phrase("U.M", _standardFont));
            clMedida.BorderWidth = 1f;
            clMedida.BorderColor = FontColour;
            clMedida.BackgroundColor = FontColourCell;
            clMedida.FixedHeight = 18f;
            //clCantidad.BorderWidthBottom = 1f;



            //PdfPCell clPrecio = new PdfPCell(new Phrase("Precio Unit", _standardFont));
            //clPrecio.BorderWidth = 1f;
            //clPrecio.BorderColor = FontColour;
            //clPrecio.BackgroundColor = FontColourCell;
            //clPrecio.FixedHeight = 18f;


            //PdfPCell clValor = new PdfPCell(new Phrase("Valor Total", _standardFont));
            //clValor.BorderWidth = 1f;
            //clValor.BorderColor = FontColour;
            //clValor.BackgroundColor = FontColourCell;
            //clValor.FixedHeight = 18f;

            PdfPCell clAlmacen = new PdfPCell(new Phrase("Almacén", _standardFont));
            clAlmacen.BorderWidth = 1f;
            clAlmacen.BorderColor = FontColour;
            clAlmacen.BackgroundColor = FontColourCell;
            clAlmacen.Colspan = 2;
            clAlmacen.FixedHeight = 18f;

            PdfPCell clModulo = new PdfPCell(new Phrase("Módulo", _standardFont));
            clModulo.BorderWidth = 1f;
            clModulo.BorderColor = FontColour;
            clModulo.BackgroundColor = FontColourCell;
            clModulo.FixedHeight = 18f;

            PdfPCell clTurno = new PdfPCell(new Phrase("Turno", _standardFont));
            clTurno.BorderWidth = 1f;
            clTurno.BorderColor = FontColour;
            clTurno.BackgroundColor = FontColourCell;
            clTurno.FixedHeight = 18f;

            PdfPCell clCosto = new PdfPCell(new Phrase("Centro Costo", _standardFont));
            clCosto.BorderWidth = 1f;
            clCosto.BorderColor = FontColour;
            clCosto.BackgroundColor = FontColourCell;
            clCosto.FixedHeight = 18f;

            PdfPCell clContable = new PdfPCell(new Phrase("Item contable", _standardFont));
            clContable.BorderWidth = 1f;
            clContable.BorderColor = FontColour;
            clContable.BackgroundColor = FontColourCell;
            clContable.FixedHeight = 18f;

            PdfPCell clObsevacion = new PdfPCell(new Phrase("Observación", _standardFont));
            clObsevacion.BorderWidth = 1f;
            clObsevacion.BorderColor = FontColour;
            clObsevacion.BackgroundColor = FontColourCell;
            clObsevacion.FixedHeight = 18f;


            // Añadimos las celdas a la tabla
            tblDetalle.AddCell(clNro);
            tblDetalle.AddCell(clCodigo);
            tblDetalle.AddCell(clDescripcion);
            tblDetalle.AddCell(clCantidad);
            tblDetalle.AddCell(clMedida);
            //tblDetalle.AddCell(clPrecio);
            //tblDetalle.AddCell(clValor);
            tblDetalle.AddCell(clAlmacen);
            tblDetalle.AddCell(clModulo);
            tblDetalle.AddCell(clTurno);
            tblDetalle.AddCell(clCosto);
            tblDetalle.AddCell(clContable);
            tblDetalle.AddCell(clObsevacion);

            NumberFormatInfo formato = new CultureInfo("es-AR").NumberFormat;

            formato.CurrencyGroupSeparator = ".";
            formato.NumberDecimalSeparator = ",";

            // Llenamos la tabla con información

            foreach (DataRow row in dt.Rows)
            {

                clNro = new PdfPCell(new Phrase(row["Fila"].ToString(), _standardFont_detalle));
                clNro.BorderWidth = 1f;
                clNro.BorderColor = FontColour;
                clNro.FixedHeight = 18f;

                clCodigo = new PdfPCell(new Phrase(row["Codigo"].ToString(), _standardFont_detalle));
                clCodigo.BorderWidth = 1f;
                clCodigo.BorderColor = FontColour;
                clCodigo.FixedHeight = 18f;
                clCodigo.HorizontalAlignment = Element.ALIGN_LEFT;

                clDescripcion = new PdfPCell(new Phrase(row["Descripcion"].ToString(), _standardFont_detalle));
                clDescripcion.BorderWidth = 1f;
                clDescripcion.HorizontalAlignment = Element.ALIGN_LEFT;
                clDescripcion.VerticalAlignment = Element.ALIGN_CENTER;
                clDescripcion.BorderColor = FontColour;
                clDescripcion.FixedHeight = 18f;

                clCantidad = new PdfPCell(new Phrase(Convert.ToDecimal(row["CantidadAlmacen"]).ToString(), _standardFont_detalle));
                clCantidad.BorderWidth = 1f;
                clCantidad.BorderColor = FontColour;
                clCantidad.FixedHeight = 18f;
                clCantidad.HorizontalAlignment = Element.ALIGN_LEFT;


                clMedida = new PdfPCell(new Phrase(row["Abreviatura"].ToString(), _standardFont_detalle));
                clMedida.BorderWidth = 1f;
                clMedida.HorizontalAlignment = Element.ALIGN_LEFT;
                clMedida.VerticalAlignment = Element.ALIGN_RIGHT;
                clMedida.BorderColor = FontColour;
                clMedida.FixedHeight = 18f;





                //clPrecio = new PdfPCell(new Phrase(Convert.ToDecimal(row["pValorado"]).ToString(), _standardFont_detalle));
                //clPrecio.BorderWidth = 1f;
                //clPrecio.HorizontalAlignment = Element.ALIGN_LEFT;
                //clPrecio.VerticalAlignment = Element.ALIGN_RIGHT;
                //clPrecio.BorderColor = FontColour;
                //clPrecio.FixedHeight = 18f;

                //clValor = new PdfPCell(new Phrase(Convert.ToDecimal(row["pValorUnitario"]).ToString(), _standardFont_detalle));
                //clValor.BorderWidth = 1f;
                //clValor.HorizontalAlignment = Element.ALIGN_LEFT;
                //clValor.VerticalAlignment = Element.ALIGN_RIGHT;
                //clValor.BorderColor = FontColour;
                //clValor.FixedHeight = 18f;


                clAlmacen = new PdfPCell(new Phrase(row["NombreAlmacen"].ToString(), _standardFont_detalle));
                clAlmacen.BorderWidth = 1f;
                clAlmacen.HorizontalAlignment = Element.ALIGN_LEFT;
                clAlmacen.VerticalAlignment = Element.ALIGN_RIGHT;
                clAlmacen.BorderColor = FontColour;
                clAlmacen.Colspan = 2;
                clAlmacen.FixedHeight = 18f;

                clModulo = new PdfPCell(new Phrase(row["Modulo"].ToString(), _standardFont_detalle));
                clModulo.BorderWidth = 1f;
                clModulo.HorizontalAlignment = Element.ALIGN_LEFT;
                clModulo.VerticalAlignment = Element.ALIGN_RIGHT;
                clModulo.BorderColor = FontColour;
                clModulo.FixedHeight = 18f;

                clTurno = new PdfPCell(new Phrase(row["Turno"].ToString(), _standardFont_detalle));
                clTurno.BorderWidth = 1f;
                clTurno.HorizontalAlignment = Element.ALIGN_LEFT;
                clTurno.VerticalAlignment = Element.ALIGN_RIGHT;
                clTurno.BorderColor = FontColour;
                clTurno.FixedHeight = 18f;

                clCosto = new PdfPCell(new Phrase(row["CentroCosto"].ToString(), _standardFont_detalle));
                clCosto.BorderWidth = 1f;
                clCosto.HorizontalAlignment = Element.ALIGN_LEFT;
                clCosto.VerticalAlignment = Element.ALIGN_RIGHT;
                clCosto.BorderColor = FontColour;
                clCosto.FixedHeight = 18f;

                clContable = new PdfPCell(new Phrase(row["ItemContable"].ToString(), _standardFont_detalle));
                clContable.BorderWidth = 1f;
                clContable.HorizontalAlignment = Element.ALIGN_LEFT;
                clContable.VerticalAlignment = Element.ALIGN_RIGHT;
                clContable.BorderColor = FontColour;
                clContable.FixedHeight = 18f;

                clObsevacion = new PdfPCell(new Phrase(row["TextoAlternativo"].ToString(), _standardFont_detalle));
                clObsevacion.BorderWidth = 1f;
                clObsevacion.HorizontalAlignment = Element.ALIGN_LEFT;
                clObsevacion.VerticalAlignment = Element.ALIGN_RIGHT;
                clObsevacion.BorderColor = FontColour;
                clObsevacion.FixedHeight = 18f;

                // Añadimos las celdas a la tabla
                //tblDetalle.AddCell(clCorrel);
                tblDetalle.AddCell(clNro);
                tblDetalle.AddCell(clCodigo);
                tblDetalle.AddCell(clDescripcion);
                tblDetalle.AddCell(clCantidad);
                tblDetalle.AddCell(clMedida);
                //tblDetalle.AddCell(clPrecio);
                //tblDetalle.AddCell(clValor);
                tblDetalle.AddCell(clAlmacen);
                tblDetalle.AddCell(clModulo);
                tblDetalle.AddCell(clTurno);
                tblDetalle.AddCell(clCosto);
                tblDetalle.AddCell(clContable);
                tblDetalle.AddCell(clObsevacion);


            }

            doc.Add(tblDetalle);
            doc.Add(Chunk.NEWLINE);

            PdfPTable tblConfirmacion = new PdfPTable(5);
            tblConfirmacion.WidthPercentage = 100;
            float[] medidaCeldasTableC = { 2f, 1f, 2f, 1f, 2f };

            tblConfirmacion.SetWidths(medidaCeldasTableC);

            PdfPCell _cell_Confirmacion = new PdfPCell(new Paragraph(" ", _standardFontMotivo));
            _cell_Confirmacion.HorizontalAlignment = Element.ALIGN_LEFT;
            _cell_Confirmacion.BorderWidthRight = 0;
            _cell_Confirmacion.BorderWidthLeft = 0;
            _cell_Confirmacion.BorderWidthBottom = 0;
            _cell_Confirmacion.BorderWidthTop = 0;
            tblConfirmacion.AddCell(_cell_Confirmacion);

            _cell_Confirmacion = new PdfPCell(new Paragraph(" ", _standardFontMotivo));
            _cell_Confirmacion.HorizontalAlignment = Element.ALIGN_LEFT;
            _cell_Confirmacion.BorderWidthRight = 0;
            _cell_Confirmacion.BorderWidthLeft = 0;
            _cell_Confirmacion.BorderWidthBottom = 0;
            _cell_Confirmacion.BorderWidthTop = 0;
            tblConfirmacion.AddCell(_cell_Confirmacion);

            _cell_Confirmacion = new PdfPCell(new Paragraph(" ", _standardFontMotivo));
            _cell_Confirmacion.HorizontalAlignment = Element.ALIGN_LEFT;
            _cell_Confirmacion.BorderWidthRight = 0;
            _cell_Confirmacion.BorderWidthLeft = 0;
            _cell_Confirmacion.BorderWidthBottom = 0;
            _cell_Confirmacion.BorderWidthTop = 0;
            tblConfirmacion.AddCell(_cell_Confirmacion);

            _cell_Confirmacion = new PdfPCell(new Paragraph(" ", _standardFontMotivo));
            _cell_Confirmacion.HorizontalAlignment = Element.ALIGN_LEFT;
            _cell_Confirmacion.BorderWidthRight = 0;
            _cell_Confirmacion.BorderWidthLeft = 0;
            _cell_Confirmacion.BorderWidthBottom = 0;
            _cell_Confirmacion.BorderWidthTop = 0;
            tblConfirmacion.AddCell(_cell_Confirmacion);

            _cell_Confirmacion = new PdfPCell(new Paragraph(" ", _standardFontMotivo));
            _cell_Confirmacion.HorizontalAlignment = Element.ALIGN_LEFT;
            _cell_Confirmacion.BorderWidthRight = 0;
            _cell_Confirmacion.BorderWidthLeft = 0;
            _cell_Confirmacion.BorderWidthBottom = 0;
            _cell_Confirmacion.BorderWidthTop = 0;
            tblConfirmacion.AddCell(_cell_Confirmacion);



            _cell_Confirmacion = new PdfPCell(new Paragraph(" ", _standardFontMotivo));
            _cell_Confirmacion.HorizontalAlignment = Element.ALIGN_LEFT;
            _cell_Confirmacion.BorderWidthRight = 0;
            _cell_Confirmacion.BorderWidthLeft = 0;
            _cell_Confirmacion.BorderWidthBottom = 0;
            _cell_Confirmacion.BorderWidthTop = 0;
            tblConfirmacion.AddCell(_cell_Confirmacion);

            _cell_Confirmacion = new PdfPCell(new Paragraph(" ", _standardFontMotivo));
            _cell_Confirmacion.HorizontalAlignment = Element.ALIGN_LEFT;
            _cell_Confirmacion.BorderWidthRight = 0;
            _cell_Confirmacion.BorderWidthLeft = 0;
            _cell_Confirmacion.BorderWidthBottom = 0;
            _cell_Confirmacion.BorderWidthTop = 0;
            tblConfirmacion.AddCell(_cell_Confirmacion);

            _cell_Confirmacion = new PdfPCell(new Paragraph(" ", _standardFontMotivo));
            _cell_Confirmacion.HorizontalAlignment = Element.ALIGN_CENTER;
            _cell_Confirmacion.BorderWidthRight = 0;
            _cell_Confirmacion.BorderWidthLeft = 1;
            _cell_Confirmacion.BorderWidthBottom = 0;
            _cell_Confirmacion.BorderWidthTop = 1;
            tblConfirmacion.AddCell(_cell_Confirmacion);

            _cell_Confirmacion = new PdfPCell(new Paragraph(" ", _standardFontMotivo));
            _cell_Confirmacion.HorizontalAlignment = Element.ALIGN_LEFT;
            _cell_Confirmacion.BorderWidthRight = 0;
            _cell_Confirmacion.BorderWidthLeft = 0;
            _cell_Confirmacion.BorderWidthBottom = 0;
            _cell_Confirmacion.BorderWidthTop = 1;
            tblConfirmacion.AddCell(_cell_Confirmacion);

            _cell_Confirmacion = new PdfPCell(new Paragraph(" ", _standardFontMotivo));
            _cell_Confirmacion.HorizontalAlignment = Element.ALIGN_CENTER;
            _cell_Confirmacion.BorderWidthRight = 1;
            _cell_Confirmacion.BorderWidthLeft = 0;
            _cell_Confirmacion.BorderWidthBottom = 0;
            _cell_Confirmacion.BorderWidthTop = 1;
            tblConfirmacion.AddCell(_cell_Confirmacion);




            _cell_Confirmacion = new PdfPCell(new Paragraph(" ", _standardFontMotivo));
            _cell_Confirmacion.HorizontalAlignment = Element.ALIGN_LEFT;
            _cell_Confirmacion.BorderWidthRight = 0;
            _cell_Confirmacion.BorderWidthLeft = 0;
            _cell_Confirmacion.BorderWidthBottom = 0;
            _cell_Confirmacion.BorderWidthTop = 0;
            tblConfirmacion.AddCell(_cell_Confirmacion);

            _cell_Confirmacion = new PdfPCell(new Paragraph(" ", _standardFontMotivo));
            _cell_Confirmacion.HorizontalAlignment = Element.ALIGN_LEFT;
            _cell_Confirmacion.BorderWidthRight = 0;
            _cell_Confirmacion.BorderWidthLeft = 0;
            _cell_Confirmacion.BorderWidthBottom = 0;
            _cell_Confirmacion.BorderWidthTop = 0;
            tblConfirmacion.AddCell(_cell_Confirmacion);

            _cell_Confirmacion = new PdfPCell(new Paragraph(" ", _standardFontMotivo));
            _cell_Confirmacion.HorizontalAlignment = Element.ALIGN_CENTER;
            _cell_Confirmacion.BorderWidthRight = 0;
            _cell_Confirmacion.BorderWidthLeft = 1;
            _cell_Confirmacion.BorderWidthBottom = 0;
            _cell_Confirmacion.BorderWidthTop = 0;
            tblConfirmacion.AddCell(_cell_Confirmacion);

            _cell_Confirmacion = new PdfPCell(new Paragraph(" ", _standardFontMotivo));
            _cell_Confirmacion.HorizontalAlignment = Element.ALIGN_LEFT;
            _cell_Confirmacion.BorderWidthRight = 0;
            _cell_Confirmacion.BorderWidthLeft = 0;
            _cell_Confirmacion.BorderWidthBottom = 0;
            _cell_Confirmacion.BorderWidthTop = 0;
            tblConfirmacion.AddCell(_cell_Confirmacion);

            _cell_Confirmacion = new PdfPCell(new Paragraph(" ", _standardFontMotivo));
            _cell_Confirmacion.HorizontalAlignment = Element.ALIGN_CENTER;
            _cell_Confirmacion.BorderWidthRight = 1;
            _cell_Confirmacion.BorderWidthLeft = 0;
            _cell_Confirmacion.BorderWidthBottom = 0;
            _cell_Confirmacion.BorderWidthTop = 0;
            tblConfirmacion.AddCell(_cell_Confirmacion);



            _cell_Confirmacion = new PdfPCell(new Paragraph(" ", _standardFontMotivo));
            _cell_Confirmacion.HorizontalAlignment = Element.ALIGN_LEFT;
            _cell_Confirmacion.BorderWidthRight = 0;
            _cell_Confirmacion.BorderWidthLeft = 0;
            _cell_Confirmacion.BorderWidthBottom = 0;
            _cell_Confirmacion.BorderWidthTop = 0;
            tblConfirmacion.AddCell(_cell_Confirmacion);

            _cell_Confirmacion = new PdfPCell(new Paragraph(" ", _standardFontMotivo));
            _cell_Confirmacion.HorizontalAlignment = Element.ALIGN_LEFT;
            _cell_Confirmacion.BorderWidthRight = 0;
            _cell_Confirmacion.BorderWidthLeft = 0;
            _cell_Confirmacion.BorderWidthBottom = 0;
            _cell_Confirmacion.BorderWidthTop = 0;
            tblConfirmacion.AddCell(_cell_Confirmacion);

            _cell_Confirmacion = new PdfPCell(new Paragraph("______________________________", _standardFont_detalle));
            _cell_Confirmacion.HorizontalAlignment = Element.ALIGN_CENTER;
            _cell_Confirmacion.BorderWidthRight = 0;
            _cell_Confirmacion.BorderWidthLeft = 1;
            _cell_Confirmacion.BorderWidthBottom = 0;
            _cell_Confirmacion.BorderWidthTop = 0;
            tblConfirmacion.AddCell(_cell_Confirmacion);

            _cell_Confirmacion = new PdfPCell(new Paragraph(" ", _standardFontMotivo));
            _cell_Confirmacion.HorizontalAlignment = Element.ALIGN_LEFT;
            _cell_Confirmacion.BorderWidthRight = 0;
            _cell_Confirmacion.BorderWidthLeft = 0;
            _cell_Confirmacion.BorderWidthBottom = 0;
            _cell_Confirmacion.BorderWidthTop = 0;
            tblConfirmacion.AddCell(_cell_Confirmacion);

            _cell_Confirmacion = new PdfPCell(new Paragraph("______________________________", _standardFont_detalle));
            _cell_Confirmacion.HorizontalAlignment = Element.ALIGN_CENTER;
            _cell_Confirmacion.BorderWidthRight = 1;
            _cell_Confirmacion.BorderWidthLeft = 0;
            _cell_Confirmacion.BorderWidthBottom = 0;
            _cell_Confirmacion.BorderWidthTop = 0;
            tblConfirmacion.AddCell(_cell_Confirmacion);



            _cell_Confirmacion = new PdfPCell(new Paragraph(" ", _standardFontMotivo));
            _cell_Confirmacion.HorizontalAlignment = Element.ALIGN_LEFT;
            _cell_Confirmacion.BorderWidthRight = 0;
            _cell_Confirmacion.BorderWidthLeft = 0;
            _cell_Confirmacion.BorderWidthBottom = 0;
            _cell_Confirmacion.BorderWidthTop = 0;
            tblConfirmacion.AddCell(_cell_Confirmacion);

            _cell_Confirmacion = new PdfPCell(new Paragraph("", _standardFontMotivo));
            _cell_Confirmacion.HorizontalAlignment = Element.ALIGN_RIGHT;
            _cell_Confirmacion.BorderWidthRight = 0;
            _cell_Confirmacion.BorderWidthLeft = 0;
            _cell_Confirmacion.BorderWidthBottom = 0;
            _cell_Confirmacion.BorderWidthTop = 0;
            tblConfirmacion.AddCell(_cell_Confirmacion);


            _cell_Confirmacion = new PdfPCell(new Paragraph("REPONSABLE ALMACÉN", _standardFont_detalle));
            _cell_Confirmacion.HorizontalAlignment = Element.ALIGN_CENTER;
            _cell_Confirmacion.BorderWidthRight = 0;
            _cell_Confirmacion.BorderWidthLeft = 1;
            _cell_Confirmacion.BorderWidthBottom = 0;
            _cell_Confirmacion.BorderWidthTop = 0;
            tblConfirmacion.AddCell(_cell_Confirmacion);

            _cell_Confirmacion = new PdfPCell(new Paragraph(" ", _standardFontMotivo));
            _cell_Confirmacion.HorizontalAlignment = Element.ALIGN_LEFT;
            _cell_Confirmacion.BorderWidthRight = 0;
            _cell_Confirmacion.BorderWidthLeft = 0;
            _cell_Confirmacion.BorderWidthBottom = 0;
            _cell_Confirmacion.BorderWidthTop = 0;
            tblConfirmacion.AddCell(_cell_Confirmacion);

            _cell_Confirmacion = new PdfPCell(new Paragraph("RECIBÍ CONFORME", _standardFont_detalle));
            _cell_Confirmacion.HorizontalAlignment = Element.ALIGN_CENTER;
            _cell_Confirmacion.BorderWidthRight = 1;
            _cell_Confirmacion.BorderWidthLeft = 0;
            _cell_Confirmacion.BorderWidthBottom = 0;
            _cell_Confirmacion.BorderWidthTop = 0;
            tblConfirmacion.AddCell(_cell_Confirmacion);


            _cell_Confirmacion = new PdfPCell(new Paragraph(" ", _standardFontMotivo));
            _cell_Confirmacion.HorizontalAlignment = Element.ALIGN_LEFT;
            _cell_Confirmacion.BorderWidthRight = 0;
            _cell_Confirmacion.BorderWidthLeft = 0;
            _cell_Confirmacion.BorderWidthBottom = 0;
            _cell_Confirmacion.BorderWidthTop = 0;
            tblConfirmacion.AddCell(_cell_Confirmacion);

            _cell_Confirmacion = new PdfPCell(new Paragraph(" ", _standardFontMotivo));
            _cell_Confirmacion.HorizontalAlignment = Element.ALIGN_LEFT;
            _cell_Confirmacion.BorderWidthRight = 0;
            _cell_Confirmacion.BorderWidthLeft = 0;
            _cell_Confirmacion.BorderWidthBottom = 0;
            _cell_Confirmacion.BorderWidthTop = 0;
            tblConfirmacion.AddCell(_cell_Confirmacion);




            _cell_Confirmacion = new PdfPCell(new Paragraph(" ", _standardFontMotivo));
            _cell_Confirmacion.HorizontalAlignment = Element.ALIGN_CENTER;
            _cell_Confirmacion.BorderWidthRight = 0;
            _cell_Confirmacion.BorderWidthLeft = 1;
            _cell_Confirmacion.BorderWidthBottom = 1;
            _cell_Confirmacion.BorderWidthTop = 0;
            tblConfirmacion.AddCell(_cell_Confirmacion);

            _cell_Confirmacion = new PdfPCell(new Paragraph(" ", _standardFontMotivo));
            _cell_Confirmacion.HorizontalAlignment = Element.ALIGN_LEFT;
            _cell_Confirmacion.BorderWidthRight = 0;
            _cell_Confirmacion.BorderWidthLeft = 0;
            _cell_Confirmacion.BorderWidthBottom = 1;
            _cell_Confirmacion.BorderWidthTop = 0;
            tblConfirmacion.AddCell(_cell_Confirmacion);

            _cell_Confirmacion = new PdfPCell(new Paragraph(" ", _standardFontMotivo));
            _cell_Confirmacion.HorizontalAlignment = Element.ALIGN_CENTER;
            _cell_Confirmacion.BorderWidthRight = 1;
            _cell_Confirmacion.BorderWidthLeft = 0;
            _cell_Confirmacion.BorderWidthBottom = 1;
            _cell_Confirmacion.BorderWidthTop = 0;
            tblConfirmacion.AddCell(_cell_Confirmacion);

           


            doc.Add(tblConfirmacion);

            doc.Close();
            writer.Close();

        }


        class HeaderFooter : PdfPageEventHelper
        {
            string PathImage = null;
            string TipoArchivo = null;
            DataTable datos;
            public HeaderFooter(string logoPath, DataTable dt, string Tipo)
            {
                PathImage = logoPath;
                TipoArchivo = Tipo;
                datos = dt;
            }

            public override void OnEndPage(PdfWriter writer, Document document)
            {
                var FontColour = new BaseColor(30, 98, 175);
                iTextSharp.text.Font _standardFontFooter = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font _standardFontHeader = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                iTextSharp.text.Font _standardHeadline = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 5, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font _standardFontHeader_det = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                PdfPTable tbHeader = new PdfPTable(4);

                // CREO UN ARREGLO QUE CONTIENE LAS MEDIDAS DE CADA UNA DE LAS COLUMNAS
                // EN MI CASO SON 4, (TB PUEDES PASAR EL ARREGLO DIRECTAMENTE)
                float[] medidaCeldas = { 2.5f, 0.05f, 0.2f, 1.50f };

                // ASIGNAS LAS MEDIDAS A LA TABLA (ANCHO)
                tbHeader.SetWidths(medidaCeldas);

                tbHeader.TotalWidth = 400f;
                tbHeader.DefaultCell.Border = 0;

                PdfPCell _cell = new PdfPCell(new Paragraph("REQUERIMIENTO DE SALIDA DE ALMACÉN ", _standardFontHeader));
                _cell.HorizontalAlignment = Element.ALIGN_CENTER;
                _cell.Border = 0;
                _cell.Colspan = 4;
                tbHeader.AddCell(_cell);


                tbHeader.WriteSelectedRows(0, -1, document.LeftMargin + 100, writer.PageSize.GetTop(document.TopMargin) + 60, writer.DirectContent);

                PdfPTable tbFooter = new PdfPTable(1);
                tbFooter.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;

                tbFooter.AddCell(new Paragraph());
                _cell = new PdfPCell(new Paragraph("INKA GOLD FARMS S.A.C", _standardFontFooter));
                _cell.PaddingTop = 5f;
                _cell.BorderWidthTop = 2f;
                _cell.BorderWidthBottom = 0f;
                _cell.BorderWidthLeft = 0f;
                _cell.BorderWidthRight = 0f;
                _cell.BorderColor = FontColour;
                _cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                tbFooter.AddCell(_cell);

                _cell = new PdfPCell(new Paragraph("Las Cucardas 1123 Urb. Las Palmas del Golf - Victor Larco - Trujillo - La Libertad", _standardFontFooter));
                _cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                _cell.Border = 0;
                tbFooter.AddCell(_cell);

                _cell = new PdfPCell(new Paragraph("Email: IGF@gmail.com", _standardFontFooter));
                _cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                _cell.Border = 0;
                tbFooter.AddCell(_cell);

                tbFooter.WriteSelectedRows(0, -1, document.LeftMargin, writer.PageSize.GetBottom(document.BottomMargin) + 25, writer.DirectContent);


                Image logo = Image.GetInstance(PathImage);
                logo.SetAbsolutePosition(document.LeftMargin, writer.PageSize.GetTop(document.TopMargin) + 10);
                logo.ScaleAbsolute(120f, 90f);

                document.Add(logo);
            }
        }

        [HttpPost]
        [ActionName("PMMM_GetDocumentosAprobacion")]
        public IHttpActionResult PMMM_GetDocumentosAprobacion(dynamic obj)
        {
            try
            {
                int prmIntUsuario = JWT.IdUsuario;
                int prmintEstado = obj.prmintEstado;
                DateTime prmdateFInicio = obj.prmFinicio;
                DateTime prmdateFFin = obj.prmfFin;
                DataTable dt = Instancia.GetDocumentosApobacion(prmIntUsuario, prmintEstado, prmdateFInicio, prmdateFFin);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_AprobarDocumentos")]
        public IHttpActionResult PMMM_AprobarDocumentos(int prmintDetaAprobacion)
        {
            try
            {
                int prmIntUsuario = JWT.IdUsuario;
                string prmStrNombreUsuario = JWT.Login;
                DataTable dt = Instancia.AprobarDocumento(prmintDetaAprobacion, prmIntUsuario, prmStrNombreUsuario);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }


        [HttpPost]
        [ActionName("Disable_Documento")]
        public IHttpActionResult Disable_Documento(dynamic obj)
        {
            try
            {
                int prmintDetaAprobacion = obj.ID;
                bool prmbitStatus = obj.FutureStatus;
                string prmStrNombreUsuario = JWT.Login;

                DataTable dt = Instancia.RechazarDocumento(prmintDetaAprobacion, prmbitStatus, prmStrNombreUsuario);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_GetMaterialesSolped")]
        public IHttpActionResult PMMM_GetMaterialesSolped(int prmintCentro)
        {
            try
            {
                DataTable dt = Instancia.GetMaterialesSolped(prmintCentro);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpPost]
        [ActionName("PMMM_GenerarSOLPED")]
        public HttpResponseMessage PMMM_GenerarSOLPED(dynamic obj)
        {
            try
            {
                string prmstrXML = Convert.ToString(obj.strXML);
                int prmintIdSociedad = JWT.IdSociedad;
                int prmintIdEmpresa = JWT.IdEmpresa;
                string prmstrUsuario = JWT.Login;


                int res = Instancia.GenerarSOLPED(prmstrXML, prmintIdEmpresa, prmintIdSociedad, prmstrUsuario);
                string mensaje = "";
                bool success = false;
                if (res == 1)
                {
                    success = true;
                    mensaje = "Se registró correctamente.";
                }


                if (res == 0)
                {
                    success = false;
                    mensaje = "Ocurrió un error. Por favor, volver a intentar.";
                }

                var respuesta = (new
                {
                    Success = success,
                    Mensaje = mensaje,
                    Respuesta = res
                }
                );

                var httpResponseMessage = Request.CreateResponse(HttpStatusCode.OK, respuesta);

                httpResponseMessage.Headers.Add("Access-Control-Allow-Origin", "*");

                return httpResponseMessage;
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }


        [HttpGet]
        [ActionName("PMMM_getEstadosSolicitudPedido")]
        public IHttpActionResult PMMM_getEstadosSolicitudPedido()
        {
            try
            {
                DataTable dt = Instancia.GetEstadosSolicitudPedido(JWT.IdSociedad, JWT.IdEmpresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_getSolicitudesPedidos")]
        public IHttpActionResult PMMM_getSolicitudesPedidos(string prmstrEstado, int prmintMaterial, string prmdateFechaDesde, string prmdateFechaHasta)
        {
            try
            {
                DataTable dt = Instancia.GetSolicitudesPedidos(JWT.IdSociedad, JWT.IdEmpresa, prmstrEstado, prmintMaterial, prmdateFechaDesde, prmdateFechaHasta, JWT.Login);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpPost]
        [ActionName("PMMM_ActualizarSolicitudPedido")]
        public HttpResponseMessage PMMM_ActualizarSolicitudPedido(dynamic obj)
        {
            try
            {
                int prmintSolicitudPedido = Convert.ToInt32(obj.SolicitudPedido);
                decimal prmdecCantidad = Convert.ToDecimal(obj.Cantidad);
                int prmintIdSociedad = JWT.IdSociedad;
                int prmintIdEmpresa = JWT.IdEmpresa;
                string prmstrUsuario = JWT.Login;


                int res = Instancia.ActualizarSolicitudPedido(prmintIdEmpresa, prmintIdSociedad, prmintSolicitudPedido, prmdecCantidad, prmstrUsuario);
                string mensaje = "";
                bool success = false;
                if (res == 1)
                {
                    success = true;
                    mensaje = "Se actualizó correctamente.";
                }


                if (res == 0)
                {
                    success = false;
                    mensaje = "Ocurrió un error. Por favor, volver a intentar.";
                }

                var respuesta = (new
                {
                    Success = success,
                    Mensaje = mensaje,
                    Respuesta = res
                }
                );

                var httpResponseMessage = Request.CreateResponse(HttpStatusCode.OK, respuesta);

                httpResponseMessage.Headers.Add("Access-Control-Allow-Origin", "*");

                return httpResponseMessage;
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpPost]
        [ActionName("PMMM_AnularSolicitudPedido")]
        public HttpResponseMessage PMMM_AnularSolicitudPedido(dynamic obj)
        {
            try
            {
                int prmintSolicitudPedido = Convert.ToInt32(obj.SolicitudPedido);
                int prmintIdSociedad = JWT.IdSociedad;
                int prmintIdEmpresa = JWT.IdEmpresa;
                string prmstrUsuario = JWT.Login;


                int res = Instancia.AnularSolicitudPedido(prmintIdEmpresa, prmintIdSociedad, prmintSolicitudPedido, prmstrUsuario);
                string mensaje = "";
                bool success = false;
                if (res == 1)
                {
                    success = true;
                    mensaje = "Se anuló correctamente.";
                }


                if (res == 0)
                {
                    success = false;
                    mensaje = "Ocurrió un error. Por favor, volver a intentar.";
                }

                var respuesta = (new
                {
                    Success = success,
                    Mensaje = mensaje,
                    Respuesta = res
                }
                );

                var httpResponseMessage = Request.CreateResponse(HttpStatusCode.OK, respuesta);

                httpResponseMessage.Headers.Add("Access-Control-Allow-Origin", "*");

                return httpResponseMessage;
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpPost]
        [ActionName("MM_GetSolpeds")]
        public IHttpActionResult MM_GetSolpeds(dynamic obj)
        {
            try
            {
                DataTable dt = Instancia.GetSolpedForOC();
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_getDetalleSolicitudPedidoFromOC")]
        public IHttpActionResult PMMM_getDetalleSolicitudPedidoFromOC(string prmstrNroSolped)
        {
            try
            {
                DataTable dt = Instancia.GetDetalleSolicitudPedidoFromOC(JWT.IdSociedad, JWT.IdEmpresa, prmstrNroSolped);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }


        [HttpGet]
        [ActionName("PMMM_getSolicitudPedidoFromOC")]
        public IHttpActionResult PMMM_getSolicitudPedidoFromOC()
        {
            try
            {
                DataTable dt = Instancia.GetSolicitudPedidoFromOC(JWT.IdSociedad, JWT.IdEmpresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }


        [HttpGet]
        [ActionName("PMMM_DataAprobador")]
        public IHttpActionResult PMMM_DataAprobador(int prmintIdDetaAprobacion, string prmstrCorrelativo)
        {
            try
            {
                DataTable dt = Instancia.dataDocumentosAprobacion(prmintIdDetaAprobacion, prmstrCorrelativo);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

    }
}

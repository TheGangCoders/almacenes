using Shared.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Util
{
    public static class MetodoComun
    {
        private static string passwordcodify = "CRYPT4X3S0DNP";
        private static string charscodify = "MEEBAKSOFT";
        public static string ConvertValueSAPToString(string prmstrCadena)
        {
            string _xml = prmstrCadena;
            _xml = _xml.Replace("<(>&<)>", "&");

            return _xml;
        }

        public static string ConvertValueToXML(string prmstrCadena)
        {
            string _xml = prmstrCadena;
            _xml = _xml.Replace("&", "&amp;");
            _xml = _xml.Replace(">", "&gt;");
            _xml = _xml.Replace("<", "&lt;");
            _xml = _xml.Replace("''", "&quot;");
            _xml = _xml.Replace("'", "&apos;");
            _xml = _xml.Replace("  ", "&#160;");
            _xml = _xml.Replace(" ", "&#160;");
            _xml = _xml.Replace("Á", "&#193;");
            _xml = _xml.Replace("á", "&#225;");
            _xml = _xml.Replace("É", "&#201;");
            _xml = _xml.Replace("é", "&#233;");
            _xml = _xml.Replace("Í", "&#205;");
            _xml = _xml.Replace("í", "&#237;");
            _xml = _xml.Replace("Ó", "&#211;");
            _xml = _xml.Replace("ó", "&#243;");
            _xml = _xml.Replace("Ú", "&#218;");
            _xml = _xml.Replace("ú", "&#250;");
            _xml = _xml.Replace("Ñ", "&#209;");
            _xml = _xml.Replace("ñ", "&#241;");
            _xml = _xml.Replace("`", "&#96;");
            _xml = _xml.Replace("´", "&#180;");
            _xml = _xml.Replace("¨", "&#168;");
            _xml = _xml.Replace("ä", "&#228;");
            _xml = _xml.Replace("ë", "&#235;");
            _xml = _xml.Replace("ö", "&#246;");
            _xml = _xml.Replace("ü", "&#252;");
            _xml = _xml.Replace("à", "&#224;");
            _xml = _xml.Replace("è", "&#232;");
            _xml = _xml.Replace("ì", "&#236;");
            _xml = _xml.Replace("ò", "&#242;");
            _xml = _xml.Replace("ù", "&#249;");
            _xml = _xml.Replace("ç", "&#231;");
            _xml = _xml.Replace("Ç", "&#199;");
            _xml = _xml.Replace("º", "&#186;");
            _xml = _xml.Replace("”", "&#148;");
            _xml = _xml.Replace("Ø", "&#216;");
            _xml = _xml.Replace("È", "&#200;");
            _xml = _xml.Replace("À", "&#192;");
            _xml = _xml.Replace("½", "&#189;");
            _xml = _xml.Replace("°", "&#176;");
            _xml = _xml.Replace("ª", "&#170;");
            _xml = _xml.Replace("¡", "&#161;");
            _xml = _xml.Replace("!", "&#33;");
            _xml = _xml.Replace("/", "&#47;");
            _xml = _xml.Replace("¿", "&#191;");
            _xml = _xml.Replace("?", "&#63;");
            _xml = _xml.Replace("=", "&#61;");
            _xml = _xml.Replace("[", "&#91;");
            _xml = _xml.Replace("]", "&#93;");
            _xml = _xml.Replace("\\", "&#92;");
            _xml = _xml.Replace("^", "&#94;");
            _xml = _xml.Replace("_", "&#95;");
            _xml = _xml.Replace("{", "&#123;");
            _xml = _xml.Replace("|", "&#124;");
            _xml = _xml.Replace("}", "&#125;");
            _xml = _xml.Replace("~", "&#126;");
            _xml = _xml.Replace("¬", "&#172;");
            _xml = _xml.Replace("¦", "&#161;");
            _xml = _xml.Replace("¯", "&#175;");
            _xml = _xml.Replace("·", "&#183;");
            _xml = _xml.Replace("$", "&#36;");
            _xml = _xml.Replace("%", "&#37;");
            _xml = _xml.Replace("€", "&#8364;");
            _xml = _xml.Replace("—", "&#8212;");
            _xml = _xml.Replace("™", "&#8482;");
            _xml = _xml.Replace("–", "&#8230;");
            _xml = _xml.Replace("‰", "&#8240;");
            _xml = _xml.Replace("•", "&#8226;");
            _xml = _xml.Replace("†", "&#8224;");
            _xml = _xml.Replace("Â", "&#194;");
            _xml = _xml.Replace("Ã", "&#195;");
            _xml = _xml.Replace("Ä", "&#196;");
            _xml = _xml.Replace("Ê", "&#202;");
            _xml = _xml.Replace("Ë", "&#203;");
            _xml = _xml.Replace("Ç", "&#199;");
            _xml = _xml.Replace("ã", "&#227;");
            _xml = _xml.Replace("»", "&#187;");
            _xml = _xml.Replace("▪", "&#9632;");
            _xml = _xml.Replace("♦", "&#9830");
            _xml = _xml.Replace("►", "&#9658;");

            return _xml;
        }

        public static string ObtenerDetalleError(Object error)
        {
            if (error.GetType() == typeof(SqlException))
            {
                SqlException ex = error as SqlException;
                return ex.Message;// + "\n" + ex.Procedure + "\n" + "Linea Número:" + ex.LineNumber + "\n" + ex.Server + "\n" + ex.StackTrace;
            }
            else
            {
                Exception ex = error as Exception;
                return ex.Message;// + "\n" + ex.StackTrace;
            }
        }

        public static dynamic ObtenerDetalleErrorDynamic(Object error)
        {
            return new ExceptionModel
            {
                TipoException = "Exception",
                Exception = MetodoComun.ObtenerDetalleError(error)
            };
        }

        public static DataTable ConvertObjectToDataTable(dynamic obj)
        {
            DataTable dt = new DataTable();

            if (obj != null && obj.Count > 0)
            {
                Type t = obj[0].GetType();
                dt.TableName = t.Name;
                foreach (PropertyInfo pi in t.GetProperties())
                {
                    dt.Columns.Add(new DataColumn(pi.Name));
                }
                foreach (dynamic o in obj)
                {
                    DataRow dr = dt.NewRow();
                    foreach (DataColumn dc in dt.Columns)
                    {
                        dr[dc.ColumnName] = o.GetType().GetProperty(dc.ColumnName).GetValue(o, null);
                    }
                    dt.Rows.Add(dr);
                }
            }

            return dt;
        }

        public static DataTable ConvertClassToDataTable<T>(dynamic list)
        {
            Type type = typeof(T);
            var properties = type.GetProperties();

            DataTable dataTable = new DataTable();
            foreach (PropertyInfo info in properties)
            {
                dataTable.Columns.Add(new DataColumn(info.Name, Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType));
            }

            object[] values = new object[properties.Length];

            for (int i = 0; i < properties.Length; i++)
            {
                values[i] = properties[i].GetValue(list);
            }

            dataTable.Rows.Add(values);

            return dataTable;
        }

        public static string ConvertDecimalToHours(Decimal hours)
        {
            string horas = "";

            TimeSpan ts = TimeSpan.FromHours(Decimal.ToDouble(hours));

            int H = ts.Hours;
            int M = ts.Minutes;

            horas = H.ToString().PadLeft(2, '0') + ":" + M.ToString().PadLeft(2, '0');

            return horas;
        }

        public static string ConsumingRFCJSON(string url)
        {
            var user = ConfigurationManager.AppSettings[Global.APP_SETTINGS_RFC_USER];
            var password = ConfigurationManager.AppSettings[Global.APP_SETTINGS_RFC_PASSWORD];

            var httpClientHandler = new HttpClientHandler()
            {
                Credentials = new NetworkCredential(user, password)
            };

            var httpClient = new HttpClient(httpClientHandler);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.MaxResponseContentBufferSize = 2147483647;
            httpClient.Timeout = new TimeSpan(0, 10, 0);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                var result = httpClient.GetStringAsync(url).Result;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string Encrypt<T>(string value) where T : SymmetricAlgorithm, new()
        {
            DeriveBytes rgb = new Rfc2898DeriveBytes(passwordcodify, Encoding.Unicode.GetBytes(charscodify));

            SymmetricAlgorithm algorithm = new T();

            byte[] rgbKey = rgb.GetBytes(algorithm.KeySize >> 3);
            byte[] rgbIV = rgb.GetBytes(algorithm.BlockSize >> 3);

            ICryptoTransform transform = algorithm.CreateEncryptor(rgbKey, rgbIV);

            using (MemoryStream buffer = new MemoryStream())
            {
                using (CryptoStream stream = new CryptoStream(buffer, transform, CryptoStreamMode.Write))
                {
                    using (StreamWriter writer = new StreamWriter(stream, Encoding.Unicode))
                    {
                        writer.Write(value);
                    }
                }

                return Convert.ToBase64String(buffer.ToArray());
            }
        }

        public static bool TryRetrieveHeader(HttpRequestMessage request, string header, out string value)
        {
            return TryRetrieveHeader(request, header, "", out value);
        }
        public static bool TryRetrieveHeader(HttpRequestMessage request, string header, string prefix, out string value)
        {
            value = null;
            IEnumerable<string> headers;
            if (!request.Headers.TryGetValues(header, out headers) || headers.Count() > 1)
            {
                return false;
            }
            var tokenValue = headers.ElementAt(0);
            if (!tokenValue.StartsWith(prefix))
            {
                return false;
            }
            value = tokenValue.Substring(prefix.Length);
            return true;
        }

        public static List<T> ConvertDataTableToList<T>(DataTable dt)
        {
            var columnNames = dt.Columns.Cast<DataColumn>()
                .Select(c => c.ColumnName)
                .ToList();

            var properties = typeof(T).GetProperties();

            return dt.AsEnumerable().Select(row =>
            {
                var objT = Activator.CreateInstance<T>();

                foreach (var pro in properties)
                {
                    try
                    {
                        var value = row[pro.Name];
                        value = value == DBNull.Value ? null : value;
                        if (columnNames.Contains(pro.Name))
                            pro.SetValue(objT, value);
                    }catch(Exception)
                    {
                        //No existe pro.Name in value
                    }
                }

                return objT;
            }).ToList();

        }
        public static ExceptionModel ObtenerExceptionModel(Exception ex)
        {
            var error = (new ExceptionModel
            {
                TipoException = "Exception",
                Exception = ObtenerDetalleError(ex)
            }
            );

            return error;
        }
    }
}

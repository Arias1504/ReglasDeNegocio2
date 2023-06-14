using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Referenciar y usar
using System.IO;

namespace libRnMiCiudad
{
    public class clsRnMiCiudad
    {
        #region "Atributos"
        private int intEstrato;
        private float fltVrKw;
        private float fltVrMt3;
        private float fltVrMinTel;
        private string strError;
        #endregion

        #region "Constructor"
        public clsRnMiCiudad()
        {
            intEstrato = 0;
            fltVrKw = 0;
            fltVrMt3 = 0;
            fltVrMinTel = 0;
            strError = string.Empty;
        }
        #endregion

        #region "Propiedades"
        //Entrada 
        public int Estrato
        {
            set { intEstrato = value; }
        }

        //Salida
        public float vrKw
        {
            get { return fltVrKw; }
        }

        public float vrMt3
        {
            get { return fltVrMt3; }
        }

        public float vrMinTel
        {
            get { return fltVrMinTel; }
        }

        public string Error
        {
            get { return strError; }
        }
        #endregion


        #region "Metodos privados"
        private bool leerArchivo()
        {
            try
            {
                string strPath = AppDomain.CurrentDomain.BaseDirectory + @"valoresServ.txt";
                int intCant = 0;  // Para la cantidad de líneas que tiene el archivo
                string strLinea;  // Para la línea leída del archivo
                string[] vectorLinea;  // Vector para almacenar la línea del archivo
                string strCodigo;
                intCant = File.ReadAllLines(strPath).Length;  // Lee la cantidad de líneas que tiene el archivo
                if (intCant <= 0)
                {
                    strError = "Sin registros";
                    return false;
                }
                StreamReader Archivo = new StreamReader(@strPath); // Crear objeto para leer el archivo 
                while ((strLinea = Archivo.ReadLine()) != null)   // Leer línea * línea el archivo 
                {
                    vectorLinea = strLinea.Split(':');
                    strCodigo = vectorLinea[0]; //Nombre Dato 
                    if (strCodigo == intEstrato.ToString())
                    {
                        fltVrKw = Convert.ToSingle(vectorLinea[1]); // Vr. Kilovatio 
                        fltVrMt3 = Convert.ToSingle(vectorLinea[2]); // Vr. Metro cúbico agua 
                        fltVrMinTel = Convert.ToSingle(vectorLinea[3]); // Vr. Minuto teléfono 
                        break;
                    }
                }
                Archivo.Close();
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }
        #endregion

        #region "Metodos publicos"
        public bool Consultar()
        {
            return leerArchivo();
        }
        #endregion
    }
}

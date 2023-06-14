using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using libRnMiCiudad;

namespace libOpeMiCiudad
{
    public class clsOpeMiCiudad
    {
        #region "Atributos"
        private int intEst;
        private float fltVrDolar, fltKEner, fltKAgu, fltKMinTel;
        private float fltVrCentavo;
        private float fltVrEner, fltVrAgu, fltVrMinTel, fltVrAPag;
        private string strError;
        #endregion

        #region "Constructor"
        public clsOpeMiCiudad()
        {
            intEst = 0;
            fltVrDolar = 0;
            fltKEner = 0;
            fltKAgu = 0;
            fltKMinTel = 0;
            fltVrCentavo = 0;
            fltVrEner = 0;
            fltVrAgu = 0;
            fltVrMinTel = 0;
            fltVrAPag = 0;
            strError = string.Empty;
        }
        #endregion

        #region "Propiedades"
        // Entrada y salida
        public int Estrato
        {
            set { intEst = value; }
            get { return intEst; }
        }

        //Entrada
        public float vrDolar
        {
            set { fltVrDolar = value; }
        }
        public float cantEnergia
        {
            set { fltKEner = value; }
        }
        public float cantAgua
        {
            set { fltKAgu = value; }
        }
        public float cantTelefono
        {
            set { fltKMinTel = value; }
        }

        //Salida
        public float vrEnergia
        {
            get { return fltVrEner; }
        }
        public float vrAgua
        {
            get { return fltVrAgu; }
        }
        public float vrTelefono
        {
            get { return fltVrMinTel; }
        }
        public float totalAPagar
        {
            get { return fltVrAPag; }
        }
        public string Error
        {
            get { return strError; }
        }
        #endregion

        #region "Metodos privados"
        private bool Validar()
        {
            if(intEst < 1 || intEst > 6)
            {
                strError = "Estrato no valido";
                return false;
            }
            if(fltVrDolar <= 0)
            {
                strError = "Valor del dolar no válido";
                return false;
            }
            if(fltKEner < 0)
            {
                strError = "Cantidad de Kw no válida";
            }
            if(fltKAgu < 0)
            {
                strError = "Cantidad de M3 no válido";
                return false;
            }
            if(fltKMinTel < 0)
            {
                strError = "Cantidad de minutos de Tel. no válido";
                return false;
            }
            return true;
        }
        private bool procesarFact()
        {
            if (!Validar())
                return false;
            try
            {
                fltVrCentavo = fltVrDolar / 100f;
                //Crear un objeto
                clsRnMiCiudad oRn = new clsRnMiCiudad();
                //Enviar info 
                oRn.Estrato = intEst;
                //Invocacion del metodo y tratamiento del error
                if(!oRn.Consultar())
                {
                    strError = oRn.Error;
                    oRn = null;
                    return false;
                }
                //Recuperacion de la info - Procesar
                fltVrEner = fltKEner * oRn.vrKw * fltVrCentavo;
                fltVrAgu = fltKAgu * oRn.vrMt3 * fltVrCentavo;
                fltVrMinTel = fltKMinTel * oRn.vrMinTel * fltVrCentavo;
                fltVrAPag = fltVrEner + fltVrAgu + fltVrMinTel;
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
        public bool Facturar()
        {
            return procesarFact();
        }
        #endregion
    }
}

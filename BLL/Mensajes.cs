using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DAL;

namespace BLL
{
    class Mensajes : ClaseMaestra
    {

        public int MensajeId { get; set; }
        public string Email { get; set; }
        public string Asunto { get; set; }
        public string Mensaje { get; set; }

        public ConexionDb conexion = new ConexionDb();

        public Mensajes(int mensajeId, string email, string asunto, string mensaje)
        {
            this.MensajeId = mensajeId;
            this.Email = email;
            this.Asunto = asunto;
            this.Mensaje = mensaje;
        }



        public override bool Insertar()
        {
            bool retornar = false;
            try
            {
                conexion.Ejecutar(String.Format("Insert into Mensajes(Email, Asunto, Mensaje) Values ('{0}','{1}','{2}')", this.Email, this.Asunto, this.Mensaje));
                retornar = true;
            }catch(Exception ex) { throw ex; }
            return retornar;
        }


        public override bool Editar()
        {
            bool retornar = false;
            try
            {
                conexion.Ejecutar(String.Format("Update Mensjaes set Email='{0}', Asunto='{1}, Mensaje='{2}' where MensajeId={3}",this.Email, this.Asunto, this.Mensaje, this.MensajeId));
                retornar = true;
            }catch(Exception ex) { throw ex; }
            return retornar;
        }

        public override bool Eliminar()
        {
            bool retornar= false;
            try
            {
                conexion.Ejecutar(String.Format("Delete from Mensajes where MensajeId={0}", this.MensajeId));
                retornar = true;
            }catch(Exception ex) { throw ex; }
            return retornar;
        }

        public override bool Buscar(int IdBuscado)
        {
            DataTable dt = new DataTable();
            dt = conexion.ObtenerDatos("Select * from Mensaje where MensajeId=" + MensajeId);

            if (dt.Rows.Count > 0)
            {
                this.MensajeId = (int)dt.Rows[0]["MensajeId"];
                this.Email = dt.Rows[0]["Email"].ToString();
                this.Asunto = dt.Rows[0]["Asunto"].ToString();
                this.Mensaje = dt.Rows[0]["Mensaje"].ToString();
            }
            return dt.Rows.Count > 0;
        }

        public override DataTable Listado(string Campos, string Condicion, string Orden)
        {
            string ordenar = "";
            if (!Orden.Equals(""))
                ordenar = "orden by" + Orden;
            return conexion.ObtenerDatos("Select" + Campos + "From Mensajes where" + Condicion + Orden);
        }
    }
}

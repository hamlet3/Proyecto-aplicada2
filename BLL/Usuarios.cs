using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DAL;

namespace BLL
{
    public class Usuarios : ClaseMaestra
    {
        public int UsuarioId { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }
        public bool Sexo { get; set; }
        public int Prioridad { get; set; }

        public ConexionDb conexion = new ConexionDb();
        public StringBuilder comando = new StringBuilder();

        public Usuarios(int usuarioId, string nombre, string direccion,string email, bool sexo, int prioridad)
        {
            this.UsuarioId = usuarioId;
            this.Nombre = nombre;
            this.Direccion = direccion;
            this.Email = email;
            this.Sexo = sexo;
            this.Prioridad = prioridad;
        }

        public Usuarios() { }

        public override bool Insertar()
        {
            bool retorno = false;
            try
            {
                 conexion.Ejecutar(String.Format("Insert into Usuarios(Nombre, Direccion, Email, Sexo, Prioridad) Values('{0}', '{1}', '{2}', '{3}', '{4}');",this.Nombre, this.Direccion, this.Email, this.Sexo, this.Prioridad));
                retorno = true;
            } catch(Exception ex) { throw ex; }
            return retorno;
        }


        public override bool Editar()
        {
            bool retorno = false;
            try
            {
                conexion.Ejecutar(String.Format("Update Usuarios set Nombre='{0}', Direccion='{1}', Email='{2}', Sexo='{3}', Prioridad='{4}' where ClienteId={5}", this.Nombre, this.Direccion, this.Email, this.Sexo, this.Prioridad, this.UsuarioId));
                retorno = true;
            }catch(Exception ex) { throw ex; }
            return retorno;
        }

        public override bool Eliminar()
        {
            bool retorno = false;
            try
            {
                conexion.Ejecutar(String.Format("Delete from Usuarios where ClienteId={0}", this.UsuarioId));
                retorno = true;
            }catch(Exception ex) { throw ex; }
            return retorno;
        }

        public override bool Buscar(int IdBuscado)
        {
            DataTable dt = new DataTable();

            dt = conexion.ObtenerDatos("Select * from Usuarios where ClienteId=" + IdBuscado);
            if (dt.Rows.Count > 0)
            {
                this.UsuarioId = (int)dt.Rows[0]["Usuarios"];
                this.Nombre = dt.Rows[0]["Nombre"].ToString();
                this.Direccion = dt.Rows[0]["Direccion"].ToString();
                this.Email = dt.Rows[0]["Email"].ToString();
                this.Sexo = (bool)dt.Rows[0]["Sexo"];
                this.Prioridad =(int)dt.Rows[0]["Prioridad"];
            }
            return dt.Rows.Count > 0;
        }

        public override DataTable Listado(string Campos, string Condicion, string Orden)
        {
            string ordenar = "";
            if (!Orden.Equals(""))
                ordenar = "order by " + Orden;
            return conexion.ObtenerDatos("Select " + Campos + " From Usuarios where " + Condicion + ordenar);
        }

    }
}

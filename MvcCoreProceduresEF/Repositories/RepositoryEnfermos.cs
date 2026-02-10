using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MvcCoreProceduresEF.Data;
using MvcCoreProceduresEF.Models;
using System;
using System.Data;
using System.Data.Common;

namespace MvcCoreProceduresEF.Repositories
{
    #region Stored PROCEDURES
   /* create procedure SP_ALL_ENFERMOS
as
select* from ENFERMO
go

create procedure SP_FIND_ENFERMO
(@inscripcion nvarchar(50))
as
select* From Enfermo where INSCRIPCION=@inscripcion

go

create procedure SP_DELETE_ENFERMO
(@inscripcion nvarchar(50))
as
delete from ENFERMO where INSCRIPCION=@inscripcion
go
   */
#endregion
    public class RepositoryEnfermos
    {

        private readonly EnfermosContext context;

        public RepositoryEnfermos(EnfermosContext context)
        {
            this.context = context;
        }

        public async Task<List<Enfermo>> GetEnfermosAsync()
        {

            using (DbCommand com = this.context.Database.GetDbConnection().CreateCommand())
            {
                string sql= "SP_ALL_ENFERMOS";
                com.CommandType=CommandType.StoredProcedure;
                    
                com.CommandText = sql;
                await com.Connection.OpenAsync();

                DbDataReader reader = await com.ExecuteReaderAsync();
                List<Enfermo> enfermos = new List<Enfermo>();
                while (await reader.ReadAsync())
                {
                    enfermos.Add(new Enfermo
                    {
                        Inscripcion = reader["INSCRIPCION"].ToString(),
                        Apellido = reader["APELLIDO"].ToString(),
                        Direccion = reader["DIRECCION"].ToString(),
                        FechaNacimiento = DateTime.Parse(reader["FECHA_NAC"].ToString()),
                        S = reader["S"].ToString(),
                        Nss = reader["NSS"].ToString()
                    });

                }
                return enfermos;
            }

         
        }
    }
}
